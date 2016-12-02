namespace GrowDT.Utility.Email
{
    public static class EmailServiceFactory
    {
        private static IEmailService _emailService;

        public static void Initialize(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public static IEmailService GetEmailService()
        {
            return _emailService;
        }
    }
}
