
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SunnyNetInternal = HTTP.封包类.接口层;

namespace HTTP.封包类
{
    public class 网络层
    {
        public static string Version()
        {
            return 中间层.GetSunnyVersion();
        }
        private SunnyNetInternal _callback = null;
        private NetHttpCallback _httpCallback = null;
        private NetTcpCallback _tcpCallback = null;
        private NetUDPCallback _udpCallback = null;
        private NetWebsockCallback _websocketCallback = null;

        private delegate void NetHttpCallback(IntPtr sunnyContext, IntPtr uniqueId, IntPtr messageId, IntPtr eventType, IntPtr method, IntPtr url, IntPtr error, IntPtr pid);
        private delegate void NetTcpCallback(IntPtr sunnyContext, IntPtr localAddr, IntPtr remoteAddr, IntPtr eventType, IntPtr messageId, IntPtr data, IntPtr dataLen, IntPtr theologyId, IntPtr pid);
        private delegate void NetUDPCallback(IntPtr sunnyContext, IntPtr localAddr, IntPtr remoteAddr, IntPtr eventType, IntPtr messageId, IntPtr theologyId, IntPtr pid);
        private delegate void NetWebsockCallback(IntPtr sunnyContext, IntPtr uniqueId, IntPtr messageId, IntPtr eventType, IntPtr method, IntPtr url, IntPtr pid, IntPtr wsMessageType);
        private delegate void NetScriptSaveCallback(IntPtr sunnyContext, IntPtr ScriptCode, IntPtr ScriptLen);
        private delegate void NetScriptLogCallback(IntPtr sunnyContext, IntPtr Log);

        private void DefaultNetTcpCallback(IntPtr sunnyContext, IntPtr localAddr, IntPtr remoteAddr, IntPtr eventType, IntPtr messageId, IntPtr data, IntPtr dataLen, IntPtr theologyId, IntPtr pid)
        {
            if (_callback != null)
            {
                TCPEvent obj = new TCPEvent(sunnyContext, 工具类.PtrToString(localAddr), 工具类.PtrToString(remoteAddr), eventType, messageId, 工具类.PtrToBytes(data, (int)dataLen.ToInt64()), theologyId, pid);
                _callback.OnTcpCallback(obj);
            }
        }

        private void DefaultNetUDPCallback(IntPtr sunnyContext, IntPtr localAddr, IntPtr remoteAddr, IntPtr eventType, IntPtr messageId, IntPtr theologyId, IntPtr pid)
        {
            if (_callback != null)
            {
                UDPEvent obj = new UDPEvent(sunnyContext, 工具类.PtrToString(localAddr), 工具类.PtrToString(remoteAddr), eventType, messageId, theologyId, pid);
                _callback.OnUdpCallback(obj);
            }
        }

        private void DefaultNetWebsocketCallback(IntPtr sunnyContext, IntPtr theologyId, IntPtr messageId, IntPtr eventType, IntPtr method, IntPtr url, IntPtr pid, IntPtr wsMessageType)
        {
            if (_callback != null)
            {
                WebSocketEvent obj = new WebSocketEvent(sunnyContext, theologyId, messageId, eventType, 工具类.PtrToString(method), 工具类.PtrToString(url), pid, wsMessageType);
                _callback.OnWebSocketCallback(obj);
            }
        }

        private void DefaultNetHttpCallback(IntPtr sunnyContext, IntPtr theologyId, IntPtr messageId, IntPtr eventType, IntPtr method, IntPtr url, IntPtr error, IntPtr pid)
        {
            if (_callback != null)
            {
                HTTPEvent obj = new HTTPEvent(sunnyContext, theologyId, messageId, eventType, 工具类.PtrToString(method), 工具类.PtrToString(url), 工具类.PtrToString(error), pid);
                _callback.OnHttpCallback(obj);
            }
        }

        private long _sunnyNetContext = 0;

        public 网络层()
        {
            _sunnyNetContext = 中间层.CreateSunnyNet();
        }

        ~网络层()
        {
            // 自动销毁
            中间层.ReleaseSunnyNet(_sunnyNetContext);
        }

        /// <summary>
        /// 导出已经设置的证书。
        /// </summary>
        public string ExportCertificate()
        {
            return 中间层.ExportCert(_sunnyNetContext);
        }

        /// <summary>
        /// 强制客户端使用 TCP，HTTPS 数据将无法解码。
        /// </summary>
        public void MustTcp(bool open)
        {
            中间层.SunnyNetMustTcp(_sunnyNetContext, open);
        }
        /// <summary>
        /// <para>中间件设置出口IP函数-全局。</para>
        /// <para>你也可以在HTTP/TCP回调中对某个请求单独设置出口IP</para>
        /// </summary>
        /// <param name="ip">请输入网卡对应的内网IP地址,输入空文本,则让系统自动选择</param>
        public void SetOutRouterIP(String ip)
        {
            中间层.SetOutRouterIP(_sunnyNetContext, ip);
        }

        /// <summary>
        /// 开启身份验证模式，客户端只能使用 S5 代理，并输入设置的账号密码。
        /// </summary>
        public void EnableAuthenticationMode(bool open)
        {
            中间层.SunnyNetVerifyUser(_sunnyNetContext, open);
        }

        /// <summary>
        /// 绑定回调地址，在启动之前调用。
        /// </summary>
        public bool BindCallback(SunnyNetInternal callback = null)
        {
            _httpCallback = new NetHttpCallback(DefaultNetHttpCallback);
            _tcpCallback = new NetTcpCallback(DefaultNetTcpCallback);
            _websocketCallback = new NetWebsockCallback(DefaultNetWebsocketCallback);
            _udpCallback = new NetUDPCallback(DefaultNetUDPCallback);

            IntPtr tcpPtr = Marshal.GetFunctionPointerForDelegate(_tcpCallback);
            IntPtr httpPtr = Marshal.GetFunctionPointerForDelegate(_httpCallback);
            IntPtr websocketPtr = Marshal.GetFunctionPointerForDelegate(_websocketCallback);
            IntPtr udpPtr = Marshal.GetFunctionPointerForDelegate(_udpCallback);

            _callback = callback;
            return 中间层.SunnyNetSetCallback(_sunnyNetContext, httpPtr, tcpPtr, websocketPtr, udpPtr, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// 在启动之前调用以绑定端口。
        /// </summary>
        public bool BindPort(int port)
        {
            return 中间层.SunnyNetSetPort(_sunnyNetContext, port);
        }

        /// <summary>
        /// 禁用非 HTTP/S 的 TCP 连接。
        /// </summary>
        public bool DisableTcpRequests(bool disable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 开启后，请求将使用随机的 TLS 指纹，关闭后固定指纹也会被取消。
        /// </summary>
        public bool EnableRandomTLSFingerprinting(bool open)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取 SunnyNet 上下文。
        /// </summary>
        public long GetSunnyNetContext()
        {
            return _sunnyNetContext;
        }

        /// <summary>
        /// 启动 SunnyNet，需先绑定端口。
        /// </summary>
        public bool Start()
        {
            return 中间层.SunnyNetStart(_sunnyNetContext);
        }

        /// <summary>
        /// 取消已设置的 IE 代理。
        /// </summary>
        public bool CancelIEProxy()
        {
            return 中间层.CancelIEProxy(_sunnyNetContext);
        }

        /// <summary>
        /// 设置当前绑定的端口号为当前 IE 代理，设置前请先绑定端口。
        /// </summary>
        public bool SetIEProxy()
        {
            return 中间层.SetIeProxy(_sunnyNetContext);
        }

        /// <summary>
        /// 停止代理并自动关闭 IE 代理。
        /// </summary>
        public bool Stop()
        {
            CancelIEProxy();
            return 中间层.SunnyNetClose(_sunnyNetContext);
        }

        /// <summary>
        /// 获取中间件启动时的错误信息。
        /// </summary>
        public string GetError()
        {
            return 中间层.SunnyNetError(_sunnyNetContext);
        }
        /// <summary>
        /// 设置全局上游代理。
        /// <para>例如，以下示例格式：</para>
        ///  <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list>
        /// </summary>
        /// <param name="proxyAddress">代理 URL，指定要使用的代理地址。
        /// <para>例如，以下示例格式：</para>
        ///  <list type="bullet">
        ///     <item>HTTP代理, 有账号密码: <c>http://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>S5代理, 有账号密码: <c>socket5://admin:123456@127.0.0.1:8888</c></item>
        ///     <item>HTTP代理, 无账号密码: <c>http://127.0.0.1:8888</c></item>
        ///     <item>S5代理, 无账号密码: <c>socket5://127.0.0.1:8888</c></item>
        /// </list></param>
        /// <param name="outTime">代理超时(毫秒)，默认30秒</param>
        public bool SetGlobalProxy(string proxyAddress, int outTime = 30000)
        {
            return 中间层.SetGlobalProxy(_sunnyNetContext, proxyAddress, outTime);
        }
        /// <summary>
        /// 取消全局上游代理。
        /// </summary>
        public void CancelGlobalProxy()
        {
            SetGlobalProxy("");
        }

        /// <summary>
        /// 设置上游代理使用规则。
        /// 输入 Host 不带端口号；在此规则内的不使用上游代理，多用分号或换行分割，例如 "127.0.0.1;192.168.*.*"。
        /// </summary>
        public bool SetGlobalProxyUsageRule(string regexp)
        {
            return 中间层.CompileProxyRegexp(_sunnyNetContext, regexp);
        }

        /// <summary>
        /// 设置 DNS 服务器地址，仅支持 TLS 的 DNS 服务器，即 853 端口的，例如: 223.5.5.5:853
        /// </summary>
        /// <param name="regexp">DNS 服务器地址。</param>
        public void SetDnsServer(string regexp)
        {
            中间层.SetDnsServer(regexp);
        }

        /// <summary>
        /// 设置是否随机使用 TLS 指纹。
        /// </summary>
        /// <param name="random">如果为 true，则随机使用 TLS 指纹；否则不随机。</param>
        /// <returns>返回设置是否成功。</returns>
        public bool RandomJa3(bool random)
        {
            return 中间层.SetRandomTLS(_sunnyNetContext, random);
        }

        /// <summary>
        /// 禁用或启用 UDP 功能.
        /// </summary>
        /// <param name="disable">是否禁用 UDP。</param>
        /// <returns>成功返回 true，失败返回 false。</returns>
        public bool DisableUDP(bool disable)
        {
            return 中间层.DisableUDP(_sunnyNetContext, disable);
        }

        /// <summary>
        /// 禁用或启用 TCP 功能.
        /// </summary>
        /// <param name="disable">是否禁用 TCP。</param>
        /// <returns>成功返回 true，失败返回 false。</returns>
        public bool DisableTCP(bool disable)
        {
            return 中间层.DisableTCP(_sunnyNetContext, disable);
        }

        /// <summary>
        /// 当前SDK是否支持脚本代码。
        /// 如果当前SDK是Mini 版本则不支持脚本代码。
        /// </summary>
        /// <returns>支持返回 true ,不支持返回 false</returns>
        public bool IsScriptCodeSupported()
        {
            return true;
        }

        /// <summary>
        /// <list type="bullet">
        ///   <item>设置 HTTP 请求的最大更新长度.</item>
        ///   <item>超过此长度将转为元素请求,无法修改数据，只能将提交内容储存到文件.</item>
        ///   <item>在回调函数中使用:<see cref="Conn.Request().IsRequestRawBody()"/>检查是否为原始请求</item>
        ///   <item>在回调函数中使用:<see cref="Conn.Request().RawRequestDataToFile(filePath)"/>将POST提交的原始内容储存到文件</item>
        /// </list> 
        /// </summary>
        /// <param name="sunnyContext">SunnyNet 上下文。</param>
        /// <param name="maxLength">最大长度。</param>
        /// <returns>返回设置结果。</returns>
        public static bool SetHTTPRequestMaxUpdateLength(long sunnyContext, long maxLength)
        {
            return 中间层.SetHTTPRequestMaxUpdateLength(sunnyContext, maxLength);
        }

        /// <summary>
        /// <list type="bullet">
        ///   <item>创建 HTTP 证书管理器对象，实现指定 Host 使用指定证书.</item>
        ///   <item>创建后,可以使用 <see cref="DelHttpCertificate"/>删除此规则</item>
        /// </list> 
        /// </summary>
        /// <param name="host">指定的 Host。</param>
        /// <param name="cert">证书管理器对象。</param>
        /// <param name="rules">规则。</param>
        /// <returns>返回添加结果。</returns>
        public static bool AddHttpCertificate(string host, 证书管理器 cert, int rules)
        {
            return 中间层.AddHttpCertificate(host, cert, rules);
        }

        /// <summary>
        /// <list type="bullet">
        ///   <item>删除 HTTP 证书管理器对象.</item>
        ///   <item>删除由 <see cref="AddHttpCertificate"/>添加的证书使用规则</item>
        /// </list>  
        /// </summary>
        /// <param name="host">指定的 Host。</param>
        public static void DelHttpCertificate(string host)
        {
            中间层.DelHttpCertificate(host);
        }

        /// <summary>
        /// 设置强制走 TCP 规则。
        /// </summary>
        /// <param name="rule">要设置的规则字符串。</param>
        /// <param name="rulesAllow">是否允许走 TCP 的规则。
        /// <para>请使用以下常量模板之一：</para>
        /// <list type="bullet">
        ///   <item><see cref="Const.MustTcpRule.Outside"/>：规则之外走 TCP</item>
        ///   <item><see cref="Const.MustTcpRule.Within"/>：规则之内走 TCP</item>
        /// </list></param>
        /// <returns>返回设置是否成功。</returns>
        public bool SetMustTcpRule(string rule, bool rulesAllow = 常量类.MustTcpRule.Within)
        {
            return 中间层.SetMustTcpRegexp(_sunnyNetContext, rule, rulesAllow);
        }

        /// <summary>
        /// 加载进程代理驱动。
        /// 只允许一个 SunnyNet 使用，自动安装所需驱动文件。
        /// isNFAPI 为 true 表示使用 NFAPI 驱动，false 表示使用 Proxifier。
        /// </summary>
        public bool LoadDriver(bool isNFAPI)
        {
            return 中间层.OpenDrive(_sunnyNetContext, isNFAPI);
        }
        /// <summary>
        /// 卸载驱动。
        /// <para>调用 <see cref="UnDrive"/> 卸载驱动，仅在 Windows 上有效【需要管理权限】。</para>
        /// <para>执行成功后会立即重启系统，若函数执行后没有重启系统，表示没有管理员权限。</para>
        /// </summary> 
        public void UnDriver()
        {
            中间层.UnDrive(_sunnyNetContext);
        }

        /// <summary>
        /// 添加指定的进程名进行捕获。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开此进程已经连接的 TCP 连接。
        /// </summary>
        public void AddProcessName(string processName)
        {
            中间层.ProcessAddName(_sunnyNetContext, processName);
        }

        /// <summary>
        /// 添加指定的进程 PID 进行捕获。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开此进程已经连接的 TCP 连接。
        /// </summary>
        public void AddProcessPid(int processPid)
        {
            中间层.ProcessAddPid(_sunnyNetContext, processPid);
        }

        /// <summary>
        /// 删除指定的进程 PID 停止捕获。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开此进程已经连接的 TCP 连接。
        /// </summary>
        public void RemoveProcessPid(int processPid)
        {
            中间层.ProcessDelPid(_sunnyNetContext, processPid);
        }

        /// <summary>
        /// 开启后，所有进程将会被捕获。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开所有进程已经连接的 TCP 连接。 
        /// </summary>
        /// <param name="enable">开启或关闭</param>
        /// <param name="stopNet">是否需要对所有进程都断网一次</param>
        public void SetCaptureAnyProcess(bool enable, bool stopNet)
        {
            中间层.ProcessALLName(_sunnyNetContext, enable, stopNet);
        }

        /// <summary>
        /// 删除指定的进程名停止捕获。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开此进程已经连接的 TCP 连接。
        /// </summary>
        public void RemoveProcessName(string processName)
        {
            中间层.ProcessDelName(_sunnyNetContext, processName);
        }

        /// <summary>
        /// 删除已设置的所有 PID 和进程名。
        /// 需调用<see cref="LoadDriver"/> 后生效，会强制断开所有进程已经连接的 TCP 连接。
        /// </summary>
        public void RemoveAllProcesses()
        {
            中间层.ProcessCancelAll(_sunnyNetContext);
        }
    }
}
