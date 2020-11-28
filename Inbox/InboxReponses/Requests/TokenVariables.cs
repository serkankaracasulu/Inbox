using System;
using System.Collections.Generic;
using System.Text;

namespace Inbox.InboxReponses.Requests
{
    internal class TokenVariables
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public TokenVariables(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
