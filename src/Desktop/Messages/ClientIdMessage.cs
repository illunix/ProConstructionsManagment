using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class ClientIdMessage
    {
        public ClientIdMessage(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; }
    }
}
