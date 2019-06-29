using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class ProjectModel
    {
        public int? ProjectID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public DateTime? CreatedDate
        {
            get; set;
        }

        public ProjectModel()
        {

        }

        public ProjectModel (DataEntities.Project p)
        {
            this.ProjectID = p.ProjectID;
            this.Name = p.Name;
            this.Description = p.Description;
            this.CreatedDate = p.CreatedDate;
        }

        public DataEntities.Project ConvertToEntity()
        {
            DataEntities.Project p = new DataEntities.Project();

            p.ProjectID = this.ProjectID;
            p.Name = this.Name;
            p.Description = this.Description;
            p.CreatedDate = this.CreatedDate;

            return p;
        }
    }
}
