using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Entities
{
    public class NewEntitySub : MWDEntity
    {
        public Guid ForeignKey_ID { get; set; }
        public string RelatedInfo { get; set; }
    }
}
