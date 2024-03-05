﻿using Microsoft.EntityFrameworkCore;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;

namespace RunGroopApp.Services
{
    public class ClubService : IClubService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Club> _clubRepo;
        private readonly ILoggerManager _logger;


        public ClubService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _clubRepo= _unitOfWork.GetRepository<Club>();

        }
        public async Task<IReadOnlyList<Club>> GetAllClubsAsync()
        {
            var clubs = await _clubRepo.ListAllAsync();
            return clubs;
        }

        public async  Task<Club> GetClubByIdAsync(int id)
        {           
            var club = await _clubRepo.GetFirstOrDefaultAsync(p => p.Id == id, include: query => query.Include(x => x.Address));
            if (club == null) _logger.LogError($"Failed to retrieve club with an {id} with an address");  
            return club;
        }
    }
}
