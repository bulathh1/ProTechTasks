using System.Text;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProTechTasks
{
    public static class ProTechTask6
    {
        public static class StringProcessing
        {
            public static String WorkWithString(string stroka, int choice)
            {
                bool isValid = true;
                JObject keyValuePairs = new JObject();
                foreach (char ch in stroka)
                {
                    if (ch < 'a' || ch > 'z')
                    {
                        keyValuePairs.Add("400 Bad Gateaway", "Invalid input string");
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    Dictionary<char, int> freq = new Dictionary<char, int>();

                    if (stroka.Length % 2 == 0)
                    {
                        stroka = reverse(stroka[..(stroka.Length / 2)]) + reverse(stroka.Substring(stroka.Length / 2));
                    }
                    else
                    {
                        stroka = reverse(stroka) + stroka;
                    }
                    keyValuePairs.Add("ProcessedString", stroka);

                    foreach (char ch in stroka)
                    {
                        if (freq.ContainsKey(ch))
                            freq[ch]++;
                        else
                            freq[ch] = 1;
                    }
                    JArray json_array = new JArray();
                    foreach (KeyValuePair<char, int> kvp in freq)
                    {
                        json_array.Add($"{kvp.Key} - {kvp.Value}");
                    }
                    keyValuePairs.Add("values", json_array);

                    char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
                    int firstIndex = stroka.IndexOfAny(vowels);
                    int lastIndex = stroka.LastIndexOfAny(vowels);
                    // Проверка существования подстроки
                    if (firstIndex == -1 || lastIndex == -1)
                    {
                        keyValuePairs.Add("vowels_substring", "Подстрока, которая начинается и заканчивается на гласную, не найдена");
                    }
                    else
                    {
                        string substring = stroka.Substring(firstIndex, lastIndex - firstIndex + 1);
                        keyValuePairs.Add("vowels_substring", substring);
                    }


                    // Выбор алгоритма сортировки
                    switch (choice)
                    {
                        case 1:
                            string sortedString = QuickSort.SortString(stroka);
                            keyValuePairs.Add("sorted_string", sortedString);
                            break;

                        case 2:
                            BinaryTree tree = new BinaryTree();
                            string bb = stroka;
                            foreach (char bukva in bb)
                            {
                                tree.Insert(bukva);
                            }
                            keyValuePairs.Add("sorted_string", tree.PrintInOrder().ToString());
                            break;

                        default:
                            keyValuePairs.Add("sorted_string", "Неверный выбор алгоритма.");
                            break;
                    }

                    // API 
                    keyValuePairs.Add("string_API", API_Async(stroka).Result.ToString());
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
                return keyValuePairs.ToString();
            }

            static async Task<string> API_Async(string stroka)
            {
                string url = "http://www.randomnumberapi.com/api/v1.0/random?min=0&max=" + stroka.Length.ToString();

                using (var client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(url); // Send a GET request to the API

                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response body as a string
                            var responseBody = await response.Content.ReadAsStringAsync();
                            responseBody = responseBody.TrimStart('[').TrimEnd(']', '\n');
                            int randomNumber = int.Parse(responseBody);

                            return stroka.Remove(randomNumber, 1).ToString();
                        }
                        else
                        {
                            // if error
                            Random random = new Random();
                            int randomNumber = random.Next(0, stroka.Length);
                            return stroka.Remove(randomNumber, 1).ToString();
                        }
                    }
                    catch (Exception ex) { return (ex.Message).ToString(); }
                }
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

            public string PrintInOrder()
            {
                StringBuilder result = new StringBuilder();
                PrintInOrderRec(root, result);
                return result.ToString();
            }

            private void PrintInOrderRec(Node root, StringBuilder result)
            {
                if (root != null)
                {
                    PrintInOrderRec(root.Left, result);
                    result.Append($"{root.Value}");
                    PrintInOrderRec(root.Right, result);
                }
            }
        }
    }
}