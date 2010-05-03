using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Std.Tweak;

namespace Mintw
{
    /// <summary>
    /// Twitterへのアクセサ
    /// </summary>
    public class Access
    {
        //OAuth APIアクセスクラス
        public MintwOAuth api = new MintwOAuth();

        //Mentionの受信カウンタ
        public int MentionCnt = 0;

        //DMの受信カウンタ
        public int DmCnt = 0;

        /// <summary>
        /// Twitterへのリクエスト状況が変化
        /// </summary>
        public event Action<bool> OnAccessStateChanged;

        /// <summary>
        /// アクセス状況の通知
        /// </summary>
        /// <param name="state">アクセスを開始したならtrue</param>
        private void SetAccessState(bool state)
        {
            if (OnAccessStateChanged != null)
                OnAccessStateChanged.Invoke(state);
        }

        /// <summary>
        /// アクセサの初期化
        /// </summary>
        public void Init()
        {
            Kernel.MinitTimer = new System.Threading.Timer(TimerCallback, null, 0, 1000 * 60);
        }

        /// <summary>
        /// Followingsリストの更新
        /// </summary>
        public void UpdateFollowingsList()
        {
            try
            {
                SetAccessState(true);
                if (Kernel.Config.UserIdSuggest)
                {
                    UpdateAccessInformations();
                    var friends = api.GetFriendsAll(null);
                    if (friends != null)
                        Kernel.Followings = friends.ToArray();
                }
            }
            finally
            {
                SetAccessState(false);
            }
        }

        /// <summary>
        /// 一分ごとに呼ばれるタイマーのコールバック
        /// </summary>
        public void TimerCallback(object o)
        {
            Check(false);
        }

        /// <summary>
        /// Twitterへアクセス
        /// </summary>
        /// <remarks>
        /// 1分ごとに呼ばれることを想定。
        /// </remarks>
        /// <param name="enforced">必ずチェックをするか(falseならカウンタ回数が規定値以上の場合にチェック)</param>
        public void Check(bool enforced)
        {
            try
            {
                SetAccessState(true);
                //カウンタ更新
                MentionCnt++;
                DmCnt++;

                //新着ステータスのリスト
                IEnumerable<TwitterStatusBase> newMentions = null;
                IEnumerable<TwitterStatusBase> newDms = null;

                //DMが来たかのフラグ
                bool DMreceived = false;
                if (enforced || MentionCnt >= Kernel.Config.MentionInverval)
                {
                    //カウンタ初期化
                    MentionCnt = 0;
                    //Mentionsのチェック
                    newMentions = CheckMentions();
                }
                if (enforced || DmCnt >= Kernel.Config.DMInterval)
                {
                    //カウンタ初期化
                    DmCnt = 0;
                    //DMのチェック
                    newDms = CheckDms();
                    if (newDms != null)
                        DMreceived = true;
                }
                if (OnReceivedNew != null && (newMentions != null || newDms != null))
                {
                    //戻り値生成 with LINQ
                    OnReceivedNew.Invoke(
                        from s in
                            newMentions != null && newDms != null ?
                            Enumerable.Union(newMentions, newDms) :
                                newMentions != null ?
                                newMentions : newDms
                        select s,
                        DMreceived);
                }
            }
            finally
            {
                SetAccessState(false);
                Kernel.Config.Save();
            }
        }

        /// <summary>
        /// Twitterへ投稿
        /// </summary>
        /// <param name="text">本文</param>
        public void Tweet(string text)
        {
            try
            {
                SetAccessState(true);
                api.UpdateStatus(text, null);
            }
            finally
            {
                SetAccessState(false);
            }
        }

        /// <summary>
        /// 受信時にExceptionが投げられました
        /// </summary>
        public event Action<Exception> OnThrownException;

        /// <summary>
        /// 新着ステータスがあります(boolがtrueならDMを含んでいます)
        /// </summary>
        public event Action<IEnumerable<TwitterStatusBase>, bool> OnReceivedNew;

        /// <summary>
        /// 新着Mentionsを列挙
        /// </summary>
        /// <returns>ステータスの列挙 もしくは NULL</returns>
        private IEnumerable<TwitterStatusBase> CheckMentions()
        {
            //アクセス情報更新
            UpdateAccessInformations();
            try
            {
                //受信
                var recv = api.GetMentions();
                if (recv != null)
                {
                    //ソート
                    var mentions = from s in recv
                                   orderby s.CreatedAt descending
                                   select (TwitterStatusBase)s;
                    if (mentions != null)
                        //フィルタして返す
                        return FilterReceivedStatuses(mentions, ref Kernel.Config.PrevReceivedMentionId);
                }
            }
            catch (Exception e)
            {
                //エラーの通知とNULL返し
                if (OnThrownException != null)
                    OnThrownException.Invoke(e);
                throw;
            }
            //受信がNULLか、エラーが発生している
            return null;
        }

        /// <summary>
        /// 新着DMを列挙
        /// </summary>
        /// <returns>ステータスの列挙 もしくは NULL</returns>
        private IEnumerable<TwitterStatusBase> CheckDms()
        {
            //アクセス情報更新
            UpdateAccessInformations();
            try
            {
                //受信
                var recv = api.GetDirectMessages();
                if (recv != null)
                {
                    //ソート
                    var dms = from s in recv
                              orderby s.CreatedAt descending
                              select (TwitterStatusBase)s;
                    if (dms != null)
                        //フィルタして返す
                        return FilterReceivedStatuses(dms, ref Kernel.Config.PrevReceivedDmId);
                }
            }
            catch (Exception e)
            {
                //エラー通知
                if (OnThrownException != null)
                    OnThrownException.Invoke(e);
                throw;
            }
            //受信がNULLか、エラーが発生している
            return null;
        }

        /// <summary>
        /// ステータスをフィルタ
        /// </summary>
        /// <param name="received">受信したステータスの列挙</param>
        /// <param name="prevReceivedId">IDの列挙</param>
        /// <returns>新着ステータスの列挙 もしくは NULL</returns>
        private IEnumerable<TwitterStatusBase> FilterReceivedStatuses(IEnumerable<TwitterStatusBase> received, ref long prevReceivedId)
        {
            if (prevReceivedId == 0)
            {
                //旧IDが無い→新着？
                foreach (var recv in received)
                {
                    prevReceivedId = recv.Id;
                    break;
                }
            }
            else
            {
                //前回の最新IDを代入
                long firstId = prevReceivedId;
                foreach (var recv in received)
                {
                    //最新IDを反映
                    prevReceivedId = recv.Id;
                    //そしてすぐ抜ける
                    break;
                }
                //旧最新IDよりもIDが大きいステータスのみを抽出して列挙
                return from s in received
                       where s.Id > firstId
                       select s;
            }
            //列挙されるべきステータスは無い
            return null;
        }

        /// <summary>
        /// アクセス情報の更新
        /// </summary>
        private void UpdateAccessInformations()
        {
            api.Secret = Kernel.Config.UserSecret;
            api.Token = Kernel.Config.UserToken;
        }

    }
}
