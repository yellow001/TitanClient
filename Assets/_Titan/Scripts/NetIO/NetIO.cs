using NetFrame.EnDecode;
using NetFrame.EnDecode.Extend;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace NetFrame {
    public class NetIO {
        private static NetIO ins;

        public static NetIO Ins {
            get {
                if (ins == null) {
                    ins = new NetIO();
                }
                return ins;
            }
        }
        
        Socket socket;

        /// <summary>
        /// socket接收数据时用到的缓冲区
        /// </summary>
        byte[] readBuffer = new byte[1042];

        //处理数据的缓冲区
        List<byte> cache = new List<byte>();

        //发送数据的消息队列
        public Queue<TransModel> write_queue = new Queue<TransModel>();

        bool isWrite, isRead;

        public List<TransModel> msg=new List<TransModel>();

        //public static string IP = AppConst.gateIP;

        private NetIO() {
            try {

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                AbsCoding.Ins = new PbCoding();

                socket.Connect(AppSetting.GetSetting("ServerIP"), int.Parse(AppSetting.GetSetting("ServerPort")));

                socket.BeginReceive(readBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, null);
            }
            catch (Exception ex) {
                Debug.Log("无法连接服务器，请重启客户端或联系服务器管理员");
                AlertMgr.Ins.AddMsg("无法连接服务器，请重启客户端或联系服务器管理员");
            }
        }

        void ReceiveCallBack(IAsyncResult result) {
            int length = socket.EndReceive(result);
            byte[] data = new byte[length];
            Buffer.BlockCopy(readBuffer, 0, data, 0, length);

            cache.AddRange(data);

            if (!isRead) {
                isRead = true;
                Read();
            }

            socket.BeginReceive(readBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, null);
        }

        /// <summary>
        /// 对缓冲区数据进行处理
        /// </summary>
        void Read() {

            TransModel model = AbsCoding.Ins.ModelDecoding<TransModel>(ref cache);
            if (model == null) {
                isRead = false;
                return;
            }
            msg.Add(model);
            //todo 处理 model
            //example
            //Console.Write(model.GetMsg<string>());

            //TransModel m = new TransModel(1, 2, 3);
            //m.SetMsg("i am yellow client");
            //Send(m);

            Read();
        }

        public void Send(TransModel model) {

            write_queue.Enqueue(model);

            if (!isWrite) {
                isWrite = true;
                Write();
            }
        }

        public void Write() {
            if (write_queue.Count == 0) {
                isWrite = false;
                return;
            }

            //消息队列不为空，取出来并发送
            TransModel m = write_queue.Dequeue();

            byte[] queue = AbsCoding.Ins.ModelEncoding(m);

            if (queue == null) {
                isWrite = false;
                return;
            }

            try {
                socket.BeginSend(queue, 0, queue.Length, SocketFlags.None, SendCallBack, null);
            }
            catch (Exception ex) {

            }

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="result"></param>
        void SendCallBack(IAsyncResult result) {
            socket.EndSend(result);
            Write();
        }
    }
}
