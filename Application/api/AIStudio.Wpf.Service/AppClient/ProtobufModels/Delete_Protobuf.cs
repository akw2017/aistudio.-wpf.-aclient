﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStudio.Wpf.Service.AppClient.ProtobufModels
{
    [ProtoContract]
    public class Delete_Protobuf
    {
        [ProtoMember(1)] 
        public List<string> ids { get; set; }
    }
}
