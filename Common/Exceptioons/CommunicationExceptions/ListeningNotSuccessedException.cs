using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptioons.CommunicationExceptions
{
    public class ListeningNotSuccessedException:Exception
    {
        public ListeningNotSuccessedException():base("The server can't listening for incoming connections") { }
    
        public ListeningNotSuccessedException(string message):base(message) { }
    }
}
