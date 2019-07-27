using System;
using System.Collections.Generic;
using System.Text;

namespace HubModels
{
    class VersionModel
    {
        public int? VersionID
        {
            get; set;
        }

        public string ServerVersion
        {
            get; set;
        }

        public DateTime CreatedDate
        {
            get; set;
        }

        public VersionModel()
        {

        }

        public VersionModel(DataEntities.Version p)
        {
            this.VersionID = p.VersionID;
            this.ServerVersion = p.ServerVersion;
            this.CreatedDate = p.CreatedDate;
        }

        public DataEntities.Version ConvertToEntity()
        {
            DataEntities.Version p = new DataEntities.Version();

            p.VersionID = this.VersionID;
            p.ServerVersion = this.ServerVersion;
            p.CreatedDate = this.CreatedDate;

            return p;
        }
    }
}
