using Inbox.InboxReponses.ResultObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inbox
{
    public interface IInbox
    {
        Task<IEnumerable<Newsletter>> GetNewsletters();
        Task<IEnumerable<Contact>> GetContactLists();
    }
}
