using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ConsoleBookStoreGitHub
{
    internal class Program
    {
        // int Id uchun
        public static int SeqInt = 0;

        // Path lar
        public static string booksPath = @"D:\BookStoreCRUD\Books.json";
        public static string booksIDPath = @"D:\BookStoreCRUD\Books_ID.json";

        // Kereli List yaratish
        public static List<Books> books = new List<Books>();
        public static List<int> booksIDs = new List<int>();

        static void Main(string[] args)
        {
            // Fayldan ko'chirish
            using (StreamReader sr = new StreamReader(booksPath))
            {
                try
                {
                    books = JsonSerializer.Deserialize<List<Books>>(sr.ReadToEnd());
                }
                catch (Exception ex) { }
            }

            using (StreamReader sr = new StreamReader(booksIDPath))
            {
                try
                {
                    booksIDs = JsonSerializer.Deserialize<List<int>>(booksIDPath);
                }catch (Exception ex) { }
            }

            // id ni maksimumini topish
            if (booksIDs.Count > 0)
            {
                SeqInt = booksIDs.Max();
            }

            //.... uyog'ini o'zila qilasila
        }
        public static int Seq()
        {
            SeqInt++;
            booksIDs.Add(SeqInt);
            Write(booksIDs, booksIDPath);
            return SeqInt;
        }
        public static void Delete(ref List<Books> books, string path)
        {
            if (books.Count > 0)
            {
                bool tryParse = false;
                int id = -1;
                while (!tryParse)
                {
                    foreach (var item in books)
                    {
                        Console.WriteLine($"ID: {item.id}, Name: {item.name}, Author: {item.authorName}, Price: {item.price}");
                    }
                    Console.Write("Id kiriting: ");
                    tryParse = int.TryParse(Console.ReadLine(), out id);
                }

                Books book = books.Find(book => book.id == id);
                if (book != null)
                {
                    books.Remove(book);
                    Write(books, path);

                    Console.WriteLine("Kitob muvaffaqiyatli o'chirildi!");
                }
                else
                {
                    Console.WriteLine("Bunday ID yo'q!");
                }
            }
            else
            {
                Console.WriteLine("Bitta ham kitob yo'q! :(");
            }
        }
        public static void Update(ref List<Books> books, string path)
        {
            if (books.Count > 0)
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
                if (book != null)
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
            else
            {
                Console.WriteLine("Bitta ham kitob yo'q! :(");
            }
        }
        public static void Write<T>(List<T> list, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                var serializedList = JsonSerializer.Serialize(list);

                sw.WriteLine(serializedList);
            }
        }
        public static void Read(ref List<Books> books, string path)
        {
            if (books.Count > 0)
            {
                books = null;
                using (StreamReader sr = new StreamReader(path))
                {
                    books = JsonSerializer.Deserialize<List<Books>>(sr.ReadToEnd());
                    foreach (var book in books)
                    {
                        Console.WriteLine($"ID: {book.id}, Name: {book.name}, Author: {book.authorName}, Price: {book.price}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Bitta ham kitob yo'q! :(");
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
