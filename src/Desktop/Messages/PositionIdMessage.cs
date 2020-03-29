using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class PositionIdMessage
    {
        public PositionIdMessage(string positionId)
        {
            PositionId = positionId;
        }

        public string PositionId { get; }
    }
}
