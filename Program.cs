using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main()
    {
        Console.Write("Enter your email address: ");
        string userEmail = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userEmail) || !userEmail.Contains("@"))
        {
            Console.WriteLine("Invalid email address.");
            return;
        }

        try
        {
            SendThankYouEmail(userEmail);
            Console.WriteLine("Thank you email sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    static void SendThankYouEmail(string toEmail)
    {
        string fromEmail = "your_email@example.com";
        string password = "your_password";

        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(fromEmail, password);

            using (var message = new MailMessage(fromEmail, toEmail))
            {
                message.Subject = "Thank You for Subscribing!";
                message.Body = "Hello,\n\nThank you for subscribing to our newsletter!\n\nBest regards,\nYour Company";

                client.Send(message); 
            }
        }
    }
}
