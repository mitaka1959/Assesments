using System;
using System.Collections.Generic;

public interface INotificationChannel
{
    void Send(string to, string message);
}

public class EmailNotificationChannel : INotificationChannel
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"Email sent to {to}: {message}");
    }
}

public class SmsNotificationChannel : INotificationChannel
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"SMS sent to {to}: {message}");
    }
}

public class PushNotificationChannel : INotificationChannel
{
    public void Send(string to, string message)
    {
        Console.WriteLine($"Push notification sent to {to}: {message}");
    }
}

public class NotificationService
{
    private readonly List<INotificationChannel> _channels;

    public NotificationService(List<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public void SendNotification(string to, string message)
    {
        foreach (var channel in _channels)
        {
            channel.Send(to, message);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var channels = new List<INotificationChannel>
        {
            new EmailNotificationChannel(),
            new SmsNotificationChannel(),
            new PushNotificationChannel()
        };

        var notificationService = new NotificationService(channels);

        Console.WriteLine("Enter recipient:");
        string recipient = Console.ReadLine();

        Console.WriteLine("Enter message:");
        string message = Console.ReadLine();

        notificationService.SendNotification(recipient, message);

        Console.WriteLine("Notification sent via all channels.");
    }
}
