using System;
using System.Linq;

class BankUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
class BankRecord
{   
    public int Id { get; set; }
    public int Balance { get; set; }
    public bool IsInDebt { get; set; }
}
class Program 
{

    public static void Main(string[] args)
    {
        var BankUsers = new BankUser[]
        {
            new BankUser {Id = 0, Name = "Georgi Petkov", Email = "georgi.petkov@example.com"},
            new BankUser {Id = 1, Name = "Ivan Petrov", Email = "ivan.petrov@example.com" },
            new BankUser {Id = 2, Name = "Dimitar Kirov", Email = "dimitar.kirov@example.com" }
        };

        var BankRecords = new BankRecord[]
        {
            new BankRecord {Id = 0, Balance = 50000, IsInDebt = false},
            new BankRecord {Id = 1, Balance = -20000, IsInDebt = true},
            new BankRecord {Id = 2, Balance = 100000, IsInDebt = false},
        };

        

        var result = BankUsers.Join(BankRecords,
            user => user.Id,
            record => record.Id,
            (user, record) => new
            {
                Name = user.Name,
                Balance = record.Balance
            });
        
        foreach(var item in result) 
        { 
            Console.WriteLine($"Name: {item.Name} \nBalance: {item.Balance}$");
        }

        var result1 = BankUsers.GroupJoin(
                      BankRecords,
                      user => user.Id,
                      record => record.Id,
                      (user, records) => new
                      {
                         Name = user.Name,
                         Balance = records.FirstOrDefault()?.Balance ?? 0,
                         IsInDebt = records.FirstOrDefault()?.IsInDebt ?? false
                      });


        foreach (var item in result1)
        {
            Console.WriteLine($"Name: {item.Name}, Balance: {item.Balance}, Is in debt: {item.IsInDebt}");
        }


        var result2 = BankUsers.Zip(BankRecords,
            (user, record) => new
            {
                Name = user.Name,
                Balance = record.Balance,
                Email = user.Email,
               
            });

        foreach( var item in result2)
        {
            Console.WriteLine($"Name: {item.Name}, Balance: {item.Balance}, Email: {item.Email}");
        }

        var deptFree = result1.GroupBy(item => item.IsInDebt);

        foreach (var group in deptFree)
        {
            Console.WriteLine(group.Key ? "Users in debt:" : "Debt-free users:");

            foreach (var item in group)
            {
                Console.WriteLine($" - {item.Name}, Balance: {item.Balance}");
            }

            Console.WriteLine(); 
        }
        char[] firstseq = { 'T', 'w', 'o' };
        char[] secondseq = { 'A', 'n', 'd' };
        char[] secondAndAHalfseq = { 'a' };
        char[] thirdseq = { 'H', 'a', 'l', 'f'};
        char[] fourthseq = { 'M', 'e', 'n' };

        var result3 = firstseq.Concat(secondseq).Concat(secondAndAHalfseq).Concat(thirdseq).Concat(fourthseq);
        Console.WriteLine("Concatenated sequense: ");
        foreach(var item in result3) { Console.Write(item); }

        int[] sequence1 = { 1, 2, 7, 4, 8, 9, 20, 43 };
        int[] sequence2 = { 1, 7, 6, 3, 4, 8, 9, 21 };
        IEnumerable<int> union = sequence1.Union(sequence2);
        Console.WriteLine("\nNew union List: ");
        foreach (var item in union) 
        {
            Console.Write(" " +item+ " ");
        }
        IEnumerable<int> intersect = sequence1.Intersect(sequence2);
        Console.WriteLine("\nNew Intersect List: ");
        foreach (var item in intersect)
        {
            Console.Write(" " + item + " ");
        }
        IEnumerable<int> except = sequence1.Except(sequence2);
        Console.WriteLine("\nNew except List: ");
        foreach (var item in except)
        {
            Console.Write(" " + item + " ");
        }
    }
}