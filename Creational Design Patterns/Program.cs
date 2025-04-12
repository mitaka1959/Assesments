using System;
using System.Collections.Generic;

public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

public class BlackCoffee : ICoffee
{
    public string GetDescription() => "Black Coffee";
    public double GetCost() => 6.00;
}

public class MilkDecorator : ICoffee
{
    private readonly ICoffee _baseCoffee;

    public MilkDecorator(ICoffee baseCoffee)
    {
        _baseCoffee = baseCoffee;
    }

    public string GetDescription() => _baseCoffee.GetDescription() + " + Milk";
    public double GetCost() => _baseCoffee.GetCost() + 0.50;
}

public abstract class CoffeeFactory
{
    public abstract ICoffee CreateCoffee();
}

public class EspressoFactory : CoffeeFactory
{
    public override ICoffee CreateCoffee()
    {
        return new BlackCoffee();
    }
}

public class CappuccinoFactory : CoffeeFactory
{
    public override ICoffee CreateCoffee()
    {
        return new MilkDecorator(new BlackCoffee());
    }
}

public class FlatWhiteFactory : CoffeeFactory
{
    public override ICoffee CreateCoffee()
    {
        return new MilkDecorator(
            new MilkDecorator(new BlackCoffee())
        );
    }
}

class Program
{
    static void Main()
    {
        CoffeeFactory espressoFactory = new EspressoFactory();
        ICoffee espresso = espressoFactory.CreateCoffee();
        Console.WriteLine($"Espresso: {espresso.GetDescription()}, Cost: {espresso.GetCost():C}");

        CoffeeFactory cappuccinoFactory = new CappuccinoFactory();
        ICoffee cappuccino = cappuccinoFactory.CreateCoffee();
        Console.WriteLine($"Cappuccino: {cappuccino.GetDescription()}, Cost: {cappuccino.GetCost():C}");

        CoffeeFactory flatWhiteFactory = new FlatWhiteFactory();
        ICoffee flatWhite = flatWhiteFactory.CreateCoffee();
        Console.WriteLine($"Flat White: {flatWhite.GetDescription()}, Cost: {flatWhite.GetCost():C}");
    }
}
