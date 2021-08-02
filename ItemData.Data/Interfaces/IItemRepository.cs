using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace ItemData.Data.Interfaces
{
    interface IItemRepository
    {
        List<string> GetItems();
        string PostItem(Item item);
        string PutItem(Item item);
        string DeleteItem(int id);
    }
}
