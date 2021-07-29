using ItemData.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ItemController : Controller

    {
        private IConfiguration Configuration;
        private static ItemRepository items;
        

        public ItemController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            items = new ItemRepository(_configuration);
        }

        [HttpGet]
        public void GetItem()
        {
            items.GetItems();
        }
        [HttpPost]
        public string PostItem(Item item)
        {         
            return items.PostItem(item);
        }

        [HttpPut]
        public string PutItem(Item item)
        {
            return items.PutItem(item);
        }
            
        [HttpDelete]

        public string DeleteItem(Item item)
        {
            return items.DeleteItem(item);
        }
    }
        
}
