using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_CommonTools.Extensions
{
    public static class Collections
    {
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> objects)
        {
            if(observableCollection is null) throw new ArgumentNullException(nameof(observableCollection));
            if(objects is null) throw new ArgumentNullException(nameof(objects));
            foreach (var obj in objects)
                observableCollection.Add(obj);
        }
    }
}
