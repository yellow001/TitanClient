using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetFrame.EnDecode
{
    public class EnDecodeFuntion {
        #region 长度编码解码
        /// <summary>
        /// 消息长度编码
        /// </summary>
        /// <param name="cache">序列化字节</param>
        /// <returns></returns>
        public static byte[] LengthEncoding(byte[] cache) {
            if (cache == null || cache.Length <= 0) { return null; }

            ByteArray ba = new ByteArray();
            ba.Write(cache.Length);
            ba.Write(cache);
            byte[] result = ba.GetBuffer();
            ba.Close();

            return result;

        }

        /// <summary>
        /// 消息长度解码
        /// </summary>
        /// <param name="cache">消息缓存区</param>
        /// <returns>消息体字节流</returns>
        public static byte[] LengthDecoding(ref List<byte> cache) {
            if (cache == null || cache.Count < 4) { return null; }
            else {

                ByteArray ba = new ByteArray(cache.ToArray());
                int len = ba.ReadInt32();

                //如果长度不够，返回
                if (cache.Count - 4 < len) {
                    ba.Close();

                    return null;

                }
                else {

                    byte[] value = new byte[len];
                    Buffer.BlockCopy(cache.ToArray(), 4, value, 0, value.Length);
                    cache.RemoveRange(0, 4 + len);

                    ba.Close();

                    return value;

                }
            }

        }
        #endregion

        #region json 编码解码
        public static byte[] JsonEncoding<T>(T message) {

            if (message == null) { return null; }
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }

        public static T JsonDecoding<T>(string value) {
            if (value == null || value.Equals(string.Empty)) { return default(T); }
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T JsonDecoding<T>(byte[] v) {
            string value = Encoding.UTF8.GetString(v);
            if (value == null || value.Equals(string.Empty)) { return default(T); }
            return JsonConvert.DeserializeObject<T>(value);
        }
        #endregion

        #region protobuf 编码解码
        public static byte[] PbEncoding<T>(T message) {
            if (message == null) { return null; }

            MemoryStream ms = new MemoryStream();

            try {
                Serializer.Serialize<T>(ms, message);
                byte[] result = new byte[ms.Length];
                //Buffer.BlockCopy(ms.ToArray(), 0, result, 0, result.Length);
                result = ms.ToArray();
                ms.Dispose();
                return result;
            }
            catch (Exception ex) {
                ms.Dispose();
                return null;
            }

        }

        public static T PbDecoding<T>(byte[] value) {
            if (value == null || value.Length == 0) { return default(T); }

            MemoryStream ms = new MemoryStream(value);

            T result;

            try {
                result = Serializer.Deserialize<T>(ms);
            }
            catch (Exception ex) {
                ms.Dispose();
                return default(T);
            }

            ms.Dispose();

            return result;

        }
        #endregion


    }
}
