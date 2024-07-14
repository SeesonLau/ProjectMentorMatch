using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class Analytics : ProfileInformation
    {
        private int analyticsID;
        private int brainReact;
        private double points;
        private int profileID;

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
        public int GetProfileID()
        {
            return profileID;
        }
    }
}
