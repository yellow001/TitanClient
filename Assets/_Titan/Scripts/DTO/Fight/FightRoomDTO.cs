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
        Dictionary<string, int> nameToModelID = new Dictionary<string, int>();

        [ProtoMember(2)]
        Dictionary<int, BaseModel> baseModelDic = new Dictionary<int, BaseModel>();

        [ProtoMember(3)]
        float allTime = 0;

        [ProtoMember(5)]
        List<string> enterName = new List<string>();

        public FightRoomDTO() { }
    }
}
