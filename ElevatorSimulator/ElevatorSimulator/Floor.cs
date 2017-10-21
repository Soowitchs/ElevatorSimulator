using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ElevatorSimulator
{
    class Floor
    {
        public List<Human> humanList = new List<Human>();
        Point position;
        double lenghtOfMezzanineFloor;
        double lenghtOfFloor;
        int numberOfFloorX;
        int numberOfFloorY;
        public Point Position
        {
            get
            {
                //Dodělat pozice
                return new Point(numberOfFloorX*20, numberOfFloorY*20);
            }
        }
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
        public int NumberOfFloorX
        {
            get
            {
                return numberOfFloorX;
            }
            set
            {
                if (value > 0)
                {
                    numberOfFloorX = value;
                }
            }
        }
        public int NumberOfFloorY
        {
            get
            {
                return numberOfFloorY;
            }
            set
            {
                if (value > 0)
                {
                    numberOfFloorY = value;
                }
            }
        }
        public double LenghtOfOneFloor ()
        {
            return lenghtOfFloor + lenghtOfMezzanineFloor;
        }
        public Floor(int numberOfFloorX, int numberOfFloorY, double lenghtOfFloor, double lenghtOfMezzanineFloor)
        {
            this.NumberOfFloorX = numberOfFloorX;
            this.NumberOfFloorY = numberOfFloorY;
            this.LenghtOfMezzanineFloor = lenghtOfMezzanineFloor;
            this.LenghtOfFloor = lenghtOfFloor;
        }
    }
}
