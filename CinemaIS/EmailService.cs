using CinemaIS.Models;
using MailKit.Net.Smtp;
using MessagingToolkit.QRCode.Codec;
using MimeKit;
using System.Drawing;
using System.Drawing.Imaging;

namespace CinemaIS
{
    public static class EmailService
    {
        public static async Task SendTicketAsync(string email, string qrPath, Ticket ticket)
        {
            using MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("ИС Кинотеатра", "ablichenkov@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = $"Билет на сеанс фильма \"{ticket.Session.Movie.Name}\", место {ticket.Place}";

            string qrData = ticket.OwnerEmail + " " + ticket.Place;

            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrCodeBitmap = encoder.Encode(qrData);
            qrCodeBitmap.Save(qrPath, ImageFormat.Jpeg);

            BodyBuilder bodyBuilder = new BodyBuilder();

            using (var stream = File.OpenRead(qrPath))
            {
                bodyBuilder.Attachments.Add(Path.GetFileName(qrPath), stream);
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("ablichenkov@gmail.com", "slhbfdqwryxeprvk");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
