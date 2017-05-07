﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace UserCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class RelationsInput: BaseInput
    {
        [DataMember]
        public int ChildId { get; set; }
        [DataMember]
        public int ParentId { get; set; }
    }
}