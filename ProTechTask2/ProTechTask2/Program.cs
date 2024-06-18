namespace ProTechTasks
{
    public static class ProTechTask2
    {
        static void Main(string[] args)
        {
            string c = Console.ReadLine();
            var invalidChars = c.Where(c => !char.IsLower(c));
            bool isValid = true;
            foreach (char ch in c)
            {
                if (ch < 'a' || ch > 'z')
                {
                    Console.Write(ch);
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (c.Length % 2 == 0)
                {
                    Console.WriteLine($"{reverse(c[..(c.Length / 2)]) + reverse(c.Substring(c.Length / 2))}");
                }
                else
                {
                    Console.WriteLine($"{reverse(c) + c}");
                }
            }

            static string reverse(string message)
            {
                string hc = "";
                foreach (var ch in message)
                {
                    hc = ch + hc;
                }
                return hc;
            }
        }
    }
}