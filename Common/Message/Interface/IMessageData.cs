﻿using Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Every message type object will have his own IMessageData attribute
    /// </summary>
    public interface IMessageData: ISerialize, IDeserialize
    {
    }
}