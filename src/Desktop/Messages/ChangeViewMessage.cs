using ProConstructionsManagment.Desktop.Enums;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class ChangeViewMessage
    {
        public ChangeViewMessage(ViewTypes view)
        {
            View = view;
        }

        public ViewTypes View { get; }
    }
}