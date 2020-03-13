namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IEnumEntity<T> : IEntity where T : struct
    {
        T Status { get; set; }
    }
}