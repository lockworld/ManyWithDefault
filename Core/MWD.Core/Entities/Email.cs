using MWD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Entities
{
    public class Email : MWDEntity, iHasDefault, iHasParent
    {
        public string EmailAddress { get; set; }
        public string Nickname { get; set; }
        public bool IsDefault { get; set; }
        public Guid ForeignKey { get; set; }
    }
}
