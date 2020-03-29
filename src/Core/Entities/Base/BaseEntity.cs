using ProConstructionsManagment.Core.Interfaces;
using System;

namespace ProConstructionsManagment.Core.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}