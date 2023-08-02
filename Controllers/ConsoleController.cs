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
}