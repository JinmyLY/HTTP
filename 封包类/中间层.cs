using HTTP.功能类;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTP.封包类
{
    internal class 中间层
    {
        ////释放指针
        private static bool Free(IntPtr Ptr)
        {
            return 服务层.Sunny_Free(Ptr);
        }

        ////创建Sunny中间件对象,可创建多个
        public static long CreateSunnyNet()
        {
            return 服务层.Sunny_CreateSunnyNet().ToInt64();
        }

        //// ReleaseSunnyNet 释放SunnyNet
        public static bool ReleaseSunnyNet(long SunnyContext)
        {
            return 服务层.Sunny_ReleaseSunnyNet(new IntPtr(SunnyContext));
        }

        ////启动Sunny中间件 成功返回true
        public static bool SunnyNetStart(long SunnyContext)
        {
            return 服务层.Sunny_SunnyNetStart(new IntPtr(SunnyContext));
        }

        ////设置指定端口 Sunny中间件启动之前调用
        public static bool SunnyNetSetPort(long SunnyContext, int Port)
        {
            return 服务层.Sunny_SunnyNetSetPort(new IntPtr(SunnyContext), new IntPtr(Port));
        }


        ////关闭停止指定Sunny中间件
        public static bool SunnyNetClose(long SunnyContext)
        {
            return 服务层.Sunny_SunnyNetClose(new IntPtr(SunnyContext));
        }

        ////设置请求超时
        public static void SetRequestOutTime(long SunnyContext, long times)
        {
            服务层.Sunny_SetRequestOutTime(new IntPtr(SunnyContext), new IntPtr(times));
        }



        ////设置中间件回调地址 httpCallback
        public static bool SunnyNetSetCallback(long SunnyContext, IntPtr httpCallback, IntPtr tcpCallback, IntPtr wsCallback, IntPtr udpCallback, IntPtr ScriptlogCallback, IntPtr ScriptsaveCallback)
        {
            服务层.Sunny_SetScriptCall(new IntPtr(SunnyContext), ScriptlogCallback, ScriptsaveCallback);
            return 服务层.Sunny_SunnyNetSetCallback(new IntPtr(SunnyContext), httpCallback, tcpCallback, wsCallback, udpCallback);
        }

        ////开启身份验证模式
        public static bool SunnyNetVerifyUser(long SunnyContext, bool open)
        {
            return 服务层.Sunny_SunnyNetVerifyUser(new IntPtr(SunnyContext), open);
        }
     
        ////设置中间件是否开启强制走TCP
        public static void SunnyNetMustTcp(long SunnyContext, bool open)
        {
            服务层.Sunny_SunnyNetMustTcp(new IntPtr(SunnyContext), open);
        }
        ////设置中间件是否开启强制走TCP
        public static bool SetRandomTLS(long SunnyContext, bool open)
        {
            return 服务层.Sunny_SetRandomTLS(new IntPtr(SunnyContext), open);
        }
        ////禁用或启用UDP功能
        public static bool DisableUDP(long SunnyContext, bool Disable)
        {
            return 服务层.Sunny_DisableUDP(new IntPtr(SunnyContext), Disable);
        }
        ////禁用或启用TCP功能
        public static bool DisableTCP(long SunnyContext, bool Disable)
        {
            return 服务层.Sunny_DisableTCP(new IntPtr(SunnyContext), Disable);
        }
        
        ////设置HTTP请求,提交数据,最大的长度,超过此长度,将无法修改数据，只能将提交内容储存到文件
        public static bool SetHTTPRequestMaxUpdateLength(long SunnyContext, long MaxLength)
        {
            return 服务层.Sunny_SetHTTPRequestMaxUpdateLength(new IntPtr(SunnyContext), MaxLength);
        }


        ////设置中间件上游代理使用规则
        public static bool CompileProxyRegexp(long SunnyContext, string Regexp)
        {
            IntPtr _Regexp = 工具类.StringToIntptr(Regexp);
            bool ret = 服务层.Sunny_CompileProxyRegexp(new IntPtr(SunnyContext), _Regexp);
            工具类.PtrFree(_Regexp);
            return ret;
        }

        ////设置DNS服务器地址 仅支持tls的dns 服务器，也就是853端口的，例如:223.5.5.5:853
        public static void SetDnsServer(string DnsServerName)
        {
            IntPtr _Regexp = 工具类.StringToIntptr(DnsServerName);
            服务层.Sunny_SetDnsServer(_Regexp);
            工具类.PtrFree(_Regexp);
        }

        ////设置强制走TCP规则,如果 打开了全部强制走TCP状态,本功能则无效 RulesAllow=false 规则之外走TCP  RulesAllow=true 规则之内走TCP
        public static bool SetMustTcpRegexp(long SunnyContext, string Regexp, bool RulesAllow)
        {
            IntPtr _Regexp = 工具类.StringToIntptr(Regexp);
            bool ret = 服务层.Sunny_SetMustTcpRegexp(new IntPtr(SunnyContext), _Regexp, RulesAllow);
            工具类.PtrFree(_Regexp);
            return ret;
        }

        ////获取中间件启动时的错误信息
        public static string SunnyNetError(long SunnyContext)
        {
            IntPtr p = 服务层.Sunny_SunnyNetError(new IntPtr(SunnyContext));
            string a = 工具类.PtrToString(p);
            Free(p);
            return a;

        }
        /// <summary>
        /// 设置全局上游代理。
        /// </summary>
        /// <param name="ProxyAddress">代理 URL，指定要使用的代理地址。
        /// 例如，以下示例格式：
        ///  <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </param> 
        /// <param name="outTime">代理超时(毫秒)，默认30秒
        /// <returns>如果成功设置代理，返回 true；否则返回 false。</returns>
        public static bool SetGlobalProxy(long SunnyContext, string ProxyAddress, int outTime = 30000)
        {
            IntPtr _ProxyAddress = 工具类.StringToIntptr(ProxyAddress);
            bool ret = 服务层.Sunny_SetGlobalProxy(new IntPtr(SunnyContext), _ProxyAddress, new IntPtr(outTime));
            工具类.PtrFree(_ProxyAddress);
            return ret;
        }
        // 导出已设置的证书
        public static string ExportCert(long SunnyContext)
        {
            IntPtr p = 服务层.Sunny_ExportCert(new IntPtr(SunnyContext));
            string a = 工具类.PtrToString(p);
            Free(p);
            return a;
        }

        // 设置IE代理
        public static bool SetIeProxy(long SunnyContext)
        {
            return 服务层.Sunny_SetIeProxy(new IntPtr(SunnyContext));
        }
        // 取消设置的IE代理
        public static bool CancelIEProxy(long SunnyContext)
        {
            return 服务层.Sunny_CancelIEProxy(new IntPtr(SunnyContext));
        }
        //中间件设置出口IP函数-全局
        public static bool SetOutRouterIP(long SunnyContext, String IP)
        {
            IntPtr a = 工具类.StringToIntptr(IP);
            bool r = 服务层.Sunny_SetOutRouterIP(new IntPtr(SunnyContext), a);
            工具类.PtrFree(a);
            return r;
        }

        //中间件TCP/HTTP请求设置出口IP函数
        public static bool RequestSetOutRouterIP(long MessageId, String IP)
        {
            IntPtr a = 工具类.StringToIntptr(IP);
            bool r = 服务层.Sunny_RequestSetOutRouterIP(new IntPtr(MessageId), a);
            工具类.PtrFree(a);
            return r;
        }

        //HTTP客户端设置出口IP函数	
        public static bool HTTPSetOutRouterIP(long Context, String IP)
        {
            IntPtr a = 工具类.StringToIntptr(IP);
            bool r = 服务层.Sunny_HTTPSetOutRouterIP(new IntPtr(Context), a);
            工具类.PtrFree(a);
            return r;
        }
        ////修改、设置 HTTP/S当前请求数据中指定Cookie
        public static void SetRequestCookie(long MessageId, string key, string value)
        {
            IntPtr a = 工具类.StringToIntptr(key);
            IntPtr b = 工具类.StringToIntptr(value);
            服务层.Sunny_SetRequestCookie(new IntPtr(MessageId), a, b);
            工具类.PtrFree(a);
            工具类.PtrFree(b);
        }

        ////修改、设置 HTTP/S当前请求数据中的全部Cookie
        public static void SetRequestAllCookie(long MessageId, string cookie)
        {
            IntPtr a = 工具类.StringToIntptr(cookie);
            服务层.Sunny_SetRequestAllCookie(new IntPtr(MessageId), a);
            工具类.PtrFree(a);
        }

        ////获取 HTTP/S当前请求数据中指定的Cookie
        public static string GetRequestCookie(long MessageId, string name)
        {
            IntPtr a = 工具类.StringToIntptr(name);
            IntPtr i1 = 服务层.Sunny_GetRequestCookie(new IntPtr(MessageId), a);
            工具类.PtrFree(a);
            string sa = 工具类.PtrToString(i1);
            Free(i1);
            return sa;
        }

        ////获取 HTTP/S 当前请求全部Cookie
        public static string GetRequestALLCookie(long MessageId)
        {
            IntPtr i1 = 服务层.Sunny_GetRequestALLCookie(new IntPtr(MessageId));
            string sa = 工具类.PtrToString(i1);
            Free(i1);
            return sa;
        }

        ////删除HTTP/S返回数据中指定的协议头
        public static void DelResponseHeader(long MessageId, string name)
        {
            IntPtr a = 工具类.StringToIntptr(name);
            服务层.Sunny_DelResponseHeader(new IntPtr(MessageId), a);
            工具类.PtrFree(a);
        }

        ////删除HTTP/S请求数据中指定的协议头
        public static void DelRequestHeader(long MessageId, string name)
        {
            IntPtr a = 工具类.StringToIntptr(name);
            服务层.Sunny_DelRequestHeader(new IntPtr(MessageId), a);
            工具类.PtrFree(a);
        }
        ////将原始请求数据保存到文件。
        public static void RawRequestDataToFile(long MessageId, string SavePath)
        {
            byte[] a = 工具类.StrToBytes(SavePath);
            IntPtr aa = 工具类.BytesToIntptr(a);
            服务层.Sunny_RawRequestDataToFile(new IntPtr(MessageId), aa, new IntPtr(a.Length));
            工具类.PtrFree(aa);

        }
        ////检查当前请求是否为原始 body。
        public static bool IsRequestRawBody(long MessageId)
        {
            return 服务层.Sunny_IsRequestRawBody(new IntPtr(MessageId));
        }

        ////设置HTTP/S请求体中的协议头
        public static void SetRequestHeader(long MessageId, string name, string val)
        {
            IntPtr i1 = 工具类.StringToIntptr(name);
            IntPtr i2 = 工具类.StringToIntptr(val);
            服务层.Sunny_SetRequestHeader(new IntPtr(MessageId), i1, i2);
            工具类.PtrFree(i1);
            工具类.PtrFree(i2);
        }

        ////修改、设置 HTTP/S当前返回数据中的指定协议头
        public static void SetResponseHeader(long MessageId, string name, string val)
        {
            IntPtr i1 = 工具类.StringToIntptr(name);
            IntPtr i2 = 工具类.StringToIntptr(val);
            服务层.Sunny_SetResponseHeader(new IntPtr(MessageId), i1, i2);
            工具类.PtrFree(i1);
            工具类.PtrFree(i2);
        }

        ////获取 HTTP/S当前请求数据中的指定协议头
        public static string GetRequestHeader(long MessageId, string name)
        {
            IntPtr i1 = 工具类.StringToIntptr(name);
            IntPtr aa = 服务层.Sunny_GetRequestHeader(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////获取 HTTP/S 当前返回数据中指定的协议头
        public static string GetResponseHeader(long MessageId, string name)
        {
            IntPtr i1 = 工具类.StringToIntptr(name);
            IntPtr aa = 服务层.Sunny_GetResponseHeader(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }
        ////获取响应的协议版本，例如 HTTP/1.1。
        public static string GetResponseProto(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetResponseProto(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////修改、设置 HTTP/S当前返回数据中的全部协议头，例如设置返回两条Cookie 使用本命令设置 使用设置、修改 单条命令无效
        public static void SetResponseAllHeader(long MessageId, string value)
        {
            IntPtr i1 = 工具类.StringToIntptr(value);
            服务层.Sunny_SetResponseAllHeader(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
        }

        ////获取 HTTP/S 当前返回全部协议头
        public static string GetResponseAllHeader(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetResponseAllHeader(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////获取 HTTP/S 当前请求数据全部协议头
        public static string GetRequestAllHeader(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetRequestAllHeader(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }
        ////获取请求的协议版本，例如 HTTP/1.1。
        public static string GetRequestProto(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetRequestProto(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        /// <summary>
        /// 设置代理。
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
        /// <param name="outTime">代理超时，单位毫秒。</param>
        /// <returns>如果成功设置代理，返回 true；否则返回 false。</returns>
        public static bool SetRequestProxy(long MessageId, string ProxyUrl, int outTime)
        {
            IntPtr i1 = 工具类.StringToIntptr(ProxyUrl);
            bool res = 服务层.Sunny_SetRequestProxy(new IntPtr(MessageId), i1, new IntPtr(outTime));
            工具类.PtrFree(i1);
            return res;
        }


        ////重置完整的协议头
        public static void SetRequestALLHeader(long MessageId, string Headers)
        {
            IntPtr i1 = 工具类.StringToIntptr(Headers);
            服务层.Sunny_SetRequestALLHeader(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
        }

        ////随机化请求的  JA3 指纹。
        public static bool RandomRequestCipherSuites(long MessageId)
        {
            return 服务层.Sunny_RandomRequestCipherSuites(new IntPtr(MessageId));
        }
        ////设置HTTP 2.0 请求指纹配置 (若服务器支持则使用,若服务器不支持,设置了也不会使用)
        public static bool SetRequestHTTP2Config(long MessageId, string config)
        {
            IntPtr i1 = 工具类.StringToIntptr(config);
            bool res = 服务层.Sunny_SetRequestHTTP2Config(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
            return res;
        }

        ////获取HTTP/S返回的状态码
        public static int GetResponseStatusCode(long MessageId)
        {
            return (int)服务层.Sunny_GetResponseStatusCode(new IntPtr(MessageId));
        }

        ////获取当前HTTP/S请求由哪个IP发起
        public static string GetRequestClientIp(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetRequestClientIp(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////获取HTTP/S返回的状态文本 例如 [200 OK]
        public static string GetResponseStatus(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetResponseStatus(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////获取取服务器响应IP地址
        public static string GetResponseServerAddress(long MessageId)
        {
            IntPtr aa = 服务层.Sunny_GetResponseServerAddress(new IntPtr(MessageId));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////修改HTTP/S返回的状态码
        public static void SetResponseStatus(long MessageId, int code)
        {
            服务层.Sunny_SetResponseStatus(new IntPtr(MessageId), new IntPtr(code));
        }

        ////修改HTTP/S当前请求的URL
        public static bool SetRequestUrl(long MessageId, string URI)
        {
            IntPtr i1 = 工具类.StringToIntptr(URI);
            bool res = 服务层.Sunny_SetRequestUrl(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
            return res;
        }

        ////获取 HTTP/S 当前请求POST提交数据长度
        public static int GetRequestBodyLen(long MessageId)
        {
            return (int)服务层.Sunny_GetRequestBodyLen(new IntPtr(MessageId)).ToInt64();
        }

        ////获取 HTTP/S 当前返回  数据长度
        public static int GetResponseBodyLen(long MessageId)
        {
            return (int)服务层.Sunny_GetResponseBodyLen(new IntPtr(MessageId)).ToInt64();
        }

        ////设置、修改 HTTP/S 当前请求返回数据 如果再发起请求时调用本命令，请求将不会被发送，将会直接返回 data=数据指针  dataLen=数据长度
        public static bool SetResponseData(long MessageId, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SetResponseData(new IntPtr(MessageId), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }

        ////设置、修改 HTTP/S 当前请求POST提交数据  data=数据指针  dataLen=数据长度
        public static bool SetRequestData(long MessageId, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SetRequestData(new IntPtr(MessageId), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }

        ////获取 HTTP/S 当前POST提交数据 返回 数据指针
        public static byte[] GetRequestBody(long MessageId)
        {
            IntPtr p = 服务层.Sunny_GetRequestBody(new IntPtr(MessageId));
            byte[] aa = 工具类.PtrToBytes(p, GetRequestBodyLen(MessageId));
            Free(p);
            return aa;

        }

        ////获取 HTTP/S 当前返回数据  返回 数据指针
        public static byte[] GetResponseBody(long MessageId)
        {
            IntPtr p = 服务层.Sunny_GetResponseBody(new IntPtr(MessageId));
            byte[] aa = 工具类.PtrToBytes(p, GetResponseBodyLen(MessageId));
            Free(p);
            return aa;
        }

        ////获取 WebSocket消息长度
        public static int GetWebsocketBodyLen(long MessageId)
        {
            return (int)服务层.Sunny_GetWebsocketBodyLen(new IntPtr(MessageId)).ToInt64();
        }

        ////获取 WebSocket消息 数据
        public static byte[] GetWebsocketBody(long MessageId)
        {
            IntPtr p = 服务层.Sunny_GetWebsocketBody(new IntPtr(MessageId));
            byte[] bs = 工具类.PtrToBytes(p, GetWebsocketBodyLen(MessageId));
            Free(p);
            return bs;
        }

        ////修改 WebSocket消息 data=数据指针  dataLen=数据长度
        public static bool SetWebsocketBody(long MessageId, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SetWebsocketBody(new IntPtr(MessageId), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }

        ////主动向Websocket服务器发送消息 MessageType=WS消息类型 data=数据 
        public static bool SendWebsocketBody(long TheologyID, long MessageType, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SendWebsocketBody(new IntPtr(TheologyID), new IntPtr(MessageType), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }
        ////主动向Websocket客户端发送消息 TheologyID=TheologyID MessageType=WS消息类型 data=数据
        public static bool SendWebsocketClientBody(long TheologyID, long MessageType, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SendWebsocketClientBody(new IntPtr(TheologyID), new IntPtr(MessageType), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }
        ////根据TheologyID关闭指定的Websocket连接  TheologyID在回调参数中
        public static bool CloseWebsocket(long TheologyID)
        {
            return 服务层.Sunny_CloseWebsocket(new IntPtr(TheologyID));
        }
        ////修改 TCP消息数据 MsgType=1 发送的消息 MsgType=2 接收的消息 如果 MsgType和MessageId不匹配，将不会执行操作  data=数据指针  dataLen=数据长度
        public static bool SetTcpBody(long MessageId, int MsgType, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_SetTcpBody(new IntPtr(MessageId), new IntPtr(MsgType), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res;
        }
        /// <summary>
        /// 对TCP设置代理。
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
        /// <param name="outTime">代理超时(毫秒)，默认30秒</param>
        /// <returns>如果成功设置代理，返回 true；否则返回 false。</returns>
        public static bool SetTcpAgent(long MessageId, string ProxyUrl, int outTime = 30000)
        {
            IntPtr i1 = 工具类.StringToIntptr(ProxyUrl);
            bool res = 服务层.Sunny_SetTcpAgent(new IntPtr(MessageId), i1, (IntPtr)outTime);
            工具类.PtrFree(i1);
            return res;
        }

        ////根据TheologyID关闭指定的TCP连接  TheologyID在回调参数中
        public static bool TcpCloseClient(long theology)
        {
            return 服务层.Sunny_TcpCloseClient(new IntPtr(theology));
        }

        ////给指定的TCP连接 修改目标连接地址 目标地址必须带端口号 例如 baidu.com:443
        public static bool SetTcpConnectionIP(long MessageId, string address)
        {
            IntPtr i1 = 工具类.StringToIntptr(address);
            bool res = 服务层.Sunny_SetTcpConnectionIP(new IntPtr(MessageId), i1);
            工具类.PtrFree(i1);
            return res;
        }

        ////指定的TCP连接 模拟客户端向服务器端主动发送数据
        public static bool TcpSendMsg(long theology, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            int res = (int)服务层.Sunny_TcpSendMsg(new IntPtr(theology), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res != 0;
        }

        ////指定的TCP连接 模拟服务器端向客户端主动发送数据
        public static bool TcpSendMsgClient(long theology, byte[] data)
        {
            IntPtr i1 = 工具类.BytesToIntptr(data);
            int res = (int)服务层.Sunny_TcpSendMsgClient(new IntPtr(theology), i1, new IntPtr(data.Length));
            工具类.PtrFree(i1);
            return res != 0;
        }

        ////将Go int的Bytes 转为int
        private static int BytesToInt(IntPtr data, int dataLen)
        {
            return (int)服务层.Sunny_BytesToInt(data, new IntPtr(dataLen));
        }
        private static byte[] PtrAutoToBytes(IntPtr data)
        {
            byte[] datas = new byte[0];
            if (data.ToInt64() != 0)
            {
                long plen = BytesToInt(data, 8);
                return 工具类.PtrToBytes(data, (int)plen, 8);
            }
            return datas;
        }
        ////Gzip解压缩
        public static byte[] GzipUnCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_GzipUnCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }


        ////ZSTD解压缩
        public static byte[] ZSTDUnCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_ZSTDDecompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////ZSTD压缩
        public static byte[] ZSTDCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_ZSTDCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }
        ////br解压缩
        public static byte[] BrUnCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_BrUnCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////br压缩
        public static byte[] BrCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_BrCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Gzip压缩
        public static byte[] GzipCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_GzipCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Zlib压缩
        public static byte[] ZlibCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_ZlibCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Zlib解压缩
        public static byte[] ZlibUnCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_ZlibUnCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Deflate解压缩 (可能等同于zlib解压缩)
        public static byte[] DeflateUnCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_DeflateUnCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Deflate压缩 (可能等同于zlib压缩)
        public static byte[] DeflateCompress(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_DeflateCompress(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }
        //===================================================================================================================================================

        ////Webp图片转JEG图片字节数组 SaveQuality=质量(默认75)
        public static byte[] WebpToJpegBytes(byte[] data, int SaveQuality)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_WebpToJpegBytes(_bin, new IntPtr(data.Length), new IntPtr(SaveQuality));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Webp图片转Png图片字节数组
        public static byte[] WebpToPngBytes(byte[] data)
        {
            IntPtr _bin = 工具类.BytesToIntptr(data);
            IntPtr p = 服务层.Sunny_WebpToPngBytes(_bin, new IntPtr(data.Length));
            byte[] ret = PtrAutoToBytes(p);
            工具类.PtrFree(_bin);
            Free(p);
            return ret;
        }

        ////Webp图片转JEG图片 根据文件名 SaveQuality=质量(默认75)
        public static bool WebpToJpeg(string webpPath, string savePath, int SaveQuality)
        {
            IntPtr _webpPath = 工具类.StringToIntptr(webpPath);
            IntPtr _savePath = 工具类.StringToIntptr(savePath);
            bool res = 服务层.Sunny_WebpToJpeg(_webpPath, _savePath, new IntPtr(SaveQuality));
            工具类.PtrFree(_webpPath);
            工具类.PtrFree(_savePath);
            return res;
        }

        ////Webp图片转Png图片 根据文件名
        public static bool WebpToPng(string webpPath, string savePath)
        {
            IntPtr _webpPath = 工具类.StringToIntptr(webpPath);
            IntPtr _savePath = 工具类.StringToIntptr(savePath);
            bool res = 服务层.Sunny_WebpToPng(_webpPath, _savePath);
            工具类.PtrFree(_webpPath);
            工具类.PtrFree(_savePath);
            return res;
        }


        // OpenDrive 开始进程代理/打开驱动 只允许一个 SunnyNet 使用 [会自动安装所需驱动文件]
        // IsNfapi 如果为true表示使用NFAPI驱动 如果为false 表示使用Proxifier
        public static bool OpenDrive(long SunnyContext, bool isNfapi)
        {
            return 服务层.Sunny_OpenDrive(new IntPtr(SunnyContext), isNfapi);
        }

        // UnDrive 卸载驱动，仅Windows 有效【需要管理权限】执行成功后会立即重启系统,若函数执行后没有重启系统表示没有管理员权限
        public static void UnDrive(long SunnyContext)
        {
            服务层.Sunny_UnDrive(new IntPtr(SunnyContext));
        }

        ////进程代理 添加进程名
        public static void ProcessAddName(long SunnyContext, string Name)
        {
            IntPtr _Name = 工具类.StringToIntptr(Name);
            服务层.Sunny_ProcessAddName(new IntPtr(SunnyContext), _Name);
            工具类.PtrFree(_Name);
        }

        ////进程代理 删除进程名
        public static void ProcessDelName(long SunnyContext, string Name)
        {
            IntPtr _Name = 工具类.StringToIntptr(Name);
            服务层.Sunny_ProcessDelName(new IntPtr(SunnyContext), _Name);
            工具类.PtrFree(_Name);
        }

        ////进程代理 添加PID
        public static void ProcessAddPid(long SunnyContext, int pid)
        {
            服务层.Sunny_ProcessAddPid(new IntPtr(SunnyContext), new IntPtr(pid));
        }

        ////进程代理 删除PID
        public static void ProcessDelPid(long SunnyContext, int pid)
        {
            服务层.Sunny_ProcessDelPid(new IntPtr(SunnyContext), new IntPtr(pid));
        }

        ////进程代理 取消全部已设置的进程名
        public static void ProcessCancelAll(long SunnyContext)
        {
            服务层.Sunny_ProcessCancelAll(new IntPtr(SunnyContext));
        }

        ////进程代理 设置是否全部进程通过
        public static void ProcessALLName(long SunnyContext, bool open, bool StopNet)
        {
            服务层.Sunny_ProcessALLName(new IntPtr(SunnyContext), open, StopNet);
        }

        ////证书管理器 获取证书 CommonName 字段
        public static string GetCommonName(long Context)
        {
            IntPtr aa = 服务层.Sunny_GetCommonName(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////证书管理器 导出为P12
        public static bool ExportP12(long Context, string path, string pass)
        {
            IntPtr _path = 工具类.StringToIntptr(path);
            IntPtr _pass = 工具类.StringToIntptr(pass);
            bool res = 服务层.Sunny_ExportP12(new IntPtr(Context), _path, _pass);
            工具类.PtrFree(_path);
            工具类.PtrFree(_pass);
            return res;
        }

        ////证书管理器 导出公钥
        public static string ExportPub(long Context)
        {
            IntPtr aa = 服务层.Sunny_ExportPub(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////证书管理器 导出私钥
        public static string ExportKEY(long Context)
        {
            IntPtr aa = 服务层.Sunny_ExportKEY(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////证书管理器 导出证书
        public static string ExportCA(long Context)
        {
            IntPtr aa = 服务层.Sunny_ExportCA(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////证书管理器 创建证书
        public static bool CreateCA(
            long Context, string Country, string Organization,
            string OrganizationalUnit, string Province, string
            CommonName, string Locality, int NotAfter)
        {
            IntPtr _Country = 工具类.StringToIntptr(Country);
            IntPtr _Organization = 工具类.StringToIntptr(Organization);
            IntPtr _OrganizationalUnit = 工具类.StringToIntptr(OrganizationalUnit);
            IntPtr _Province = 工具类.StringToIntptr(Province);
            IntPtr _CommonName = 工具类.StringToIntptr(CommonName);
            IntPtr _Locality = 工具类.StringToIntptr(Locality);
            bool res = 服务层.Sunny_CreateCA(new IntPtr(Context), _Country, _Organization, _OrganizationalUnit, _Province, _CommonName, _Locality, new IntPtr(2048), new IntPtr(NotAfter));
            工具类.PtrFree(_Country);
            工具类.PtrFree(_Organization);
            工具类.PtrFree(_OrganizationalUnit);
            工具类.PtrFree(_Province);
            工具类.PtrFree(_CommonName);
            工具类.PtrFree(_Locality);
            return res;
        }

        ////证书管理器 设置ClientAuth
        public static bool AddClientAuth(long Context, int val)
        {
            return 服务层.Sunny_AddClientAuth(new IntPtr(Context), new IntPtr(val));
        }

        ////证书管理器 设置信任的证书 从 文本
        public static bool AddCertPoolText(long Context, string cer)
        {
            IntPtr cert = 工具类.StringToIntptr(cer);
            bool res = 服务层.Sunny_AddCertPoolText(new IntPtr(Context), cert);
            工具类.PtrFree(cert);
            return res;
        }
        ////证书管理器 设置CipherSuites
        public static bool SetCipherSuites(long Context, string cer)
        {
            IntPtr cert = 工具类.StringToIntptr(cer);
            bool res = 服务层.Sunny_SetCipherSuites(new IntPtr(Context), cert);
            工具类.PtrFree(cert);
            return res;
        }

        ////证书管理器 设置信任的证书 从 文件
        public static bool AddCertPoolPath(long Context, string cer)
        {
            IntPtr cert = 工具类.StringToIntptr(cer);
            bool res = 服务层.Sunny_AddCertPoolPath(new IntPtr(Context), cert);
            工具类.PtrFree(cert);
            return res;
        }

        ////证书管理器 取ServerName
        public static string GetServerName(long Context)
        {
            IntPtr aa = 服务层.Sunny_GetServerName(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////证书管理器 设置ServerName
        public static bool SetServerName(long Context, string name)
        {
            IntPtr cert = 工具类.StringToIntptr(name);
            bool res = 服务层.Sunny_SetServerName(new IntPtr(Context), cert);
            工具类.PtrFree(cert);
            return res;
        }

        ////证书管理器 设置跳过主机验证
        public static bool SetInsecureSkipVerify(long Context, bool b)
        {
            return 服务层.Sunny_SetInsecureSkipVerify(new IntPtr(Context), b);
        }

        ////证书管理器 载入X509证书
        public static bool LoadX509Certificate(long Context, string Host, string CA, string KEY)
        {
            IntPtr _Host = 工具类.StringToIntptr(Host);
            IntPtr _CA = 工具类.StringToIntptr(CA);
            IntPtr _KEY = 工具类.StringToIntptr(KEY);
            bool res = 服务层.Sunny_LoadX509Certificate(new IntPtr(Context), _Host, _CA, _KEY);
            工具类.PtrFree(_Host);
            工具类.PtrFree(_CA);
            工具类.PtrFree(_KEY);
            return res;
        }

        ////证书管理器 载入X509证书2
        public static bool LoadX509KeyPair(long Context, string CaPath, string KeyPath)
        {
            IntPtr _CaPath = 工具类.StringToIntptr(CaPath);
            IntPtr _KeyPath = 工具类.StringToIntptr(KeyPath);
            bool res = 服务层.Sunny_LoadX509KeyPair(new IntPtr(Context), _CaPath, _KeyPath);
            工具类.PtrFree(_CaPath);
            工具类.PtrFree(_KeyPath);
            return res;
        }

        ////证书管理器 载入p12证书
        public static bool LoadP12Certificate(long Context, string Name, string Password)
        {
            IntPtr _Name = 工具类.StringToIntptr(Name);
            IntPtr _Password = 工具类.StringToIntptr(Password);
            bool res = 服务层.Sunny_LoadP12Certificate(new IntPtr(Context), _Name, _Password);
            工具类.PtrFree(_Name);
            工具类.PtrFree(_Password);
            return res;
        }

        ////释放 证书管理器 对象
        public static void RemoveCertificate(long Context)
        {
            服务层.Sunny_RemoveCertificate(new IntPtr(Context));
        }

        ////创建 证书管理器 对象
        public static long CreateCertificate()
        {
            return 服务层.Sunny_CreateCertificate().ToInt64();
        }

        ////GoMap 写字符串
        public static void KeysWriteStr(long KeysHandle, string name, string val)
        {
            byte[] bs = 工具类.StrToBytes(val);
            IntPtr _val = 工具类.BytesToIntptr(bs);
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysWriteStr(new IntPtr(KeysHandle), _name, _val, new IntPtr(bs.Length));
            工具类.PtrFree(_val);
            工具类.PtrFree(_name);
        }

        ////GoMap 转为JSON字符串
        public static string KeysGetJson(long KeysHandle)
        {
            IntPtr aa = 服务层.Sunny_KeysGetJson(new IntPtr(KeysHandle));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////GoMap 取数量
        public static int KeysGetCount(long KeysHandle)
        {
            return (int)服务层.Sunny_KeysGetCount(new IntPtr(KeysHandle));
        }

        ////GoMap 清空
        public static void KeysEmpty(long KeysHandle)
        {
            服务层.Sunny_KeysEmpty(new IntPtr(KeysHandle));
        }

        ////GoMap 读整数
        public static int KeysReadInt(long KeysHandle, string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            int res = (int)服务层.Sunny_KeysReadInt(new IntPtr(KeysHandle), _name).ToInt64();
            工具类.PtrFree(_name);
            return res;
        }

        ////GoMap 写整数
        public static void KeysWriteInt(long KeysHandle, string name, int val)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysWriteInt(new IntPtr(KeysHandle), _name, new IntPtr(val));
            工具类.PtrFree(_name);
        }

        ////GoMap 读长整数
        public static long KeysReadLong(long KeysHandle, string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            long res = 服务层.Sunny_KeysReadLong(new IntPtr(KeysHandle), _name);
            工具类.PtrFree(_name);
            return res;
        }

        ////GoMap 写长整数
        public static void KeysWriteLong(long KeysHandle, string name, long val)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysWriteLong(new IntPtr(KeysHandle), _name, val);
            工具类.PtrFree(_name);
        }

        ////GoMap 读浮点数
        public static double KeysReadFloat(long KeysHandle, string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            double res = 服务层.Sunny_KeysReadFloat(new IntPtr(KeysHandle), _name);
            工具类.PtrFree(_name);
            return res;
        }

        ////GoMap 写浮点数
        public static void KeysWriteFloat(long KeysHandle, string name, double val)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysWriteFloat(new IntPtr(KeysHandle), _name, val);
            工具类.PtrFree(_name);
        }

        ////GoMap 写字节数组
        public static void KeysWrite(long KeysHandle, string name, byte[] val)
        {
            IntPtr _val = 工具类.BytesToIntptr(val);
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysWrite(new IntPtr(KeysHandle), _name, _val, new IntPtr(val.Length));
            工具类.PtrFree(_val);
            工具类.PtrFree(_name);
        }

        ////GoMap 写读字符串/字节数组
        public static byte[] KeysRead(long KeysHandle, string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            IntPtr r = 服务层.Sunny_KeysRead(new IntPtr(KeysHandle), _name);
            工具类.PtrFree(_name);
            byte[] aaa = PtrAutoToBytes(r);
            Free(r);
            return aaa;
        }

        ////GoMap 删除
        public static void KeysDelete(long KeysHandle, string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_KeysDelete(new IntPtr(KeysHandle), _name);
            工具类.PtrFree(_name);
        }

        ////GoMap 删除GoMap
        public static void RemoveKeys(long KeysHandle)
        {
            服务层.Sunny_RemoveKeys(new IntPtr(KeysHandle));
        }

        ////GoMap 创建
        public static long CreateKeys()
        {
            return 服务层.Sunny_CreateKeys().ToInt64();
        }

        ////HTTP 客户端 设置重定向
        public static bool HTTPSetRedirect(long Context, bool Redirect)
        {
            return 服务层.Sunny_HTTPSetRedirect(new IntPtr(Context), Redirect);
        }

        ////HTTP 客户端 返回响应状态码
        public static int HTTPGetCode(long Context)
        {
            return (int)服务层.Sunny_HTTPGetCode(new IntPtr(Context));
        }

        ////HTTP 客户端 返回响应内容
        public static byte[] HTTPGetBody(long Context)
        {

            int l = HTTPGetBodyLen(Context);
            IntPtr P = 服务层.Sunny_HTTPGetBody(new IntPtr(Context));
            byte[] a = 工具类.PtrToBytes(P, l);
            Free(P);
            return a;
        }
        ////设置是否随机使用 TLS 指纹。
        public static bool HTTPSetRandomTLS(long Context, bool Random)
        {
            return 服务层.Sunny_HTTPSetRandomTLS(new IntPtr(Context), Random);
        }

        ////设置HTTP2指纹
        public static bool HTTPSetH2Config(long Context, string config)
        {
            IntPtr _config = 工具类.StringToIntptr(config);
            bool res = 服务层.Sunny_HTTPSetH2Config(new IntPtr(Context), _config);
            工具类.PtrFree(_config);
            return res;
        }

        ////取添加的全部协议头
        public static string HTTPGetRequestHeader(long Context)
        {
            IntPtr _config = 服务层.Sunny_HTTPGetRequestHeader(new IntPtr(Context));
            string ss = 工具类.PtrToString(_config);
            Free(_config);
            return ss;
        }

        ////HTTP 客户端 返回响应全部Heads
        public static string HTTPGetHeads(long Context)
        {
            IntPtr aa = 服务层.Sunny_HTTPGetHeads(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////HTTP 客户端 返回响应长度
        public static int HTTPGetBodyLen(long Context)
        {
            return (int)服务层.Sunny_HTTPGetBodyLen(new IntPtr(Context));
        }

        ////HTTP 客户端 发送Body
        public static void HTTPSendBin(long Context, byte[] body)
        {
            IntPtr _body = 工具类.BytesToIntptr(body);
            服务层.Sunny_HTTPSendBin(new IntPtr(Context), _body, new IntPtr(body.Length));
            工具类.PtrFree(_body);
        }

        ////HTTP 客户端 设置超时 毫秒
        public static void HTTPSetTimeouts(long Context, int timeOut)
        {
            服务层.Sunny_HTTPSetTimeouts(new IntPtr(Context), new IntPtr(timeOut));
        }

        /// <summary>
        /// 设置代理。
        /// </summary>
        /// <param name="ProxyURL">代理 URL，指定要使用的代理地址。
        /// 例如，以下示例格式：
        ///  <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </param>
        public static void HTTPSetProxyIP(long Context, string ProxyURL)
        {
            IntPtr _ProxyURL = 工具类.StringToIntptr(ProxyURL);
            服务层.Sunny_HTTPSetProxyIP(new IntPtr(Context), _ProxyURL);
            工具类.PtrFree(_ProxyURL);
        }

        /// HTTP 客户端 设置协议头
        public static void HTTPSetHeader(long Context, string name, string value)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            IntPtr _value = 工具类.StringToIntptr(value);
            服务层.Sunny_HTTPSetHeader(new IntPtr(Context), _name, _value);
            工具类.PtrFree(_name);
            工具类.PtrFree(_value);
        }

        /// <summary>
        /// 设置请求实际连接地址
        /// 设置后将不再使用URL或协议头中的HOST地址
        /// 某些时候,协议头中的HOST以及URL中的地址不能修改,修改后请求无法发送，这种情况下有用
        /// </summary>
        /// <param name="ip">例如:8.8.8.8:443,只能IP+端口，如果格式错误，不会使用</param>
        public static void HTTPSetServerIP(long Context, string ip)
        {
            IntPtr _ip = 工具类.StringToIntptr(ip);
            服务层.Sunny_HTTPSetServerIP(new IntPtr(Context), _ip);
            工具类.PtrFree(_ip);
        }

       
        ////HTTP 客户端 Open
        public static void HTTPOpen(long Context, string Method, string URL)
        {
            IntPtr _Method = 工具类.StringToIntptr(Method);
            IntPtr _URL = 工具类.StringToIntptr(URL);
            服务层.Sunny_HTTPOpen(new IntPtr(Context), _Method, _URL);
        }

        ////释放 HTTP客户端
        public static void RemoveHTTPClient(long Context)
        {
            服务层.Sunny_RemoveHTTPClient(new IntPtr(Context));
        }

        ////创建 HTTP 客户端
        public static long CreateHTTPClient()
        {
            return 服务层.Sunny_CreateHTTPClient().ToInt64();
        }

        ////JSON格式的protobuf数据转为protobuf二进制数据
        public static byte[] JsonToPB(string Json)
        {
            byte[] bs = 工具类.StrToBytes(Json);
            IntPtr _Json = 工具类.BytesToIntptr(bs);
            IntPtr aa = 服务层.Sunny_JsonToPB(_Json, new IntPtr(bs.Length));
            byte[] ss = PtrAutoToBytes(aa);
            Free(aa);
            工具类.PtrFree(_Json);
            return ss;
        }

        ////protobuf数据转为JSON格式
        public static string PbToJson(byte[] bin)
        {
            IntPtr _bin = 工具类.BytesToIntptr(bin);
            IntPtr aa = 服务层.Sunny_PbToJson(_bin, new IntPtr(bin.Length));
            string ss = 工具类.PtrToString(aa);
            工具类.PtrFree(_bin);
            Free(aa);
            return ss;
        }

        ////队列弹出
        public static byte[] QueuePull(string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            IntPtr aa = 服务层.Sunny_QueuePull(_name);
            byte[] ss = PtrAutoToBytes(aa);
            工具类.PtrFree(_name);
            Free(aa);
            return ss;
        }

        ////加入队列
        public static void QueuePush(string name, byte[] val)
        {
            IntPtr _bin = 工具类.BytesToIntptr(val);
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_QueuePush(_name, _bin, new IntPtr(val.Length));
            工具类.PtrFree(_bin);
            工具类.PtrFree(_name);
        }

        ////取队列长度
        public static int QueueLength(string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            int res = (int)服务层.Sunny_QueueLength(_name).ToInt64();
            工具类.PtrFree(_name);
            return res;
        }

        ////清空销毁队列
        public static void QueueRelease(string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_QueueRelease(_name);
            工具类.PtrFree(_name);
        }

        ////队列是否为空
        public static bool QueueIsEmpty(string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            bool res = 服务层.Sunny_QueueIsEmpty(_name);
            工具类.PtrFree(_name);
            return res;
        }

        ////创建队列
        public static void CreateQueue(string name)
        {
            IntPtr _name = 工具类.StringToIntptr(name);
            服务层.Sunny_CreateQueue(_name);
            工具类.PtrFree(_name);
        }

        ////TCP客户端 发送数据
        public static int SocketClientWrite(long Context, int OutTimes, byte[] val)
        {
            IntPtr _val = 工具类.BytesToIntptr(val);
            int res = (int)服务层.Sunny_SocketClientWrite(new IntPtr(Context), new IntPtr(OutTimes), _val, new IntPtr(val.Length)).ToInt64();
            工具类.PtrFree(_val);
            return res;
        }

        ////TCP客户端 断开连接
        public static void SocketClientClose(long Context)
        {
            服务层.Sunny_SocketClientClose(new IntPtr(Context));
        }

        ////TCP客户端 同步模式下 接收数据
        public static byte[] SocketClientReceive(long Context, int OutTimes)
        {
            IntPtr aa = 服务层.Sunny_SocketClientReceive(new IntPtr(Context), new IntPtr(OutTimes));
            byte[] ss = PtrAutoToBytes(aa);
            Free(aa);
            return ss;
        }

        ////TCP客户端 连接
        public static bool SocketClientDial(long Context, string addr, IntPtr call, bool isTls, bool synchronous, string ProxyUrl, 证书管理器 Cert, int timeOut, string RouterIP)
        {
            IntPtr _addr = 工具类.StringToIntptr(addr);
            IntPtr _RouterIP = 工具类.StringToIntptr(RouterIP);
            IntPtr _ProxyUrl = 工具类.StringToIntptr(ProxyUrl);
            IntPtr id = IntPtr.Zero;
            if (Cert != null)
            {
                id = new IntPtr(Cert.Context());
            }
            bool res = 服务层.Sunny_SocketClientDial(new IntPtr(Context), _addr, call, isTls, synchronous, _ProxyUrl, id, new IntPtr(timeOut), _RouterIP);
            工具类.PtrFree(_addr);
            工具类.PtrFree(_ProxyUrl);
            工具类.PtrFree(_RouterIP);
            return res;
        }

        ////TCP客户端 置缓冲区大小
        public static bool SocketClientSetBufferSize(long Context, int BufferSize)
        {
            return 服务层.Sunny_SocketClientSetBufferSize(new IntPtr(Context), new IntPtr(BufferSize));
        }

        ////TCP客户端 取错误
        public static string SocketClientGetErr(long Context)
        {
            IntPtr aa = 服务层.Sunny_SocketClientGetErr(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////释放 TCP客户端
        public static void RemoveSocketClient(long Context)
        {
            服务层.Sunny_RemoveSocketClient(new IntPtr(Context));
        }

        ////创建 TCP客户端
        public static long CreateSocketClient()
        {
            return 服务层.Sunny_CreateSocketClient().ToInt64();
        }

        ////Websocket客户端 同步模式下 接收数据 返回数据指针 失败返回0 length=返回数据长度
        public static byte[] WebsocketClientReceive(out int wsType, long Context, int OutTimes)
        {
            wsType = 常量类.WsMessageType.Invalid;
            IntPtr p = 服务层.Sunny_WebsocketClientReceive(new IntPtr(Context), new IntPtr(OutTimes));
            if (p.ToInt64() < 1)
            {
                return new byte[0];
            }
            long plen = BytesToInt(p, 8);
            byte[] mbin = 工具类.PtrToBytes(p, 8, 8);
            IntPtr p1 = 工具类.BytesToIntptr(mbin);
            int msgtype = BytesToInt(p1, 8);
            工具类.PtrFree(p1);
            wsType = msgtype;
            mbin = 工具类.PtrToBytes(p, (int)plen, 16);
            Free(p);
            return mbin;
        }

        ////Websocket客户端  发送数据
        public static bool WebsocketReadWrite(long Context, byte[] data, int messageType)
        {
            IntPtr aa = 工具类.BytesToIntptr(data);
            bool res = 服务层.Sunny_WebsocketReadWrite(new IntPtr(Context), aa, new IntPtr(data.Length), new IntPtr(messageType));
            工具类.PtrFree(aa);
            return res;
        }

        ////Websocket客户端 断开
        public static void WebsocketClose(long Context)
        {
            服务层.Sunny_WebsocketClose(new IntPtr(Context));
        }

        ////Websocket客户端 连接
        public static bool WebsocketDial(long Context, string URL, string Heads, IntPtr call, bool synchronous, string ProxyUrl, 证书管理器 Cert, int outTime, string RouterIP)
        {
            IntPtr _URL = 工具类.StringToIntptr(URL);
            IntPtr _Heads = 工具类.StringToIntptr(Heads);
            IntPtr _RouterIP = 工具类.StringToIntptr(RouterIP);
            IntPtr _ProxyUrl = 工具类.StringToIntptr(ProxyUrl);
            IntPtr id = IntPtr.Zero;
            if (Cert != null)
            {
                id = new IntPtr(Cert.Context());
            }
            bool res = 服务层.Sunny_WebsocketDial(new IntPtr(Context), _URL, _Heads, call, synchronous, _ProxyUrl, id, new IntPtr(outTime), _RouterIP);
            工具类.PtrFree(_URL);
            工具类.PtrFree(_Heads);
            工具类.PtrFree(_RouterIP);
            工具类.PtrFree(_ProxyUrl);
            return res;
        }

        ////Websocket客户端 设置心跳函数
        public static void WebsocketHeartbeat(long Context, long HeartbeatTime, IntPtr call)
        {
            服务层.Sunny_WebsocketHeartbeat(new IntPtr(Context), new IntPtr(HeartbeatTime), call);
        }

        ////Websocket客户端 获取错误
        public static string WebsocketGetErr(long Context)
        {
            IntPtr aa = 服务层.Sunny_WebsocketGetErr(new IntPtr(Context));
            string ss = 工具类.PtrToString(aa);
            Free(aa);
            return ss;
        }

        ////释放 Websocket客户端 对象
        public static void RemoveWebsocket(long Context)
        {
            服务层.Sunny_RemoveWebsocket(new IntPtr(Context));
        }

        ////创建 Websocket客户端 对象
        public static long CreateWebsocket()
        {
            return 服务层.Sunny_CreateWebsocket().ToInt64();
        }

        ////创建 Http证书管理器 对象 实现指定Host使用指定证书
        public static bool AddHttpCertificate(string host, 证书管理器 Cert, int Rules)
        {
            IntPtr id = IntPtr.Zero;
            if (Cert != null)
            {
                id = new IntPtr(Cert.Context());
            }
            IntPtr _host = 工具类.StringToIntptr(host);
            bool res = 服务层.Sunny_AddHttpCertificate(_host, id, new IntPtr(Rules));
            工具类.PtrFree(_host);
            return res;
        }

        ////删除 Http证书管理器 对象
        public static void DelHttpCertificate(string host)
        {
            IntPtr _host = 工具类.StringToIntptr(host);
            服务层.Sunny_DelHttpCertificate(_host);
            工具类.PtrFree(_host);
        }

        //// Redis 订阅消息
        public static bool RedisSubscribe(long Context, string scribe, IntPtr call, bool nc)
        {
            IntPtr _scribe = 工具类.StringToIntptr(scribe);
            bool rs = 服务层.Sunny_RedisSubscribe(new IntPtr(Context), _scribe, call, nc);
            工具类.PtrFree(_scribe);
            return rs;
        }

        //// Redis 删除
        public static bool RedisDelete(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            bool res = 服务层.Sunny_RedisDelete(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            return res;
        }

        //// Redis 清空当前数据库
        public static void RedisFlushDB(long Context)
        {
            服务层.Sunny_RedisFlushDB(new IntPtr(Context));
        }

        //// Redis 清空redis服务器
        public static void RedisFlushAll(long Context)
        {
            服务层.Sunny_RedisFlushAll(new IntPtr(Context));
        }

        //// Redis 关闭
        public static void RedisClose(long Context)
        {
            服务层.Sunny_RedisClose(new IntPtr(Context));
        }

        //// Redis 取整数值
        public static long RedisGetInt(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            long res = 服务层.Sunny_RedisGetInt(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            return res;
        }

        //// Redis 取指定条件键名
        public static string[] RedisGetKeys(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr res = 服务层.Sunny_RedisGetKeys(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            byte[] aa = PtrAutoToBytes(res);
            return SplitByZero(aa);

        }
        private static string[] SplitByZero(byte[] data)
        {
            List<string> result = new List<string>();
            List<byte> current = new List<byte>();

            foreach (var b in data)
            {
                if (b == 0)
                {
                    if (current.Count > 0)
                    {
                        byte[] bs = current.ToArray();
                        result.Add(工具类.BytesToStr(bs));
                        current.Clear();
                    }
                }
                else
                {
                    current.Add(b);
                }
            }

            // 添加最后一组数据（如果存在）
            if (current.Count > 0)
            {
                byte[] bs = current.ToArray();
                result.Add(工具类.BytesToStr(bs));
                current.Clear();
            }

            return result.ToArray();
        }
        //// Redis 自定义 执行和查询命令 返回操作结果可能是值 也可能是JSON文本
        public static string RedisDo(long Context, string args, IntPtr error)
        {
            IntPtr _args = 工具类.StringToIntptr(args);
            IntPtr res = 服务层.Sunny_RedisDo(new IntPtr(Context), _args, error);
            工具类.PtrFree(_args);
            string rr = 工具类.PtrToString(res);
            Free(res);
            return rr;
        }

        //// Redis 取文本值
        public static string RedisGetStr(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr res = 服务层.Sunny_RedisGetStr(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            string rr = 工具类.PtrToString(res);
            Free(res);
            return rr;
        }

        //// Redis 取Bytes值
        public static byte[] RedisGetBytes(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr res = 服务层.Sunny_RedisGetBytes(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            byte[] rr = PtrAutoToBytes(res);
            Free(res);
            return rr;
        }

        //// Redis 检查指定 key 是否存在
        public static bool RedisExists(long Context, string key)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            bool res = 服务层.Sunny_RedisExists(new IntPtr(Context), _key);
            工具类.PtrFree(_key);
            return res;
        }

        //// Redis 设置NX 【如果键名存在返回假】
        public static bool RedisSetNx(long Context, string key, string val, int expr)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr _val = 工具类.StringToIntptr(val);
            bool res = 服务层.Sunny_RedisSetNx(new IntPtr(Context), _key, _val, new IntPtr(expr));
            工具类.PtrFree(_key);
            工具类.PtrFree(_val);
            return res;
        }

        //// Redis 设置值
        public static bool RedisSet(long Context, string key, string val, int expr)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr _val = 工具类.StringToIntptr(val);
            bool res = 服务层.Sunny_RedisSet(new IntPtr(Context), _key, _val, new IntPtr(expr));
            工具类.PtrFree(_key);
            工具类.PtrFree(_val);
            return res;
        }

        //// Redis 设置Bytes值
        public static bool RedisSetBytes(long Context, string key, byte[] val, int expr)
        {
            IntPtr _key = 工具类.StringToIntptr(key);
            IntPtr _val = 工具类.BytesToIntptr(val);
            bool res = 服务层.Sunny_RedisSetBytes(new IntPtr(Context), _key, _val, new IntPtr(val.Length), new IntPtr(expr));
            工具类.PtrFree(_key);
            工具类.PtrFree(_val);
            return res;
        }

        //// Redis 连接
        public static bool RedisDial(long Context, string host, string pass, int db, int PoolSize, int MinIdleCons, int DialTimeout, int ReadTimeout, int WriteTimeout, int PoolTimeout, int IdleCheckFrequency, int IdleTimeout, IntPtr error)
        {
            IntPtr _host = 工具类.StringToIntptr(host);
            IntPtr _pass = 工具类.StringToIntptr(pass);
            bool res = 服务层.Sunny_RedisDial(new IntPtr(Context), _host, _pass, new IntPtr(db), new IntPtr(PoolSize), new IntPtr(MinIdleCons), new IntPtr(DialTimeout), new IntPtr(ReadTimeout), new IntPtr(WriteTimeout), new IntPtr(PoolTimeout), new IntPtr(IdleCheckFrequency), new IntPtr(IdleTimeout), error);
            工具类.PtrFree(_host);
            工具类.PtrFree(_pass);
            return res;
        }

        //// 释放 Redis 对象
        public static void RemoveRedis(long Context)
        {
            服务层.Sunny_RemoveRedis(new IntPtr(Context));
        }

        //// 创建 Redis 对象
        public static long CreateRedis()
        {
            return 服务层.Sunny_CreateRedis().ToInt64();
        }


        ////获取 UDP消息 返回数据指针
        public static byte[] GetUdpData(long MessageId)
        {
            IntPtr d = 服务层.Sunny_GetUdpData(new IntPtr(MessageId));
            byte[] sss = PtrAutoToBytes(d);
            Free(d);
            return sss;
        }

        ////修改设置 UDP消息 返回数据指针
        public static bool SetUdpData(long MessageId, byte[] val)
        {
            IntPtr _val = 工具类.BytesToIntptr(val);
            bool res = 服务层.Sunny_SetUdpData(new IntPtr(MessageId), _val, new IntPtr(val.Length));
            工具类.PtrFree(_val);
            return res;
        }
        ////加指定的UDP连接 模拟客户端向服务器端主动发送数据
        public static bool UdpSendToServer(long MessageId, byte[] val)
        {
            IntPtr _val = 工具类.BytesToIntptr(val);
            bool res = 服务层.Sunny_UdpSendToServer(new IntPtr(MessageId), _val, new IntPtr(val.Length));
            工具类.PtrFree(_val);
            return res;
        }
        ////指定的UDP连接 模拟服务器端向客户端主动发送数据
        public static bool UdpSendToClient(long MessageId, byte[] val)
        {
            IntPtr _val = 工具类.BytesToIntptr(val);
            bool res = 服务层.Sunny_UdpSendToClient(new IntPtr(MessageId), _val, new IntPtr(val.Length));
            工具类.PtrFree(_val);
            return res;
        }
        /// <summary> 
        /// 获取SunnyNet DLL 版本
        /// </summary>
        /// <returns></returns>
        public static string GetSunnyVersion()
        {
            IntPtr d = 服务层.Sunny_GetSunnyVersion();
            string ss = 工具类.PtrToString(d);
            Free(d);
            return ss;
        }
    }
}
