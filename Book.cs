using Paksalaszlo;

class Book
{
    private static Random random = new Random();
    private static HashSet<long> isbnSet = new HashSet<long>();

    public long ISBN { get; private set; }
    public List<Author> Authors { get; private set; }
    public string Title { get; private set; }
    public int PublicationYear { get; private set; }
    public string Language { get; private set; }
    public int Stock { get; private set; }
    public int Price { get; private set; }

    public Book(long isbn, List<Author> authors, string title, int publicationYear, string language, int stock, int price)
    {
        if (isbn.ToString().Length != 10 || !isbnSet.Add(isbn))
            throw new ArgumentException("Az ISBN azonosító érvénytelen vagy már létezik!");

        if (title.Length < 3 || title.Length > 64)
            throw new ArgumentException("A cím nem megfelelő!");

        if (publicationYear < 2007 || publicationYear > DateTime.Now.Year)
            throw new ArgumentException("A kiadás éve nem megfelelő!");

        if (language != "angol" && language != "német" && language != "magyar")
            throw new ArgumentException("A nyelv nem megfelelő!");

        if (stock < 0)
            throw new ArgumentException("A készlet nem lehet negatív!");

        if (price < 1000 || price > 10000 || price % 100 != 0)
            throw new ArgumentException("Az ár nem megfelelő!");

        ISBN = isbn;
        Authors = authors;
        Title = title;
        PublicationYear = publicationYear;
        Language = language;
        Stock = stock;
        Price = price;
    }

    public Book(string title, string authorName)
    {
        ISBN = GenerateRandomISBN();
        Authors = new List<Author> { new Author(authorName) };
        Title = title;
        PublicationYear = DateTime.Now.Year;
        Language = "magyar";
        Stock = 0;
        Price = 4500;
    }

    private static long GenerateRandomISBN()
    {
        long isbn;
        do
        {
            isbn = random.Next(1000000000, 999999999);
        } while (isbnSet.Contains(isbn));
        return isbn;
    }

    public override string ToString()
    {
        string authorLabel = Authors.Count == 1 ? "szerző:" : "szerzők:";
        string stockStatus = Stock == 0 ? "beszerzés alatt" : $"{Stock} db";
        return $"{Title}\n{authorLabel} {string.Join(", ", Authors.Select(a => $"{a.FirstName} {a.LastName}"))}\nKiadás éve: {PublicationYear}\nNyelv: {Language}\nKészlet: {stockStatus}\nÁr: {Price} Ft";
    }

    public void DecreaseStock()
    {
        if (Stock > 0)
        {
            Stock--;
        }
    }

    public void IncreaseStock(int amount)
    {
        Stock += amount;
    }

    public bool IsAvailable()
    {
        return Stock > 0;
    }
}
