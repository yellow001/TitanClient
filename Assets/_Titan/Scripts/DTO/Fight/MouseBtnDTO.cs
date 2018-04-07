using ProtoBuf;
using System;

namespace ServerSimple.DTO.Fight {
    [Serializable]
    [ProtoContract]
    public class MouseBtnDTO {
        [ProtoMember(1)]
        public int modelID;

        [ProtoMember(2)]
        public bool btn;
    }
}
