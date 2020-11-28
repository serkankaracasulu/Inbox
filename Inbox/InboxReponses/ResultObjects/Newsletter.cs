using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.ResultObjects
{
    public class Newsletter : IResultObject
    {
        public string id { get; set; }
        public DateTime createTime { get; set; }
        public DateTime updateTime { get; set; }
        public string subject { get; set; }
        public int type { get; set; }
        public string language { get; set; }
    }
}
