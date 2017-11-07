using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ElevatorSimulator
{
    class Elevator
    {
        int number = 0;
        public enum Door { open, closed}
        public enum Lock { unlocked, locked}
        int width;
        int step = 0;
        int maxLoad;
        int maxCapacity;
        int maxSpeed;
        int acceleration;
        int floor;
        int capacity;
        int weight;
        int fixus;
        Building building;
        Point position;
        Human.Direction direction;
        public Door door;
        public List<Human> humanList = new List<Human>();
        Lock locked;
        Random rnd = new Random();
        #region Properties
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                if (value > 0)
                {
                    width = value;
                }
            }
        }
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
        public List<int> Queue
        {
            get
            {
                if (humanList.Count() == 0)
                {
                    List<int> output = new List<int>();
                    output.Add(1);
                    return output;
                }
                else
                {
                    List<int> output = new List<int>();
                    foreach (Human human in humanList)
                    {
                        output.Add(human.StartFloor);
                        output.Add(human.EndFloor);
                    }
                    List<int> output2 = new List<int>();
                    string text = "";
                    foreach (Human human in humanList)
                    {
                        foreach (Human human2 in humanList)
                        {
                            if (human2.StartFloor < human.StartFloor && human2.StartFloor > human.EndFloor)
                            {
                                output2.Add(human2.StartFloor);
                                output2.Add(human.StartFloor);
                            }
                        }
                    }
                    return output;
                }
            }
            set
            {
                Queue = value;
            }
        }
        public string Output()
        {
            string output = "";
            foreach (int queueFloors in Queue)
            {
                output += queueFloors + ";";
            }
            if (Queue.Count > 1)
            {

                return (this.ToString() + ", Floor: " + this.Floor + ". Position: " + this.Position.ToString() + ", Queue: " + this.Queue.First() + ", All Queue: " + output); 
            }
            return "";
        }
        public async void FloorCheck(Building building)
        {
            if (this.Queue.Count >= 2 && this.door == Elevator.Door.closed)
            {
                if (this.Queue.First() == this.floor)
                {
                    this.downscale();
                    await Task.Delay(1500);
                    if (humanList.First().StartFloor == this.floor)
                    {
                        humanList.First().Selected = true;
                        humanList.First().StartFloor = humanList.First().EndFloor;
                    }
                    if (humanList.First().EndFloor == this.floor)
                    {
                        humanList.Remove(humanList.First());
                        if (this.Queue.First() < this.floor)
                        {
                            fixus = this.floor - 1;
                        }
                        else if (this.Queue.First() > this.floor)
                        {
                            fixus    = this.floor + 1;
                        }
                    }
                    this.upscale();
                    await Task.Delay(1500);
                }
                else if (this.Queue.First() > this.floor)
                {
                    if (this.Queue.First() - this.floor == 1 || fixus - this.floor == 1)
                    {
                        this.ElevatorStepUp((int)building.LenghtOfOneFloor, this.Queue.First(), 1);
                    }
                    else
                    {
                        this.ElevatorStepUp((int)building.LenghtOfOneFloor, this.Queue.First(), 2);
                    }
                }
                else if (this.Queue.First() < this.floor)
                {
                    if (this.floor - this.Queue.First() == 1 || this.floor - fixus == 1)
                    {
                        this.ElevatorStepDown((int)building.LenghtOfOneFloor, this.Queue.First(), 1);
                    }
                    else
                    {
                        this.ElevatorStepDown((int)building.LenghtOfOneFloor, this.Queue.First(), 2);
                    }
                } 
            }
        }

        public void ElevatorStepUp(int floorHeight, int buildingFloor, int speed)
        {
            if (this.floor < buildingFloor || this.Position.Y < this.floor*building.LenghtOfOneFloor)
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
            if (this.floor > buildingFloor || this.Position.Y > this.floor * building.LenghtOfOneFloor)
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
        public Elevator (int maxLoad, int maxCapacity, int maxSpeed, int acceleration, int floor, Human.Direction direction, int weight, Point position, Door door, Lock locked, int number, Building building, int width)
        {
            this.Width = width;
            this.building = building;
            this.Number = number;
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
        }
        public override string ToString()
        {
            return "Elevator number: " + this.number;
        }
        public async void downscale()
        {
            this.door = Elevator.Door.open;
            for (int i = 19; i > 0; i--)
            {
                this.width--;
                await Task.Delay(50);
            }
        }
        public async void upscale()
        {
            for (int i = 0; i < 19; i++)
            {
                this.width++;
                await Task.Delay(50);
            }
            this.door = Elevator.Door.closed;
        }
    }
}
