using System;
using System.Linq;
using System.Collections.Generic;
using System.Security;
using System.Reflection.Metadata;

public delegate bool Condition(int number);
public delegate bool ConditionCount(int number, List<int> numbers1);
public delegate float CalculateDDS(float sum);

public class Actor
{
    public string firstName { get; set; }

    public string lastName { get; set; }
}

public class Movie 
{
    public string title { get; set; }

    public string starring { get; set; }

    public double profits { get; set; }
}
class Program
{
    public static void Main(string[] args)
    {
        List<int> numbers = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        List<int> numbers1 = new List<int> { 1, 2, 2, 2, 4, 5, 5, 6, 7, 7, 8, 9 };
        List<float> sums = new List<float> { 1300f, 2150f, 4000f, 5500f, 7800f };

        var actors = new[]
        {
            new Actor {firstName = "Steven", lastName = "Seagal"},
            new Actor {firstName = "Tom", lastName = "Cruise"},
            new Actor {firstName = "Johny", lastName = "Depp"},
        };

        IEnumerable<string> mostFamous = actors.Where(Actor => Actor.firstName == "Johny")
                                               .Select(Actor => $"{Actor.firstName} {Actor.lastName}");




        var movies = new[]
        {
            new Movie {title = "China Salesman", starring = "Steven Seagal", profits = 175000000 },
            new Movie {title = "Mission Impossible", starring = "Tom Crise", profits = 238000000},
            new Movie {title = "Pirates of the Caribean", starring = "Johny Depp", profits = 342000000},
        };

        IEnumerable<string> mostProfitable = movies.OrderByDescending(movie => movie.profits)
                                                   .Select(movie => $"{movie.title} - {movie.profits}");
        List<int> evenNumbers = Filter(numbers, IsEven);
        List<int> oddNumbers = Filter(numbers, IsOdd);

        List<int> repeatedNumbers = FilterRepeated(numbers, delegate(int num, List<int> nums)
        {
            int count = 0;
            foreach (int n in nums)
            {
                if (n == num) count++;
                if (count > 1) return true;
            }
            return false;
        });

        List<float> numberOfDDS = FilterDDS(sums, sum => ((sum / 1.2f) - sum) * -1);
        List<float> netSum = sums.extractionDDS();

        Console.WriteLine("Repeated numbers:");
        repeatedNumbers.ForEach(n => Console.WriteLine(n));


        Console.WriteLine("Even numbers:");
        evenNumbers.ForEach(n => Console.WriteLine(n));

        Console.WriteLine("\nOdd numbers:");
        oddNumbers.ForEach(n => Console.WriteLine(n));

        Console.WriteLine("\nSumm DDS:");
        numberOfDDS.ForEach(sum => Console.WriteLine(sum));

        Console.WriteLine("\nSum without DDS");
        netSum.ForEach(sum => Console.WriteLine(sum));

       
        foreach(var actor in mostFamous)
        {
            Console.WriteLine("\nMost famous actor: " + actor);
        }
        foreach(var movie in mostProfitable)
        {
            Console.WriteLine("\nMost porfitable movies: " + movie + "$");
        }
        
    }

    static List<int> Filter(List<int> numbers, Condition condition)
    {
        List<int> result = new List<int>();
        foreach (int number in numbers)
        {
            if (condition(number))
            {
                result.Add(number);
            }
            
        }
        return result;
    }


    static List<int> FilterRepeated(List<int> numbers1, ConditionCount condition1) 
    {
        List<int> result = new List<int> ();
        foreach (int number1 in numbers1)
        {
            if (condition1(number1, numbers1) && !result.Contains(number1))

            {
                result.Add(number1);
            }
        }
        return result;
    }

    static List<float> FilterDDS(List<float> sums, CalculateDDS calculation)
    {
        List<float> result = new List<float>();
        foreach (float sum in sums)
        {
            result.Add(calculation(sum));
        }
        return result;
    }

    static bool IsEven(int number) => number % 2 == 0;
    static bool IsOdd(int number) => number % 2 != 0;
}

public static class ListFloatExtensions
{
    public static List<float> extractionDDS(this List<float> sums)
    {
        List<float> result = new List<float>();
        foreach (float sum in sums)
        {
            float dds = sum - (sum / 5f);
            result.Add(dds);
        }
        return result;
    }
}
