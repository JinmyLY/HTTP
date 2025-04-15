using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HTTP.功能类
{
    class 服务层
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        private static string LibraryName = Environment.Is64BitProcess ? "LiuYun64.dll" : "LiuYun.dll";
        static IntPtr Handle = LoadLibrary(LibraryName);

        private static IntPtr GetFunction(string func)
        {
            if (Handle == IntPtr.Zero)
            {
                Debug.WriteLine("载入 SunnyNet Library[" + LibraryName + "] 失败");
                throw new Exception("载入 SunnyNet Library 失败");
            }
            IntPtr functionPointer = GetProcAddress(Handle, func);
            if (functionPointer == IntPtr.Zero)
            {
                throw new Exception("无法从 SunnyNet Library 找到方法函数 " + func + "");
            }
            return functionPointer;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateSunnyNet();
        public static api_CreateSunnyNet Sunny_CreateSunnyNet = (api_CreateSunnyNet)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateSunnyNet"), typeof(api_CreateSunnyNet));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_ReleaseSunnyNet(IntPtr SunnyContext);
        public static api_ReleaseSunnyNet Sunny_ReleaseSunnyNet = (api_ReleaseSunnyNet)Marshal.GetDelegateForFunctionPointer(GetFunction("ReleaseSunnyNet"), typeof(api_ReleaseSunnyNet));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetStart(IntPtr SunnyContext);
        public static api_SunnyNetStart Sunny_SunnyNetStart = (api_SunnyNetStart)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetStart"), typeof(api_SunnyNetStart));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetSetPort(IntPtr SunnyContext, IntPtr Port);
        public static api_SunnyNetSetPort Sunny_SunnyNetSetPort = (api_SunnyNetSetPort)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetSetPort"), typeof(api_SunnyNetSetPort));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetClose(IntPtr SunnyContext);
        public static api_SunnyNetClose Sunny_SunnyNetClose = (api_SunnyNetClose)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetClose"), typeof(api_SunnyNetClose));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetSetCert(IntPtr SunnyContext, IntPtr CertificateManagerId);
        public static api_SunnyNetSetCert Sunny_SunnyNetSetCert = (api_SunnyNetSetCert)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetSetCert"), typeof(api_SunnyNetSetCert));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SunnyNetInstallCert(IntPtr SunnyContext);
        public static api_SunnyNetInstallCert Sunny_SunnyNetInstallCert = (api_SunnyNetInstallCert)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetInstallCert"), typeof(api_SunnyNetInstallCert));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetSetCallback(IntPtr SunnyContext, IntPtr httpCallback, IntPtr tcpCallback, IntPtr wsCallback, IntPtr udpCallback);
        public static api_SunnyNetSetCallback Sunny_SunnyNetSetCallback = (api_SunnyNetSetCallback)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetSetCallback"), typeof(api_SunnyNetSetCallback));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetSocket5AddUser(IntPtr SunnyContext, IntPtr User, IntPtr Pass);
        public static api_SunnyNetSocket5AddUser Sunny_SunnyNetSocket5AddUser = (api_SunnyNetSocket5AddUser)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetSocket5AddUser"), typeof(api_SunnyNetSocket5AddUser));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetVerifyUser(IntPtr SunnyContext, bool open);
        public static api_SunnyNetVerifyUser Sunny_SunnyNetVerifyUser = (api_SunnyNetVerifyUser)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetVerifyUser"), typeof(api_SunnyNetVerifyUser));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SunnyNetSocket5DelUser(IntPtr SunnyContext, IntPtr User);
        public static api_SunnyNetSocket5DelUser Sunny_SunnyNetSocket5DelUser = (api_SunnyNetSocket5DelUser)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetSocket5DelUser"), typeof(api_SunnyNetSocket5DelUser));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SunnyNetMustTcp(IntPtr SunnyContext, bool open);
        public static api_SunnyNetMustTcp Sunny_SunnyNetMustTcp = (api_SunnyNetMustTcp)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetMustTcp"), typeof(api_SunnyNetMustTcp));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_HTTPSetCertManager(IntPtr Context, IntPtr CertManagerContext);
        public static api_HTTPSetCertManager Sunny_HTTPSetCertManager = (api_HTTPSetCertManager)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetCertManager"), typeof(api_HTTPSetCertManager));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_CompileProxyRegexp(IntPtr SunnyContext, IntPtr Regexp);
        public static api_CompileProxyRegexp Sunny_CompileProxyRegexp = (api_CompileProxyRegexp)Marshal.GetDelegateForFunctionPointer(GetFunction("CompileProxyRegexp"), typeof(api_CompileProxyRegexp));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetMustTcpRegexp(IntPtr SunnyContext, IntPtr Regexp, bool RulesAllow);
        public static api_SetMustTcpRegexp Sunny_SetMustTcpRegexp = (api_SetMustTcpRegexp)Marshal.GetDelegateForFunctionPointer(GetFunction("SetMustTcpRegexp"), typeof(api_SetMustTcpRegexp));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SunnyNetError(IntPtr SunnyContext);
        public static api_SunnyNetError Sunny_SunnyNetError = (api_SunnyNetError)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetError"), typeof(api_SunnyNetError));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetRequestOutTime(IntPtr Context, IntPtr times);
        public static api_SetRequestOutTime Sunny_SetRequestOutTime = (api_SetRequestOutTime)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestOutTime"), typeof(api_SetRequestOutTime));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetGlobalProxy(IntPtr SunnyContext, IntPtr ProxyAddress, IntPtr outTime);
        public static api_SetGlobalProxy Sunny_SetGlobalProxy = (api_SetGlobalProxy)Marshal.GetDelegateForFunctionPointer(GetFunction("SetGlobalProxy"), typeof(api_SetGlobalProxy));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ExportCert(IntPtr SunnyContext);
        public static api_ExportCert Sunny_ExportCert = (api_ExportCert)Marshal.GetDelegateForFunctionPointer(GetFunction("ExportCert"), typeof(api_ExportCert));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetIeProxy(IntPtr SunnyContext);
        public static api_SetIeProxy Sunny_SetIeProxy = (api_SetIeProxy)Marshal.GetDelegateForFunctionPointer(GetFunction("SetIeProxy"), typeof(api_SetIeProxy));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_CancelIEProxy(IntPtr SunnyContext);
        public static api_CancelIEProxy Sunny_CancelIEProxy = (api_CancelIEProxy)Marshal.GetDelegateForFunctionPointer(GetFunction("CancelIEProxy"), typeof(api_CancelIEProxy));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetRequestCookie(IntPtr MessageId, IntPtr name, IntPtr val);
        public static api_SetRequestCookie Sunny_SetRequestCookie = (api_SetRequestCookie)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestCookie"), typeof(api_SetRequestCookie));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetRequestAllCookie(IntPtr MessageId, IntPtr val);
        public static api_SetRequestAllCookie Sunny_SetRequestAllCookie = (api_SetRequestAllCookie)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestAllCookie"), typeof(api_SetRequestAllCookie));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestCookie(IntPtr MessageId, IntPtr name);
        public static api_GetRequestCookie Sunny_GetRequestCookie = (api_GetRequestCookie)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestCookie"), typeof(api_GetRequestCookie));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestALLCookie(IntPtr MessageId);
        public static api_GetRequestALLCookie Sunny_GetRequestALLCookie = (api_GetRequestALLCookie)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestALLCookie"), typeof(api_GetRequestALLCookie));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_DelResponseHeader(IntPtr MessageId, IntPtr name);
        public static api_DelResponseHeader Sunny_DelResponseHeader = (api_DelResponseHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("DelResponseHeader"), typeof(api_DelResponseHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_DelRequestHeader(IntPtr MessageId, IntPtr name);
        public static api_DelRequestHeader Sunny_DelRequestHeader = (api_DelRequestHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("DelRequestHeader"), typeof(api_DelRequestHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RawRequestDataToFile(IntPtr MessageId, IntPtr name, IntPtr nameLen);
        public static api_RawRequestDataToFile Sunny_RawRequestDataToFile = (api_RawRequestDataToFile)Marshal.GetDelegateForFunctionPointer(GetFunction("RawRequestDataToFile"), typeof(api_RawRequestDataToFile));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_IsRequestRawBody(IntPtr MessageId);
        public static api_IsRequestRawBody Sunny_IsRequestRawBody = (api_IsRequestRawBody)Marshal.GetDelegateForFunctionPointer(GetFunction("IsRequestRawBody"), typeof(api_IsRequestRawBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetRequestHeader(IntPtr MessageId, IntPtr name, IntPtr val);
        public static api_SetRequestHeader Sunny_SetRequestHeader = (api_SetRequestHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestHeader"), typeof(api_SetRequestHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetResponseHeader(IntPtr MessageId, IntPtr name, IntPtr val);
        public static api_SetResponseHeader Sunny_SetResponseHeader = (api_SetResponseHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("SetResponseHeader"), typeof(api_SetResponseHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestHeader(IntPtr MessageId, IntPtr name);
        public static api_GetRequestHeader Sunny_GetRequestHeader = (api_GetRequestHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestHeader"), typeof(api_GetRequestHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseHeader(IntPtr MessageId, IntPtr name);
        public static api_GetResponseHeader Sunny_GetResponseHeader = (api_GetResponseHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseHeader"), typeof(api_GetResponseHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetResponseAllHeader(IntPtr MessageId, IntPtr value);
        public static api_SetResponseAllHeader Sunny_SetResponseAllHeader = (api_SetResponseAllHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("SetResponseAllHeader"), typeof(api_SetResponseAllHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseProto(IntPtr MessageId);
        public static api_GetResponseProto Sunny_GetResponseProto = (api_GetResponseProto)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseProto"), typeof(api_GetResponseProto));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseAllHeader(IntPtr MessageId);
        public static api_GetResponseAllHeader Sunny_GetResponseAllHeader = (api_GetResponseAllHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseAllHeader"), typeof(api_GetResponseAllHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestAllHeader(IntPtr MessageId);
        public static api_GetRequestAllHeader Sunny_GetRequestAllHeader = (api_GetRequestAllHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestAllHeader"), typeof(api_GetRequestAllHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestProto(IntPtr MessageId);
        public static api_GetRequestProto Sunny_GetRequestProto = (api_GetRequestProto)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestProto"), typeof(api_GetRequestProto));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetRequestProxy(IntPtr MessageId, IntPtr ProxyUrl, IntPtr outTime);
        public static api_SetRequestProxy Sunny_SetRequestProxy = (api_SetRequestProxy)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestProxy"), typeof(api_SetRequestProxy));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RandomRequestCipherSuites(IntPtr MessageId);
        public static api_RandomRequestCipherSuites Sunny_RandomRequestCipherSuites = (api_RandomRequestCipherSuites)Marshal.GetDelegateForFunctionPointer(GetFunction("RandomRequestCipherSuites"), typeof(api_RandomRequestCipherSuites));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetRequestALLHeader(IntPtr MessageId, IntPtr value);
        public static api_SetRequestALLHeader Sunny_SetRequestALLHeader = (api_SetRequestALLHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestALLHeader"), typeof(api_SetRequestALLHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetRequestHTTP2Config(IntPtr MessageId, IntPtr config);
        public static api_SetRequestHTTP2Config Sunny_SetRequestHTTP2Config = (api_SetRequestHTTP2Config)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestHTTP2Config"), typeof(api_SetRequestHTTP2Config));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseStatusCode(IntPtr MessageId);
        public static api_GetResponseStatusCode Sunny_GetResponseStatusCode = (api_GetResponseStatusCode)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseStatusCode"), typeof(api_GetResponseStatusCode));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestClientIp(IntPtr MessageId);
        public static api_GetRequestClientIp Sunny_GetRequestClientIp = (api_GetRequestClientIp)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestClientIp"), typeof(api_GetRequestClientIp));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseStatus(IntPtr MessageId);
        public static api_GetResponseStatus Sunny_GetResponseStatus = (api_GetResponseStatus)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseStatus"), typeof(api_GetResponseStatus));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseServerAddress(IntPtr MessageId);
        public static api_GetResponseServerAddress Sunny_GetResponseServerAddress = (api_GetResponseServerAddress)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseServerAddress"), typeof(api_GetResponseServerAddress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetResponseStatus(IntPtr MessageId, IntPtr code);
        public static api_SetResponseStatus Sunny_SetResponseStatus = (api_SetResponseStatus)Marshal.GetDelegateForFunctionPointer(GetFunction("SetResponseStatus"), typeof(api_SetResponseStatus));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetRequestUrl(IntPtr MessageId, IntPtr URI);
        public static api_SetRequestUrl Sunny_SetRequestUrl = (api_SetRequestUrl)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestUrl"), typeof(api_SetRequestUrl));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestBodyLen(IntPtr MessageId);
        public static api_GetRequestBodyLen Sunny_GetRequestBodyLen = (api_GetRequestBodyLen)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestBodyLen"), typeof(api_GetRequestBodyLen));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseBodyLen(IntPtr MessageId);
        public static api_GetResponseBodyLen Sunny_GetResponseBodyLen = (api_GetResponseBodyLen)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseBodyLen"), typeof(api_GetResponseBodyLen));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetResponseData(IntPtr MessageId, IntPtr data, IntPtr dataLen);
        public static api_SetResponseData Sunny_SetResponseData = (api_SetResponseData)Marshal.GetDelegateForFunctionPointer(GetFunction("SetResponseData"), typeof(api_SetResponseData));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetRequestData(IntPtr MessageId, IntPtr data, IntPtr dataLen);
        public static api_SetRequestData Sunny_SetRequestData = (api_SetRequestData)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRequestData"), typeof(api_SetRequestData));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetRequestBody(IntPtr MessageId);
        public static api_GetRequestBody Sunny_GetRequestBody = (api_GetRequestBody)Marshal.GetDelegateForFunctionPointer(GetFunction("GetRequestBody"), typeof(api_GetRequestBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetResponseBody(IntPtr MessageId);
        public static api_GetResponseBody Sunny_GetResponseBody = (api_GetResponseBody)Marshal.GetDelegateForFunctionPointer(GetFunction("GetResponseBody"), typeof(api_GetResponseBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetWebsocketBodyLen(IntPtr MessageId);
        public static api_GetWebsocketBodyLen Sunny_GetWebsocketBodyLen = (api_GetWebsocketBodyLen)Marshal.GetDelegateForFunctionPointer(GetFunction("GetWebsocketBodyLen"), typeof(api_GetWebsocketBodyLen));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetWebsocketBody(IntPtr MessageId);
        public static api_GetWebsocketBody Sunny_GetWebsocketBody = (api_GetWebsocketBody)Marshal.GetDelegateForFunctionPointer(GetFunction("GetWebsocketBody"), typeof(api_GetWebsocketBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetWebsocketBody(IntPtr MessageId, IntPtr data, IntPtr dataLen);
        public static api_SetWebsocketBody Sunny_SetWebsocketBody = (api_SetWebsocketBody)Marshal.GetDelegateForFunctionPointer(GetFunction("SetWebsocketBody"), typeof(api_SetWebsocketBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SendWebsocketBody(IntPtr TheologyID, IntPtr MessageType, IntPtr data, IntPtr dataLen);
        public static api_SendWebsocketBody Sunny_SendWebsocketBody = (api_SendWebsocketBody)Marshal.GetDelegateForFunctionPointer(GetFunction("SendWebsocketBody"), typeof(api_SendWebsocketBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SendWebsocketClientBody(IntPtr TheologyID, IntPtr MessageType, IntPtr data, IntPtr dataLen);
        public static api_SendWebsocketClientBody Sunny_SendWebsocketClientBody = (api_SendWebsocketClientBody)Marshal.GetDelegateForFunctionPointer(GetFunction("SendWebsocketClientBody"), typeof(api_SendWebsocketClientBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_CloseWebsocket(IntPtr theology);
        public static api_CloseWebsocket Sunny_CloseWebsocket = (api_CloseWebsocket)Marshal.GetDelegateForFunctionPointer(GetFunction("CloseWebsocket"), typeof(api_CloseWebsocket));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetTcpBody(IntPtr MessageId, IntPtr MsgType, IntPtr data, IntPtr dataLen);
        public static api_SetTcpBody Sunny_SetTcpBody = (api_SetTcpBody)Marshal.GetDelegateForFunctionPointer(GetFunction("SetTcpBody"), typeof(api_SetTcpBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetTcpAgent(IntPtr MessageId, IntPtr ProxyUrl, IntPtr outTime);
        public static api_SetTcpAgent Sunny_SetTcpAgent = (api_SetTcpAgent)Marshal.GetDelegateForFunctionPointer(GetFunction("SetTcpAgent"), typeof(api_SetTcpAgent));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_TcpCloseClient(IntPtr theology);
        public static api_TcpCloseClient Sunny_TcpCloseClient = (api_TcpCloseClient)Marshal.GetDelegateForFunctionPointer(GetFunction("TcpCloseClient"), typeof(api_TcpCloseClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetTcpConnectionIP(IntPtr MessageId, IntPtr address);
        public static api_SetTcpConnectionIP Sunny_SetTcpConnectionIP = (api_SetTcpConnectionIP)Marshal.GetDelegateForFunctionPointer(GetFunction("SetTcpConnectionIP"), typeof(api_SetTcpConnectionIP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_TcpSendMsg(IntPtr theology, IntPtr data, IntPtr dataLen);
        public static api_TcpSendMsg Sunny_TcpSendMsg = (api_TcpSendMsg)Marshal.GetDelegateForFunctionPointer(GetFunction("TcpSendMsg"), typeof(api_TcpSendMsg));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_TcpSendMsgClient(IntPtr theology, IntPtr data, IntPtr dataLen);
        public static api_TcpSendMsgClient Sunny_TcpSendMsgClient = (api_TcpSendMsgClient)Marshal.GetDelegateForFunctionPointer(GetFunction("TcpSendMsgClient"), typeof(api_TcpSendMsgClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_BytesToInt(IntPtr data, IntPtr dataLen);
        public static api_BytesToInt Sunny_BytesToInt = (api_BytesToInt)Marshal.GetDelegateForFunctionPointer(GetFunction("BytesToInt"), typeof(api_BytesToInt));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GzipUnCompress(IntPtr data, IntPtr dataLen);
        public static api_GzipUnCompress Sunny_GzipUnCompress = (api_GzipUnCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("GzipUnCompress"), typeof(api_GzipUnCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ZSTDDecompress(IntPtr data, IntPtr dataLen);
        public static api_ZSTDDecompress Sunny_ZSTDDecompress = (api_ZSTDDecompress)Marshal.GetDelegateForFunctionPointer(GetFunction("ZSTDDecompress"), typeof(api_ZSTDDecompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ZSTDCompress(IntPtr data, IntPtr dataLen);
        public static api_ZSTDCompress Sunny_ZSTDCompress = (api_ZSTDCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("ZSTDCompress"), typeof(api_ZSTDCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_BrUnCompress(IntPtr data, IntPtr dataLen);
        public static api_BrUnCompress Sunny_BrUnCompress = (api_BrUnCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("BrUnCompress"), typeof(api_BrUnCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_BrCompress(IntPtr data, IntPtr dataLen);
        public static api_BrCompress Sunny_BrCompress = (api_BrCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("BrCompress"), typeof(api_BrCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_BrotliCompress(IntPtr data, IntPtr dataLen);
        public static api_BrotliCompress Sunny_BrotliCompress = (api_BrotliCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("BrotliCompress"), typeof(api_BrotliCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GzipCompress(IntPtr data, IntPtr dataLen);
        public static api_GzipCompress Sunny_GzipCompress = (api_GzipCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("GzipCompress"), typeof(api_GzipCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ZlibCompress(IntPtr data, IntPtr dataLen);
        public static api_ZlibCompress Sunny_ZlibCompress = (api_ZlibCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("ZlibCompress"), typeof(api_ZlibCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ZlibUnCompress(IntPtr data, IntPtr dataLen);
        public static api_ZlibUnCompress Sunny_ZlibUnCompress = (api_ZlibUnCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("ZlibUnCompress"), typeof(api_ZlibUnCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_DeflateUnCompress(IntPtr data, IntPtr dataLen);
        public static api_DeflateUnCompress Sunny_DeflateUnCompress = (api_DeflateUnCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("DeflateUnCompress"), typeof(api_DeflateUnCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_DeflateCompress(IntPtr data, IntPtr dataLen);
        public static api_DeflateCompress Sunny_DeflateCompress = (api_DeflateCompress)Marshal.GetDelegateForFunctionPointer(GetFunction("DeflateCompress"), typeof(api_DeflateCompress));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_WebpToJpegBytes(IntPtr data, IntPtr dataLen, IntPtr SaveQuality);
        public static api_WebpToJpegBytes Sunny_WebpToJpegBytes = (api_WebpToJpegBytes)Marshal.GetDelegateForFunctionPointer(GetFunction("WebpToJpegBytes"), typeof(api_WebpToJpegBytes));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_WebpToPngBytes(IntPtr data, IntPtr dataLen);
        public static api_WebpToPngBytes Sunny_WebpToPngBytes = (api_WebpToPngBytes)Marshal.GetDelegateForFunctionPointer(GetFunction("WebpToPngBytes"), typeof(api_WebpToPngBytes));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_WebpToJpeg(IntPtr webpPath, IntPtr savePath, IntPtr SaveQuality);
        public static api_WebpToJpeg Sunny_WebpToJpeg = (api_WebpToJpeg)Marshal.GetDelegateForFunctionPointer(GetFunction("WebpToJpeg"), typeof(api_WebpToJpeg));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_WebpToPng(IntPtr webpPath, IntPtr savePath);
        public static api_WebpToPng Sunny_WebpToPng = (api_WebpToPng)Marshal.GetDelegateForFunctionPointer(GetFunction("WebpToPng"), typeof(api_WebpToPng));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_OpenDrive(IntPtr SunnyContext, bool isNFAPI);
        public static api_OpenDrive Sunny_OpenDrive = (api_OpenDrive)Marshal.GetDelegateForFunctionPointer(GetFunction("OpenDrive"), typeof(api_OpenDrive));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_UnDrive(IntPtr SunnyContext);
        public static api_UnDrive Sunny_UnDrive = (api_UnDrive)Marshal.GetDelegateForFunctionPointer(GetFunction("UnDrive"), typeof(api_UnDrive));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessAddName(IntPtr SunnyContext, IntPtr Name);
        public static api_ProcessAddName Sunny_ProcessAddName = (api_ProcessAddName)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessAddName"), typeof(api_ProcessAddName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessDelName(IntPtr SunnyContext, IntPtr Name);
        public static api_ProcessDelName Sunny_ProcessDelName = (api_ProcessDelName)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessDelName"), typeof(api_ProcessDelName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessAddPid(IntPtr SunnyContext, IntPtr pid);
        public static api_ProcessAddPid Sunny_ProcessAddPid = (api_ProcessAddPid)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessAddPid"), typeof(api_ProcessAddPid));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessDelPid(IntPtr SunnyContext, IntPtr pid);
        public static api_ProcessDelPid Sunny_ProcessDelPid = (api_ProcessDelPid)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessDelPid"), typeof(api_ProcessDelPid));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessCancelAll(IntPtr SunnyContext);
        public static api_ProcessCancelAll Sunny_ProcessCancelAll = (api_ProcessCancelAll)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessCancelAll"), typeof(api_ProcessCancelAll));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_ProcessALLName(IntPtr SunnyContext, bool open, bool StopNet);
        public static api_ProcessALLName Sunny_ProcessALLName = (api_ProcessALLName)Marshal.GetDelegateForFunctionPointer(GetFunction("ProcessALLName"), typeof(api_ProcessALLName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetCommonName(IntPtr Context);
        public static api_GetCommonName Sunny_GetCommonName = (api_GetCommonName)Marshal.GetDelegateForFunctionPointer(GetFunction("GetCommonName"), typeof(api_GetCommonName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_ExportP12(IntPtr Context, IntPtr path, IntPtr pass);
        public static api_ExportP12 Sunny_ExportP12 = (api_ExportP12)Marshal.GetDelegateForFunctionPointer(GetFunction("ExportP12"), typeof(api_ExportP12));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ExportPub(IntPtr Context);
        public static api_ExportPub Sunny_ExportPub = (api_ExportPub)Marshal.GetDelegateForFunctionPointer(GetFunction("ExportPub"), typeof(api_ExportPub));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ExportKEY(IntPtr Context);
        public static api_ExportKEY Sunny_ExportKEY = (api_ExportKEY)Marshal.GetDelegateForFunctionPointer(GetFunction("ExportKEY"), typeof(api_ExportKEY));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_ExportCA(IntPtr Context);
        public static api_ExportCA Sunny_ExportCA = (api_ExportCA)Marshal.GetDelegateForFunctionPointer(GetFunction("ExportCA"), typeof(api_ExportCA));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_CreateCA(IntPtr Context, IntPtr Country, IntPtr Organization, IntPtr OrganizationalUnit, IntPtr Province, IntPtr CommonName, IntPtr Locality, IntPtr bits, IntPtr NotAfter);
        public static api_CreateCA Sunny_CreateCA = (api_CreateCA)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateCA"), typeof(api_CreateCA));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_AddClientAuth(IntPtr Context, IntPtr val);
        public static api_AddClientAuth Sunny_AddClientAuth = (api_AddClientAuth)Marshal.GetDelegateForFunctionPointer(GetFunction("AddClientAuth"), typeof(api_AddClientAuth));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_AddCertPoolText(IntPtr Context, IntPtr cer);
        public static api_AddCertPoolText Sunny_AddCertPoolText = (api_AddCertPoolText)Marshal.GetDelegateForFunctionPointer(GetFunction("AddCertPoolText"), typeof(api_AddCertPoolText));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_AddCertPoolPath(IntPtr Context, IntPtr cer);
        public static api_AddCertPoolPath Sunny_AddCertPoolPath = (api_AddCertPoolPath)Marshal.GetDelegateForFunctionPointer(GetFunction("AddCertPoolPath"), typeof(api_AddCertPoolPath));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetServerName(IntPtr Context);
        public static api_GetServerName Sunny_GetServerName = (api_GetServerName)Marshal.GetDelegateForFunctionPointer(GetFunction("GetServerName"), typeof(api_GetServerName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetServerName(IntPtr Context, IntPtr name);
        public static api_SetServerName Sunny_SetServerName = (api_SetServerName)Marshal.GetDelegateForFunctionPointer(GetFunction("SetServerName"), typeof(api_SetServerName));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetInsecureSkipVerify(IntPtr Context, bool b);
        public static api_SetInsecureSkipVerify Sunny_SetInsecureSkipVerify = (api_SetInsecureSkipVerify)Marshal.GetDelegateForFunctionPointer(GetFunction("SetInsecureSkipVerify"), typeof(api_SetInsecureSkipVerify));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_LoadX509Certificate(IntPtr Context, IntPtr Host, IntPtr CA, IntPtr KEY);
        public static api_LoadX509Certificate Sunny_LoadX509Certificate = (api_LoadX509Certificate)Marshal.GetDelegateForFunctionPointer(GetFunction("LoadX509Certificate"), typeof(api_LoadX509Certificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_LoadX509KeyPair(IntPtr Context, IntPtr CaPath, IntPtr KeyPath);
        public static api_LoadX509KeyPair Sunny_LoadX509KeyPair = (api_LoadX509KeyPair)Marshal.GetDelegateForFunctionPointer(GetFunction("LoadX509KeyPair"), typeof(api_LoadX509KeyPair));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_LoadP12Certificate(IntPtr Context, IntPtr Name, IntPtr Password);
        public static api_LoadP12Certificate Sunny_LoadP12Certificate = (api_LoadP12Certificate)Marshal.GetDelegateForFunctionPointer(GetFunction("LoadP12Certificate"), typeof(api_LoadP12Certificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveCertificate(IntPtr Context);
        public static api_RemoveCertificate Sunny_RemoveCertificate = (api_RemoveCertificate)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveCertificate"), typeof(api_RemoveCertificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateCertificate();
        public static api_CreateCertificate Sunny_CreateCertificate = (api_CreateCertificate)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateCertificate"), typeof(api_CreateCertificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysWriteStr(IntPtr KeysHandle, IntPtr name, IntPtr val, IntPtr len);
        public static api_KeysWriteStr Sunny_KeysWriteStr = (api_KeysWriteStr)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysWriteStr"), typeof(api_KeysWriteStr));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_KeysGetJson(IntPtr KeysHandle);
        public static api_KeysGetJson Sunny_KeysGetJson = (api_KeysGetJson)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysGetJson"), typeof(api_KeysGetJson));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_KeysGetCount(IntPtr KeysHandle);
        public static api_KeysGetCount Sunny_KeysGetCount = (api_KeysGetCount)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysGetCount"), typeof(api_KeysGetCount));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysEmpty(IntPtr KeysHandle);
        public static api_KeysEmpty Sunny_KeysEmpty = (api_KeysEmpty)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysEmpty"), typeof(api_KeysEmpty));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_KeysReadInt(IntPtr KeysHandle, IntPtr name);
        public static api_KeysReadInt Sunny_KeysReadInt = (api_KeysReadInt)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysReadInt"), typeof(api_KeysReadInt));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysWriteInt(IntPtr KeysHandle, IntPtr name, IntPtr val);
        public static api_KeysWriteInt Sunny_KeysWriteInt = (api_KeysWriteInt)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysWriteInt"), typeof(api_KeysWriteInt));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long api_KeysReadLong(IntPtr KeysHandle, IntPtr name);
        public static api_KeysReadLong Sunny_KeysReadLong = (api_KeysReadLong)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysReadLong"), typeof(api_KeysReadLong));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysWriteLong(IntPtr KeysHandle, IntPtr name, Int64 val);
        public static api_KeysWriteLong Sunny_KeysWriteLong = (api_KeysWriteLong)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysWriteLong"), typeof(api_KeysWriteLong));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Double api_KeysReadFloat(IntPtr KeysHandle, IntPtr name);
        public static api_KeysReadFloat Sunny_KeysReadFloat = (api_KeysReadFloat)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysReadFloat"), typeof(api_KeysReadFloat));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysWriteFloat(IntPtr KeysHandle, IntPtr name, Double val);
        public static api_KeysWriteFloat Sunny_KeysWriteFloat = (api_KeysWriteFloat)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysWriteFloat"), typeof(api_KeysWriteFloat));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysWrite(IntPtr KeysHandle, IntPtr name, IntPtr val, IntPtr length);
        public static api_KeysWrite Sunny_KeysWrite = (api_KeysWrite)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysWrite"), typeof(api_KeysWrite));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_KeysRead(IntPtr KeysHandle, IntPtr name);
        public static api_KeysRead Sunny_KeysRead = (api_KeysRead)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysRead"), typeof(api_KeysRead));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_KeysDelete(IntPtr KeysHandle, IntPtr name);
        public static api_KeysDelete Sunny_KeysDelete = (api_KeysDelete)Marshal.GetDelegateForFunctionPointer(GetFunction("KeysDelete"), typeof(api_KeysDelete));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveKeys(IntPtr KeysHandle);
        public static api_RemoveKeys Sunny_RemoveKeys = (api_RemoveKeys)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveKeys"), typeof(api_RemoveKeys));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateKeys();
        public static api_CreateKeys Sunny_CreateKeys = (api_CreateKeys)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateKeys"), typeof(api_CreateKeys));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_HTTPSetRedirect(IntPtr Context, bool Redirect);
        public static api_HTTPSetRedirect Sunny_HTTPSetRedirect = (api_HTTPSetRedirect)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetRedirect"), typeof(api_HTTPSetRedirect));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_HTTPGetCode(IntPtr Context);
        public static api_HTTPGetCode Sunny_HTTPGetCode = (api_HTTPGetCode)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPGetCode"), typeof(api_HTTPGetCode));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_HTTPGetBody(IntPtr Context);
        public static api_HTTPGetBody Sunny_HTTPGetBody = (api_HTTPGetBody)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPGetBody"), typeof(api_HTTPGetBody));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_HTTPGetHeads(IntPtr Context);
        public static api_HTTPGetHeads Sunny_HTTPGetHeads = (api_HTTPGetHeads)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPGetHeads"), typeof(api_HTTPGetHeads));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_HTTPGetBodyLen(IntPtr Context);
        public static api_HTTPGetBodyLen Sunny_HTTPGetBodyLen = (api_HTTPGetBodyLen)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPGetBodyLen"), typeof(api_HTTPGetBodyLen));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPSendBin(IntPtr Context, IntPtr body, IntPtr bodyLength);
        public static api_HTTPSendBin Sunny_HTTPSendBin = (api_HTTPSendBin)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSendBin"), typeof(api_HTTPSendBin));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPSetTimeouts(IntPtr Context, IntPtr t1);
        public static api_HTTPSetTimeouts Sunny_HTTPSetTimeouts = (api_HTTPSetTimeouts)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetTimeouts"), typeof(api_HTTPSetTimeouts));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPSetProxyIP(IntPtr Context, IntPtr ProxyURL);
        public static api_HTTPSetProxyIP Sunny_HTTPSetProxyIP = (api_HTTPSetProxyIP)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetProxyIP"), typeof(api_HTTPSetProxyIP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPSetHeader(IntPtr Context, IntPtr name, IntPtr value);
        public static api_HTTPSetHeader Sunny_HTTPSetHeader = (api_HTTPSetHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetHeader"), typeof(api_HTTPSetHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPSetServerIP(IntPtr Context, IntPtr ip);
        public static api_HTTPSetServerIP Sunny_HTTPSetServerIP = (api_HTTPSetServerIP)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetServerIP"), typeof(api_HTTPSetServerIP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_HTTPOpen(IntPtr Context, IntPtr Method, IntPtr URL);
        public static api_HTTPOpen Sunny_HTTPOpen = (api_HTTPOpen)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPOpen"), typeof(api_HTTPOpen));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveHTTPClient(IntPtr Context);
        public static api_RemoveHTTPClient Sunny_RemoveHTTPClient = (api_RemoveHTTPClient)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveHTTPClient"), typeof(api_RemoveHTTPClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateHTTPClient();
        public static api_CreateHTTPClient Sunny_CreateHTTPClient = (api_CreateHTTPClient)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateHTTPClient"), typeof(api_CreateHTTPClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_JsonToPB(IntPtr bin, IntPtr binLen);
        public static api_JsonToPB Sunny_JsonToPB = (api_JsonToPB)Marshal.GetDelegateForFunctionPointer(GetFunction("JsonToPB"), typeof(api_JsonToPB));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_PbToJson(IntPtr bin, IntPtr binLen);
        public static api_PbToJson Sunny_PbToJson = (api_PbToJson)Marshal.GetDelegateForFunctionPointer(GetFunction("PbToJson"), typeof(api_PbToJson));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_QueuePull(IntPtr name);
        public static api_QueuePull Sunny_QueuePull = (api_QueuePull)Marshal.GetDelegateForFunctionPointer(GetFunction("QueuePull"), typeof(api_QueuePull));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_QueuePush(IntPtr name, IntPtr val, IntPtr valLen);
        public static api_QueuePush Sunny_QueuePush = (api_QueuePush)Marshal.GetDelegateForFunctionPointer(GetFunction("QueuePush"), typeof(api_QueuePush));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_QueueLength(IntPtr name);
        public static api_QueueLength Sunny_QueueLength = (api_QueueLength)Marshal.GetDelegateForFunctionPointer(GetFunction("QueueLength"), typeof(api_QueueLength));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_QueueRelease(IntPtr name);
        public static api_QueueRelease Sunny_QueueRelease = (api_QueueRelease)Marshal.GetDelegateForFunctionPointer(GetFunction("QueueRelease"), typeof(api_QueueRelease));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_QueueIsEmpty(IntPtr name);
        public static api_QueueIsEmpty Sunny_QueueIsEmpty = (api_QueueIsEmpty)Marshal.GetDelegateForFunctionPointer(GetFunction("QueueIsEmpty"), typeof(api_QueueIsEmpty));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_CreateQueue(IntPtr name);
        public static api_CreateQueue Sunny_CreateQueue = (api_CreateQueue)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateQueue"), typeof(api_CreateQueue));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SocketClientWrite(IntPtr Context, IntPtr OutTimes, IntPtr val, IntPtr valLen);
        public static api_SocketClientWrite Sunny_SocketClientWrite = (api_SocketClientWrite)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientWrite"), typeof(api_SocketClientWrite));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SocketClientClose(IntPtr Context);
        public static api_SocketClientClose Sunny_SocketClientClose = (api_SocketClientClose)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientClose"), typeof(api_SocketClientClose));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SocketClientReceive(IntPtr Context, IntPtr OutTimes);
        public static api_SocketClientReceive Sunny_SocketClientReceive = (api_SocketClientReceive)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientReceive"), typeof(api_SocketClientReceive));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SocketClientDial(IntPtr Context, IntPtr addr, IntPtr call, bool isTls, bool synchronous, IntPtr ProxyUrl, IntPtr CertificateConText, IntPtr timeOut, IntPtr RouterIP);
        public static api_SocketClientDial Sunny_SocketClientDial = (api_SocketClientDial)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientDial"), typeof(api_SocketClientDial));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SocketClientSetBufferSize(IntPtr Context, IntPtr BufferSize);
        public static api_SocketClientSetBufferSize Sunny_SocketClientSetBufferSize = (api_SocketClientSetBufferSize)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientSetBufferSize"), typeof(api_SocketClientSetBufferSize));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SocketClientGetErr(IntPtr Context);
        public static api_SocketClientGetErr Sunny_SocketClientGetErr = (api_SocketClientGetErr)Marshal.GetDelegateForFunctionPointer(GetFunction("SocketClientGetErr"), typeof(api_SocketClientGetErr));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveSocketClient(IntPtr Context);
        public static api_RemoveSocketClient Sunny_RemoveSocketClient = (api_RemoveSocketClient)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveSocketClient"), typeof(api_RemoveSocketClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateSocketClient();
        public static api_CreateSocketClient Sunny_CreateSocketClient = (api_CreateSocketClient)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateSocketClient"), typeof(api_CreateSocketClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_WebsocketClientReceive(IntPtr Context, IntPtr OutTimes);
        public static api_WebsocketClientReceive Sunny_WebsocketClientReceive = (api_WebsocketClientReceive)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketClientReceive"), typeof(api_WebsocketClientReceive));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_WebsocketReadWrite(IntPtr Context, IntPtr val, IntPtr valLen, IntPtr messageType);
        public static api_WebsocketReadWrite Sunny_WebsocketReadWrite = (api_WebsocketReadWrite)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketReadWrite"), typeof(api_WebsocketReadWrite));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_WebsocketClose(IntPtr Context);
        public static api_WebsocketClose Sunny_WebsocketClose = (api_WebsocketClose)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketClose"), typeof(api_WebsocketClose));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_WebsocketDial(IntPtr Context, IntPtr URL, IntPtr Heads, IntPtr call, bool synchronous, IntPtr ProxyUrl, IntPtr CertificateConText, IntPtr outTime, IntPtr RouterIP);
        public static api_WebsocketDial Sunny_WebsocketDial = (api_WebsocketDial)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketDial"), typeof(api_WebsocketDial));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_WebsocketHeartbeat(IntPtr Context, IntPtr HeartbeatTime, IntPtr call);
        public static api_WebsocketHeartbeat Sunny_WebsocketHeartbeat = (api_WebsocketHeartbeat)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketHeartbeat"), typeof(api_WebsocketHeartbeat));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_WebsocketGetErr(IntPtr Context);
        public static api_WebsocketGetErr Sunny_WebsocketGetErr = (api_WebsocketGetErr)Marshal.GetDelegateForFunctionPointer(GetFunction("WebsocketGetErr"), typeof(api_WebsocketGetErr));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveWebsocket(IntPtr Context);
        public static api_RemoveWebsocket Sunny_RemoveWebsocket = (api_RemoveWebsocket)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveWebsocket"), typeof(api_RemoveWebsocket));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateWebsocket();
        public static api_CreateWebsocket Sunny_CreateWebsocket = (api_CreateWebsocket)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateWebsocket"), typeof(api_CreateWebsocket));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_AddHttpCertificate(IntPtr host, IntPtr CertManagerId, IntPtr Rules);
        public static api_AddHttpCertificate Sunny_AddHttpCertificate = (api_AddHttpCertificate)Marshal.GetDelegateForFunctionPointer(GetFunction("AddHttpCertificate"), typeof(api_AddHttpCertificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_DelHttpCertificate(IntPtr host);
        public static api_DelHttpCertificate Sunny_DelHttpCertificate = (api_DelHttpCertificate)Marshal.GetDelegateForFunctionPointer(GetFunction("DelHttpCertificate"), typeof(api_DelHttpCertificate));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisSubscribe(IntPtr Context, IntPtr scribe, IntPtr call, bool nc);
        public static api_RedisSubscribe Sunny_RedisSubscribe = (api_RedisSubscribe)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisSubscribe"), typeof(api_RedisSubscribe));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisDelete(IntPtr Context, IntPtr key);
        public static api_RedisDelete Sunny_RedisDelete = (api_RedisDelete)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisDelete"), typeof(api_RedisDelete));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RedisFlushDB(IntPtr Context);
        public static api_RedisFlushDB Sunny_RedisFlushDB = (api_RedisFlushDB)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisFlushDB"), typeof(api_RedisFlushDB));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RedisFlushAll(IntPtr Context);
        public static api_RedisFlushAll Sunny_RedisFlushAll = (api_RedisFlushAll)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisFlushAll"), typeof(api_RedisFlushAll));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RedisClose(IntPtr Context);
        public static api_RedisClose Sunny_RedisClose = (api_RedisClose)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisClose"), typeof(api_RedisClose));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Int64 api_RedisGetInt(IntPtr Context, IntPtr key);
        public static api_RedisGetInt Sunny_RedisGetInt = (api_RedisGetInt)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisGetInt"), typeof(api_RedisGetInt));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_RedisGetKeys(IntPtr Context, IntPtr key);
        public static api_RedisGetKeys Sunny_RedisGetKeys = (api_RedisGetKeys)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisGetKeys"), typeof(api_RedisGetKeys));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_RedisDo(IntPtr Context, IntPtr args, IntPtr error);
        public static api_RedisDo Sunny_RedisDo = (api_RedisDo)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisDo"), typeof(api_RedisDo));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_RedisGetStr(IntPtr Context, IntPtr key);
        public static api_RedisGetStr Sunny_RedisGetStr = (api_RedisGetStr)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisGetStr"), typeof(api_RedisGetStr));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_RedisGetBytes(IntPtr Context, IntPtr key);
        public static api_RedisGetBytes Sunny_RedisGetBytes = (api_RedisGetBytes)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisGetBytes"), typeof(api_RedisGetBytes));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisExists(IntPtr Context, IntPtr key);
        public static api_RedisExists Sunny_RedisExists = (api_RedisExists)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisExists"), typeof(api_RedisExists));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisSetNx(IntPtr Context, IntPtr key, IntPtr val, IntPtr expr);
        public static api_RedisSetNx Sunny_RedisSetNx = (api_RedisSetNx)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisSetNx"), typeof(api_RedisSetNx));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisSet(IntPtr Context, IntPtr key, IntPtr val, IntPtr expr);
        public static api_RedisSet Sunny_RedisSet = (api_RedisSet)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisSet"), typeof(api_RedisSet));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisSetBytes(IntPtr Context, IntPtr key, IntPtr val, IntPtr valLen, IntPtr expr);
        public static api_RedisSetBytes Sunny_RedisSetBytes = (api_RedisSetBytes)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisSetBytes"), typeof(api_RedisSetBytes));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RedisDial(IntPtr Context, IntPtr host, IntPtr pass, IntPtr db, IntPtr PoolSize, IntPtr MinIdleCons, IntPtr DialTimeout, IntPtr ReadTimeout, IntPtr WriteTimeout, IntPtr PoolTimeout, IntPtr IdleCheckFrequency, IntPtr IdleTimeout, IntPtr error);
        public static api_RedisDial Sunny_RedisDial = (api_RedisDial)Marshal.GetDelegateForFunctionPointer(GetFunction("RedisDial"), typeof(api_RedisDial));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_RemoveRedis(IntPtr Context);
        public static api_RemoveRedis Sunny_RemoveRedis = (api_RemoveRedis)Marshal.GetDelegateForFunctionPointer(GetFunction("RemoveRedis"), typeof(api_RemoveRedis));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_CreateRedis();
        public static api_CreateRedis Sunny_CreateRedis = (api_CreateRedis)Marshal.GetDelegateForFunctionPointer(GetFunction("CreateRedis"), typeof(api_CreateRedis));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetUdpData(IntPtr MessageId);
        public static api_GetUdpData Sunny_GetUdpData = (api_GetUdpData)Marshal.GetDelegateForFunctionPointer(GetFunction("GetUdpData"), typeof(api_GetUdpData));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetUdpData(IntPtr MessageId, IntPtr val, IntPtr valLen);
        public static api_SetUdpData Sunny_SetUdpData = (api_SetUdpData)Marshal.GetDelegateForFunctionPointer(GetFunction("SetUdpData"), typeof(api_SetUdpData));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_UdpSendToClient(IntPtr MessageId, IntPtr val, IntPtr valLen);
        public static api_UdpSendToClient Sunny_UdpSendToClient = (api_UdpSendToClient)Marshal.GetDelegateForFunctionPointer(GetFunction("UdpSendToClient"), typeof(api_UdpSendToClient));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_UdpSendToServer(IntPtr MessageId, IntPtr val, IntPtr valLen);
        public static api_UdpSendToServer Sunny_UdpSendToServer = (api_UdpSendToServer)Marshal.GetDelegateForFunctionPointer(GetFunction("UdpSendToServer"), typeof(api_UdpSendToServer));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_Free(IntPtr Ptr);
        public static api_Free Sunny_Free = (api_Free)Marshal.GetDelegateForFunctionPointer(GetFunction("Free"), typeof(api_Free));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_GetSunnyVersion();
        public static api_GetSunnyVersion Sunny_GetSunnyVersion = (api_GetSunnyVersion)Marshal.GetDelegateForFunctionPointer(GetFunction("GetSunnyVersion"), typeof(api_GetSunnyVersion));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SunnyNetGetSocket5User(IntPtr WeiYiId);
        public static api_SunnyNetGetSocket5User Sunny_SunnyNetGetSocket5User = (api_SunnyNetGetSocket5User)Marshal.GetDelegateForFunctionPointer(GetFunction("SunnyNetGetSocket5User"), typeof(api_SunnyNetGetSocket5User));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetCipherSuites(IntPtr Context, IntPtr val);
        public static api_SetCipherSuites Sunny_SetCipherSuites = (api_SetCipherSuites)Marshal.GetDelegateForFunctionPointer(GetFunction("SetCipherSuites"), typeof(api_SetCipherSuites));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_HTTPSetRandomTLS(IntPtr Context, bool RandomTLS);
        public static api_HTTPSetRandomTLS Sunny_HTTPSetRandomTLS = (api_HTTPSetRandomTLS)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetRandomTLS"), typeof(api_HTTPSetRandomTLS));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_HTTPGetRequestHeader(IntPtr Context);
        public static api_HTTPGetRequestHeader Sunny_HTTPGetRequestHeader = (api_HTTPGetRequestHeader)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPGetRequestHeader"), typeof(api_HTTPGetRequestHeader));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_HTTPSetH2Config(IntPtr Context, IntPtr config);
        public static api_HTTPSetH2Config Sunny_HTTPSetH2Config = (api_HTTPSetH2Config)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetH2Config"), typeof(api_HTTPSetH2Config));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetDnsServer(IntPtr dns);
        public static api_SetDnsServer Sunny_SetDnsServer = (api_SetDnsServer)Marshal.GetDelegateForFunctionPointer(GetFunction("SetDnsServer"), typeof(api_SetDnsServer));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetRandomTLS(IntPtr Context, bool RandomTLS);
        public static api_SetRandomTLS Sunny_SetRandomTLS = (api_SetRandomTLS)Marshal.GetDelegateForFunctionPointer(GetFunction("SetRandomTLS"), typeof(api_SetRandomTLS));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_DisableUDP(IntPtr Context, bool Disable);
        public static api_DisableUDP Sunny_DisableUDP = (api_DisableUDP)Marshal.GetDelegateForFunctionPointer(GetFunction("DisableUDP"), typeof(api_DisableUDP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_DisableTCP(IntPtr Context, bool Disable);
        public static api_DisableTCP Sunny_DisableTCP = (api_DisableTCP)Marshal.GetDelegateForFunctionPointer(GetFunction("DisableTCP"), typeof(api_DisableTCP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SetScriptPage(IntPtr Context, IntPtr config);
        public static api_SetScriptPage Sunny_SetScriptPage = (api_SetScriptPage)Marshal.GetDelegateForFunctionPointer(GetFunction("SetScriptPage"), typeof(api_SetScriptPage));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr api_SetScriptCode(IntPtr Context, IntPtr config);
        public static api_SetScriptCode Sunny_SetScriptCode = (api_SetScriptCode)Marshal.GetDelegateForFunctionPointer(GetFunction("SetScriptCode"), typeof(api_SetScriptCode));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetHTTPRequestMaxUpdateLength(IntPtr Context, long MaxLength);
        public static api_SetHTTPRequestMaxUpdateLength Sunny_SetHTTPRequestMaxUpdateLength = (api_SetHTTPRequestMaxUpdateLength)Marshal.GetDelegateForFunctionPointer(GetFunction("SetHTTPRequestMaxUpdateLength"), typeof(api_SetHTTPRequestMaxUpdateLength));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void api_SetScriptCall(IntPtr Context, IntPtr ScriptlogCallback, IntPtr ScriptsaveCallback);
        public static api_SetScriptCall Sunny_SetScriptCall = (api_SetScriptCall)Marshal.GetDelegateForFunctionPointer(GetFunction("SetScriptCall"), typeof(api_SetScriptCall));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_SetOutRouterIP(IntPtr Context, IntPtr ip);
        public static api_SetOutRouterIP Sunny_SetOutRouterIP = (api_SetOutRouterIP)Marshal.GetDelegateForFunctionPointer(GetFunction("SetOutRouterIP"), typeof(api_SetOutRouterIP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_RequestSetOutRouterIP(IntPtr MessageId, IntPtr ip);
        public static api_RequestSetOutRouterIP Sunny_RequestSetOutRouterIP = (api_RequestSetOutRouterIP)Marshal.GetDelegateForFunctionPointer(GetFunction("RequestSetOutRouterIP"), typeof(api_RequestSetOutRouterIP));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool api_HTTPSetOutRouterIP(IntPtr Context, IntPtr ip);
        public static api_HTTPSetOutRouterIP Sunny_HTTPSetOutRouterIP = (api_HTTPSetOutRouterIP)Marshal.GetDelegateForFunctionPointer(GetFunction("HTTPSetOutRouterIP"), typeof(api_HTTPSetOutRouterIP));

    }
}
