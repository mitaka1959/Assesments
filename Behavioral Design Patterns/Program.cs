using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(string message);
}
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}
public class BookStore: ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    private string _orders;

    public void Attach(IObserver observer) => observers.Add(observer);
    public void Detach(IObserver observer) => observers.Remove(observer);

    public void SetOrders(string orders)
    {
        _orders = orders;
        Notify();
    }
    public void Notify()
    {
        foreach (var observer in observers)
        {
            observer.Update(_orders);
        }
    }
}
public class User : IObserver
{
    private string _name;

    public User(string name) => _name = name;

    public void Update(string order)
    {
        Console.WriteLine($"{_name} has been notified about order: {order}");
    }
}

public class Staff : IObserver
{
    private string _name;

    public Staff(string name) => _name = name;

    public void Update(string order)
    {
        Console.WriteLine($"{_name} has been notified about order: {order}");
    }
}

public class Program
{
    public static void Main()
    {
        BookStore store = new BookStore();

        
        User user1 = new User("Alice (Email)");
        User user2 = new User("Bob (SMS)");
        Staff staff1 = new Staff("Warehouse Team");
        Staff staff2 = new Staff("Shipping Team");

        
        store.Attach(user1);
        store.Attach(user2);
        store.Attach(staff1);
        store.Attach(staff2);

       
        store.SetOrders("Order #1001 has been placed.");

        Console.WriteLine("\n Bob unsubscribes \n");

        
        store.Detach(user2);

        
        store.SetOrders("Order #1001 is ready for shipping.");
    }
}
