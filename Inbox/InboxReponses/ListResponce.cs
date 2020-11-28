using Inbox.InboxReponses.ResultObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses
{
    internal class ListResponce<R> : IResultObject where R : IResultObject
    {
        public int displayCount { get; set; }
        public int totalCount { get; set; }
        public IEnumerable<R> items { get; set; }
    }
}
