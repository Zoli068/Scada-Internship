using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    /// <summary>
    /// The exception that is thrown when inside the DTO class there is a bad value;
    /// </summary> 
    public class MessageDTOBadValuesExceptioncs : Exception
    {
        /// <summary>
        /// The exception that is thrown when inside the DTO class there is a bad value;
        /// </summary>  
        public MessageDTOBadValuesExceptioncs() : base("Value is out from the interval") { }

        /// <summary>
        /// The exception that is thrown when inside the DTO class there is a bad value;
        /// </summary> 
        public MessageDTOBadValuesExceptioncs(string message) : base(message) { }
    }
}
