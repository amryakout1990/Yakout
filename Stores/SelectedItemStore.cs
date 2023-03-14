using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yakout.Models;

namespace Yakout.Stores
{
    public class SelectedItemStore
    {
        private ItemStore _SelectedItem;

        public ItemStore SelectedItem
        {
            get
            {
                if (_SelectedItem == null)
                {
                    _SelectedItem = new ItemStore();
                }
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                /// ممكن نكتب الحدث بدون ميثود
                /// لما القيمة تتغير
                /// بيعمل فيرنج للحدث
                /// و دا بستغلة في عمل ريفريش للخصائص المربوطة
                /// و اقدر انشط البروبيرتي تشانج الموجدة في الفيو مودل بيز
                 SelectedItemChanged?.Invoke();
            }
        }

        public event Action SelectedItemChanged;

        //when firing it  will grap the property

    }

}
