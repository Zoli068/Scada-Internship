using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommunicationOptions
    {
        CommunicationType CommunicationType { get; }

        SecurityMode SecurityMode { get; }

        //needed for the messages, to know where to find the length value in the responses
        int LengthAttributePosition { get; }
    }
}
