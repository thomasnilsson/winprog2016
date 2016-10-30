using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace Diagram_WinProg2016.Model
{

    //Dette er en abstrakt klasse som bliver brugt til at definere INotifyPropertyChanged funktionaliteten som bliver brugt af flere af modelklasserne. 
    //Så det ikke skal defineres i hver klassefil.

    //Ideen med INotifyPropertyChanged er at informere GUI delen om at egenskaben af et bundet element har ændret sig. Således den korresponderende grafik kan blive opdateret i henhold hertil!

    public abstract class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {

            var propertyName = (propertyExpression?.Body as MemberExpression)?.Member?.Name;
            NotifyPropertyChanged(propertyName);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName != null && PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

