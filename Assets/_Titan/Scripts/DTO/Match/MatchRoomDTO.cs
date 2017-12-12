using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSimple.DTO.Match
{
    [Serializable]
    [ProtoContract]
    public class MatchRoomDTO
    {
        [ProtoMember(1)]
        /// <summary>
        /// 房间id
        /// </summary>
        public int index;

        [ProtoMember(2)]
        /// <summary>
        /// 最多人数
        /// </summary>
        public int maxNum;

        [ProtoMember(3)]
        public string masterName;

        [ProtoMember(4)]
        /// <summary>
        /// 密码
        /// </summary>
        public string passwd;

        [ProtoMember(5)]
        public List<string> playerList = new List<string>();

        public MatchRoomDTO() { }

        public MatchRoomDTO(int inx,int maxNum,string mName,string[] pList,string pwd=null) {
            index = inx;
            this.maxNum = maxNum;
            masterName = mName;
            playerList.AddRange(playerList);
            passwd = pwd;
        }
    }
}
