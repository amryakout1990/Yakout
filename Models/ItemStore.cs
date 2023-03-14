using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Models
{
    public class ItemStore
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string Price { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Notes { get; set; }

    }

}
