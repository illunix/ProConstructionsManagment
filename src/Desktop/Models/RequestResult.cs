namespace ProConstructionsManagment.Desktop.Models
{
    public sealed class RequestResult<T> where T : class
    {
        internal RequestResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
        
        public bool IsSuccessful  { get; }
    }
}