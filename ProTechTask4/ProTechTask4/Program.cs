﻿using System.Collections.Generic;

namespace ProTechTasks
{
    public static class ProTechTask4
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

                char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
                int firstIndex = c.IndexOfAny(vowels);
                int lastIndex = c.LastIndexOfAny(vowels);
                // Проверка существования подстроки
                if (firstIndex == -1 || lastIndex == -1)
                {
                    Console.WriteLine("Подстрока, которая начинается и заканчивается на гласную, не найдена.");
                }
                else
                {
                    string substring = c.Substring(firstIndex, lastIndex - firstIndex + 1);
                    Console.WriteLine(substring);
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