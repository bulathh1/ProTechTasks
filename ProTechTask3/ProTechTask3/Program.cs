using System.Collections.Generic;

namespace ProTechTasks
{
    public static class ProTechTask3
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
                Dictionary<char, int> freq = new Dictionary<char, int>();

                if (c.Length % 2 == 0)
                {
                    c = reverse(c[..(c.Length / 2)]) + reverse(c.Substring(c.Length / 2));
                }
                else
                {
                    c = reverse(c) + c;
                }
                Console.WriteLine(c);

                foreach (char ch in c)
                {
                    if (freq.ContainsKey(ch))
                        freq[ch]++;
                    else
                        freq[ch] = 1;
                }
                foreach (KeyValuePair<char, int> kvp in freq)
                {
                    Console.WriteLine($"{kvp.Key} - {kvp.Value}");
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