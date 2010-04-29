using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Std.Tweak
{
    public class TwitterList
    {
        /// <summary>
        /// List id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// List name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List full-name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// List slug
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// List description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// List subscriber count
        /// </summary>
        public long SubscriberCount { get; set; }

        /// <summary>
        /// List member count
        /// </summary>
        public long MemberCount { get; set; }

        /// <summary>
        /// List partial uri
        /// </summary>
        public string PartialUri { get; set; }

        /// <summary>
        /// List open mode
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Is private this list
        /// </summary>
        public bool Private
        {
            get { return this.Mode == "private"; }
        }

        /// <summary>
        /// Parent user
        /// </summary>
        public TwitterUser User { get; set; }

        /// <summary>
        /// Members enumerator
        /// </summary>
        public TwitterUser[] Members { get; set; }

        public static TwitterList CreateByNode(XElement lNode)
        {
            return new TwitterList(lNode);
        }

        public TwitterList() { }

        private TwitterList(XElement node)
        {
            this.Id = node.Element("id").ParseLong();

            this.Name = node.Element("name").ParseString();

            this.FullName = node.Element("full_name").ParseString();

            this.Slug = node.Element("slug").ParseString();

            this.Description = node.Element("description").ParseString();

            this.SubscriberCount = node.Element("subscriber_count").ParseLong();

            this.MemberCount = node.Element("member_count").ParseLong();

            this.PartialUri = node.Element("uri").ParseString();

            this.Mode = node.Element("mode").ParseString();

            this.User = TwitterUser.CreateByNode(node.Element("user"));
        }

        public void SetUsers(IEnumerable<TwitterUser> members)
        {
            this.Members = members.ToArray();
        }

        /// <summary>
        /// Get list id
        /// </summary>
        public static string GetListId(XElement node)
        {
            if (node == null ||  node.Element("id") == null)
                return null;
            return node.Element("id").Value;
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
