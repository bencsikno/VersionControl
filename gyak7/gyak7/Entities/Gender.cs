using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace gyak7.Entities
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }
    public class Person
    {
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public int NbrOfChildren { get; set; }
        public bool IsAlive { get; set; }

        public Person()
        {
            IsAlive = true;
        }
    }
    public class BirthProbability
    {
        public double BirthProbabilities { get; set; }
        public int Age { get; set; }
        public int NbrOfChildren { get; set; }



    }
    public class DeathProbability
    {
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public double DeathProbabilities { get; set; }
    }
   

}

