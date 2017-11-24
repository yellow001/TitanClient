using System;
using System.Collections.Generic;
using System.Text;

namespace NetFrame.EnDecode.Extend
{

    public class JsonCoding : AbsCoding {

        public override byte[] MsgEncoding<T>(T msg) {
            return EnDecodeFuntion.JsonEncoding(msg);
        }

        public override T MsgDecoding<T>(byte[] msgBytes) {
            return EnDecodeFuntion.JsonDecoding<T>(msgBytes);
        }

        public override T ModelDecoding<T>(ref List<byte> cache) {
            try {
                //长度解码
                byte[] value = EnDecodeFuntion.LengthDecoding(ref cache);

                //解码失败(长度不够)
                if (value == null) {
                    return null;
                }

                //否则，调用子类的解码方法(加密、压缩等)
                byte[] value2 = ExDecode(value);

                //解码失败(子类解码出错)
                if (value2 == null) {
                    return null;
                }

                //最后，调用传输模型的解码方法
                return EnDecodeFuntion.JsonDecoding<T>(Encoding.UTF8.GetString(value2));
            }
            catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
                return null;
            }

        }

        public override byte[] ModelEncoding<T>(T model) {

            try {
                //先进行传输模型编码
                byte[] value = EnDecodeFuntion.JsonEncoding(model);

                //编码失败，序列化出错
                if (value == null) {
                    return null;
                }

                //否则，调用子类的编码方法（加密、压缩等）
                byte[] value2 = ExEncode(value);

                //编码失败，子类编码出错
                if (value2 == null) {
                    return null;
                }

                //最后，进行长度编码
                return EnDecodeFuntion.LengthEncoding(value2);
            }
            catch (Exception ex) {
                //Console.WriteLine(ex.ToString());
                return null;
            }

        }
    }
}
