/**
<summary>
Encryiption and Decryption Controller
Basic Binary Enc-Dec
</summary>
*/
public class EncDecController : ConsoleController
{
    public static string EncryptToBinary(string data)
    {
        string result = "";
        for (int j = 0; j < data.Length; j++)
        {
            for (int i = Convert.ToInt32(data[j]); i >= 1; i /= 2)
            {
                result = (i % 2) + result;
            }
            if (j + 1 < data.Length)
            {
                result = " " + result;
            }
        }
        return result;
    }

    public static string DecryptToString(string data)
    {
        string result = "";
        int asciiNum = 0;
        foreach (string word in data.Split(" "))
        {
            asciiNum = 0;
            for (int i = word.Length - 1; i >= 0; i--)
            {
                asciiNum += Convert.ToInt32(Math.Pow(2, i) * Convert.ToInt32("" + word[word.Length - 1 - i]));
            }
            result = Convert.ToChar(asciiNum) + result;
        }
        return result;
    }
}