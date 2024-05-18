using System;

namespace ProTechTasks
{
    class StringW
    {
        private static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string StringEdit(string input)
        {
            if (input.Length % 2 == 0)
            {
                int mid = input.Length / 2;
                string firstHalf = Reverse(input.Substring(0, mid));
                string secondHalf = Reverse(input.Substring(mid));
                return firstHalf + secondHalf;
            }
            else
            {
                string reversed = Reverse(input);
                return reversed + input;
            }
        }
    }
    class Taks1
    {
        static void Main()
        {
            Console.WriteLine("Введите строку:");
            string userInput = Console.ReadLine();
            string result = StringW.StringEdit(userInput);
            Console.WriteLine("Обработанная строка: " + result);
        }
    }
}