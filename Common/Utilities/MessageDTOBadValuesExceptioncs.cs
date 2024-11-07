using System;

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
