using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace ServerSimple.DTO.Login
{
    [Serializable]
    [ProtoContract]
    public class UserDTO {
        [ProtoMember(1)]
        public string name;

        [ProtoMember(2)]
        public string password;

        [ProtoMember(3)]
        public int winCount;

        [ProtoMember(4)]
        public string headID;

        [ProtoMember(5)]
        public string hairData;

        [ProtoMember(6)]
        public string clothData;

        public UserDTO() { }

        public UserDTO(string n, string p,string head,string hair,string cloth, int wc=0) {
            name = n;
            password = p;
            headID = head;
            hairData = hair;
            clothData = cloth;
            winCount = wc;
        }
    }
}
