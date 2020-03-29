using System;
using System.Collections.Generic;
using System.Text;
using ProConstructionsManagment.Core.Entities.Base;

namespace ProConstructionsManagment.Core.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }
        public string JobDescription { get; set; }
    }
}
