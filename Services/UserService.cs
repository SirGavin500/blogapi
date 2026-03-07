using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using blogapiLecture.Models.DTO;
using blogapiLecture.Services.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace blogapi.Services;

public class UserService : ControllerBase
{
    private readonly DataContext _context;
    public UserService(DataContext context)
    {
        _context = context;
    }
// we need a method to check if a user exists in our database
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

    public IEnumerable<UserModel> GetAllUsers()
    {
        return _context.UserInfo;
    }
// GetAllUsersDataByUsername
public UserModel GetAllUserDataByUsername(string username)
    {
        return _context.UserInfo.FirstOrDefault(user => user.Username == username);
    }


// Login Function
    public IActionResult Login(LoginDTO user)
    {
            IActionResult result = Unauthorized();
        // if user exists
        if (DoesUserExist(user.Username))
        {

UserModel foundUser = GetAllUserDataByUsername(user.Username);
            if(verifyuserPassword(user.Password, foundUser.Hash, foundUser.Salt))
            {
                
            //  create a secret key used to sign the jtw token
            // this should be stored securely (Not hardcoded in production)
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supadupasecurepasswordcucdadsadadasdadasdasdasdash"));
            // Create signing credentials using the secret key and HMACSHA256 algorithm

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            // cant be tampered with
            
            // build the jwt with toke metadata

            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience:"https://localhost:5001",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            // return thr token as JSON to the client 
            result = Ok(new {Token = tokenString});
            }
            
        }
        // Return either the token (if the user exists) or unauthorized  if user does not exist
        return result;
    }

    internal UserIdDTO GetUserIdDTOByUserName(string username)
    {
        throw new NotImplementedException();
    }

    // Helper function to help us find a user 
    public UserModel GetUserByUsername(string username)
    {
        return _context.UserInfo.SingleOrDefault(user=> user.Username == username);
    }
// Delete User
    public bool DeleteUser(string userToDelete)
    {
        UserModel foundUser = GetUserByUsername(userToDelete);
        bool result = false;
    if (foundUser != null)
        {
            foundUser.Username = userToDelete;
            _context.Remove(foundUser);
            result = _context.SaveChanges() != 0;


        }
        return result;
    }

public UserModel GetUserById(int id)
    {
        return _context.UserInfo.SingleOrDefault(user => user.Id == id);
    }
    public bool UpdateUser(int id, string username)
    {
        UserModel foundUser = GetUserById(id);
        bool result = false;

        if (foundUser != null){
            foundUser.Username = username;
            _context.Update(foundUser);
            result = _context.SaveChanges() !=0;
    }
    return result;
}
}