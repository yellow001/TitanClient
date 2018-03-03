using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class ShootDataDTO
    {
        [ProtoMember(1)]
        public bool Aim;

        [ProtoMember(2)]
        public bool Fire;
    }
}
