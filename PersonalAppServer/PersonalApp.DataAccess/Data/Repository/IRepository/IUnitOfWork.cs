﻿using PersonalApp.Models.Entities;
using PersonalApp.Models.Identity;

namespace PersonalApp.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Event> Events { get; }
        IGenericRepository<Notification> Notifications { get; }
        IGenericRepository<ApplicationRole> ApplicationRoles { get; }
        IGenericRepository<QuizzTest> QuizzTest { get; }
        IGenericRepository<QuizzTopic> QuizzTopic { get; }
        IGenericRepository<QuizzMultipleChoiceQuestion> QuizzMultiplechoiceQuestion { get; }
        IGenericRepository<QuizzEssayQuestion> QuizzEssayQuestion { get; }
        IGenericRepository<GoogleImage> GoogleImage { get; }
        Task<bool> SaveChangeAsync();
    }
}
