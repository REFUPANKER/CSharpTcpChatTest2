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
            writer.WriteLine("$add_client");
            Thread th1 = new Thread(() =>
            {
                try
                {
                    while (inputLine != "$exit")
                    {
                        response = reader.ReadLine();
                        if (response != null)
                        {
                            string[] respargs = response.Split(" ");
                            switch (respargs[0])
                            {
                                case "$receive":
                                    cwl("Receive file command sent");
                                    string filepath = "./files/output/output" + EncDecController.randomId(10) + ".format";
                                    response = reader.ReadLine();
                                    List<byte> filebytes = new List<byte>();
                                    string[] responsebytes = (response + "").Split(",");
                                    int perc = 0;
                                    cw("Receiving file ");
                                    for (int i = 0; i < responsebytes.Length; i++)
                                    {
                                        perc = ((i * 100) / responsebytes.Length) + 1;
                                        filebytes.Add(byte.Parse(responsebytes[i]));
                                        cw("%" + perc);
                                        for (int j = 0; j < perc.ToString().Length + 1; j++)
                                        {
                                            cw("\b");
                                        }
                                    }
                                    cwl("");
                                    File.WriteAllBytes(filepath, filebytes.ToArray());
                                    cwl("File received");
                                    break;
                            }
                        }
                    }
                }
                catch
                {
                    cwl("Disconnected");
                }
            });
            th1.Start();
            // th1.Join();
            while ((inputLine = Console.ReadLine() + "") != "$exit")
            {
                if (inputLine.StartsWith("$"))
                {
                    SendMessage(writer, inputLine, false);
                }
                else if (inputLine.StartsWith("<k>"))
                {
                    cwl("keyboard activated");
                    SendMessage(writer, "<k>", false);
                    ConsoleKeyInfo? kpress = null;
                    while (true)
                    {
                        kpress = Console.ReadKey();
                        if (kpress?.KeyChar == '/')
                        {
                            cwl("\ntype exit for close keyboard");
                            string kexit = Console.ReadLine() + "";
                            if (kexit.StartsWith("exit"))
                            {
                                SendMessage(writer, "/exit", false);
                                break;
                            }
                            cwl("no command selected");
                        }
                        else
                        {
                            SendMessage(writer, Convert.ToInt32(kpress?.KeyChar) + "", false);
                        }
                    }
                    cwl("keyboard deactivated");
                }
                else
                {
                    SendMessage(writer, inputLine, false);
                }
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
        catch (Exception)
        {
            Console.WriteLine("Disconnected");
        }
        finally
        {
            client.Close();
            cwl("client ended");
        }
    }
    public bool ConvertibleToInt(string input)
    {
        try
        {
            Convert.ToInt32(input);
            return true;
        }
        catch
        {
            return false;
        }
    }
    private void SendMessage(StreamWriter writer, string message, bool encrypt = true)
    {
        if (encrypt)
        {
            writer.WriteLine(EncDecController.EncryptToBinary(message));
        }
        else
        {
            writer.WriteLine(message);
        }

    }
}