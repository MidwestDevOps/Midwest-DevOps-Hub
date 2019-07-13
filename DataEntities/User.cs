using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities
{
    public class User : BaseEntity
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
    }
}
