using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTP.封包类
{
    public class HTTPEvent
    {
        /// <summary>
        /// HTTP 事件类型常量：发起请求。
        /// </summary>
        public const int EventType_HTTP_Request = 1;

        /// <summary>
        /// HTTP 事件类型常量：请求完成。
        /// </summary>
        public const int EventType_HTTP_Response = 2;

        /// <summary>
        /// HTTP 事件类型常量：请求错误。
        /// </summary>
        public const int EventType_HTTP_Error = 3;

        private long __SunnyNetContext;
        private long __TheologyID;
        private long __MessageId;
        private long __EventType;
        private string __Method;
        private string __Url;
        private string __err;
        private bool ___Debug;
        private long __pid;
        private Request __Request;
        private Response __Response;

        /// <summary>
        /// HTTPEvent 构造函数，用于初始化事件对象。
        /// </summary>
        /// <param name="SunnyContext">Sunny 上下文。</param>
        /// <param name="TheologyID">事件的唯一标识符。</param>
        /// <param name="MessageId">消息 ID。</param>
        /// <param name="EventType">事件类型。</param>
        /// <param name="Method">HTTP 方法（如 GET、POST）。</param>
        /// <param name="Url">请求的 URL。</param>
        /// <param name="err">错误信息（如果有）。</param>
        /// <param name="pid">进程 ID。</param>
        public HTTPEvent(IntPtr SunnyNetContext, IntPtr TheologyID, IntPtr MessageId, IntPtr EventType, string Method, string Url, string err, IntPtr pid)
        {
            __SunnyNetContext = SunnyNetContext.ToInt64();
            __TheologyID = TheologyID.ToInt64();
            __MessageId = MessageId.ToInt64();
            __EventType = EventType.ToInt64();
            __Method = Method;
            __Url = Url;
            __err = err;
            __pid = pid.ToInt64();
            ___Debug = __err == "Debug";
            if (___Debug)
            {
                __err = "";
            }
            __Request = new Request(__MessageId);
            __Response = new Response(__MessageId);
        }
        /// <summary>
        /// 获取请求的 URL。
        /// </summary>
        /// <returns>请求的 URL。</returns>
        public string URL()
        {
            return __Url;
        }

        /// <summary>
        /// 获取请求方法。
        /// </summary>
        /// <returns>HTTP 方法。</returns>
        public string Method()
        {
            return __Method;
        }

        /// <summary>
        /// 返回唯一 ID。
        /// </summary>
        /// <returns>事件的唯一标识符。</returns>
        public long TheologyID()
        {
            return __TheologyID;
        }

        /// <summary>
        /// SunnyNet 上下文
        /// </summary>
        public long Context()
        {
            return __SunnyNetContext;
        }

        /// <summary>
        /// 返回事件由哪个进程发起。如果返回 0，表示远程设备通过代理连接。
        /// </summary>
        /// <returns>进程 ID。</returns>
        public int PID()
        {
            return (int)__pid;
        }

        /// <summary>
        /// 请求错误的信息。当 Type() 为 Conn.EventType_HTTP_Error 时有效。
        /// </summary>
        /// <returns>错误信息。</returns>
        public string Error()
        {
            return __err;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <remarks>
        /// 请使用以下常量来判断事件类型：
        /// <list type="bullet">
        ///   <item><see cref="HTTPEvent.EventType_HTTP_Request"/> 发起请求</item>
        ///   <item><see cref="HTTPEvent.EventType_HTTP_Response"/> 请求完成</item>
        ///   <item><see cref="HTTPEvent.EventType_HTTP_Error"/> 请求错误</item>
        /// </list>
        /// </remarks>
        /// <returns>HTTP 当前 事件类型。</returns>
        public int Type()
        {
            return (int)__EventType;
        }
        public new int GetType()
        {
            return Type();

        }

        /// <summary>
        /// 获取数据来源客户端的 IP 地址。
        /// </summary>
        /// <returns>客户端 IP 地址。</returns>
        public string ClientIP()
        {
            return 中间层.GetRequestClientIp(__MessageId);
        }
        /// <summary>
        /// 获取请求对象。当 Type() 为 Conn.EventType_HTTP_Request 时有效。
        /// </summary>
        /// <returns>请求对象。</returns>
        public Request Request()
        {
            return __Request;
        }

        /// <summary>
        /// 获取响应对象。当 Type() 为以下情况时有效:
        /// <list type="bullet">
        ///     <item>Conn.EventType_HTTP_Request - 发起请求</item>
        ///     <item>或</item>
        ///     <item>Conn.EventType_HTTP_Response - 请求完成</item>
        /// </list>
        /// </summary>
        /// <returns>响应对象。</returns>
        public Response Response()
        {
            return __Response;
        }
    }

    public class TCPEvent
    {
        /// <summary>
        /// 消息发送目标常量：发送到客户端。
        /// </summary>
        public const int SendToClient = 1;

        /// <summary>
        /// 消息发送目标常量：发送到服务器。
        /// </summary>
        public const int SendToServer = 2;

        /// <summary>
        /// TCP 事件类型常量：即将开始连接。
        /// </summary>
        public const int EventType_TCP_About = 4;

        /// <summary>
        /// TCP 事件类型常量：连接成功。
        /// </summary>
        public const int EventType_TCP_OK = 0;

        /// <summary>
        /// TCP 事件类型常量：客户端发送数据。
        /// </summary>
        public const int EventType_TCP_Send = 1;

        /// <summary>
        /// TCP 事件类型常量：客户端收到数据。
        /// </summary>
        public const int EventType_TCP_Receive = 2;

        /// <summary>
        /// TCP 事件类型常量：连接关闭或连接失败。
        /// </summary>
        public const int EventType_TCP_Close = 3;

        private long __SunnyNetContext;
        private long __TheologyID;
        private long __MessageId;
        private long __EventType;
        private string __LocalAddr;
        private string __RemoteAddr;
        private EventValue __MessageData;
        private long __pid;

        public TCPEvent(IntPtr SunnyNetContext, string LocalAddr, string RemoteAddr, IntPtr EventType, IntPtr MessageId, byte[] data, IntPtr TheologyID, IntPtr pid)
        {
            __SunnyNetContext = SunnyNetContext.ToInt64();
            __LocalAddr = LocalAddr;
            __RemoteAddr = RemoteAddr;
            __EventType = EventType.ToInt64();
            __MessageId = MessageId.ToInt64();
            __TheologyID = TheologyID.ToInt64();
            __MessageData = new EventValue(data);
            __pid = pid.ToInt64();
        }

        /// <summary>
        /// 获取 SunnyNet 上下文。
        /// </summary>
        /// <returns>上下文的长整型值。</returns>
        public long Context()
        {
            return __SunnyNetContext;
        }

        /// <summary>
        /// 返回唯一 ID。
        /// </summary>
        /// <returns>事件的唯一标识符。</returns>
        public long TheologyID()
        {
            return __TheologyID;
        }

        /// <summary>
        /// <para>设置出口IP</para>
        /// <para>你也可以在中间件中设置全局出口IP</para>
        /// </summary>
        /// <param name="ip">请输入网卡对应的内网IP地址,输入空文本,则让系统自动选择</param>
        public void SetOutRouterIP(String ip)
        {
            中间层.RequestSetOutRouterIP(__MessageId, ip);
        }
        /// <summary>
        /// 返回事件由哪个进程发起。如果返回 0，表示远程设备通过代理连接。
        /// </summary>
        /// <returns>进程 ID。</returns>
        public int PID()
        {
            return (int)__pid;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <remarks>
        /// 请使用以下常量来判断事件类型：
        /// <list type="bullet">
        ///   <item><see cref="EventType_TCP_About"/> 即将连接</item>
        ///   <item><see cref="EventType_TCP_OK"/> 连接成功</item>
        ///   <item><see cref="EventType_TCP_Send"/> 发送数据</item>
        ///   <item><see cref="EventType_TCP_Receive"/> 接收数据</item>
        ///   <item><see cref="EventType_TCP_Close"/> 连接关闭</item>
        /// </list>
        /// </remarks>
        /// <returns>TCP 当前 事件类型。</returns>
        public int Type()
        {
            return (int)__EventType;
        }

        /// <summary>
        /// 获取本地地址。
        /// </summary>
        /// <returns>本地地址字符串。</returns>
        public string LocalAddr()
        {
            return __LocalAddr;
        }

        /// <summary>
        /// 获取远程地址。
        /// </summary>
        /// <returns>远程地址字符串。</returns>
        public string RemoteAddr()
        {
            return __RemoteAddr;
        }

        /// <summary>
        /// 获取事件数据内容。
        /// </summary>
        /// <returns>返回字节数组。</returns>
        public EventValue Body()
        {
            return __MessageData;
        }

        /// <summary>
        /// 修改当前事件的消息内容。
        /// </summary>
        /// <param name="data">新的消息内容，作为字节数组。</param>
        /// <returns>如果成功修改则返回 true；否则返回 false。</returns>
        public bool Body(byte[] data)
        {
            __MessageData = new EventValue(data);
            return 中间层.SetTcpBody(__MessageId, (int)__EventType, __MessageData.Bytes);
        }

        /// <summary>
        /// 修改当前事件的消息内容。
        /// </summary>
        /// <param name="data">新的消息内容，作为字符串。</param>
        /// <returns>如果成功修改则返回 true；否则返回 false。</returns>
        public bool Body(string data, string Encoding = "UTF-8")
        {
            return Body(工具类.StrToBytes(data, Encoding));
        }

        /// <summary>
        /// 设置代理，例如，以下示例格式：
        /// <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </summary>
        /// <param name="ProxyUrl">代理 URL，指定要使用的代理地址。
        /// 例如，以下示例格式：
        /// <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </param>
        /// <param name="outTime">代理超时(毫秒)，默认30秒</param>
        /// <returns>如果成功设置代理，返回 true；否则返回 false。</returns>
        public bool SetProxy(string ProxyUrl, int outTime = 30000)
        {
            if (__EventType != EventType_TCP_About)
            {
                return false;
            }
            return 中间层.SetTcpAgent(__MessageId, ProxyUrl, outTime);
        }

        /// <summary>
        /// 重定向到另一个地址，仅限 TCP 回调，即将连接时使用。
        /// </summary>
        /// <param name="newAddress">请提供带端口的 IP 地址，例如: <c>8.8.8.8:443</c></param>
        /// <returns>如果事件类型不是连接即将开始，则返回 false；否则返回 true。</returns>
        public bool Redirect(string newAddress)
        {
            if (__EventType != EventType_TCP_About)
            {
                return false;
            }
            return 中间层.SetTcpConnectionIP(__MessageId, newAddress);
        }

        /// <summary>
        /// 发送消息到指定的目标。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字节数组。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, byte[] message)
        {
            return TCP工具类.SendMessage(SendTarget, __TheologyID, message);
        }

        /// <summary>
        /// 发送消息到指定的目标。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字符串。</param>
        /// <param name="Encoding">消息编码格式，默认为 "UTF-8"。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, string message, string Encoding = "UTF-8")
        {
            return TCP工具类.SendMessage(SendTarget, __TheologyID, message, Encoding);
        }

        /// <summary>
        /// 关闭当前 TCP 会话。
        /// </summary>
        /// <returns>如果成功则返回 true；否则返回 false。</returns>
        public bool Close()
        {
            return TCP工具类.Close(__TheologyID);
        }
    }

    public class WebSocketEvent
    {
        /// <summary>
        /// 消息发送目标常量：发送到客户端。
        /// </summary>
        public const int SendToClient = 1;

        /// <summary>
        /// 消息发送目标常量：发送到服务器。
        /// </summary>
        public const int SendToServer = 2;

        /// <summary>
        /// WebSocket 事件类型常量：连接成功。
        /// </summary>
        public const int EventType_Websocket_OK = 1;

        /// <summary>
        /// WebSocket 事件类型常量：客户端发送数据。
        /// </summary>
        public const int EventType_Websocket_Send = 2;

        /// <summary>
        /// WebSocket 事件类型常量：客户端收到数据。
        /// </summary>
        public const int EventType_Websocket_Receive = 3;

        /// <summary>
        /// WebSocket 事件类型常量：断开连接。
        /// </summary>
        public const int EventType_Websocket_Close = 4;

        private long __SunnyNetContext;
        private long __TheologyID;
        private long __MessageId;
        private long __EventType;
        private string __Method;
        private string __Url;
        private long __pid;
        private int __WsMsgType;
        private Request __Request;

        public WebSocketEvent(IntPtr SunnyNetContext, IntPtr TheologyID, IntPtr MessageId, IntPtr EventType, string Method, string Url, IntPtr pid, IntPtr WsMsgType)
        {
            __SunnyNetContext = SunnyNetContext.ToInt64();
            __TheologyID = TheologyID.ToInt64();
            __MessageId = MessageId.ToInt64();
            __EventType = EventType.ToInt64();
            __Method = Method;
            __Url = Url;
            __pid = pid.ToInt64();
            __WsMsgType = (int)WsMsgType.ToInt64();
            __Request = new Request(__MessageId);
        }

        /// <summary>
        /// 获取请求的 URL。
        /// </summary>
        /// <returns>请求的 URL。</returns>
        public string URL()
        {
            return __Url;
        }

        /// <summary>
        /// 获取请求方法。
        /// </summary>
        /// <returns>HTTP 方法。</returns>
        public string Method()
        {
            return __Method;
        }

        /// <summary>
        /// 返回唯一 ID。
        /// </summary>
        /// <returns>事件的唯一标识符。</returns>
        public long TheologyID()
        {
            return __TheologyID;
        }

        /// <summary>
        /// 获取 SunnyNet 上下文。
        /// </summary>
        /// <returns>上下文的长整型值。</returns>
        public long Context()
        {
            return __SunnyNetContext;
        }

     
        /// <summary>
        /// 返回事件由哪个进程发起。如果返回 0，表示远程设备通过代理连接。
        /// </summary>
        /// <returns>进程 ID。</returns>
        public int PID()
        {
            return (int)__pid;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <remarks>
        /// 请使用以下常量来判断事件类型：
        /// <list type="bullet">
        ///   <item><see cref="EventType_Websocket_OK"/> 连接成功</item>
        ///   <item><see cref="EventType_Websocket_Send"/> 发送数据</item>
        ///   <item><see cref="EventType_Websocket_Receive"/> 接收数据</item>
        ///   <item><see cref="EventType_Websocket_Close"/> 连接关闭</item>
        /// </list>
        /// </remarks>
        /// <returns>WebSocket 当前事件类型。</returns>
        public int Type()
        {
            return (int)__EventType;
        }

        /// <summary>
        /// 获取当前 WebSocket 消息的类型。
        /// <param>消息类型，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="Const.WsMessageType.Text"/> 文本消息</item>
        ///   <item><see cref="Const.WsMessageType.Binary"/> 二进制消息</item>
        ///   <item><see cref="Const.WsMessageType.Ping"/> Ping 消息</item>
        ///   <item><see cref="Const.WsMessageType.Pong"/> Pong 消息</item>
        ///   <item><see cref="Const.WsMessageType.Close"/> 关闭消息</item>
        ///   <item><see cref="Const.WsMessageType.Invalid"/> 无效的消息</item>
        /// </list></param>
        /// </summary>
        /// <returns>当前事件消息的类型。</returns>
        public long MessageType()
        {
            return __WsMsgType;
        }

        /// <summary>
        /// 获取 WebSocket 连接时的 Headers 信息。
        /// </summary>
        /// <returns>Headers 信息字符串。</returns>
        public string Headers()
        {
            return __Request.GetAllHeader();
        }
        /// <summary>
        /// 获取指定协议头。
        /// 如果有多个同名协议头，将返回第一个。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        /// <returns>返回指定名称的协议头值。</returns>
        public string Header(string key)
        {
            return __Request.GetHeader(key);
        }
        /// <summary>
        /// 获取全部 Cookies。
        /// </summary>
        /// <returns>返回所有 提交的 Cookies  字符串。</returns>
        public string Cookies()
        {
            return __Request.GetCookies();
        }
        /// <summary>
        /// 获取指定 Cookie。
        /// </summary>
        /// <param name="key">Cookie 名。</param>
        /// <returns>返回指定 Cookie 的字符串。</returns>
        public string Cookie(string key)
        {
            return __Request.GetCookie(key);
        }

        /// <summary>
        /// 获取指定 Cookie，不包含键名。
        /// </summary>
        /// <param name="key">Cookie 名。</param>
        /// <returns>返回指定 Cookie 的值。</returns>
        public string Cookie_value(string key)
        {
            return __Request.GetCookie_value(key);
        }
        /// <summary>
        /// 获取事件数据内容。
        /// </summary>
        /// <returns>返回字节数组。</returns>
        public EventValue Body()
        {
            return new EventValue(中间层.GetWebsocketBody(__MessageId));
        }

        /// <summary>
        /// 修改事件数据内容。
        /// </summary>
        /// <param name="data">新的事件数据，作为字节数组。</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public bool Body(byte[] data)
        {
            return 中间层.SetWebsocketBody(__MessageId, data ?? new byte[0]);
        }

        /// <summary>
        /// 修改事件数据内容。
        /// </summary>
        /// <param name="data">新的事件数据，作为字符串。</param>
        /// <param name="Encoding">消息编码格式，默认为 "UTF-8"。</param>
        /// <returns>成功返回 true；否则返回 false。</returns>
        public bool Body(string data, string Encoding = "UTF-8")
        {
            return Body(工具类.StrToBytes(data, Encoding));
        }

        /// <summary>
        /// 发送消息到指定的目标。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="theology">消息的唯一标识符，通常用于标识目标连接。</param>
        /// <param name="wsMessageType">消息类型，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="Const.WsMessageType.Text"/> 文本消息</item>
        ///   <item><see cref="Const.WsMessageType.Binary"/> 二进制消息</item>
        ///   <item><see cref="Const.WsMessageType.Ping"/> Ping 消息</item>
        ///   <item><see cref="Const.WsMessageType.Pong"/> Pong 消息</item>
        ///   <item><see cref="Const.WsMessageType.Close"/> 关闭消息</item>
        ///   <item><see cref="Const.WsMessageType.Invalid"/> 无效的消息</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字节数组。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, long wsMessageType, byte[] message)
        {
            return WebSocket工具类.SendMessage(SendTarget, __TheologyID, wsMessageType, message);
        }

        /// <summary>
        /// 发送消息到指定的目标（字节数组）。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字节数组。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, byte[] message)
        {
            return SendMessage(SendTarget, __WsMsgType, message);
        }

        /// <summary>
        /// 发送消息到指定的目标（字符串格式）。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="wsMessageType">消息类型，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="Const.WsMessageType.Text"/> 文本消息</item>
        ///   <item><see cref="Const.WsMessageType.Binary"/> 二进制消息</item>
        ///   <item><see cref="Const.WsMessageType.Ping"/> Ping 消息</item>
        ///   <item><see cref="Const.WsMessageType.Pong"/> Pong 消息</item>
        ///   <item><see cref="Const.WsMessageType.Close"/> 关闭消息</item>
        ///   <item><see cref="Const.WsMessageType.Invalid"/> 无效的消息</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字符串。</param>
        /// <param name="Encoding">消息编码格式，默认为 "UTF-8"。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, long wsMessageType, string message, string Encoding = "UTF-8")
        {
            return SendMessage(SendTarget, wsMessageType, 工具类.StrToBytes(message, Encoding));
        }

        /// <summary>
        /// 发送消息到指定的目标（字符串格式）。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字符串。</param>
        /// <param name="Encoding">消息编码格式，默认为 "UTF-8"。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, string message, string Encoding = "UTF-8")
        {
            return SendMessage(SendTarget, 工具类.StrToBytes(message, Encoding));
        }

        /// <summary>
        /// 关闭当前 WebSocket 连接。
        /// </summary>
        /// <returns>如果连接成功关闭，则返回 true；否则返回 false。</returns>
        public bool Close()
        {
            return WebSocket工具类.Close(__TheologyID);
        }
    }

    public class UDPEvent
    {
        /// <summary>
        /// 消息发送目标常量：发送到客户端。
        /// </summary>
        public const int SendToClient = 1;

        /// <summary>
        /// 消息发送目标常量：发送到服务器。
        /// </summary>
        public const int SendToServer = 2;

        /// <summary>
        /// UDP 事件类型常量：连接关闭。
        /// </summary>
        public const int EventType_UDP_Closed = 1;

        /// <summary>
        /// UDP 事件类型常量：客户端发送数据。
        /// </summary>
        public const int EventType_UDP_Send = 2;

        /// <summary>
        /// UDP 事件类型常量：客户端收到数据。
        /// </summary>
        public const int EventType_UDP_Receive = 3;

        private long __SunnyNetContext;
        private long __TheologyID;
        private long __MessageId;
        private long __EventType;
        private string __LocalAddr;
        private string __RemoteAddr;
        private long __pid;

        public UDPEvent(IntPtr SunnyNetContext, string LocalAddr, string RemoteAddr, IntPtr EventType, IntPtr MessageId, IntPtr TheologyID, IntPtr pid)
        {
            __SunnyNetContext = SunnyNetContext.ToInt64();
            __LocalAddr = LocalAddr;
            __RemoteAddr = RemoteAddr;
            __EventType = EventType.ToInt64();
            __MessageId = MessageId.ToInt64();
            __TheologyID = TheologyID.ToInt64();
            __pid = pid.ToInt64();
        }

        /// <summary>
        /// 获取 SunnyNet 上下文。
        /// </summary>
        /// <returns>上下文的长整型值。</returns>
        public long Context()
        {
            return __SunnyNetContext;
        }

        /// <summary>
        /// 返回唯一 ID。
        /// </summary>
        /// <returns>事件的唯一标识符。</returns>
        public long TheologyID()
        {
            return __TheologyID;
        }

        /// <summary>
        /// 返回事件由哪个进程发起。如果返回 0，表示远程设备通过代理连接。
        /// </summary>
        /// <returns>进程 ID。</returns>
        public int PID()
        {
            return (int)__pid;
        }

        /// <summary>
        /// 获取事件类型。
        /// </summary>
        /// <remarks>
        /// 请使用以下常量来判断事件类型：
        /// <list type="bullet"> 
        ///   <item><see cref="EventType_UDP_Send"/> 发送数据</item>
        ///   <item><see cref="EventType_UDP_Receive"/> 接收数据</item>
        ///   <item><see cref="EventType_UDP_Closed"/> 连接关闭</item>
        /// </list>
        /// </remarks>
        /// <returns>UDP 当前 事件类型。</returns>
        public int Type()
        {
            return (int)__EventType;
        }

        /// <summary>
        /// 获取本地地址。
        /// </summary>
        /// <returns>本地地址字符串。</returns>
        public string LocalAddr()
        {
            return __LocalAddr;
        }

        /// <summary>
        /// 获取远程地址。
        /// </summary>
        /// <returns>远程地址字符串。</returns>
        public string RemoteAddr()
        {
            return __RemoteAddr;
        }

        /// <summary>
        /// 获取事件数据内容。
        /// </summary>
        /// <returns>返回字节数组。</returns>
        public EventValue Body()
        {
            return new EventValue(中间层.GetUdpData(__MessageId));
        }

        /// <summary>
        /// 修改当前事件的消息内容。
        /// </summary>
        /// <param name="data">新的消息内容，作为字节数组。</param>
        /// <returns>如果成功修改则返回 true；否则返回 false。</returns>
        public bool Body(byte[] data)
        {
            return 中间层.SetUdpData(__MessageId, data ?? new byte[0]);
        }
        /// <summary>
        /// 修改当前事件的消息内容。
        /// </summary>
        /// <param name="data">新的消息内容，作为字符串。</param>
        /// <returns>如果成功修改则返回 true；否则返回 false。</returns>
        public bool Body(string data, string Encoding = "UTF-8")
        {
            return Body(工具类.StrToBytes(data, Encoding));
        }

        /// <summary>
        /// 发送消息到指定的目标。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字节数组。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, byte[] message)
        {
            return TCP工具类.SendMessage(SendTarget, __TheologyID, message);
        }

        /// <summary>
        /// 发送消息到指定的目标。
        /// </summary>
        /// <param name="SendTarget">发送目标，请使用以下常量之一：
        /// <list type="bullet">
        ///   <item><see cref="SendToClient"/> 发送到客户端</item>
        ///   <item><see cref="SendToServer"/> 发送到服务器</item>
        /// </list>
        /// </param>
        /// <param name="message">要发送的消息内容，作为字符串。</param>
        /// <param name="Encoding">消息编码格式，默认为 "UTF-8"。</param>
        /// <returns>如果消息成功发送，则返回 true；否则返回 false。</returns>
        public bool SendMessage(int SendTarget, string message, string Encoding = "UTF-8")
        {
            return TCP工具类.SendMessage(SendTarget, __TheologyID, message, Encoding);
        }
    }
    public class Request
    {
        private long MessageId = 0;

        /// <summary>
        /// 初始化 Request 对象。
        /// </summary>
        /// <param name="_MessageId">消息 ID。</param>
        public Request(long _MessageId)
        {
            MessageId = _MessageId;
        }

        /// <summary>
        /// 将原始请求数据保存到文件。
        /// 请使用 "Conn.Request().IsRequestRawBody()" 来检查当前请求是否为原始 body。
        /// 如果是，将无法修改提交的 Body，请使用此命令来储存原始提交数据到文件。
        /// </summary>
        /// <param name="SavePath">保存路径。</param>
        public void RawRequestDataToFile(string SavePath)
        {
            中间层.RawRequestDataToFile(MessageId, SavePath);
        }

        /// <summary>
        /// 检查当前请求是否为原始 body。
        /// 如果是，将无法修改提交的 Body，请使用 "Conn.Request().RawRequestDataToFile(filePath)" 命令来储存到文件。
        /// 如果当前请求为原始 body，返回 true；否则返回 false。
        /// </summary>
        /// <returns>如果是原始 body 返回 true， 否则返回 false。</returns>
        public bool IsRequestRawBody()
        {
            return 中间层.IsRequestRawBody(MessageId);
        }

        /// <summary>
        /// 请求协议头中去除压缩标记。
        /// 如果不删除压缩标记，返回数据可能是压缩后的。
        /// </summary>
        public void RemoveCompressionMark()
        {
            DelHeader("Accept-Encoding");
        }

        /// <summary>
        /// 获取 POST 提交数据长度。
        /// </summary>
        /// <returns>POST 数据长度。</returns>
        public int BodyLen()
        {
            return 中间层.GetRequestBodyLen(MessageId);
        }
        /// <summary>
        /// <para>设置出口IP</para>
        /// <para>你也可以在中间件中设置全局出口IP</para>
        /// </summary>
        /// <param name="ip">请输入网卡对应的内网IP地址,输入空文本,则让系统自动选择</param>
        public void SetOutRouterIP(String ip)
        {
            中间层.RequestSetOutRouterIP(MessageId, ip);
        }
        /// <summary>
        /// 获取 POST 提交数据 
        /// </summary>
        /// <returns>POST 提交数据的字节数组。</returns>
        public EventValue Body()
        {
            return new EventValue(中间层.GetRequestBody(MessageId));
        }

        /// <summary>
        /// 修改 POST 提交数据，传入字节数组。
        /// </summary>
        /// <param name="data">要设置的字节数组。</param>
        /// <returns>如果成功修改，返回 true；否则返回 false。</returns>
        public bool Body(byte[] data)
        {
            return 中间层.SetRequestData(MessageId, data);
        }

        /// <summary>
        /// 修改 POST 提交数据，传入字符串。
        /// </summary>
        /// <param name="data">要设置的字符串。</param>
        /// <param name="Encoding">编码格式，默认为 UTF-8。</param>
        /// <returns>如果成功修改，返回 true；否则返回 false。</returns>
        public bool Body(string data, string Encoding = "UTF-8")
        {
            return Body(工具类.StrToBytes(data, Encoding));
        }

        /// <summary>
        /// 终止请求。在发起请求时使用，使用本命令后，此请求将不会被发送出去。
        /// </summary>
        public void Stop()
        {
            中间层.SetResponseHeader(MessageId, "Connection", "Close");
        }

        /// <summary>
        /// 设置请求超时，以毫秒为单位。
        /// </summary>
        /// <param name="OutTime">超时时间，单位为毫秒。</param>
        public void SetOutTime(int OutTime)
        {
            中间层.SetRequestOutTime(MessageId, OutTime);
        }

        /// <summary>
        /// 设置 HTTP/2 指纹。如果服务器支持则使用，否则将不使用。
        /// 请使用以下常量模板之一，模板中的数值可以随机化以达到随机指纹的效果。
        /// </summary>
        /// <param name="h2Config">HTTP/2 配置字符串。</param>
        /// <returns>如果成功设置配置，返回 true；否则返回 false。</returns>
        public bool SetH2Config(string config)
        {
            return 中间层.SetRequestHTTP2Config(MessageId, config);
        }

        /// <summary>
        /// 随机化请求的 JA3 指纹。
        /// 如果全局设置中没有开启 TLS 指纹随机，可以单独使用此命令来随机化 JA3 指纹。
        /// </summary>
        /// <returns>如果成功随机化指纹，返回 true；否则返回 false。</returns>
        public bool RandomJA3()
        {
            return 中间层.RandomRequestCipherSuites(MessageId);
        }
        /// <summary>
        /// 设置代理。
        /// 例如，以下示例格式：
        ///  <list type="bullet"> 
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </summary>
        /// <param name="ProxyUrl">代理 URL，指定要使用的代理地址。
        /// 例如，以下示例格式：
        ///  <list type="bullet"> 
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </param>
        /// <param name="OutTime">代理超时，单位毫秒。</param>
        /// <returns>如果成功设置代理，返回 true；否则返回 false。</returns>
        public bool SetProxy(string ProxyUrl, int outTime)
        {
            return 中间层.SetRequestProxy(MessageId, ProxyUrl, outTime);
        }
        /// <summary>
        /// 重置完整的协议头。
        /// </summary>
        /// <param name="Headers">完整的协议头字符串。</param>
        public void SetALLHeaders(string Headers)
        {
            中间层.SetRequestALLHeader(MessageId, Headers);
        }

        /// <summary>
        /// 修改全部 Cookie，设置请求的全部 Cookies，例如 a=1;b=2;c=3。
        /// </summary>
        /// <param name="cookie">Cookies 字符串，无需前缀（Cookie:）。</param>
        public void SetAllCookie(string cookie)
        {
            中间层.SetRequestAllCookie(MessageId, cookie);
        }

        /// <summary>
        /// 设置多个同名的协议头。
        /// 如果本身无指定的协议头，则为新增；如果有，则为修改。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        /// <param name="value">协议头值的数组。</param>
        public void SetHeader(string key, string[] value)
        {
            SetHeader(key, string.Join("\n", value));
        }

        /// <summary>
        /// 设置单个协议头。
        /// 如果本身无指定的协议头，则为新增；如果有，则为修改。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        /// <param name="value">协议头值。</param>
        public void SetHeader(string key, string value)
        {
            中间层.SetRequestHeader(MessageId, key, value);
        }

        /// <summary>
        /// 修改请求的 URL。
        /// 可以用于转向，例如从网址 A 转向网址 B。
        /// </summary>
        /// <param name="newUrl">新的 URL。</param>
        /// <returns>如果成功修改，返回 true；否则返回 false。</returns>
        public bool SetUrl(string newUrl)
        {
            return 中间层.SetRequestUrl(MessageId, newUrl);
        }

        /// <summary>
        /// 设置 Cookie。
        /// 如果 key 存在则修改，不存在则新增。
        /// </summary>
        /// <param name="key">Cookie 名。</param>
        /// <param name="value">Cookie 值。</param>
        public void SetCookie(string key, string value)
        {
            中间层.SetRequestCookie(MessageId, key, value);
        }

        /// <summary>
        /// 删除指定的协议头。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        public void DelHeader(string key)
        {
            中间层.DelRequestHeader(MessageId, key);
        }

        /// <summary>
        /// 获取全部协议头。
        /// </summary>
        /// <returns>返回所有协议头的字符串。</returns>
        public string GetAllHeader()
        {
            return 中间层.GetRequestAllHeader(MessageId);
        }

        /// <summary>
        /// 获取指定协议头的数组。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        /// <returns>返回指定名称的协议头值数组。</returns>
        public string[] GetHeaderArray(string key)
        {
            return 中间层.GetRequestHeader(MessageId, key).Replace("\r", "").Split('\n');
        }

        /// <summary>
        /// 获取指定协议头。
        /// 如果有多个同名协议头，将返回第一个。
        /// </summary>
        /// <param name="key">协议头名称。</param>
        /// <returns>返回指定名称的协议头值。</returns>
        public string GetHeader(string key)
        {
            string[] sa = GetHeaderArray(key);
            if (sa.Length < 1)
            {
                return "";
            }
            return sa[0];
        }

        /// <summary>
        /// 获取请求的协议版本，例如 HTTP/1.1。
        /// </summary>
        /// <returns>请求的协议版本。</returns>
        public string GetProto()
        {
            return 中间层.GetRequestProto(MessageId);
        }

        /// <summary>
        /// 获取全部 Cookies。
        /// </summary>
        /// <returns>返回所有 Cookies 的字符串。</returns>
        public string GetCookies()
        {
            return 中间层.GetRequestALLCookie(MessageId);
        }

        /// <summary>
        /// 获取指定 Cookie。
        /// </summary>
        /// <param name="key">Cookie 名。</param>
        /// <returns>返回指定 Cookie 的字符串。</returns>
        public string GetCookie(string key)
        {
            return 中间层.GetRequestCookie(MessageId, key);
        }

        /// <summary>
        /// 获取指定 Cookie，不包含键名。
        /// </summary>
        /// <param name="key">Cookie 名。</param>
        /// <returns>返回指定 Cookie 的值。</returns>
        public string GetCookie_value(string key)
        {
            string s = GetCookie(key);
            string[] arr = Regex.Split(s, "=", RegexOptions.IgnoreCase);
            if (arr.Length < 2)
            {
                return "";
            }
            string value = arr[1].Replace(arr[0] + "=", "");
            return value;
        }

        /// <summary>
        /// 删除全部协议头。
        /// </summary>
        public void DelAllHeader()
        {
            string[] arr = Regex.Split(GetAllHeader(), "\r\n", RegexOptions.IgnoreCase);
            foreach (string s in arr)
            {
                string[] arr1 = Regex.Split(s, ":", RegexOptions.IgnoreCase);
                if (arr1.Length >= 1)
                {
                    DelHeader(arr1[0]);
                }
            }
        }
    }

    public class Response
    {
        private long MessageId = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="_MessageId">消息 ID。</param>
        public Response(long _MessageId)
        {
            MessageId = _MessageId;
        }

        /// <summary>
        /// 修改响应状态码。
        /// </summary>
        /// <param name="Code">状态码，默认为 200。</param>
        public void StatusCode(int Code = 200)
        {
            中间层.SetResponseStatus(MessageId, Code);
        }

        /// <summary>
        /// 获取响应状态码。
        /// </summary>
        /// <returns>响应状态码。</returns>
        public int StatusCode()
        {
            return 中间层.GetResponseStatusCode(MessageId);
        }

        /// <summary>
        /// 获取状态码对应的状态文本。
        /// </summary>
        /// <returns>状态文本。</returns>
        public string StatusText()
        {
            return 中间层.GetResponseStatus(MessageId);
        }

        /// <summary>
        /// 获取服务器响应 IP 地址。
        /// </summary>
        /// <returns>服务器响应 IP 地址。</returns>
        public string ServerAddress()
        {
            return 中间层.GetResponseStatus(MessageId);
        }

        /// <summary>
        /// 获取响应数据长度。
        /// </summary>
        /// <returns>响应数据的字节长度。</returns>
        public int BodyLen()
        {
            return 中间层.GetResponseBodyLen(MessageId);
        }
        /// <summary>
        /// 获取响应数据字节数组。
        /// </summary>
        /// <returns>响应数据字节数组。</returns>
        public EventValue Body()
        {
            return new EventValue(中间层.GetResponseBody(MessageId));
        }

        /// <summary>
        /// 修改响应数据字节数组。
        /// </summary>
        /// <param name="data">新的响应数据字节数组。</param>
        /// <returns>是否成功修改。</returns>
        public bool SetBody(byte[] data)
        {
            return 中间层.SetResponseData(MessageId, data);
        }

        /// <summary>
        /// 修改响应数据为字符串。
        /// </summary>
        /// <param name="data">新的响应数据字符串。</param>
        /// <param name="Encoding">编码，默认为 "UTF-8"。</param>
        /// <returns>是否成功修改。</returns>
        public bool SetBody(string data, string Encoding = "UTF-8")
        {
            byte[] A = 工具类.StrToBytes(data, Encoding);
            return SetBody(A);
        }


        /// <summary>
        /// 获取响应协议头。如果有多个同名协议头，将返回第一个。
        /// </summary>
        /// <param name="key">协议头的键名。</param>
        /// <returns>对应的协议头值。</returns>
        public string GetHeader(string key)
        {
            string[] arr = GetHeaderArray(key);
            if (arr.Length < 1)
            {
                return "";
            }
            return arr[0];
        }

        /// <summary>
        /// 获取响应协议头。如果有多个同名协议头，将返回数组。
        /// </summary>
        /// <param name="key">协议头的键名。</param>
        /// <returns>对应的协议头值数组。</returns>
        public string[] GetHeaderArray(string key)
        {
            return 中间层.GetResponseHeader(MessageId, key).Replace("\r", "").Split('\n');
        }

        /// <summary>
        /// 删除指定协议头。
        /// </summary>
        /// <param name="key">协议头的键名。</param>
        public void DelHeader(string key)
        {
            中间层.DelResponseHeader(MessageId, key);
        }

        /// <summary>
        /// 设置指定协议头，支持多个同名协议头。
        /// </summary>
        /// <param name="key">协议头的键名。</param>
        /// <param name="value">协议头的值数组。</param>
        public void SetHeader(string key, string[] value)
        {
            SetHeader(key, string.Join("\n", value));
        }

        /// <summary>
        /// 设置指定协议头，支持多个同名协议头，用换行分割。
        /// </summary>
        /// <param name="key">协议头的键名。</param>
        /// <param name="value">协议头的值。</param>
        public void SetHeader(string key, string value)
        {
            中间层.SetResponseHeader(MessageId, key, value);
        }

        /// <summary>
        /// 重置完整的协议头。
        /// </summary>
        /// <param name="Heads">协议头字符串。</param>
        public void SetAllHeader(string Heads)
        {
            中间层.SetResponseAllHeader(MessageId, Heads);
        }

        /// <summary>
        /// 获取全部协议头。
        /// </summary>
        /// <returns>完整的协议头字符串。</returns>
        public string GetAllHeader()
        {
            return 中间层.GetResponseAllHeader(MessageId);
        }

        /// <summary>
        /// 删除全部协议头。
        /// </summary>
        public void DelAllHeader()
        {
            string[] arr = GetAllHeader().Replace("\r", "").Split('\n');
            foreach (string s in arr)
            {
                string[] arr1 = Regex.Split(s, ":", RegexOptions.IgnoreCase);
                if (arr1.Length >= 1)
                {
                    DelHeader(arr1[0]);
                }
            }
        }

        /// <summary>
        /// 获取响应的协议版本，例如 HTTP/1.1。
        /// </summary>
        /// <returns>协议版本字符串。</returns>
        public string GetProto()
        {
            return 中间层.GetResponseProto(MessageId);
        }
    }
}
