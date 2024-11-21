using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ConsoleBookStoreGitHub
{
    internal class Program
    {
        // Path lar
        public static string bookPath = @"D:\BookStoreCRUD\Books.json";
        public static string bookIDPath = @"D:\BookStoreCRUD\Books_ID.json";

        // Kereli List yaratish
        public static List<Books> books = new List<Books>();
        public static List<int> bookIDs = new List<int>();

        static void Main(string[] args)
        {
            // Fayldan ko'chirish
            using (StreamReader sr = new StreamReader(bookPath))
            {
                try
                {
                    books = JsonSerializer.Deserialize<List<Books>>(sr.ReadToEnd());
                }
                catch (Exception ex) { }
            }
        }
        public static void Write(List<Books> books, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                var booksSerialized = JsonSerializer.Serialize(books);

                sw.WriteLine(booksSerialized);
            }
        }
        public class Books
        {
            public int id { get; set; }
            public int price { get; set; }
            public string name { get; set; }
            public string authorName { get; set; }
        }
    }
}
