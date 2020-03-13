using System;
using ProConstructionsManagment.Core.Interfaces;

namespace ProConstructionsManagment.Core.Entities.Base
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}