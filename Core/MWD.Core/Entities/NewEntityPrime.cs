using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Entities
{
    public class NewEntityPrime : MWDEntity
    {
        public string Name { get; set; }

        [ForeignKey("ForeignKey_ID")]
        public List<NewEntitySub> Subs { get; set; }
    }
}
