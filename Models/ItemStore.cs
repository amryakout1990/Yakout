using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Models
{
    public class ItemStore
    {
        private int _ItemId;

        public int ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        private int _CategoryId;
        public int CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value; }
        }

        private string _CategoryName;
        public string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }
        private string _Price;
        public string Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private string _Notes;
        public string Notes
        {
            get { return _Notes; }
            set { _Notes = value; }
        }
        private byte[] _Image;

        public byte[] Image
        {
            get { return _Image; }
            set { _Image = value; }
        }


    }

}
