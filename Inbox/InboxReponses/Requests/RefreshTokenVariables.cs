using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.Requests
{
    internal class RefreshTokenVariables
    {
        public string RefreshToken { get; set; }
        public RefreshTokenVariables(string token)
        {
            RefreshToken = token;
        }
    }
}
