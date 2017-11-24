using System;
using System.Collections.Generic;
using System.Text;

namespace NetFrame.EnDecode
{
    public class AbsCoding {
        static AbsCoding ins;

        public static AbsCoding Ins {
            get {
                if (ins == null) {
                    return null;
                }
                return ins;
            }
            set {
                ins = value;
            }

        }

        public virtual byte[] MsgEncoding<T>(T msg) {
            return null;
        }

        public virtual T MsgDecoding<T>(byte[] msgBytes) {
            return default(T);
        }

        public virtual byte[] ModelEncoding<T>(T model) where T : TransModel {
            return null;
        }

        public virtual T ModelDecoding<T>(ref List<byte> cache) where T : TransModel {
            return null;
        }

        protected virtual byte[] ExEncode(byte[] value) {
            //nothing to do
            return value;
        }

        protected virtual byte[] ExDecode(byte[] value) {
            //nothing to do
            return value;
        }
    }
}
