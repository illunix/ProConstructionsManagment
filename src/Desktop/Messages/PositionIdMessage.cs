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