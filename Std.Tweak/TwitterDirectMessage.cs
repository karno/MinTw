using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Std.Tweak
{
    /// <summary>
    /// Twitter direct message
    /// </summary>
    public class TwitterDirectMessage : TwitterStatusBase
    {
        /// <summary>
        /// Direct message class create by node
        /// </summary>
        /// <param name="dmNode">node</param>
        /// <returns>instance</returns>
        public static TwitterDirectMessage CreateByNode(XElement dmNode)
        {
            return new TwitterDirectMessage(dmNode);
        }

        private TwitterDirectMessage(XElement node)
            : base()
        {
            System.Diagnostics.Debug.WriteLine(node.ToString());
            this.Id = node.Element("id").ParseLong();

            this.Text = node.Element("text").ParseString();

            this.CreatedAt = node.Element("created_at").ParseDateTime("ddd MMM d HH':'mm':'ss zzz yyyy");

            this.Sender = TwitterUser.CreateByNode(node.Element("sender"));

            this.Recipient = TwitterUser.CreateByNode(node.Element("recipient"));
        }

        /// <summary>
        /// Status kind
        /// </summary>
        public override TwitterStatusBase.StatusKind Kind
        {
            get { return StatusKind.DirectMessage; }
            set { }
        }

        /// <summary>
        /// Alias of User property
        /// </summary>
        public TwitterUser Sender
        {
            get { return this.User; }
            set { this.User = value; }
        }

        /// <summary>
        /// Recipient user data
        /// </summary>
        public TwitterUser Recipient { get; set; }
    }
}
