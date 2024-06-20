namespace ProTechTasks
{
    public static class ProTechTask5
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


                // Использование сортировки/выбор алгоритма
                Console.WriteLine("Выберите алгоритм сортировки цифрой (1 - Быстрая сортировка, 2 - Сортировка деревом):");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        string sortedString = QuickSort.SortString(c);
                        Console.WriteLine(sortedString);
                        break;

                    case 2:
                        BinaryTree tree = new BinaryTree();
                        string stroka = c;
                        foreach (char bukva in stroka)
                        {
                            tree.Insert(bukva);
                        }
                        Console.Write("Sorted tree: ");
                        tree.PrintInOrder();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор алгоритма.");
                        return;
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

        // Quick Sort
        public class QuickSort
        {
            public static string SortString(string input)
            {
                if (string.IsNullOrEmpty(input) || input.Length <= 1)
                {
                    return input;
                }

                char[] charArray = input.ToCharArray();
                Sort(charArray, 0, charArray.Length - 1);

                return new string(charArray);
            }

            private static void Sort(char[] array, int low, int high)
            {
                if (low < high)
                {
                    int partitionIndex = Partition(array, low, high);

                    Sort(array, low, partitionIndex - 1);
                    Sort(array, partitionIndex + 1, high);
                }
            }

            private static int Partition(char[] array, int low, int high)
            {
                char pivot = array[high];
                int i = low;

                for (int j = low; j < high; j++)
                {
                    if (array[j] < pivot)
                    {
                        char temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                        i++;
                    }
                }

                char temp1 = array[i];
                array[i] = array[high];
                array[high] = temp1;

                return i;
            }
        }


        // Tree sort
        public class Node
        {
            public char Value;
            public Node Left;
            public Node Right;

            public Node(char value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        public class BinaryTree
        {
            private Node root;

            private Node InsertRec(Node root, char value)
            {
                if (root == null)
                {
                    root = new Node(value);
                    return root;
                }

                if (value < root.Value)
                {
                    root.Left = InsertRec(root.Left, value);
                }
                else if (value >= root.Value)
                {
                    root.Right = InsertRec(root.Right, value);
                }

                return root;
            }

            public void Insert(char value)
            {
                root = InsertRec(root, value);
            }

            public void PrintInOrder()
            {
                PrintInOrderRec(root);
            }

            private void PrintInOrderRec(Node root)
            {
                if (root != null)
                {
                    PrintInOrderRec(root.Left);
                    Console.Write($"{root.Value}");
                    PrintInOrderRec(root.Right);
                }
            }
        }
    }
}