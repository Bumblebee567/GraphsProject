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
        public static Brush[] GenerateRandomBrushes(int size)
        {
            //Brush result = Brushes.Transparent;

            //Random rnd = new Random();

            //Type brushesType = typeof(Brushes);

            //PropertyInfo[] properties = brushesType.GetProperties();

            //var indexes = Enumerable.Range(0, size).OrderBy(Guid => new Guid());
            //var indexesArray = indexes.ToArray();
            //Brush[] results = new Brush[size];
            //for (int i = 0; i < size; i++)
            //{
            //    results[i] = (Brush)properties[indexesArray[i]].GetValue(null, null);
            //}  

            //return results;
            Brush[] results = new Brush[] 
            {
                Brushes.Red,
                Brushes.Blue,
                Brushes.Green,
                Brushes.Cyan,
                Brushes.Brown,
                Brushes.Chocolate,
                Brushes.DarkGray,
                Brushes.DarkKhaki,
                Brushes.DarkViolet,
                Brushes.Fuchsia,
                Brushes.Gold,
                Brushes.GreenYellow,
                Brushes.Honeydew,
                Brushes.HotPink,
                Brushes.Ivory,
                Brushes.Khaki,
                Brushes.LightBlue,
                Brushes.LightYellow,
                Brushes.Linen,
                Brushes.Orange
            };
            return results.Take(size).ToArray();
        }
    }
}
