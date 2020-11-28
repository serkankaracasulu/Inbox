using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.ResultObjects
{
    internal class Token : IResultObject
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
