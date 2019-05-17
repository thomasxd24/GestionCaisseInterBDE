using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Utilities
{
    public abstract class BaseViewModel : ObservableObject
    {
    }
    
    public abstract class BaseViewModel<TModel> : BaseViewModel
    {
        public TModel Model
        {
            get
            {
                return mModel;
            }
            set
            {
                SetProperty(ref mModel, value, () => Model);
            }
        }
        TModel mModel;
    }
}
