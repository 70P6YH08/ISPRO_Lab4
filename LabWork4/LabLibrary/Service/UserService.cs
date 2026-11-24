using LabLibrary.Contexts;
using LabLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabLibrary.Services
{
    public class UserService
    {
        private readonly PractWorkDbContext _context = new();
        public async Task<List<User>> GetUsersAsync() => _context.Users.ToListAsync();

        public async Task AddUserAsync(User user) { 
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user is null)
                return;

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task UpdateUserAsync(User user)
        {
            var selectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (user is null)
                return;

            selectedUser.Email = user.Email;
            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.Email = user.Email;
            selectedUser.Login = user.Login;
            selectedUser.Password = user.Password;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();
        }
    }
}
