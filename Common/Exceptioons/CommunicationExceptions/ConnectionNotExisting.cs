using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommunicationExceptions
{
    public class ConnectionNotExisting:Exception
    {
        public ConnectionNotExisting():base("There is no existing connection") { }

        public ConnectionNotExisting(string message):base(message) { }
    }
}
