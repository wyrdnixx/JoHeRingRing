using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoHeRingRing
{
    class Params
    {
        private string computer;

        private string user;

        private string stationFile;
        public string Computer
        {
            get
            {
                return computer;
            }

            set
            {
                computer = value;
            }
        }

        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public string StationFile
        {
            get
            {
                return stationFile;
            }

            set
            {
                stationFile = value;
            }
        }
    }
}
