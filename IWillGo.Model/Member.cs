using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWillGo.Model
{
    public class Member : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        /*public bool Confirm { get; set; }*/

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
