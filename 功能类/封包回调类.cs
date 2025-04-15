using HTTP.封包类;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace HTTP.功能类
{
    public class 封包回调类 : 封包类.接口层
    {
        public void OnHttpCallback(HTTPEvent Conn) {}

        public void OnUdpCallback(UDPEvent Conn){}

        public void OnWebSocketCallback(WebSocketEvent Conn){}

        public void OnTcpCallback(TCPEvent Conn)
        {
            //你可以记录保存 Conn.TheologyID() 唯一ID,使用以下函数,在回调函数以外的任意位置发送数据
            //SunnyNet.Tools.TCPTools.SendMessage()
            //SunnyNet.Tools.TCPTools.Close()
            switch (Conn.Type())
            {

                case TCPEvent.EventType_TCP_About:
                    //Debug.WriteLine("TCP 即将连接:" + Conn.LocalAddr() + " -> " + Conn.RemoteAddr());
                    break;
                case TCPEvent.EventType_TCP_OK:
                    //Debug.WriteLine("TCP 连接成功:" + Conn.LocalAddr() + " -> " + Conn.RemoteAddr());
                    break;
                case TCPEvent.EventType_TCP_Send:
                    if (工具类.IsContains(Conn.Body().Bytes, new byte[] { 0x01, 0x2D, 0x00, 0x8D, 0x00, 0x00, 0x00 })) 
                    {
                        Debug.WriteLine("TCP 拦截到->发送端->过图数据:" + Conn.LocalAddr() + " -> " + Conn.RemoteAddr() + ",发送:" + Conn.Body().ToString());
                    }
                    break;
                case TCPEvent.EventType_TCP_Receive:
                    //Debug.WriteLine("TCP 收到数据:" + Conn.LocalAddr() + " -> " + Conn.RemoteAddr() + ",接收:" + Conn.Body().Length + " / byte");
                    break;
                case TCPEvent.EventType_TCP_Close:
                    //Debug.WriteLine("TCP 连接关闭:" + Conn.LocalAddr() + " -> " + Conn.RemoteAddr());
                    break;
            }
        }

       
    }
}
