using ProConstructionsManagment.Desktop.Enums;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class CurrentViewTypeMessage
    {
        public CurrentViewTypeMessage(ViewTypes viewType)
        {
            ViewType = viewType;
        }

        public ViewTypes ViewType { get; }
    }
}