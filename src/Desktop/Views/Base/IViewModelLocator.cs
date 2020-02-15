namespace ProConstructionsManagment.Desktop.Views.Base
{
    public interface IViewModelLocator
    {
        T Get<T>();
    }
}