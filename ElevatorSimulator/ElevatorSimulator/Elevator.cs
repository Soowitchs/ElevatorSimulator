using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Elevator
    {
        public enum Door { open, closed}
        public enum Lock { unlocked, locked}
        int maxLoad;
        int maxCapacity;
        int maxSpeed;
        int acceleration;
        int floor;
        int capacity;
        int weight;
        double position;
        Human.Direction direction;
        Door door;
        List<Floor> queue = new List<Floor>();
        Lock locked;
        #region Properties
        public void AddWeight(int weight)
        {
            this.weight += weight;
        }
        public void AddCapacity ()
        {
            capacity++;
        }
        public void RemoveWeight(int weight)
        {
            this.weight -= weight;
        }
        public void RemoveCapacity()
        {
            capacity--;
        }
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value >= 0)
                {
                    weight = value;
                }
            }
        }
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value > 0)
                {
                    capacity = value;
                }
            }
        }
        public int Acceleration
        {
            get
            {
                return acceleration;
            }
            set
            {
                if (value > 0)
                {
                    acceleration = value;
                }
            }
        }
        public int MaxLoad
        {
            get
            {
                return maxLoad;
            }
            set
            {
                if (value > 0)
                {
                    maxLoad = value;
                }
            }
        }
        public int MaxCapacity
        {
            get
            {
                return maxCapacity;
            }
            set
            {
                if (value > 0)
                {
                    maxCapacity = value;
                }
            }
        }
        public int MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                if (value > 0)
                {
                    maxSpeed = value;
                }
            }
        }
        public int Floor
        {
            get
            {
                return floor;
            }
            set
            {
                if (value > 0)
                {
                    floor = value;
                }
            }
        }
        public double Position
        {
            get
            {
                return position;
            }
            set
            {
                if (value >= 0)
                {
                    position = value;
                }
            }
        }
        
        #endregion
        public Elevator (int maxLoad, int maxCapacity, int maxSpeed, int acceleration, int floor, Human.Direction direction, int weight, double position, Door door, Lock locked)
        {
            this.MaxLoad = maxLoad;
            this.MaxCapacity = maxCapacity;
            this.MaxSpeed = maxSpeed;
            this.Acceleration = acceleration;
            this.Floor = floor;

        }
    }
}
