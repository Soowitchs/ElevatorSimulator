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
        int numberOfElevators;
        int buildingNumber;
        int width;
        Random rnd;
        Human human;
        Elevator elevator;
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
            set{}
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
        public Building (int numberOfFloors, double lenghtOfFloors, double lenghtOfMezzanineFloor, int numberOfElevators, int buildingNumber)
        {
            this.width = 20;
            this.BuildingNumber = buildingNumber;
            for (int i = 1; i <= numberOfFloors; i++)
            {
                floor = new Floor(i, BuildingNumber, lenghtOfFloors, lenghtOfMezzanineFloor);
                floorList.Add(floor);
            }
            Point position = new Point(20*buildingNumber,20);
            int j = 1;
            for (int i = 1; i <= numberOfElevators; i++)
            {
                elevator = new Elevator(1000, 10, 10, 1, 1, Human.Direction.nowhere, 1, position, Elevator.Door.open, Elevator.Lock.unlocked, j);
                elevatorList.Add(elevator);
                position.X += 20;
                j++;
                Thread.Sleep(15);
            }
            //MessageBox.Show("Floors: " + NumberOfFloors + "\n" + "Elevators: " + NumberOfElevators + "\n" + "Lenght of one floor: " + floor.LenghtOfOneFloor() + "\n" +  "Total lenght: " + TotalLenght + "\n");
        }
        public void GenerateHuman()
        {
            rnd = new Random();
            Thread.Sleep(15);
            human = new Human(Human.Direction.down, 50, rnd.Next(1, NumberOfFloors), rnd.Next(1, NumberOfFloors), buildingNumber, floor);
            elevator.humanList.Add(human);
            //MessageBox.Show(this.humanList.First().Position.ToString() + " " + rnd.Next(1, this.NumberOfElevators).ToString());
        }
        public Building()
        {

        }
    }
}
