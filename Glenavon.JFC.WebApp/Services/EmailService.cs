using Azure.Core;
using Glenavon.JFC.WebApp.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;

namespace Glenavon.JFC.WebApp.Services;

public class EmailService
{
    private readonly GraphServiceClient _graphClient;
    private readonly string _senderEmail;

    public EmailService(TokenCredential tokenCredential, IOptions<GraphSettings> options)
    {
        var settings = options.Value;
        _senderEmail = settings.SenderEmail;
        _graphClient = new GraphServiceClient(tokenCredential);
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = BodyType.Html,
                Content = body
            },
            ToRecipients = new List<Recipient>
            {
                new()
                {
                    EmailAddress = new EmailAddress { Address = to }
                }
            },
            From = new Recipient
            {
                EmailAddress = new EmailAddress { Address = _senderEmail }
            },
            Sender = new Recipient
            {
                EmailAddress = new EmailAddress { Address = _senderEmail }
            }
        };

        await _graphClient.Users[_senderEmail].SendMail.PostAsync(
            new SendMailPostRequestBody
            {
                Message = message,
                SaveToSentItems = true
            });
    }
}