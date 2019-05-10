using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iut.GestionCaisseInterBDE.Wpf.Utilities
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> expression)
        {
            string propertyName = GetPropertyName(expression);
            OnPropertyChanged(propertyName);
        }

        private string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            var lambdaExpression = (LambdaExpression)expression;
            MemberExpression memberExpression;
            if (lambdaExpression.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambdaExpression.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambdaExpression.Body;
            }
            return memberExpression.Member.Name;
        }

        protected bool SetProperty<T>(ref T member, T newValue, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(member, newValue))
            {
                return false;
            }
            member = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected bool SetProperty<T>(ref T member, T newValue, Expression<Func<T>> expression)
        {
            if(EqualityComparer<T>.Default.Equals(member, newValue))
            {
                return false;
            }
            member = newValue;
            OnPropertyChanged(GetPropertyName(expression));
            return true;
        }
    }
}