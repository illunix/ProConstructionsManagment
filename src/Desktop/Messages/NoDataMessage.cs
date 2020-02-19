namespace ProConstructionsManagment.Desktop.Messages
{
    public class NoDataMessage
    {
        public NoDataMessage(bool noData)
        {
            NoData = noData;
        }

        public bool NoData { get; }
    }
}