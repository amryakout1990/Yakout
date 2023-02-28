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
                OnSelectedCategoryStoreChanged();
            }
        }

        private void OnSelectedCategoryStoreChanged()
        {
            SelectedPropertyStoreChanged?.Invoke();
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
        private DataView _GridDataView;

        public DataView GridDataView
        {
            get { return _GridDataView; }
            set { _GridDataView = value; }
        }

    }

    //    //when firing it  will grap the property

}
