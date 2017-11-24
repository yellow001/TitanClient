using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetFrame {
    public class ByteArray {
        MemoryStream ms;
        BinaryReader br;
        BinaryWriter bw;

        public ByteArray() {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        public ByteArray(byte[] cache) {
            ms = new MemoryStream(cache);
            bw = new BinaryWriter(ms);
            br = new BinaryReader(ms);
        }

        #region write

        public void Write(string value) {
            bw.Write(value);
        }

        public void Write(float value) {
            bw.Write(value);
        }

        public void Write(ulong value) {
            bw.Write(value);
        }

        public void Write(long value) {
            bw.Write(value);
        }

        public void Write(uint value) {
            bw.Write(value);
        }

        public void Write(int value) {
            bw.Write(value);
        }

        public void Write(ushort value) {
            bw.Write(value);
        }

        public void Write(short value) {
            bw.Write(value);
        }

        public void Write(char[] chars, int index, int count) {
            bw.Write(chars, index, count);
        }

        public void Write(double value) {
            bw.Write(value);
        }

        public void Write(char[] value) {
            bw.Write(value);
        }

        public void Write(char value) {
            bw.Write(value);
        }

        public void Write(sbyte value) {
            bw.Write(value);
        }

        public void Write(byte[] value) {
            bw.Write(value);
        }

        public void Write(byte value) {
            bw.Write(value);
        }

        public void Write(bool value) {
            bw.Write(value);
        }

        public void Write(decimal value) {
            bw.Write(value);
        }

        public void Write(byte[] buffer, int index, int count) {
            bw.Write(buffer, index, count);
        }

        #endregion

        #region read
        public bool ReadBool() {
            return br.ReadBoolean();
        }

        public byte ReadByte() {
            return br.ReadByte();
        }

        public byte[] ReadBytes(int count) {
            return br.ReadBytes(count);
        }
        public char ReadChar() {
            return br.ReadChar();
        }
        public char[] ReadChars(int count) {
            return br.ReadChars(count);
        }
        public decimal ReadDecimal() {
            return br.ReadDecimal();
        }

        public double ReadDouble() {
            return br.ReadDouble();
        }

        public short ReadInt16() {
            return br.ReadInt16();
        }

        public int ReadInt32() {
            return br.ReadInt32();
        }

        public long ReadInt64() {
            return br.ReadInt64();
        }

        public sbyte ReadSByte() {
            return br.ReadSByte();
        }
        public float ReadSingle() {
            return br.ReadSingle();
        }

        public string ReadString() {
            return br.ReadString();
        }

        public ushort ReadUInt16() {
            return br.ReadUInt16();
        }

        public uint ReadUInt32() {
            return br.ReadUInt32();
        }

        public ulong ReadUInt64() {
            return br.ReadUInt64();
        }


        #endregion

        public bool CanRead() {
            return ms.CanRead;
        }

        public byte[] GetBuffer() {
            byte[] result = new byte[ms.Length];
            //Buffer.BlockCopy(ms.ToArray(), 0, result, 0, (int)ms.Length);
            result = ms.ToArray();
            return result;
        }

        public byte[] GetLastBuffer() {
            long length = GetLength() - GetPosition();
            byte[] result = new byte[ms.Length];
            result = ReadBytes(result.Length);
            return result;
        }

        public long GetLength() {
            return ms.Length;
        }

        public long GetPosition() {
            return ms.Position;
        }

        public void Close() {
            br.Close();
            bw.Close();
            ms.Dispose();
        }
    }
}
