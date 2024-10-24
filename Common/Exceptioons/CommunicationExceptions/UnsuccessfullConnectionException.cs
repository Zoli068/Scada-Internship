using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UnsuccessfullConnectionException:Exception
    {
        public UnsuccessfullConnectionException():base("Can't connect to the server") { }

        public UnsuccessfullConnectionException(string message) : base(message) { }
    }
}
