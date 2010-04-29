using System;
using System.Xml.Linq;

namespace Std.Tweak
{
    public class TwitterStatus : TwitterStatusBase
    {
        /// <summary>
        /// Create twitter status from XML node
        /// </summary>
        public static TwitterStatus CreateByNode(XElement sNode)
        {
            return new TwitterStatus(sNode);
        }

        /// <summary>
        /// Twitter status constructor with XML data
        /// </summary>
        /// <param name="node"></param>
        private TwitterStatus(XElement node)
            : base()
        {
            this.Id = node.Element("id").ParseLong();

            this.Truncated = node.Element("truncated").ParseBool();

            this.Text = node.Element("text").ParseString();

            this.Source = node.Element("source").ParseString();

            this.Favorited = node.Element("favorited").ParseBool();

            this.CreatedAt = node.Element("created_at").ParseDateTime("ddd MMM d HH':'mm':'ss zzz yyyy");

            this.InReplyToStatusId = node.Element("in_reply_to_status_id").ParseLong();

            this.InReplyToUserId = node.Element("in_reply_to_user_id").ParseString();

            this.InReplyToScreenName = node.Element("in_reply_to_screen_name").ParseString();

            if (node.Element("retweeted_status") != null)
            {
                this.Kind = StatusKind.Retweeted;
                this.RetweetedOriginal = TwitterStatus.CreateByNode(node.Element("retweeted_status"));
            }
            else
            {
                this.Kind = StatusKind.Normal;
            }

            this.User = TwitterUser.CreateByNode(node.Element("user"));
        }

        /// <summary>
        /// Truncated status
        /// </summary>
        public bool Truncated { get; set; }

        /// <summary>
        /// Favorited this
        /// </summary>
        public bool Favorited { get; set; }

        /// <summary>
        /// Source param
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Status id which is mentioned this
        /// </summary>
        public long InReplyToStatusId { get; set; }

        /// <summary>
        /// User id which has someone who is mentioned this
        /// </summary>
        public string InReplyToUserId { get; set; }

        /// <summary>
        /// User screen name which has someone who is mentioned this
        /// </summary>
        public string InReplyToScreenName { get; set; }

        /// <summary>
        /// Original status in this if this tweet is official-retweet some tweet.
        /// </summary>
        public TwitterStatus RetweetedOriginal { get; set; }
    }
}
