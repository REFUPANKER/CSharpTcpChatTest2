using System.IO;
using System.Net;
using System.Net.Sockets;

public class TcpServerController : ConsoleController
{
    private TcpClientController ctrlClient = new TcpClientController();

    private int port = 9090;

    public void Run()
    {
        cwl("o_O Server Controller created");
        StartToListen();
    }

    private void StartToListen()
    {
        cwl($"Ears opening...");
        Thread HearAndSay = new Thread(() =>
        {
            string ipAddress = GetServerIpAddress();
            cwl("^-^ i found your ip (IpV4) :" + ipAddress);
            TcpListener ears = new TcpListener(IPAddress.Parse(ipAddress), port);
            try
            {
                ears.Start();
                cwl($"Ears active! im listening : {port}");
                while (true)
                {
                    TcpClient client = ears.AcceptTcpClient();
                    cwl("Ears heard somebody join");
                    ManageClients(client);
                }
            }
            catch (Exception excp)
            {
                cwl("Ears heard wrong things x_x");
                cwl(excp.Message);
            }
            finally
            {
                ears.Stop();
                cwl("Ears disabled,Diving into silence -_-");
            }
        });
        HearAndSay.Start();
    }
    public string GetServerIpAddress()
    {
        foreach (IPAddress address in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }
        return "";
    }

    private void ManageClients(TcpClient client)
    {
        Thread cleintThread = new Thread(() =>
        {
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
            try
            {
                string? inputLine;
                while ((inputLine = reader.ReadLine()) != null)
                {
                    if (inputLine != null)
                    {
                        Console.WriteLine("Message received : " + inputLine);
                        // cwl(EncDecController.DecryptToString(inputLine));
                        writer.WriteLine("Message confirmed");
                    }
                }
            }catch (IOException){
                cwl("Client disconnected");
            }catch(Exception){}
            finally
            {
                client.Close();
            }
        });
        cleintThread.Start();
    }
}