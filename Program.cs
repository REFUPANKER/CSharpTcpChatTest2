namespace TcpChatTest2
{
    public class TcpChatTest2 : ConsoleController
    {
        static TcpServerController ctrlServer = new TcpServerController();
        static TcpClientController ctrlClient = new TcpClientController();
        public static void Main(string[] args)
        {
            cls();
            cwl("App running");
            CheckStartup(args);
        }

        private static void CheckStartup(string[] args)
        {
            if (args.Length > 0)
            {
                SelectType(args[0], args);
            }
            else
            {
                cwl("server or client");
                string memberType = Console.ReadLine() + "";
                SelectType(memberType);
            }
        }
        private static void SelectType(string memberType, string[]? args = null)
        {
            switch (memberType.ToLower())
            {
                case "server":
                    ctrlServer.Run();
                    break;
                case "client":
                    if (args != null && args.Length > 1 && string.IsNullOrEmpty(args[1]) == false)
                    {
                        ctrlClient.Run(args[1]);
                    }
                    else
                    {
                        ctrlClient.Run();
                    }

                    break;
                default:
                    cwl("there is no type like " + memberType);
                    break;
            }
        }
    }
}