using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BombPlane
{
    public static class Utility
    {
        public static string Receive(Socket socket) //接收客户端数据
        {
            try
            {
                byte[] buffer = new byte[1024];
                int len = socket.Receive(buffer);
                return Encoding.Unicode.GetString(buffer, 0, len);
            }
            catch (SocketException e) { throw new ReceiveExpection(e); }
        }

        public static void Send(Socket socket, string text) // 向客户端发送数据
        {
            try
            {
                byte[] buffer = Encoding.Unicode.GetBytes(text);    //将字符串转成字节格式发送
                socket.Send(buffer);  //调用Send()向客户端发送数据
            }
            catch (SocketException e) { throw new SendException(e); }
        }

        public static string Execute(string FileName) { return Execute(FileName, null); }

        public static string Execute(string FileName, string? arguments)
        {
            Process process = new();
            process.StartInfo.FileName = FileName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            if (arguments != null)
                process.StartInfo.Arguments = arguments;

            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();

            process.Kill();
            return output;
        }
    }
}
