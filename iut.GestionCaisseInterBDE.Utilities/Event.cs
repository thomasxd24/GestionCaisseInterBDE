using System;
using System.Collections.Generic;
using System.Text;

namespace iut.GestionCaisseInterBDE.Utilities
{
    
    public class Event
    {

        public delegate void MyEventHandler(object sender);
        public event MyEventHandler OnUpdateProduct;
        public event MyEventHandler OnClearBasket;

        public void InvolveUpdateProduct()
        {
            OnUpdateProduct?.Invoke(this);
        }

        public void InvolveClearBasket()
        {
            OnClearBasket?.Invoke(this);
        }

    }
}
