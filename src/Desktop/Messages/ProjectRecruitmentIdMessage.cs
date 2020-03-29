using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProConstructionsManagment.Desktop.Messages
{
    public class ProjectRecruitmentIdMessage
    {
        public ProjectRecruitmentIdMessage(string projectRecruitmentId)
        {
            ProjectRecruitmentId = projectRecruitmentId;
        }

        public string ProjectRecruitmentId { get; }
    }
}
