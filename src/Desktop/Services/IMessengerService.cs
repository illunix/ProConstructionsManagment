using System;

namespace ProConstructionsManagment.Desktop.Services
{
    public interface IMessengerService
    {
        void Send<TMessage>(TMessage message);

        void Register<TMessage>(object receipient, Action<TMessage> action);

        void UnregisterAllMessages(object context);
    }
}