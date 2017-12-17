using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    class Utiles
    {
        private Brush[] GenerateRandomBrushes(int size)
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            var indexes = Enumerable.Range(0, size).OrderBy(Guid => new Guid());
            var indexesArray = indexes.ToArray();
            Brush[] results = new Brush[size];
            for (int i = 0; i < size; i++)
            {
                results[i] = (Brush)properties[indexesArray[i]].GetValue(null, null);
            }  

            return results;
        }
    }
}
