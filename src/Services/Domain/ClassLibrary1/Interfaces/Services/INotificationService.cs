﻿using PicPaySimplificado.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPaySimplificado.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(User user, string message);
    }
}
