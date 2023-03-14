using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Models
{
    class PosStore
    {
        public string PosItemId { get; set; }

        public string PosCategoryId { get; set; }

        public DataTable PosTable { get; set; }
    }
}
