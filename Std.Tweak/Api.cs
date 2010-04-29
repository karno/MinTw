using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Std.Tweak
{
    /// <summary>
    /// Twitter API
    /// </summary>
    public static class Api
    {
        #region Constant variables

        public const string TwitterApiUri = "http://api.twitter.com/1/";

        #endregion

        #region Timelines

        /// <summary>
        /// Get twitter timeline
        /// </summary>
        private static IEnumerable<TwitterStatus> GetTimeline(this CredentialProvider provider, string partialUri, IEnumerable<KeyValuePair<string, string>> param)
        {
            var doc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.GET, param);
            if (doc == null)
                return null;
            List<TwitterStatus> statuses = new List<TwitterStatus>();
            HashSet<string> hashes = new HashSet<string>();
            return from n in doc.Descendants("status")
                   let s = TwitterStatus.CreateByNode(n)
                   where s != null
                   select s;
        }

        /// <summary>
        /// Get timeline with full parameters
        /// </summary>
        public static IEnumerable<TwitterStatus> GetTimeline(this CredentialProvider provider, string partialUri, long? sinceId, long? maxId, long? count, long? page, string userId, string screenName)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (sinceId != null && sinceId.HasValue)
                para.Add(new KeyValuePair<string, string>("since_id", sinceId.Value.ToString()));

            if (maxId != null && maxId.HasValue)
                para.Add(new KeyValuePair<string, string>("max_id", maxId.Value.ToString()));

            if (count != null)
                para.Add(new KeyValuePair<string, string>("count", count.ToString()));

            if (page != null)
                para.Add(new KeyValuePair<string, string>("page", page.ToString()));

            if (!String.IsNullOrEmpty(userId))
                para.Add(new KeyValuePair<string, string>("user_id", userId.ToString()));

            if (!String.IsNullOrEmpty(screenName))
                para.Add(new KeyValuePair<string, string>("screen_name", screenName));

            return provider.GetTimeline(partialUri, para);
        }

        /// <summary>
        /// Get public timeline<para />
        /// This result will caching while 60 seconds in Twitter server.
        /// </summary>
        public static IEnumerable<TwitterStatus> GetPublicTimeline(this CredentialProvider provider)
        {
            return provider.GetTimeline("statuses/public_timeline.xml", null);
        }

        /// <summary>
        /// Get home timeline (it contains following users' tweets)
        /// </summary>
        public static IEnumerable<TwitterStatus> GetHomeTimeline(this CredentialProvider provider)
        {
            return provider.GetTimeline("statuses/home_timeline.xml", null);
        }

        /// <summary>
        /// Get home timeline with full params (it contains following users' tweets)
        /// </summary>
        public static IEnumerable<TwitterStatus> GetHomeTimeline(this CredentialProvider provider, long? sinceId, long? maxId, long? count, long? page)
        {
            return provider.GetTimeline("statuses/home_timeline.xml", sinceId, maxId, count, page, null, null);
        }

        /// <summary>
        /// Get mentions
        /// </summary>
        public static IEnumerable<TwitterStatus> GetMentions(this CredentialProvider provider)
        {
            return provider.GetTimeline("statuses/mentions.xml", null);
        }

        /// <summary>
        /// Get mentions with full params
        /// </summary>
        public static IEnumerable<TwitterStatus> GetMentions(this CredentialProvider provider, long? sinceId, long? maxId, long? count, long? page)
        {
            return provider.GetTimeline("statuses/mentions.xml", sinceId, maxId, count, page, null, null);
        }

        #endregion

        #region Status methods

        /// <summary>
        /// Get status
        /// </summary>
        private static TwitterStatus GetStatus(this CredentialProvider provider, string partialUriFormat, CredentialProvider.RequestMethod method, long id)
        {
            string partialUri = string.Format(partialUriFormat, id);
            var doc = provider.RequestAPI(partialUri, method, null);
            if (doc == null)
                return null;
            TwitterStatus s = TwitterStatus.CreateByNode(doc.Element("status"));
            if (s == null)
                throw new Exceptions.TwitterXmlParseException("status can't read.");
            return s;
        }

        /// <summary>
        /// Get status from id
        /// </summary>
        public static TwitterStatus GetStatus(this CredentialProvider provider, long id)
        {
            return provider.GetStatus("statuses/show/{0}.xml", CredentialProvider.RequestMethod.GET, id);
        }

        /// <summary>
        /// Update new status
        /// </summary>
        public static TwitterStatus UpdateStatus(this CredentialProvider provider, string body, long? inReplyToStatusId)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            para.Add(new KeyValuePair<string, string>("status", Tweak.CredentialProviders.OAuth.UrlEncode(body, Encoding.UTF8, true)));
            if (inReplyToStatusId != null && inReplyToStatusId.HasValue)
            {
                para.Add(new KeyValuePair<string, string>("in_reply_to_status_id", inReplyToStatusId.Value.ToString()));
            }
            var doc = provider.RequestAPI("statuses/update.xml", CredentialProvider.RequestMethod.POST, para);
            if (doc != null)
                return TwitterStatus.CreateByNode(doc.Element("status"));
            else
                return null;
        }

        /// <summary>
        /// Delete your tweet
        /// </summary>
        public static TwitterStatus DestroyStatus(this CredentialProvider provider, long id)
        {
            return provider.GetStatus("statuses/destroy/{0}.xml", CredentialProvider.RequestMethod.POST, id);
        }

        #endregion

        #region Direct message methods

        /// <summary>
        /// Get direct messages
        /// </summary>
        private static IEnumerable<TwitterDirectMessage> GetDirectMessages(this CredentialProvider provider, string partialUri, IEnumerable<KeyValuePair<string, string>> param)
        {
            var doc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.GET, param);
            if (doc == null)
                return null;
            List<TwitterStatus> statuses = new List<TwitterStatus>();
            HashSet<string> hashes = new HashSet<string>();
            return from n in doc.Descendants("direct_message")
                   let dm = TwitterDirectMessage.CreateByNode(n)
                   where dm != null
                   select dm;
        }

        /// <summary>
        /// Get direct messages with full params
        /// </summary>
        private static IEnumerable<TwitterDirectMessage> GetDirectMessages(this CredentialProvider provider, string partialUri, long? sinceId, long? maxId, long? count, long? page)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (sinceId != null && sinceId.HasValue)
            {
                para.Add(new KeyValuePair<string, string>("since_id", sinceId.Value.ToString()));
            }
            if (maxId != null && maxId.HasValue)
            {
                para.Add(new KeyValuePair<string, string>("max_id", maxId.Value.ToString()));
            }
            if (count != null && count.HasValue)
            {
                para.Add(new KeyValuePair<string, string>("count", count.Value.ToString()));
            }
            if (page != null && page.HasValue)
            {
                para.Add(new KeyValuePair<string, string>("page", page.Value.ToString()));
            }

            return provider.GetDirectMessages(partialUri, para);
        }

        /// <summary>
        /// Get direct messages
        /// </summary>
        public static IEnumerable<TwitterDirectMessage> GetDirectMessages(this CredentialProvider provider)
        {
            return provider.GetDirectMessages("direct_messages.xml", null);
        }

        /// <summary>
        /// Get direct messages with full params
        /// </summary>
        public static IEnumerable<TwitterDirectMessage> GetDirectMessages(this CredentialProvider provider, long? sinceId, long? maxId, long? count, long? page)
        {
            return provider.GetDirectMessages("direct_messages.xml", sinceId, maxId, count, page);
        }

        /// <summary>
        /// Get direct messages you sent
        /// </summary>
        public static IEnumerable<TwitterDirectMessage> GetSentDirectMessages(this CredentialProvider provider)
        {
            return provider.GetDirectMessages("direct_messages/sent.xml", null);
        }

        /// <summary>
        /// Get direct messages you sent with full params
        /// </summary>
        public static IEnumerable<TwitterDirectMessage> GetSentDirectMessages(this CredentialProvider provider, long? sinceId, long? maxId, long? count, long? page)
        {
            return provider.GetDirectMessages("direct_messages/sent.xml", sinceId, maxId, count, page);
        }

        /// <summary>
        /// Send new direct message
        /// </summary>
        /// <param name="userId">target user id</param>
        /// <param name="screenName">target screen name</param>
        /// <param name="text">send body</param>
        public static TwitterDirectMessage SendDirectMessage(this CredentialProvider provider, string userId, string screenName, string text)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            para.Add(new KeyValuePair<string,string>("text", text));
            if (userId != null)
            {
                para.Add(new KeyValuePair<string,string>("user_id", userId.ToString()));
            }
            if (screenName != null)
            {
                para.Add(new KeyValuePair<string,string>("screen_name", screenName));
            }

            var xmlDoc = provider.RequestAPI("direct_messages/new.xml", CredentialProvider.RequestMethod.POST, para);
            if (xmlDoc == null)
                return null;
            var sent = TwitterDirectMessage.CreateByNode(xmlDoc.Element("direct_message"));
            if (sent == null)
                throw new Exceptions.TwitterRequestException(xmlDoc);

            return sent;
        }

        /// <summary>
        /// Delete a direct message which you sent
        /// </summary>
        public static TwitterDirectMessage DestroyDirectMessage(this CredentialProvider provider, string id)
        {
            string partialUri = string.Format("direct_messages/destroy/{0}.xml", id);
            var xmlDoc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.POST, null);
            if (xmlDoc == null)
                return null;
            var destroyed = TwitterDirectMessage.CreateByNode(xmlDoc.Element("direct_message"));
            if (destroyed == null)
                throw new Exceptions.TwitterRequestException(xmlDoc);
            return destroyed;
        }

        #endregion

        #region User methods

        /// <summary>
        /// Get user with full params
        /// </summary>
        private static TwitterUser GetUser(this CredentialProvider provider, string partialUri, CredentialProvider.RequestMethod method, string userId, string screenName)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (userId != null)
            {
                para.Add(new KeyValuePair<string,string>("user_id", userId.ToString()));
            }
            if (screenName != null)
            {
                para.Add(new KeyValuePair<string,string>("screen_name", screenName));
            }
            var doc = provider.RequestAPI(partialUri, method, para);
            if (doc == null)
                return null;
            return TwitterUser.CreateByNode(doc.Element("user"));
        }

        /// <summary>
        /// Get user
        /// </summary>
        public static TwitterUser GetUser(this CredentialProvider provider, string userId)
        {
            return provider.GetUser("users/show.xml", CredentialProvider.RequestMethod.GET, userId, null);
        }

        /// <summary>
        /// Get user by screen name
        /// </summary>
        public static TwitterUser GetUserByScreenName(this CredentialProvider provider, string screenName)
        {
            return provider.GetUser("users/show.xml", CredentialProvider.RequestMethod.GET, null, screenName);
        }

        /// <summary>
        /// Get users
        /// </summary>
        private static IEnumerable<TwitterUser> GetUsers(this CredentialProvider provider, string partialUri, IEnumerable<KeyValuePair<string, string>> para, out long prevCursor, out long nextCursor)
        {
            prevCursor = 0;
            nextCursor = 0;
            var doc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.GET, para);
            if (doc == null)
                return null;
            List<TwitterUser> users = new List<TwitterUser>();
            var ul = doc.Element("users_list");
            if (ul != null)
            {
                var nc = ul.Element("next_cursor");
                if (nc != null)
                    nextCursor = (long)nc.ParseLong();
                var pc = ul.Element("previous_cursor");
                if (pc != null)
                    prevCursor = (long)pc.ParseLong();
            }
            return from n in doc.Descendants("user")
                   let usr = TwitterUser.CreateByNode(n)
                   where usr != null
                   select usr;
        }

        /// <summary>
        /// Get users with full params
        /// </summary>
        private static IEnumerable<TwitterUser> GetUsers(this CredentialProvider provider, string partialUri, string userId, string screenName, long? cursor, out long prevCursor, out long nextCursor)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (userId != null)
            {
                para.Add(new KeyValuePair<string, string>("user_id", userId.ToString()));
            }
            if (screenName != null)
            {
                para.Add(new KeyValuePair<string, string>("screen_name", screenName));
            }
            if (cursor != null)
            {
                para.Add(new KeyValuePair<string, string>("cursor", cursor.ToString()));
            }
            return provider.GetUsers(partialUri, para, out prevCursor, out nextCursor);
        }

        /// <summary>
        /// Get users with use cursor params
        /// </summary>
        private static IEnumerable<TwitterUser> GetUsersAll(this CredentialProvider provider, string partialUri, string userId, string screenName)
        {
            long n_cursor = -1;
            long c_cursor = -1;
            long p;
            while (n_cursor != 0)
            {
                var users = provider.GetUsers(partialUri, userId, screenName, c_cursor, out p, out n_cursor);
                if (users != null)
                    foreach (var u in users)
                        yield return u;
                c_cursor = n_cursor;
            }
        }

        /// <summary>
        /// Get friends all
        /// </summary>
        public static IEnumerable<TwitterUser> GetFriendsAll(this CredentialProvider provider, string userId)
        {
            return provider.GetUsersAll("statuses/friends.xml", userId, null);
        }

        /// <summary>
        /// Get friends with full params
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="userId"></param>
        /// <param name="screenName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static IEnumerable<TwitterUser> GetFriends(this CredentialProvider provider, string userId, string screenName, long? cursor, out long prevCursor, out long nextCursor)
        {
            if (cursor == null)
                cursor = -1;
            return provider.GetUsers("statuses/friends.xml", userId, screenName, cursor, out prevCursor, out nextCursor);
        }


        /// <summary>
        /// Get followers all
        /// </summary>
        public static IEnumerable<TwitterUser> GetFollowersAll(this CredentialProvider provider, string userId, string screenName)
        {
            return provider.GetUsersAll("statuses/followers.xml", userId, screenName);
        }

        /// <summary>
        /// Get followers with full params
        /// </summary>
        public static IEnumerable<TwitterUser> GetFollowers(this CredentialProvider provider, string userId, string screenName, long? page, out long prevCursor, out long nextCursor)
        {
            return provider.GetUsers("statuses/followers.xml", userId, screenName, page, out prevCursor, out nextCursor);
        }

        #endregion

        #region Favorite methods

        /// <summary>
        /// Favorites a tweet
        /// </summary>
        /// <param name="id">the id of the tweet to favorite.</param>
        /// <returns>The favorited tweet.</returns>
        /// <exception cref="System.Net.WebException">HTTP request failed.</exception>
        /// <exception cref="System.Xml.XmlException">Responce XML was something wrong.</exception>
        /// <exception cref="IronTweet.UnexpectedXmlException">Received unexpected XML document.</exception>
        /// <exception cref="System.NotSupportedException">HTTP request was something wrong.</exception>
        public static TwitterStatus CreateFavorites(this CredentialProvider provider, long id)
        {
            return provider.GetStatus("favorites/create/{0}.xml", CredentialProvider.RequestMethod.POST, id);
        }

        /// <summary>
        /// Unfavorites a tweet
        /// </summary>
        /// <param name="id">the id of the tweet to unffavorite.</param>
        /// <returns>The unfavorited tweet.</returns>
        /// <exception cref="System.Net.WebException">HTTP request failed.</exception>
        /// <exception cref="System.Xml.XmlException">Responce XML was something wrong.</exception>
        /// <exception cref="IronTweet.UnexpectedXmlException">Received unexpected XML document.</exception>
        /// <exception cref="System.NotSupportedException">HTTP request was something wrong.</exception>
        public static TwitterStatus DestroyFavorites(this CredentialProvider provider, long id)
        {
            return provider.GetStatus("favorites/destroy/{0}.xml", CredentialProvider.RequestMethod.POST, id);
        }

        #endregion

        #region Retweet methods
        
        //http://twitter.com/statuses/retweet
        /// <summary>
        /// Retweet status
        /// </summary>
        /// <param name="id">status id</param>
        /// <returns>retweeted status</returns>
        public static TwitterStatus Retweet(this CredentialProvider provider, long id)
        {
            return provider.GetStatus("statuses/retweet/{0}.xml", CredentialProvider.RequestMethod.POST, id);
        }

        #endregion

        #region List methods

        /// <summary>
        /// Get list statuses
        /// </summary>
        /// <param name="userId">list owner user id</param>
        /// <param name="listId">list id</param>
        /// <returns>timeline</returns>
        public static IEnumerable<TwitterStatus> GetListStatuses(this CredentialProvider provider, string userId, string listId)
        {
            return provider.GetListStatuses(userId, listId, null, null, null, null);
        }

        /// <summary>
        /// Get list statuses
        /// </summary>
        /// <param name="userId">list owner user id</param>
        /// <param name="listId">list id</param>
        /// <param name="sinceId">since_id</param>
        /// <param name="maxId">max_id</param>
        /// <param name="perPage">per_page</param>
        /// <param name="page">page</param>
        /// <returns>timeline</returns>
        public static IEnumerable<TwitterStatus> GetListStatuses(this CredentialProvider provider, string userId, string listId, string sinceId, string maxId, long? perPage, long? page)
        {
            listId = listId.Replace("_", "-");
            var partialUri = userId + "/lists/" + listId + "/statuses.xml";

            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();

            if (!String.IsNullOrEmpty(sinceId))
                para.Add(new KeyValuePair<string, string>("since_id", sinceId.ToString()));

            if (!String.IsNullOrEmpty(maxId))
                para.Add(new KeyValuePair<string, string>("max_id", maxId.ToString()));

            if(perPage != null)
                para.Add(new KeyValuePair<string, string>("per_page", perPage.ToString()));

            if (page != null)
                para.Add(new KeyValuePair<string, string>("page", page.ToString()));

            return provider.GetTimeline(partialUri, para);
        }

        /// <summary>
        /// Get list members
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="userId"></param>
        /// <param name="listId"></param>
        /// <returns></returns>
        public static IEnumerable<TwitterUser> GetListMembersAll(this CredentialProvider provider, string userId, string listId)
        {
            long n_cursor = -1;
            long c_cursor = -1;
            long p;
            while (n_cursor != 0)
            {
                foreach (var m in provider.GetListMembers(userId, listId, c_cursor, out p, out n_cursor))
                    yield return m;
                c_cursor = n_cursor;
            }
        }

        /// <summary>
        /// Get list members
        /// </summary>
        /// <param name="userId">user name</param>
        /// <param name="listId">list name</param>
        /// <returns>user array</returns>
        public static IEnumerable<TwitterUser> GetListMembers(this CredentialProvider provider, string userId, string listId)
        {
            long p, n;
            return provider.GetListMembers(userId, listId, -1, out p, out n);
        }

        /// <summary>
        /// Get list members
        /// </summary>
        /// <param name="userId">user name</param>
        /// <param name="listId">list name</param>
        /// <param name="cursor">cursor</param>
        /// <param name="prevCursor">previous cursor</param>
        /// <param name="nextCursor">next cursor</param>
        /// <returns>User enumeration</returns>
        public static IEnumerable<TwitterUser> GetListMembers(this CredentialProvider provider, string userId, string listId, long? cursor, out long prevCursor, out long nextCursor)
        {
            listId = listId.Replace("_", "-");

            var partialUri = userId + "/" + listId + "/members.xml";

            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (cursor != null)
            {
                para.Add(new KeyValuePair<string, string>("cursor", cursor.ToString()));
            }

            prevCursor = -1;
            nextCursor = -1;

            var doc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.GET, para);
            if (doc == null)
                return null;

            var ul = doc.Element("users_list");
            if (ul != null)
            {
                var nc = ul.Element("next_cursor");
                if (nc != null)
                    nextCursor = (long)nc.ParseLong();
                var pc = ul.Element("previous_cursor");
                if (pc != null)
                    prevCursor = (long)pc.ParseLong();
            }
            List<TwitterUser> users = new List<TwitterUser>();
            return from n in doc.Descendants("user")
                   let u = TwitterUser.CreateByNode(n)
                   where u != null
                   select u;
        }

        /// <summary>
        /// Get list full data
        /// </summary>
        private static IEnumerable<TwitterList> GetListData(this CredentialProvider provider, string partialUri, long? cursor, out long prevCursor, out long nextCursor)
        {
            List<KeyValuePair<string, string>> para = new List<KeyValuePair<string, string>>();
            if (cursor != null)
            {
                para.Add(new KeyValuePair<string, string>("cursor", cursor.ToString()));
            }

            prevCursor = 0;
            nextCursor = 0;

            var doc = provider.RequestAPI(partialUri, CredentialProvider.RequestMethod.GET, para);
            if (doc == null)
                return null;
            var ll = doc.Element ("lists_list");
            if (ll != null)
            {
                var nc = ll.Element("next_cursor");
                if (nc != null)
                    nextCursor = (long)nc.ParseLong();
                var pc = ll.Element("previous_cursor");
                if (pc != null)
                    prevCursor = (long)pc.ParseLong();
            }


            return from n in doc.Descendants("list")
                   let l = TwitterList.CreateByNode(n)
                   where l != null
                   select l;
        }

        /// <summary>
        /// Get lists you following
        /// </summary>
        public static IEnumerable<TwitterList> GetFollowingListsAll(this CredentialProvider provider, string userId)
        {
            foreach (var l in provider.GetListsAll(userId))
                yield return l;
            foreach (var l in provider.GetSubscribedListsAll(userId))
                yield return l;
        }

        /// <summary>
        /// Get lists all
        /// </summary>
        public static IEnumerable<TwitterList> GetListsAll(this CredentialProvider provider, string userId)
        {
            long n_cursor = -1;
            long c_cursor = -1;
            long p;
            while (n_cursor != 0)
            {
                var lists = provider.GetLists(userId, c_cursor, out p, out n_cursor);
                if (lists != null)
                    foreach (var l in lists)
                        yield return l;
                c_cursor = n_cursor;
            }
        }

        /// <summary>
        /// Get lists someone created 
        /// </summary>
        public static IEnumerable<TwitterList> GetLists(this CredentialProvider provider, string userId)
        {
            long n, p;
            return provider.GetLists(userId, -1, out p, out n);
        }

        /// <summary>
        /// Get lists someone created with full params
        /// </summary>
        public static IEnumerable<TwitterList> GetLists(this CredentialProvider provider, string userId, long? cursor, out long prevCursor, out long nextCursor)
        {
            var partialUri = userId + "/lists.xml";
            return provider.GetListData(partialUri, cursor, out prevCursor, out nextCursor);
        }

        /// <summary>
        /// Get all lists which member contains you
        /// </summary>
        public static IEnumerable<TwitterList> GetMembershipListsAll(this CredentialProvider provider, string userId)
        {
            long n_cursor = -1;
            long c_cursor = -1;
            long p;
            while (n_cursor != 0)
            {
                var lists = provider.GetMembershipLists(userId, c_cursor, out p, out n_cursor);
                if (lists != null)
                    foreach (var l in lists)
                        yield return l;
                c_cursor = n_cursor;
            }
        }

        /// <summary>
        /// Get lists which member contains you
        /// </summary>
        public static IEnumerable<TwitterList> GetMembershipLists(this CredentialProvider provider, string userId)
        {
            long n, p;
            return provider.GetMembershipLists(userId, null, out p, out n);
        }

        /// <summary>
        /// Get lists which member contains you with page params
        /// </summary>
        public static IEnumerable<TwitterList> GetMembershipLists(this CredentialProvider provider, string userId, long? cursor, out long prevCursor, out long nextCursor)
        {
            var partialUri = userId + "/lists/memberships.xml";
            return provider.GetListData(partialUri, cursor, out prevCursor, out nextCursor);
        }

        /// <summary>
        /// Get subscribed lists all
        /// </summary>
        public static IEnumerable<TwitterList> GetSubscribedListsAll(this CredentialProvider provider, string userId)
        {
            long n_cursor = -1;
            long c_cursor = -1;
            long p;
            while (n_cursor != 0)
            {
                var lists = provider.GetSubscribedLists(userId, c_cursor, out p, out n_cursor);
                if (lists != null)
                    foreach (var l in lists)
                        yield return l;
                c_cursor = n_cursor;
            }
        }

        /// <summary>
        /// Get lists you subscribed
        /// </summary>
        public static IEnumerable<TwitterList> GetSubscribedLists(this CredentialProvider provider, string userId)
        {
            long n, p;
            return provider.GetSubscribedLists(userId, null, out p, out n);
        }

        /// <summary>
        /// Get lists you subscribed with full params
        /// </summary>
        public static IEnumerable<TwitterList> GetSubscribedLists(this CredentialProvider provider, string userId, long? cursor, out long prevCursor, out long nextCursor)
        {
            var partialUri = userId + "/lists/subscriptions.xml";
            return provider.GetListData(partialUri, cursor, out prevCursor, out nextCursor);
        }

        /// <summary>
        /// Get list data
        /// </summary>
        public static TwitterList GetList(this CredentialProvider provider, string userId, string listId)
        {
            var list = provider.RequestAPI(userId + "/lists/" + listId + ".xml",
                 CredentialProvider.RequestMethod.GET, null).Element("list");
            if (list != null)
                return TwitterList.CreateByNode(list);
            else
                return null;
        }

        /// <summary>
        /// Create or update list
        /// </summary>
        /// <returns></returns>
        private static TwitterList CreateOrUpdateList(this CredentialProvider provider, string id, string name, string description, bool? inPrivate)
        {
            var kvp = new List<KeyValuePair<string, string>>();
            if (id != null)
                kvp.Add(new KeyValuePair<string, string>("id", id));
            if (name != null)
                kvp.Add(new KeyValuePair<string, string>("name", name));
            if (description != null)
                kvp.Add(new KeyValuePair<string, string>("description", description));
            if (inPrivate != null)
                kvp.Add(new KeyValuePair<string, string>("mode", inPrivate.Value ? "private" : "public"));
            var list = provider.RequestAPI(
                "user/lists.xml",
                 CredentialProvider.RequestMethod.POST,
                 kvp).Element("list");
            if (list != null)
                return TwitterList.CreateByNode(list);
            else
                return null;
        }

        /// <summary>
        /// Create new list
        /// </summary>
        public static TwitterList CreateList(this CredentialProvider provider, string name, string description, bool? inPrivate)
        {
            return provider.CreateOrUpdateList(null, name, description, inPrivate);
        }

        /// <summary>
        /// Update list information<para />
        /// </summary>
        public static TwitterList UpdateList(this CredentialProvider provider, string id, string newName, string description, bool? inPrivate)
        {
            return provider.CreateOrUpdateList(id, newName, description, inPrivate);
        }

        /// <summary>
        /// Delete list you created
        /// </summary>
        public static TwitterList DeleteList(this CredentialProvider provider, string userId, string listId)
        {
            var kvp = new[] { new KeyValuePair<string, string>("_method", "DELETE") };
            var list = provider.RequestAPI(
                 userId + "/lists/" + listId + ".xml",
                  CredentialProvider.RequestMethod.POST,
                  kvp).Element("list");
            if (list != null)
                return TwitterList.CreateByNode(list);
            else
                return null;
        }

        #endregion

    }
}
