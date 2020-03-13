namespace ProConstructionsManagment.Desktop.Messages
{
    public class ProjectIdMessage
    {
        public ProjectIdMessage(string projectId)
        {
            ProjectId = projectId;
        }

        public string ProjectId { get; }
    }
}