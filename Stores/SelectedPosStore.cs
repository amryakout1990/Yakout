using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Models;

namespace Yakout.Stores
{
    class SelectedPosStore:Utilities.ViewModelBase
    {
        private PosStore _selectedPos;
        public PosStore SelectedPos
        {
            get {
                if (_selectedPos != null)
                   {
                       return _selectedPos;
                   }
                else
                  {
                    return new PosStore();
                  } 
                }
            set { _selectedPos = value; SelectedPosStoreChanged?.Invoke(); }
        }
        public event Action SelectedPosStoreChanged;

    }
}
