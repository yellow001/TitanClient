using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class DamageDTO
    {
        [ProtoMember(1)]
        public int SrcID;

        [ProtoMember(2)]
        public int DstID;

        [ProtoMember(3)]
        public int DamageType;
    }
}
