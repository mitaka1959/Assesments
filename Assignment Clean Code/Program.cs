using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoredSpeakerApp
{
    public interface IRepository
    {
        int SaveSpeaker(Speaker speaker);
    }

    public class Session
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
    }

    public class WebBrowser
    {
        public enum BrowserName { InternetExplorer, Chrome, Firefox, Safari, Edge }
        public BrowserName Name { get; set; }
        public int MajorVersion { get; set; }
    }

    public interface ISpeakerValidator
    {
        void Validate(Speaker speaker);
    }

    public class SpeakerValidator : ISpeakerValidator
    {
        private readonly List<string> disallowedEmailDomains = new List<string> { "aol.com", "hotmail.com", "prodigy.com", "CompuServe.com" };
        private readonly List<string> preferredEmployers = new List<string> { "Microsoft", "Google", "Fog Creek Software", "37Signals" };

        public void Validate(Speaker speaker)
        {
            if (string.IsNullOrWhiteSpace(speaker.FirstName))
                throw new ArgumentNullException("First Name is required");

            if (string.IsNullOrWhiteSpace(speaker.LastName))
                throw new ArgumentNullException("Last Name is required");

            if (string.IsNullOrWhiteSpace(speaker.Email))
                throw new ArgumentNullException("Email is required");

            if (!MeetsRequirements(speaker) && IsUsingBadEmailOrOldBrowser(speaker))
                throw new Speaker.SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our standards.");

            if (speaker.Sessions == null || !speaker.Sessions.Any())
                throw new ArgumentException("Can't register speaker with no sessions to present.");
        }

        private bool MeetsRequirements(Speaker speaker)
        {
            return speaker.Exp > 10
                || speaker.HasBlog
                || (speaker.Certifications != null && speaker.Certifications.Count > 3)
                || preferredEmployers.Contains(speaker.Employer);
        }

        private bool IsUsingBadEmailOrOldBrowser(Speaker speaker)
        {
            string emailDomain = speaker.Email.Split('@').Last();
            bool badDomain = disallowedEmailDomains.Contains(emailDomain);
            bool oldBrowser = speaker.Browser.Name == WebBrowser.BrowserName.InternetExplorer && speaker.Browser.MajorVersion < 9;
            return badDomain || oldBrowser;
        }
    }

    public interface ISessionApprover
    {
        bool ApproveSessions(List<Session> sessions);
    }

    public class SessionApprover : ISessionApprover
    {
        private readonly List<string> outdatedTechnologies = new List<string> { "Cobol", "Punch Cards", "Commodore", "VBScript" };

        public bool ApproveSessions(List<Session> sessions)
        {
            bool anyApproved = false;

            foreach (var session in sessions)
            {
                if (outdatedTechnologies.Any(tech =>
                    session.Title.Contains(tech, StringComparison.OrdinalIgnoreCase)
                    || session.Description.Contains(tech, StringComparison.OrdinalIgnoreCase)))
                {
                    session.Approved = false;
                }
                else
                {
                    session.Approved = true;
                    anyApproved = true;
                }
            }

            return anyApproved;
        }
    }

    public interface IRegistrationFeeCalculator
    {
        int CalculateRegistrationFee(int? experienceYears);
    }

    public class RegistrationFeeCalculator : IRegistrationFeeCalculator
    {
        public int CalculateRegistrationFee(int? experienceYears)
        {
            if (experienceYears <= 1)
                return 500;
            if (experienceYears >= 2 && experienceYears <= 3)
                return 250;
            if (experienceYears >= 4 && experienceYears <= 5)
                return 100;
            if (experienceYears >= 6 && experienceYears <= 9)
                return 50;
            return 0;
        }
    }

    public class Speaker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Exp { get; set; }
        public bool HasBlog { get; set; }
        public string BlogURL { get; set; }
        public WebBrowser Browser { get; set; }
        public List<string> Certifications { get; set; }
        public string Employer { get; set; }
        public int RegistrationFee { get; set; }
        public List<Session> Sessions { get; set; }

        private readonly ISpeakerValidator _validator;
        private readonly ISessionApprover _sessionApprover;
        private readonly IRegistrationFeeCalculator _feeCalculator;

        public Speaker(
            ISpeakerValidator validator,
            ISessionApprover sessionApprover,
            IRegistrationFeeCalculator feeCalculator)
        {
            _validator = validator;
            _sessionApprover = sessionApprover;
            _feeCalculator = feeCalculator;
        }

        public int? Register(IRepository repository)
        {
            _validator.Validate(this);

            bool anyApproved = _sessionApprover.ApproveSessions(Sessions);
            if (!anyApproved)
                throw new NoSessionsApprovedException("No sessions approved.");

            RegistrationFee = _feeCalculator.CalculateRegistrationFee(Exp);

            try
            {
                return repository.SaveSpeaker(this);
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while saving the speaker.");
            }
        }

        public class SpeakerDoesntMeetRequirementsException : Exception
        {
            public SpeakerDoesntMeetRequirementsException(string message) : base(message) { }
        }

        public class NoSessionsApprovedException : Exception
        {
            public NoSessionsApprovedException(string message) : base(message) { }
        }
    }

    public class DummyRepository : IRepository
    {
        public int SaveSpeaker(Speaker speaker)
        {
            Console.WriteLine("Saving speaker...");
            return new Random().Next(1, 1000);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var speaker = new Speaker(
                new SpeakerValidator(),
                new SessionApprover(),
                new RegistrationFeeCalculator())
            {
                FirstName = "John",
                LastName = "Sins",
                Email = "johny.sins@gmail.com",
                Exp = 5,
                HasBlog = true,
                Certifications = new List<string> { "Cert1", "Cert2", "Cert3", "Cert4" },
                Employer = "Google",
                Browser = new WebBrowser { Name = WebBrowser.BrowserName.Chrome, MajorVersion = 99 },
                Sessions = new List<Session>
                {
                    new Session { Title = "C# Best Practices", Description = "Learn C#." },
                    new Session { Title = "Cobol Legacy Code", Description = "Old Cobol codebase." }
                }
            };

            var repository = new DummyRepository();

            try
            {
                int? speakerId = speaker.Register(repository);
                Console.WriteLine($"Speaker registered with ID: {speakerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration failed: {ex.Message}");
            }
        }
    }
}
