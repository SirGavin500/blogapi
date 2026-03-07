using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using blogapi.Services;
using blogapiLecture.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace blogapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _data;

    public UserController(UserService dataFromService)
    {
        _data = dataFromService;
    }

    // Function to add our user type of CreateAccountsDTO call UserToAdd this will return a bool once user is added
    // Add user

    [HttpPost("AddUser")]
    public bool AddUser(CreateAccountDTO UserToAdd)
    {
        return _data.AddUser(UserToAdd);
    }

    [HttpGet("GetAllUsers")]

    public IEnumerable<UserModel> GetAllUsers()
    {
        return _data.GetAllUsers();
    }
// Login Endpoint
    [HttpPost("Login")]

       public IActionResult Login([FromBody] LoginDTO User)
    {
        return _data.Login(User);
    }



    // GetUserByUsername

    [HttpGet("GetUserByUsername")]


    public UserIdDTO GetUserDTOUserName(string username)
    {
        return _data.GetUserIdDTOByUserName(username);
    }
// Delete a user
[HttpPost("DeleteUser/{userToDelete}")]

public bool DeleteUser(string userToDelete)
    {
       return _data.DeleteUser(userToDelete);
    }

   public bool UpdateUser(int id, string username)
    {
        return _data.UpdateUser(id, username);
    }
// Get Published blog items



}
