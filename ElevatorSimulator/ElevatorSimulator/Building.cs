using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ElevatorSimulator
{
    class Building
    {
        public List<Floor> floorList = new List<Floor>();
        public List<Elevator> elevatorList = new List<Elevator>();
        public Point position;
        Floor floor;
        double totalLenght;
        int buildingNumber;
        int width;
        Random rnd;
        Human human;
        Elevator elevator;
        bool selected;
        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
        public Point Position
        {
            get
            {
                return new Point(this.buildingNumber * this.width, 10);
            }
            set
            {
                position = value;
            }
        }
        public double LenghtOfOneFloor
        {
            get
            {
                return floor.LenghtOfFloor;
            }
            set
            {

            }
        }
        public int NumberOfFloors
        {
            get
            {
                return floorList.Count();
            }
            set
            {

            }
        }
        public double TotalLenght
        {
            get
            {
                return NumberOfFloors * floor.LenghtOfOneFloor() - floor.LenghtOfMezzanineFloor;
            }
            set
            {
                if (value > 0)
                {
                    totalLenght = value; 
                }
            }
        }
        public int NumberOfElevators
        {
            get
            {
                return elevatorList.Count;
            }
            set
            {

            }
        }
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
        public int BuildingNumber
        {
            get
            {
                return buildingNumber;
            }
            set
            {
                if (value > 0)
                {
                    buildingNumber = value;
                }
            }
        }
        public Building (int numberOfFloors, double lenghtOfFloors, double lenghtOfMezzanineFloor, int numberOfElevators, int buildingNumber, bool selected)
        {
            this.Selected = selected;
            this.width = 20;
            this.BuildingNumber = buildingNumber;
            for (int i = 1; i <= numberOfFloors; i++)
            {
                floor = new Floor(i, BuildingNumber, lenghtOfFloors, lenghtOfMezzanineFloor);
                floorList.Add(floor);
            }
            Point position = new Point(20*buildingNumber,20);
            for (int i = 1; i <= numberOfElevators; i++)
            {
                elevator = new Elevator(1000, 10, 10, 1, 1, Human.Direction.nowhere, 1, position, Elevator.Door.closed ,Elevator.Lock.unlocked, buildingNumber, this, 19);
                elevatorList.Add(elevator);
                position.X += 20;
            }
            //MessageBox.Show("Floors: " + NumberOfFloors + "\n" + "Elevators: " + NumberOfElevators + "\n" + "Lenght of one floor: " + floor.LenghtOfOneFloor() + "\n" +  "Total lenght: " + TotalLenght + "\n");
        }
        public void GenerateHuman()
        {
            rnd = new Random();
            //human = new Human(Human.Direction.down, 50, 4, 4, buildingNumber, floor);
            human = new Human(Human.Direction.down, 50, rnd.Next(1, NumberOfFloors + 1), rnd.Next(1, NumberOfFloors + 1), buildingNumber, floor, false);
            elevator.humanList.Add(human);
            //MessageBox.Show(this.humanList.First().Position.ToString() + " " + rnd.Next(1, this.NumberOfElevators).ToString());
        }
        public Building()
        {

        }
        public override string ToString()
        {
            return "Building number: " + buildingNumber;
        }
    }
}
