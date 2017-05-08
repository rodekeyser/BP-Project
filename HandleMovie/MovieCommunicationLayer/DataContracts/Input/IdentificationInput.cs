﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieCommunicationLayer.DataContracts.Input
{
    [DataContract]
    public class IdentificationInput: BaseInput
    {
        [DataMember]
        public int Id { get; set; }
    }
}