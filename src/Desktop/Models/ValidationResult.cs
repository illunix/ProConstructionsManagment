namespace ProConstructionsManagment.Desktop.Models
{
    public sealed class ValidationResult
    {
        internal ValidationResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
        
        public bool IsSuccessful  { get; }
    }
}