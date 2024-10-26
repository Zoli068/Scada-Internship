using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    public interface IAsyncSecureCommunication
    {
        /// <summary>
        /// Definition of the method for securing a stream
        /// </summary>
        /// <param name="stream">The stream which we want to secure</param>
        /// <returns>The secured stream
        Task<Stream> SecureStream(Stream stream);
    }
}
