﻿using Api.Dtos;
using Api.Models;
using CorePlusAngular.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    public class AuthRepository:IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            this._context = context; 
        }
        public async Task<User>Login(string username,string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
              var  computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHast(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
               
            await _context.SaveChangesAsync();
           
            return user;
            
        }

        private void CreatePasswordHast(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == username))
                return true;

            return false;
        }
        public async Task<User> GetUser(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
          
            return user;
        }
        public  void Update(User user)
        {
           if(user != null)
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
