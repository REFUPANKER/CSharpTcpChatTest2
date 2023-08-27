using System.IO;
using System.Net;
using System.Net.Sockets;

public class TcpClientController : ConsoleController
{
    public void Run(string? ipAdress = null)
    {

        string serverAddress;
        if (ipAdress != null)
        {
            serverAddress = ipAdress;
        }
        else
        {
            cw("server ip :");
            serverAddress = Console.ReadLine() + "";
        }
        int port = 9090;
        TcpClient client = new TcpClient(serverAddress, port);
        StreamReader reader = new StreamReader(client.GetStream());
        StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
        try
        {
            cwl($"connected to server {serverAddress}");
            cwl("type $exit for exit");
            string? inputLine = "";
            string? response = "";
            writer.WriteLine("add_client");
            Thread th1 = new Thread(() =>
            {
                while (inputLine!="$exit")
                {
                    cwl(((response = reader.ReadLine()) != null) ? response : "");
                }

            });
            th1.Start();
            // th1.Join();
            while ((inputLine = Console.ReadLine() + "") != "$exit")
            {
                SendMessage(writer, inputLine);

                // Thread th2=new Thread(() =>
                // {
                //     cwl((((response = reader.ReadLine()) != null && string.IsNullOrEmpty(response) == false) ? "\b\b\b\b" + response : ""));
                // });
                // th2.Start();
            }
            writer.WriteLine("$exit");
            writer.WriteLine("remove_client");
            cwl("Press Enter key to continue");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            client.Close();
            cwl("client ended");
        }
    }

    private void SendMessage(StreamWriter writer, string message)
    {
        writer.WriteLine(EncDecController.EncryptToBinary(message));
    }
}