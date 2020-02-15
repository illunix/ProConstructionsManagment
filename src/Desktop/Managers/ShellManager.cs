using ProConstructionsManagment.Desktop.Messages;
using ProConstructionsManagment.Desktop.Services;

namespace ProConstructionsManagment.Desktop.Managers
{
    public class ShellManager : IShellManager
    {
        private readonly IMessengerService _messengerService;

        public ShellManager(IMessengerService messengerService)
        {
            _messengerService = messengerService;
        }

        public void SetLoadingData(bool isLoadingData)
        {
            _messengerService.Send(new LoadingDataMessage(isLoadingData));
        }
    }
}