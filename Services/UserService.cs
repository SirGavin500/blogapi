using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using blogapiLecture.Services.Context;

namespace blogapi.Services;

public class UserService
{
    private readonly DataContext _context;
    public UserService(DataContext context)
    {
        _context = context;
    }

    public bool DoesUserExist( string username)
    {
        // Check our tabeles to see if the user exists if it does return true if not return false
return _context.UserInfo.SingleOrDefault(user => user.Username == username) != null;
    }
        
    public bool AddUser(CreateAccountDTO userToAdd)
    {
bool result = false;
if (userToAdd.Username != null && !DoesUserExist(userToAdd.Username))
        {
            UserModel newUser = new UserModel();
            var HashedPassword = HashPassword(userToAdd.Password);

            newUser.Id = userToAdd.Id;
            newUser.Username = userToAdd.Username;

            newUser.Salt = HashedPassword.Salt;
            newUser.Hash = HashedPassword.Hash;
            _context.Add(newUser);
           result = _context.SaveChanges() !=  0;

        }
        return result;
        // We are going to need hash helper function help us hash our password
        //  we need to set out newuser.id - UserToAdd.id

        // username
        // salt 
        // hash

        // then we add it to our datacontext

        // save our changes
        // return a bool to return true or false

        // Function that will help our password

        
    }

    public PasswordDTO HashPassword(string? password)
    {
        PasswordDTO newHashedPassword = new PasswordDTO();

        byte[] SaltBytes = new byte[64];

        var provider = RandomNumberGenerator.Create();
        provider.GetNonZeroBytes(SaltBytes);

        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

        var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

        var Salt = Convert.ToBase64String(SaltBytes);

        newHashedPassword.Salt = Salt;
        newHashedPassword.Hash = Hash;

        return newHashedPassword;
    }
    public bool verifyuserPassword(string? Password, string? StoredHash, string? StoredSalt)
    {
        var SaltBytes = Convert.FromBase64String(StoredSalt);

        var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password ?? "", SaltBytes, 10000, HashAlgorithmName.SHA256);

var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
        return newHash == StoredHash;
    }
    
}
