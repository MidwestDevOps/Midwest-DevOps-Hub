using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    public class ProjectModel : BaseModel
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

        public ProjectModel()
        {

        }

        public ProjectModel (DataEntities.Project p)
        {
            this.ProjectID = p.ProjectID;
            this.Name = p.Name;
            this.Description = p.Description;
            this.Active = p.Active;
            this.CreatedBy = p.CreatedBy;
            this.CreatedDate = p.CreatedDate;
            this.ModifiedBy = p.ModifiedBy;
            this.ModifiedDate = p.ModifiedDate;
        }

        public DataEntities.Project ConvertToEntity()
        {
            DataEntities.Project p = new DataEntities.Project();

            p.ProjectID = this.ProjectID;
            p.Name = this.Name;
            p.Description = this.Description;
            p.Active = this.Active;
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = this.ModifiedBy;
            p.ModifiedDate = this.ModifiedDate;

            return p;
        }
    }
}
