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

        public int? CreatedBy
        {
            get; set;
        }

        public DateTime? CreatedDate
        {
            get; set;
        }

        public int? ModifiedBy
        {
            get; set;
        }

        public DateTime? ModifiedDate
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
            p.CreatedBy = this.CreatedBy;
            p.CreatedDate = this.CreatedDate;
            p.ModifiedBy = p.ModifiedBy;
            p.ModifiedDate = p.ModifiedDate;

            return p;
        }
    }
}
