using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class PointInBucket
    {
        public int Id;
        public PointInBucket ConnectedPoint;
        public bool IsConnected;
        public PointInBucket(int id)
        {
            Id = id;
        }
        public PointInBucket()
        {
        }
    }
}
