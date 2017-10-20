using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class ItemDetails
    {
        public string Title { get; set; }
        public string OriginalPrice { get; set; }
        public string SalePrice { get; set; }
        List<ItemDetails> Items = new List<ItemDetails>();

    }
}
