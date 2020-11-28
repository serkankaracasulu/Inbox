using Inbox.InboxReponses.ResultObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses
{
    internal class MainReponce<R> where R : IResultObject
    {
        public string version { get; set; }
        public bool resultStatus { get; set; }
        public int resultCode { get; set; }
        public string resultMessage { get; set; }
        public R resultObject { get; set; }

    }
}
