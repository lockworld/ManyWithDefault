using MWD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Entities
{
    public class Person : MWDEntity, iHasEmail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
