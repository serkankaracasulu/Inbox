using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.ResultObjects
{
    internal class RefreshToken : Token
    {
        public string refresh_token { get; set; }
    }
}

