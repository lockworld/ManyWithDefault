using MWD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Entities
{
    public class MWDEntity
    {
        public MWDEntity()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }

    }
}
