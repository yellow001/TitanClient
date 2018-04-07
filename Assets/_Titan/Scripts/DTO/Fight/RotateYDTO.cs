using ProtoBuf;
using System;

namespace ServerSimple.DTO.Fight {
    [Serializable]
    [ProtoContract]
    public class RotateYDTO {
        [ProtoMember(1)]
        public int modelID;

        [ProtoMember(2)]
        public float rotateY;
    }
}
