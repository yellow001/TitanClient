using NetFrame.EnDecode;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFrame {
    [Serializable]
    [ProtoContract]
    public class TransModel {
        [ProtoMember(1)]
        public int pID;
        [ProtoMember(2)]
        public int area;
        [ProtoMember(3)]
        public byte[] msgBytes;

        public TransModel() { }

        public TransModel(int pid, int a) {
            pID = pid;
            area = a;
        }

        public void SetMsg<T>(T msg) {
            try {
                if (AbsCoding.Ins != null) {
                    msgBytes = AbsCoding.Ins.MsgEncoding(msg);
                }
            }
            catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
            }
        }

        public T GetMsg<T>() {
            try {
                if (AbsCoding.Ins != null && msgBytes != null && msgBytes.Length > 0) {
                    return AbsCoding.Ins.MsgDecoding<T>(msgBytes);
                }
                return default(T);
            }
            catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
                return default(T);
            }

        }
    }
}
