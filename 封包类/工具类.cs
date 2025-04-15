using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HTTP.封包类
{
    public class 工具类
    {

        #region 查找一个字节数组是否包含另一个字节数组

        /// <summary>
        /// 查找一个字节数组是否包含另一个字节数组
        /// </summary>
        /// <param name="source">源字节数组</param>
        /// <param name="search">要查找的字节数组</param>
        /// <returns>是否包含</returns>
        public static bool IsContains(byte[] source, byte[] search)
        {
            List<byte> sourceList = source.ToList();
            int index = 0;
            return IsContainBytes(sourceList, search, ref index);
        }

        /// <summary>
        /// 递归查找字节数组
        /// </summary>
        private static bool IsContainBytes(
            List<byte> sourceList,
            byte[] search,
            ref int index)
        {
            // 查找第一个元素的索引
            index = sourceList.IndexOf(search[0], index);

            if (index < 0)
                return false;

            if (search.Length == 1)
                return true;

            bool isContain = true;

            // 验证后续元素是否匹配
            for (int i = 1; i < search.Length; i++)
            {
                if (sourceList.Count <= index + i ||
                    sourceList[index + i] != search[i])
                {
                    isContain = false;
                    break;
                }
            }

            if (!isContain)
            {
                index += 1;
                return IsContainBytes(sourceList, search, ref index);
            }
            return true;
        }

        #endregion

        /// <summary>   
        /// 根据数据的长度申请非托管空间   
        /// </summary>   
        /// <param name="strData">要申请非托管空间的数据</param>   
        /// <returns>指向非拖管空间的指针</returns>
        public static IntPtr StringToIntptr(string strData, string Encoding = "UTF-8")
        {
            //先将字符串转化成字节方式   
            Byte[] btData = System.Text.Encoding.GetEncoding(Encoding).GetBytes(strData);
            return BytesToIntptr(btData);
        }
        public static string BytesToStr(byte[] byteArray, string Encoding = "UTF-8")
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return "";
            }
            return System.Text.Encoding.GetEncoding(Encoding).GetString(byteArray);
        }

        public static string BytesToHexStr(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
            {
                return "";
            }
            return BitConverter.ToString(byteArray).Replace("-", " "); ;
        }

        public static byte[] HexStrToBytes(string hexStr)
        {
            if (string.IsNullOrEmpty(hexStr))
                return new byte[0];

            // 移除所有空格并验证偶数长度
            string cleaned = hexStr.Replace(" ", "");
            if (cleaned.Length % 2 != 0)
                throw new ArgumentException("非法字符，转换失败");

            // 每两个字符解析为一个字节
            byte[] bytes = new byte[cleaned.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(cleaned.Substring(i * 2, 2), 16);
            }

            return bytes;
        }

        public static byte[] StrToBytes(string str, string Encoding = "UTF-8")
        {
            return System.Text.Encoding.GetEncoding(Encoding).GetBytes(str);
        }
        /// <summary>   
        /// 将指针转换为字节数组。   
        /// </summary>   
        /// <param name="data">要操作的指针。</param>   
        /// <param name="n">数据长度，默认为 -1，表示读取直到遇到零字节。</param>   
        /// <param name="sikp">偏移量，默认为 0。</param>   
        /// <returns>转换后的字节数组。</returns>
        public static byte[] PtrToBytes(IntPtr data, long n = -1, int sikp = 0)
        {
            // 创建字节数组以存储结果 
            if (data.ToInt64() != 0)
            {
                if (n == -1)
                {
                    List<byte> list = new List<byte>();
                    var i = sikp;
                    while (true)
                    {
                        byte b = (byte)Marshal.ReadByte(data, i);
                        if (b == 0)
                        {
                            return list.ToArray();
                        }
                        i++;
                        list.Add(b);
                    }
                }
                else
                {
                    byte[] datas = new byte[n < 0 ? 1024 : n]; // 初始化为一个默认大小
                    for (var i = 0; i < n; i++)
                    {
                        datas[i] = (byte)Marshal.ReadByte(data, i + sikp);
                    }
                    return datas;
                }
            }
            return new byte[] { };
        }

        /// <summary>   
        /// 指针到字符串
        /// </summary>   
        /// <param name="data">要操作的指针</param>    
        /// <returns>指针到整数</returns>
        public static string PtrToString(IntPtr data, string Encoding = "UTF-8")
        {
            byte[] byteArray = new byte[0];
            if (data.ToInt64() != 0)
            {
                byte[] t = new byte[1];
                int i = 0;
                while (true)
                {
                    byte p = (byte)Marshal.ReadByte(data, i);
                    if (p != 0)
                    {
                        t[0] = p;
                        byteArray = byteArray.Concat(t).ToArray();
                        i++;
                        continue;
                    }
                    break;
                }
            }
            return System.Text.Encoding.GetEncoding(Encoding).GetString(byteArray);
        }
        /// <summary>   
        /// 指针到整数   
        /// </summary>   
        /// <param name="p">要操作的指针</param>   
        /// <returns>指针到整数</returns>
        public static long PtrTolong(IntPtr p)
        {
            return Marshal.ReadInt64(p, 0);
        }
        /// <summary>   
        /// 释放指针，不是C#请求的内存 不需要释放  
        /// </summary>   
        /// <param name="p">要释放指针</param>   
        /// <returns>释放指针，不是C#请求的内存 不需要释放</returns>
        public static void PtrFree(IntPtr p)
        {
            Marshal.FreeHGlobal(p);
        }
        /// <summary>   
        /// 根据数据的长度申请非托管空间   
        /// </summary>   
        /// <param name="btData">要申请非托管空间的数据</param>   
        /// <returns>指向非拖管空间的指针</returns>
        public static IntPtr BytesToIntptr(byte[] btData)
        {

            //申请非拖管空间   
            IntPtr m_ptr = Marshal.AllocHGlobal(btData.Length + 1);

            //给非拖管空间清０    
            Byte[] btZero = new Byte[btData.Length + 1]; //因为C字符串以0结尾
            Marshal.Copy(btZero, 0, m_ptr, btZero.Length);

            //给指针指向的空间赋值   
            Marshal.Copy(btData, 0, m_ptr, btData.Length);

            return m_ptr;
        }

        /// <summary>   
        /// 根据长度申请非托管空间   
        /// </summary>   
        /// <param name="length">要申请非托管空间的大小</param>   
        /// <returns>指向非拖管空间的指针</returns>   
        public static IntPtr mallocIntptr(int length)
        {
            //申请非拖管空间   
            IntPtr m_ptr = Marshal.AllocHGlobal(length + 1);
            //给非拖管空间清0
            Byte[] btZero = new Byte[length + 1];
            Marshal.Copy(btZero, 0, m_ptr, btZero.Length);
            //给指针指向的空间赋值   
            Marshal.Copy(btZero, 0, m_ptr, length);
            return m_ptr;
        }
        /// <summary>
        /// ansi 转UTF8
        /// </summary>
        /// <param name="data"> GBK 字符串</param>
        /// <returns></returns>
        public static byte[] ANSI2utf8(string data)
        {
            Encoding ANSI = Encoding.GetEncoding(1252);
            byte[] ansiBytes = ANSI.GetBytes(data);
            byte[] utf8Bytes = Encoding.Convert(ANSI, Encoding.UTF8, ansiBytes);
            //String utf8String = Encoding.UTF8.GetString(utf8Bytes);
            return utf8Bytes;
        }

        public string GetSubstringBetweenTwoStrings(string source, string left, string right)
        {
            int start = source.IndexOf(left);
            if (start == -1) return null; // Left string is not found.
            start += left.Length; // Skip the left string.

            int end = source.IndexOf(right, start);
            if (end == -1) return null; // Right string is not found after left string.

            return source.Substring(start, end - start);
        }

    }
}
