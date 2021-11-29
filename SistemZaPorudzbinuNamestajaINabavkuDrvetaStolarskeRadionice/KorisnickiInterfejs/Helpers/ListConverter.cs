using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Helpers
{
    public static class ListConverter<T>
    {
        public static ObservableCollection<T> KonvertujListu(ICollection<T> lista)
        {
            ObservableCollection<T> ret = new ObservableCollection<T>();
            foreach (var item in lista)
                ret.Add(item);

            return ret;
        }
    }
}
