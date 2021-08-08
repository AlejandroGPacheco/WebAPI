using ItemData.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;

using System.Collections.Generic;

using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : Controller
    {
        private IConfiguration Configuration;
        private static ItemRepository items;
        ILogger _logger;

        
        public ItemController(IConfiguration _configuration, ILogger logger )
        {
            Configuration = _configuration;
            items = new ItemRepository(_configuration);
            _logger = logger;
 
        }

        [HttpGet]
        public List<string> GetItem()
        {
            List<string> itemList = items.GetItems();
            foreach (string x in itemList)
            {
                _logger.Information(x);
            }
            return itemList;

        }
        [HttpPost]
        public string PostItem(Item item)
        {
            _logger.Information("Item Inserted: " + item.Name + " " + item.Description);
            return items.PostItem(item);
        }

        [HttpPut]
        public string PutItem(Item item)
        {
            _logger.Information("Item Updated: " + item.Id + " " + item.Name + " " + item.Description);
            return items.PutItem(item);
        }
            
        [HttpDelete ("{id}")]

        public string DeleteItem(int id)
        {
            _logger.Information("Item with ID " + id + " has been deleted");
            return items.DeleteItem(id);
        }

        

    }

    

}
