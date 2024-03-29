using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSE.Core.Pipes
{
    public class NamedPipeServer
    {
        public NamedPipeServerStream pipeServer;
        //StreamReader sr;
        //StreamWriter sw;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="pipename"></param>
        /// <returns></returns>
        public NamedPipeServerStream CreatPipeServer(string pipename)
        {
            pipeServer = new NamedPipeServerStream(pipename, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.None);
            //sr = new StreamReader(pipeServer);
            //sw = new StreamWriter(pipeServer);
            return pipeServer;
        }


        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        public string Receive()
        {
            string str = "";
            try
            {
                str = pipeServer.ReadByte().ToString();//读取单字节

                //str = sr.ReadLine();                 //读取字符串
                //byte[] buffff = new byte[8];
                //pipeServer.Read(buffff, 0, 8);       //读取指定长度的字节数据
                //str = buffff[0].ToString ();

            }
            catch (Exception ex)
            {

            }
            return str;
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="command"></param>
        public void Send(byte command)
        {
            try
            {
                pipeServer.WriteByte(command);   //发送单字节
                pipeServer.Flush();

                //sw.WriteLine(command);              //发送字符串
                //sw.Flush();

                //byte[] buffff = new byte[2];
                //buffff[0] = command;
                //pipeServer.Write(buffff, 0, 2);   //发送指定长度的字节数据
                Console.WriteLine("Send: " + command);
            }
            catch (Exception ex)
            {

            }
        }
        
    }
    public class NamedPipeClient
    {
        public NamedPipeClientStream pipeClient;
        //StreamReader sr;
        //StreamWriter sw;

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="pipename"></param>
        /// <returns></returns>
        public NamedPipeClientStream CreatPipeClient(string pipename)
        {
            pipeClient = new NamedPipeClientStream(".", pipename);
            //sr = new StreamReader(pipeClient);
            //sw = new StreamWriter(pipeClient);
            return pipeClient;
        }


        /// <summary>
        /// 接收
        /// </summary>
        /// <returns></returns>
        public string Receive()
        {
            string str = "";
            try
            {
                str = pipeClient.ReadByte().ToString();

                //str = sr.ReadLine();

                //byte[] buffff = new byte[8];
                //pipeClient.Read(buffff, 0, 8);
                //str = buffff[0].ToString();
            }
            catch (Exception ex)
            {

            }
            return str;
        }


        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="command"></param>
        public void Send(byte command)
        {
            try
            {
                pipeClient.WriteByte(command);
                pipeClient.Flush();

                //sw.WriteLine(command);
                //sw.Flush();

                //byte[] buffff = new byte[2];
                //buffff[0] = command;
                //pipeClient.Write(buffff, 0, 2);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
