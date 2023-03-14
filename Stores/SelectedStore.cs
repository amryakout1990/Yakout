using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Stores
{
    class SelectedStore:Utilities.ViewModelBase
    {
        private SelectedPropertyStore _SelectedPropertyStore;

        public SelectedPropertyStore SelectedPropertyStore
        {
            get
            {
                if (_SelectedPropertyStore == null)
                {
                    _SelectedPropertyStore = new SelectedPropertyStore();
                }
                return _SelectedPropertyStore;
            }
            set
            {
                _SelectedPropertyStore = value;
                SelectedPropertyStoreChanged?.Invoke();
            }
        }

        public event Action SelectedPropertyStoreChanged;


    }

    public class SelectedPropertyStore
    {
        private string _CategoryId;
        public string CategoryId
        {
            get { return _CategoryId; }
            set { _CategoryId = value;}
        }

        private string _ItemId;
        public string ItemId
        {
            get { return _ItemId; }
            set { _ItemId = value; }
        }

        private string _itemName;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }


        private DataTable _gridDataTable;

        public DataTable GridDataTable
        {
            get { return _gridDataTable; }
            set { _gridDataTable = value; }
        }


    }

    //    //when firing it  will grap the property

}
