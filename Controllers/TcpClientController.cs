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
            string? response;
            while ((inputLine = Console.ReadLine() + "") != "$exit")
            {
                SendMessage(writer, inputLine);
                response = reader.ReadLine();
                if (response != null)
                {
                    cwl("[Server]" + response);
                }
            }
            SendMessage(writer, "Client Disconnected");
            reader.ReadLine();
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