using System;

abstract class Phone
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Memory { get; set; }

    public Phone(string make, string model, int year, int memory)
    {
        Make = make;
        Model = model;
        Year = year;
        Memory = memory;
    }

    public override string ToString()
    {
        return $"{Make} {Model} ({Year}) - {Memory}GB";
    }

    public void Call(int number)
    {
        Console.WriteLine($"Calling: {number}");
    }

    public void Call(int number, bool isVideo)
    {
        if (isVideo)
            Console.WriteLine($"Making a video call with: {number}");
        else
            Call(number);
    }

    public void Calling(bool IsPicking = false)
    {
        if (IsPicking)
            Console.WriteLine("The number is busy");
        else
            Console.WriteLine("Ringing...");
    }
}

class HomePhone : Phone
{
    public bool IsStillUsed { get; set; }

    public HomePhone(string make, string model, int year, int memory, bool isStillUsed)
        : base(make, model, year, memory)
    {
        IsStillUsed = isStillUsed;
    }
}

class Program
{
    static void Main()
    {
        Phone myPhone = new HomePhone("Iphone", "13", 2018, 128, true);

        Console.WriteLine(myPhone);

        myPhone.Call(123456789);
        myPhone.Call(123456789, true);

        myPhone.Calling(IsPicking: true);

        CheckPhoneType(myPhone);
    }

    static void CheckPhoneType(Phone phone)
    {
        if (phone is HomePhone homePhone)
        {
            Console.WriteLine("This is a HomePhone.");
        }

        var casted = phone as HomePhone;
        if (casted != null)
        {
            Console.WriteLine($"IsStillUsed: {casted.IsStillUsed}");
        }
    }
}
