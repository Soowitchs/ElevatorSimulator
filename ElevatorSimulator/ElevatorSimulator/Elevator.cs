using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSimulator
{
    class Elevator
    {
        int number = 0;
        public enum Door { open, closed}
        public enum Lock { unlocked, locked}
        int step = 0;
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
        List<int> queue;
        Lock locked;
        Random rnd = new Random();
        Timer timer = new Timer();
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
        public int Rnd(Building building)
        {
            return rnd.Next(1, building.NumberOfFloors);
        }
        public string FloorCheck(Building building)
        {
            if (queue.Count < 10)
            {
                this.queue.Add(Rnd(building)); 
            }
            string output = "";
            foreach (int queueFloors in queue)
            {
                output += queueFloors + ";";
            }
            if (this.queue.First() == this.floor)
            {
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Start();
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + this.queue.First() + ", All queue: " + output);
            }
            else if (this.queue.First() > this.floor)
            {
                if (this.queue.First() - this.floor == 1)
                {
                    this.ElevatorStepUp((int)building.LenghtOfOneFloor, this.queue.First(), 1);
                }
                else
                {
                    this.ElevatorStepUp((int)building.LenghtOfOneFloor, this.queue.First(), 5);
                }
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + this.queue.First() + ", All queue: " + output);
            }
            else if (this.queue.First() < this.floor)
            {
                if (this.floor - this.queue.First() == 1)
                {
                    this.ElevatorStepDown((int)building.LenghtOfOneFloor, this.queue.First(), 1);
                }
                else
                {
                this.ElevatorStepDown((int)building.LenghtOfOneFloor, this.queue.First(), 5);
                }
                return ("Výtah číslo: " + this.Number + ", patro: " + this.Floor + ". pozice: " + this.Position.ToString() + ", queue: " + this.queue.First() + ", All queue: " + output);
            }
            return "";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.queue.Remove(this.queue.First());
            timer.Stop();
        }

        public void ElevatorStepUp(int floorHeight, int buildingFloor, int speed)
        {
            if (this.floor < buildingFloor)
            {
                this.position.Y += speed;
                step += speed;
                if (step == floorHeight)
                {
                    step = 0;
                    floor++;  
                }
            }
        }
        public void ElevatorStepDown(int floorHeight, int buildingFloor, int speed)
        {
            if (this.floor > buildingFloor)
            {
                this.position.Y -= speed;
                step += speed;
                if (step == floorHeight)
                {
                    step = 0;
                    floor--;
                }
            }
        }
        public Elevator (int maxLoad, int maxCapacity, int maxSpeed, int acceleration, int floor, Human.Direction direction, int weight, Point position, Door door, Lock locked, List<int> queue, int number)
        {
            queue = new List<int>();
            queue.Add(4);
            this.number = number;
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
        public override string ToString()
        {
            return "Výtah číslo: " + this.number;
        }
    }
}
