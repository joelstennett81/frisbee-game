using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frisbeev01
{   // This is used solely for global constants. It chooses how much info to print out to the user, as well as what gameplay is being played
    static class FullProgram
    {
        public const int Verbosity = 1;  // 1 means only important info. 2 means more info. 3 means literally everything
        public const int GameType = 2;  // 1 means simulation, 2 means manual play
    }
}
