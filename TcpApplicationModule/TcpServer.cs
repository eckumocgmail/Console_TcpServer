using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using static InputApplicationProgram;

public class TcpServer
{
   
    /// <summary>
    /// 
    /// </summary>
    public static void Start(int port, Func<string, string> handle)
    {
        Info($"127.0.0.1:{port}");

        System.Net.Sockets.TcpListener server = null;
        try
        {                                                         
            server = new System.Net.Sockets.TcpListener(IPAddress.Parse("127.0.0.1"), port);                    
            server.Start();
                    
            Byte[] bytes = new Byte[256];
                              
            while (true)
            {
                System.Net.Sockets.TcpClient client = server.AcceptTcpClient();
                
                string request = null;                        
                NetworkStream stream = client.GetStream();

                int readed;                        
                while ((readed= stream.ReadByte()) != -1)                                            
                    request += System.Text.Encoding.ASCII.GetString(new byte[] { (byte)readed }, 0, 1);                                    
                Info($"< {request}");

                string response = handle(request);
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(request);
                stream.Write(msg, 0, msg.Length);
                Info($"> {response}");
     

                client.Close();
            }
        }
        catch (SocketException e)
        {
            Error("SocketException: {0}", e);
        }
        finally
        {                    
            server.Stop();
        }               
    }

    
}
 