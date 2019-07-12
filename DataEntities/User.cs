using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class User
    {
        public int? UserID
        {
            get; set;
        }

        public string UUID
        {
            get; set;
        }

        public string Username
        {
            get; set;
        }

        public string Password
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
    }
}
