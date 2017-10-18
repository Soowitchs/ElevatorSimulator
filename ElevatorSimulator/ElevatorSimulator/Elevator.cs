using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Elevator
    {
        int number;
        public enum Door { open, closed}
        public enum Lock { unlocked, locked}
        int maxLoad;
        int maxCapacity;
        int maxSpeed;
        int acceleration;
        int floor;
        int capacity;
        int weight;
        Point position;
        Human.Direction direction;
        Door door;
        List<int> queue = new List<int>();
        Lock locked;
        #region Properties
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value > 0)
                {
                    number = value;
                }
            }
        }
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
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        
        #endregion
        public string FloorCheck(Building building)
        {
            Random rnd = new Random();
            queue.Add(rnd.Next(1, building.NumberOfFloors));
            if (queue.First() == this.floor)
            {
                queue.Remove(queue.First());
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + queue.First());
            }
            else if (queue.First() > this.floor)
            {
                this.ElevatorStepUp((int)building.LenghtOfOneFloor, queue.First());
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + queue.First());
            }
            else if (queue.First() < this.floor)
            {
                this.ElevatorStepDown((int)building.LenghtOfOneFloor, queue.First());
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + queue.First());
            }
            return "";
        }
        public void ElevatorStepUp(int floorHeight, int buildingFloor)
        {
            if (this.floor < buildingFloor)
            {
                this.position.Y += floorHeight;
                floor++; 
            }
        }
        
        public void ElevatorStepDown(int floorHeight, int buildingFloor)
        {
            if (this.floor > buildingFloor)
            {

                this.position.Y -= floorHeight;
                floor--;
            }
        }
        public Elevator (int maxLoad, int maxCapacity, int maxSpeed, int acceleration, int floor, Human.Direction direction, int weight, Point position, Door door, Lock locked, List<int> queue)
        {
            this.MaxLoad = maxLoad;
            this.MaxCapacity = maxCapacity;
            this.MaxSpeed = maxSpeed;
            this.Acceleration = acceleration;
            this.Floor = floor;
            this.direction = direction;
            this.weight = weight;
            this.position = position;
            this.door = door;
            this.locked = locked;
            this.queue = queue;
        }
    }
}
