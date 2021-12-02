using Caliburn.Micro;
using KorisnickiInterfejs.Helpers;
using KorisnickiInterfejs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorisnickiInterfejs.Helpers
{
    //T -> <NazivEntiteta>Model
 
    public class GenericCRUDViewModel<T> : Screen
        where T : class
    {
        protected IWindowManager Manager = new WindowManager();
        protected ObservableCollection<T> _kolekcija = new ObservableCollection<T>();
        protected T _selektovano = null;

        public T Selektovano
        {
            get { return _selektovano; }
            set
            {
                _selektovano = value;
                NotifyOfPropertyChange(() => Selektovano);
            }
        }


        public ObservableCollection<T> Kolekcija
        {
            get { return _kolekcija; }
            set
            {
                _kolekcija = value;
                NotifyOfPropertyChange(() => Kolekcija);
            }
        }

        protected string _validacionaGreska = "";

        public string ValidacionaGreska
        {
            get { return _validacionaGreska; }
            set
            {
                _validacionaGreska = value;
                NotifyOfPropertyChange(() => ValidacionaGreska);
            }
        }
    }
}
