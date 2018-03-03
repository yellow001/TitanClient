using ProtoBuf;
using ServerSimple.DTO.Login;
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
        public long id;

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
        public List<UserDTO> playerList = new List<UserDTO>();

        public MatchRoomDTO() { }

        public MatchRoomDTO(long inx,int maxNum,string mName, UserDTO[] pList,string pwd=null) {
            id = inx;
            this.maxNum = maxNum;
            masterName = mName;
            playerList.AddRange(pList);
            passwd = pwd;
        }
    }
}
