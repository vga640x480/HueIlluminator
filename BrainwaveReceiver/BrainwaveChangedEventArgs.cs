using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sony.TS.Brainwave
{
    public class BrainwaveChangedEventArgs
    {
        public DateTime Timestamp
        {
            get;
            set;
        }

        public BrainwaveData Data
        {
            get;
            set;
        }
    }
}
