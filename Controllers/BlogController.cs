using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Services;
using Microsoft.AspNetCore.Mvc;


namespace blogapi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogItemService _data;
    public BlogController(BlogItemService dataFromService)
    {
        _data = dataFromService;
    }

     [HttpPost("AddBlogItem")]
     public bool AddBlogItems(BlogItemModel newBlogItem)
    {
        return _data.AddBlogItems(newBlogItem);
    }
// get all our blog items
    [HttpGet("GetBlogItems")]
    public IEnumerable<BlogItemModel> GetAllBlogItems()
    {
        return _data.GetAllBlogItems();
    }
    // GetBlogItemsByCategory
    [HttpGet("GetBlogItemByCategory/{category}")]

        public IEnumerable<BlogItemModel> GetItemsByCategory(string category)
        
    {
       return _data.GetBlogItemsByCategory(category);

    }
[HttpGet("GetItemByTag/{tag}")]
public List<BlogItemModel> GetItemsByTag(string tag)
    {
        return _data.GetItemsByTag(tag);
    }
    [HttpGet("GetItemsByData/{date}")]
    public IEnumerable<BlogItemModel>GetItemsByDate(string date)
    {
        return _data.GetItemsByDate(date);

    }
    [HttpPut("blogUpdate")]
public bool UpdateBlogItems(BlogItemModel blogUpdate)
    {
        return _data.UpdateBlogItems(blogUpdate);
    }
    [HttpPut("DeleteBlogItem/{BlogToDelete}")]
    public bool DeleteBlogItem(BlogItemModel BlogItemToDelete)
    {
        return _data.DeleteBlogItem(BlogItemToDelete);
    }
    [HttpGet("GetPublishedItems")]
public IEnumerable<BlogItemModel> GetPublishedItems()
    {
        return _data.GetPublishedItems();
    }

    }
