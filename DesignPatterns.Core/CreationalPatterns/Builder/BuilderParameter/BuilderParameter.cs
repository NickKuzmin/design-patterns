using System;

namespace DesignPatterns.Core.CreationalPatterns.Builder.BuilderParameter
{
    public class MailService
    {
        public class EmailBuilder
        {
            public class Email
            {
                public string From, To, Subject, Body;
            }

            private readonly Email _email;

            public EmailBuilder(Email email) => _email = email;

            public EmailBuilder From(string from)
            {
                _email.From = from;
                return this;
            }

            public EmailBuilder To(string to)
            {
                _email.To = to;
                return this;
            }

            public EmailBuilder Subject(string subject)
            {
                _email.Subject = subject;
                return this;
            }

            public EmailBuilder Body(string body)
            {
                _email.Body = body;
                return this;
            }
        }

        // Приватный метод
        private void SendEmailInternal(EmailBuilder.Email email) { }

        // Публичный API, который принимает только Builder'ы, поэтому нет других вариантов кастомного конструирования экземпляра EmailBuilder.Email
        public void SendEmail(Action<EmailBuilder> builder)
        {
            var email = new EmailBuilder.Email();
            builder(new EmailBuilder(email));
            SendEmailInternal(email);
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            var ms = new MailService();
            ms.SendEmail(email => email.From("foo@bar.com")
                .To("bar@baz.com")
                .Body("Hello, how are you?"));
        }
    }
}
