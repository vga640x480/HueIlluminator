using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Sony.TS.Brainwave
{
    [DataContract]
    public class BrainwaveData
    {
        string _timestamp_string;
        DateTime _timestamp;

        [DataMember]
        public double alpha;

        [DataMember]
        public double beta;

        [DataMember]
        public double theta;

        [DataMember(Name = "timestamp")]
        private string timestamp_string
        {
            get
            {
                return _timestamp_string;
            }
            set
            {
                _timestamp = DateTime.Parse(value);
                _timestamp_string = value;
            }
        }

        public DateTime timestamp
        {
            get
            {
                return _timestamp;
            }
            private set
            {
                _timestamp = value;
            }
        }

    
    }
}
