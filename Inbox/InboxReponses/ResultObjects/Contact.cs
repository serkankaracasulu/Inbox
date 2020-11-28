using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.ResultObjects
{
    public class Contact : IResultObject
    {
        public string id { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public string listName { get; set; }
        public string groupId { get; set; }
        public int legislation { get; set; }
        public int count { get; set; }
    }
}
