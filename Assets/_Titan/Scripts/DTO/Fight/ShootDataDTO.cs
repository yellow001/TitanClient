using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;
using ServerSimple.Data;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class ShootDTO
    {
        [ProtoMember(1)]
        public Vector3Ex BulletPos;

        [ProtoMember(2)]
        public Vector3Ex BulletRotate;

        [ProtoMember(3)]
        public int modelID;
    }
}
