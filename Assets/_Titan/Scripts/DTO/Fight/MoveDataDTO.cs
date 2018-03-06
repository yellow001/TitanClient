using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class MoveDataDTO
    {
        [ProtoMember(1)]
        public bool Fwd;

        [ProtoMember(2)]
        public bool Bwd;

        [ProtoMember(3)]
        public bool Left;

        [ProtoMember(4)]
        public bool Right;


        [ProtoMember(5)]
        public int modelID;
    }
}
