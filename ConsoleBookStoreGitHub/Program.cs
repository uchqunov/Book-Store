using System.Data;
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
        public static void Update(ref List<Books> books, string path)
        {
            bool tryParse = false;
            int id = -1;
            while (!tryParse)
            {
                Read(ref books, path);
                Console.Write("Id kiriting: ");
                tryParse = int.TryParse(Console.ReadLine(), out id);
            }

            Books book = books.Find(book => book.id == id);
            if (book !=  null) 
            {
                Console.Write("Kitobni yangi Nomini kiriting: ");
                book.name = Console.ReadLine();

                bool tryParseForPrice = false;
                int priceForBook = -1;
                while (!tryParseForPrice)
                {
                    Console.Write("Kitob narxini kiriting(Son kiriting!): ");
                    tryParseForPrice = int.TryParse(Console.ReadLine(), out priceForBook);
                }
                book.price = priceForBook;

                Write(books, path);
                Console.WriteLine("Muvaffaqiyatli yangilandi!");
            }
            else
            {
                Console.WriteLine("Bunday ID yo'q!");
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
        public static void Read(ref List<Books> books, string path)
        {
            books = null;
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    books = JsonSerializer.Deserialize<List<Books>>(sr.ReadToEnd());
                    foreach (var book in books)
                    {
                        Console.WriteLine($"ID: {book.id}, Name: {book.name}, Author: {book.authorName}, Price: {book.price}");
                    }
                }
                catch (Exception ex) { }
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
