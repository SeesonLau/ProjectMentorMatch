using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Analytics : ProfileInformation
    {
        private int analyticsID = RandomID.analyticsID();
        private int brainReact = 0;
        private double points = 0;
        private int profileID = 0;

        public int GetAnalyticsID()
        {
            return analyticsID;
        }
        public int GetBrainReact()
        {
            return brainReact;
        }
        public double GetPoints()
        {
            return points;
        }
    }
}
