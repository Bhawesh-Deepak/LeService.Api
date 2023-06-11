using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LeService.Api.Helpers
{
    public static class EmailHelper
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "leservicehelp@gmail.com"; //Sender Email Address  
        static string password = "omnie@2022"; //Sender Password  
        static string emailToAddress = "receiver@gmail.com"; //Receiver Email Address  

        static string WelcomeEmail = @"Hi [Customer],
            Welcome on board! We are extremely thrilled to have you join <b>LE-Service</b>.<br/>

            We’re confident that our Services will help you and make your life much easier. To help you better understand how our services works, You can directly contact our Customer Support Team.<br/>

            You can always subscribe to our blogs or follow us on Social to explore great tips and expand your knowledge. If you have any doubts or need assistance, feel free to contact me or our team at any time. We are always here to make this a delightful journey for you. <br/>

            Thanks, <br/>
            LE-Service Customer Support Team";

        public static void SendEmail(string body, string subject, string toEmailAddress)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(toEmailAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

        public static void SendWelcomeEmail(string toEmailAddress)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(toEmailAddress);
                mail.Subject = "Welcome To LeService";
                mail.Body = WelcomeEmail;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
    }
}
