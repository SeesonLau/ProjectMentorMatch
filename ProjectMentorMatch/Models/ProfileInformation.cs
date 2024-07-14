using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMentorMatch.Models
{
    public class ProfileInformation : Profile
    {
        private int courseID;
        private int subjectTaughtID;
        private int subjectTakenID;
        private int scheduleID;
        private int addressID;

        private string? subjectTaught;
        private string? subjectTaken;
   
        private string? courseName;

        private string? day; //e.g. Monday, Tuesday, Wednesday, Thursday, Friday
        private double initialTime;
        private double finalTime;
        private string? gender;
        private string? addressCity;
        private string? addressProvince;




    }
}
