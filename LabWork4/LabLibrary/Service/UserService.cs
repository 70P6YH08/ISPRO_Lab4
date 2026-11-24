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
        public async Task<List<User>> GetUsersAsync() => await _context.Users.Include(u=> u.Role).ToListAsync();

        public async Task AddUserAsync(User user) { 
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            User? selectedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (user is null)
                return;

            selectedUser.FirstName = user.FirstName;
            selectedUser.LastName = user.LastName;
            selectedUser.Email = user.Email;
            selectedUser.Login = user.Login;
            selectedUser.Password = user.Password;
            selectedUser.PhoneNumber = user.PhoneNumber;
            selectedUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);

        public async Task<List<Role>> GetRolesAsync() => await _context.Roles.ToListAsync();
    }
}
