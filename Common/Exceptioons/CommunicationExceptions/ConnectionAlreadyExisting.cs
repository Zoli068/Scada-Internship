using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommunicationExceptions
{
    public class ConnectionAlreadyExisting:Exception
    {
        public ConnectionAlreadyExisting():base("Connection already got established") { }

        public ConnectionAlreadyExisting(string message) : base(message) { }
    }
}
