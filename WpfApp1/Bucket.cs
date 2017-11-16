using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Bucket
    {
        public int Id;
        public List<PointInBucket> Points = new List<PointInBucket>();
        public List<Bucket> ConnectedBuckets = new List<Bucket>();
    }
}
