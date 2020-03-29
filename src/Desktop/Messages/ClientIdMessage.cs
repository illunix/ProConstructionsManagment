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