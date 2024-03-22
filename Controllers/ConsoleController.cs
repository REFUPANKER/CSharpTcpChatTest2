/**
<summary>
<b>Console Controllers</b><br></br>
Gives speed at accessing to console commands
</summary>
*/
public class ConsoleController
{
    /**<summary>
    Static Console Write Line
    </summary>*/
    public static void cwl(Object msg, object? overloaded = null)
    {
        System.Console.WriteLine(msg);
    }

    /**<summary>
    Static Console Write
    </summary>*/
    public static void cw(Object msg, object? overloaded = null)
    {
        System.Console.Write(msg);
    }

    /**<summary>
    Static Console Write
    </summary>*/
    public static void cls(object? overloaded = null)
    {
        Console.Clear();
    }

    public static string randomId(int length)
    {
        Random rnd = new Random();
        string res = "";
        for (int i = 0; i < length; i++)
        {
            switch (rnd.Next(0, 3))
            {
                case 0:
                    res += ((char)rnd.Next(65, 91));
                    break;
                case 1:
                    res += ((char)rnd.Next(97, 122));
                    break;
                case 2:
                    res += ((char)rnd.Next(48, 57));
                    break;
                default:
                    System.Console.WriteLine("rnd 3");
                    break;
            }
        }
        return res;
    }

}