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
        void GetItems();
        string PostItem(Item item);
        string PutItem(Item item);
        string DeleteItem(Item item);
    }
}
