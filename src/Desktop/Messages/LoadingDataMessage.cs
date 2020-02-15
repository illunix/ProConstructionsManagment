namespace ProConstructionsManagment.Desktop.Messages
{
    public class LoadingDataMessage
    {
        public LoadingDataMessage(bool isLoadingData)
        {
            IsLoadingData = isLoadingData;
        }

        public bool IsLoadingData { get; }
    }
}