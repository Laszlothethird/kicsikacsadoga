using Paksalaszlo;

class Program
{
    private static Random random = new Random();

    static void Main()
    {
        List<Book> books = GenerateRandomBooks();
        int initialStockCount = books.Sum(b => b.Stock);
        decimal totalRevenue = 0;
        int outOfStockCount = 0;

        for (int i = 0; i < 100; i++)
        {
            var selectedBook = books[random.Next(books.Count)];

            if (selectedBook.IsAvailable())
            {
                totalRevenue += selectedBook.Price;
                selectedBook.DecreaseStock();
            }
            else
            {
                if (random.NextDouble() < 0.5)
                {
                    int amountToAdd = random.Next(1, 11);
                    selectedBook.IncreaseStock(amountToAdd);
                }
                else
                {
                    outOfStockCount++;
                    books.Remove(selectedBook);
                }
            }
        }

        int finalStockCount = books.Sum(b => b.Stock);
        Console.WriteLine($"Bruttó bevétel: {totalRevenue} Ft");
        Console.WriteLine($"Könyv(k) elfogytak a nagykerben: {outOfStockCount}");
        Console.WriteLine($"Készlet állapot:\n Kb. {initialStockCount} db volt, Most {finalStockCount} db van.\n Előjeles különbség: {finalStockCount - initialStockCount}");
    }

    static List<Book> GenerateRandomBooks()
    {
        List<Book> books = new List<Book>();
        var names = new[] { "John Doe", "Jane Smith", "Alice Johnson", "Bob Brown", "Chris Davis" };
        var titles = new[]
        {
            "A magyar nyelv csodái",
            "Tanulmányok az angolról",
            "A német nyelv titkai",
            "Kalandok az angol nyelvben",
            "A magyar hagyományok",
        };

        for (int i = 0; i < 15; i++)
        {
            var randomName = names[random.Next(names.Length)];
            var randomTitle = titles[random.Next(titles.Length)];
            var language = random.NextDouble() < 0.8 ? "magyar" : (random.NextDouble() < 0.5 ? "angol" : "német");
            long isbn = GenerateRandomISBN();

            int publicationYear = random.Next(2007, DateTime.Now.Year + 1);
            int stock = random.Next(0, 11) < 3 ? 0 : random.Next(5, 11);
            int price = random.Next(10) * 100 + 1000;

            List<Author> authors = new List<Author>();
            int authorCount = (random.NextDouble() < 0.7 ? 1 : (random.NextDouble() < 0.5 ? 2 : 3));
            for (int j = 0; j < authorCount; j++)
            {
                authors.Add(new Author(names[random.Next(names.Length)]));
            }

            books.Add(new Book(isbn, authors, randomTitle, publicationYear, language, stock, price));
        }

        return books;
    }

    static long GenerateRandomISBN()
    {
        long isbn;
        do
        {
            isbn = random.Next(1000000000, 999999999);
        } while (isbn.Contains(isbn));
        return isbn;
    }
}