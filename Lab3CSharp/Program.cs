//v10
using System;

//task 1
class Date
{
    private int day;
    private int month;
    private int year;

    public Date(int day, int month, int year)
    {
        Day = day;
        Month = month;
        Year = year;
    }

    public int Day
    {
        get { return day; }
        set
        {
            if (value >= 1 && value <= 31)
                day = value;
            else
                throw new ArgumentException("Invalid day value.");
        }
    }

    public int Month
    {
        get { return month; }
        set
        {
            if (value >= 1 && value <= 12)
                month = value;
            else
                throw new ArgumentException("Invalid month value.");
        }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public void PrintDate()
    {
        string[] months = {"січня", "лютого", "березня", "квітня", "травня", "червня",
                           "липня", "серпня", "вересня", "жовтня", "листопада", "грудня"};
        Console.WriteLine($"{Day} {months[Month - 1]} {Year} року");
    }

    public void PrintDateFormatted()
    {
        Console.WriteLine($"{Day:D2}.{Month:D2}.{Year}");
    }

    public bool IsValidDate()
    {
        if (Year < 1)
            return false;

        if (Month < 1 || Month > 12)
            return false;

        if (Day < 1)
            return false;

        int[] daysInMonth = {31, 28 + (IsLeapYear(Year) ? 1 : 0), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

        return Day <= daysInMonth[Month - 1];
    }

    private bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public int DifferenceInDays(Date other)
    {
        DateTime date1 = new DateTime(Year, Month, Day);
        DateTime date2 = new DateTime(other.Year, other.Month, other.Day);
        TimeSpan difference = date2 - date1;
        return Math.Abs(difference.Days);
    }

    public int Century
    {
        get { return (Year - 1) / 100 + 1; }
    }
}

//task 2
class Document
{
    protected string documentType;
    protected string issuer;
    protected string recipient;
    protected DateTime date;

    public Document(string documentType, string issuer, string recipient, DateTime date)
    {
        this.documentType = documentType;
        this.issuer = issuer;
        this.recipient = recipient;
        this.date = date;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Document Type: {documentType}");
        Console.WriteLine($"Issuer: {issuer}");
        Console.WriteLine($"Recipient: {recipient}");
        Console.WriteLine($"Date: {date:d}");
    }
}

class Receipt : Document
{
    private decimal amount;

    public Receipt(string issuer, string recipient, DateTime date, decimal amount)
        : base("Receipt", issuer, recipient, date)
    {
        this.amount = amount;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Amount: {amount:C}");
    }
}

class Invoice : Document
{
    private string product;
    private int quantity;

    public Invoice(string issuer, string recipient, DateTime date, string product, int quantity)
        : base("Invoice", issuer, recipient, date)
    {
        this.product = product;
        this.quantity = quantity;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Product: {product}");
        Console.WriteLine($"Quantity: {quantity}");
    }
}

class Bill : Document
{
    private decimal totalAmount;

    public Bill(string issuer, string recipient, DateTime date, decimal totalAmount)
        : base("Bill", issuer, recipient, date)
    {
        this.totalAmount = totalAmount;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Total Amount: {totalAmount:C}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Lab3");
        Console.WriteLine("Task1");
        Date date1 = new Date(5, 1, 2022);
        Date date2 = new Date(10, 2, 2023);

        date1.PrintDate();
        date1.PrintDateFormatted();

        if (date1.IsValidDate())
            Console.WriteLine("Date 1 is valid.");
        else
            Console.WriteLine("Date 1 is invalid.");

        Console.WriteLine($"Difference in days between date1 and date2: {date1.DifferenceInDays(date2)}");

        Console.WriteLine($"Century of date1's year: {date1.Century}");

        Date[] dates = new Date[3];
        dates[0] = new Date(5, 1, 2022);
        dates[1] = new Date(29, 2, 2024);
        dates[2] = new Date(31, 4, 2023);

        for (int i = 0; i < dates.Length; i++)
        {
            Console.WriteLine($"Date {i + 1}:");
            Console.WriteLine($"Day: {dates[i].Day}, Month: {dates[i].Month}, Year: {dates[i].Year}");
            if (dates[i].IsValidDate())
                Console.WriteLine("This date is valid.");
            else
                Console.WriteLine("This date is invalid.");
            Console.WriteLine();
        }

        Console.WriteLine("Task2");
        Document[] documents = new Document[3];
        documents[0] = new Receipt("Company A", "Customer X", DateTime.Now, 100.50m);
        documents[1] = new Invoice("Company B", "Customer Y", DateTime.Now, "Product Z", 2);
        documents[2] = new Bill("Company C", "Customer Z", DateTime.Now, 500.75m);

        foreach (var document in documents)
        {
            document.Show();
            Console.WriteLine();
        }

    }
}
