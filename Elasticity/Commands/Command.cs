using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticity.Commands
{
    public class Command
    {
        public Command()
        {
        }

        public int OriginalVersion { get; protected set; }
    }
}
