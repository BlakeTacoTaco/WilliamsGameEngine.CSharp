namespace testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 65; i < 91; i++)
            {
                Console.WriteLine("case '" + (char)i + "':");
                Console.WriteLine("\treturn Keyboard.Key." + (char)i + ";");
            }
        }
    }
}