using System;
using System.Collections.Generic;
using System.Linq;

public interface IEntity
{
    int Id { get; set; }
}

public interface IRepository<T>
{
    void Add(T item);
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Update(T item);
    void Delete(int id);
}

public class InMemoryRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _items = new List<T>();

    public void Add(T item)
    {
        _items.Add(item);
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }

    public T GetById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public void Update(T item)
    {
        var existingItem = GetById(item.Id);
        if (existingItem != null)
        {
            _items.Remove(existingItem);
            _items.Add(item);
        }
    }

    public void Delete(int id)
    {
        var item = GetById(id);
        if (item != null)
        {
            _items.Remove(item);
        }
    }
}

public class Product : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"Product [Id={Id}, Name={Name}, Price={Price}]";
    }
}

class Program
{
    static void Main(string[] args)
    {
        IRepository<Product> productRepository = new InMemoryRepository<Product>();

        productRepository.Add(new Product { Id = 1, Name = "Laptop", Price = 1200.00m });
        productRepository.Add(new Product { Id = 2, Name = "Smartphone", Price = 800.00m });

        Console.WriteLine("All products:");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nGet product with Id = 1:");
        var product1 = productRepository.GetById(1);
        Console.WriteLine(product1);

        product1.Price = 1100.00m;
        productRepository.Update(product1);

        Console.WriteLine("\nAfter update:");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine(product);
        }

        productRepository.Delete(2);

        Console.WriteLine("\nAfter deletion:");
        foreach (var product in productRepository.GetAll())
        {
            Console.WriteLine(product);
        }
    }
}
