using System;
using ProConstructionsManagment.Core.Entities;

namespace ProConstructionsManagment.Core.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}