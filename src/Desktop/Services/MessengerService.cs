using GalaSoft.MvvmLight.Messaging;
using System;

namespace ProConstructionsManagment.Desktop.Services
{
    public class MessengerService : IMessengerService
    {
        public void Send<TMessage>(TMessage message)
        {
            Messenger.Default.Send(message);
        }

        public void Register<TMessage>(object receipient, Action<TMessage> action)
        {
            Messenger.Default.Register(receipient, action);
        }

        public void UnregisterAllMessages(object context)
        {
            Messenger.Default.Unregister(context);
        }
    }
}