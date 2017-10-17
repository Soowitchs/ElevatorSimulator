using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Floor
    {
        double lenghtOfMezzanineFloor;
        double lenghtOfFloor;
        int numberOfFloor;
        public double LenghtOfMezzanineFloor
        {
            get
            {
                return lenghtOfMezzanineFloor;
            }
            set
            {
                if (value > 0)
                {
                    lenghtOfMezzanineFloor = value;
                }
            }
        }
        public double LenghtOfFloor
        {
            get
            {
                return lenghtOfFloor;
            }
            set
            {
                if (value > 0)
                {
                    lenghtOfFloor = value;
                }
            }
        }
        public int NumberOfFloor
        {
            get
            {
                return numberOfFloor;
            }
            set
            {
                if (value > 0)
                {
                    numberOfFloor = value;
                }
            }
        }
        public double LenghtOfOneFloor ()
        {
            return lenghtOfFloor + lenghtOfMezzanineFloor;
        }
        public Floor(int numberOfFloor, double lenghtOfFloor, double lenghtOfMezzanineFloor)
        {
            this.NumberOfFloor = numberOfFloor;
            this.LenghtOfMezzanineFloor = lenghtOfMezzanineFloor;
            this.LenghtOfFloor = lenghtOfFloor;
        }
    }
}
