﻿using System.Threading.Tasks;

namespace ShareMe.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
