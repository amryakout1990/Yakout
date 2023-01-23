using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yakout.Utilities
{
    public class ViewModelBase : INotifyPropertyChanged,IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string PropName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }

        public virtual void Dispose() { }

    }

}
