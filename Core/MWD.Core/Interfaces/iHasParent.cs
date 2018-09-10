using MWD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWD.Core.Interfaces
{
    public interface iHasParent
    {
        Guid ForeignKey { get; set; }
    }
}
