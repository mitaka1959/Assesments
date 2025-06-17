using System;

public class NegativeNumberException : Exception
{
    public NegativeNumberException(string message) : base(message) { }
}

public class ZeroNotAllowedException : Exception
{
    public ZeroNotAllowedException(string message) : base(message) { }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a number:");

        try
        {
            string input = Console.ReadLine();
            int number = ParseNumber(input);

            double result = Divide(100, number);
            Console.WriteLine($"100 divided by {number} = {result}");
        }
        catch (NegativeNumberException ex)
        {
            Console.WriteLine($"Custom Exception: {ex.Message}");
        }
        catch (ZeroNotAllowedException ex)
        {
            Console.WriteLine($"Custom Exception: {ex.Message}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Format Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Exception: {ex.Message}");
            throw;
        }
        finally
        {
            Console.WriteLine("Program has completed (finally was executed).");
        }
    }

    static int ParseNumber(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input), "Input cannot be empty.");

        int number = int.Parse(input);

        if (number < 0)
            throw new NegativeNumberException("Negative numbers are not allowed.");

        if (number == 0)
            throw new ZeroNotAllowedException("Zero is not allowed.");

        return number;
    }

    static double Divide(int numerator, int denominator)
    {
#if DEBUG
        Console.WriteLine("DEBUG MODE: Performing division...");
#endif

        return (double)numerator / denominator;
    }
}