using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using ServerSimple.Data;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class MoveDataDTO
    {
        [ProtoMember(1)]
        public float Horizontal;

        [ProtoMember(2)]
        public float Vertical;

        [ProtoMember(3)]
        public int modelID;

        [ProtoMember(4)]
        public bool RMB;

        [ProtoMember(5)]
        public float boneRoX;

        [ProtoMember(6)]
        public bool LMB;

        [ProtoMember(7)]
        public Vector3Ex Rotation;

    }
}
