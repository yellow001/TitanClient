using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace ServerSimple.DTO.Fight
{
    [Serializable]
    [ProtoContract]
    public class FightRoomDTO
    {
        [ProtoMember(1)]
        public Dictionary<string, int> nameToModelID = new Dictionary<string, int>();

        [ProtoMember(2)]
        public Dictionary<int, BaseModel> baseModelDic = new Dictionary<int, BaseModel>();

        [ProtoMember(3)]
        public float allTime = 0;

        [ProtoMember(5)]
        public List<string> enterName = new List<string>();

        public FightRoomDTO() { }
    }
}
