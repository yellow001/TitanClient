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

        public UserDTO() { }

        public UserDTO(string n, string p,int wc=0) {
            name = n;
            password = p;
            winCount = wc;
        }
    }
}
