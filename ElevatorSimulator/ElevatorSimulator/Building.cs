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
        List<Floor> floorList = new List<Floor>();
        List<int> ahoj = new List<int>();
        public List<Elevator> elevatorList = new List<Elevator>();
        Floor floor;
        double totalLenght;
        int numberOfElevators;
        public string queue
        {
            get
            {
                string output = "";
                foreach (int item in ahoj)
                {
                    output += item.ToString() + ";";
                }
                return output;
            }
            set
            {
                ahoj.Add(Convert.ToInt32(value));
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
        public Building (int numberOfFloors, double lenghtOfFloors, double lenghtOfMezzanineFloor, int numberOfElevators)
        {
            for (int i = 1; i <= numberOfFloors; i++)
            {
                floor = new Floor(i, lenghtOfFloors, lenghtOfMezzanineFloor);
                floorList.Add(floor);
            }
            Point position = new Point(10,10);
            for (int i = 1; i <= numberOfElevators; i++)
            {
                Elevator elevator = new Elevator(1000, 10, 10, 1, 1, Human.Direction.nowhere, 1, position, Elevator.Door.open, Elevator.Lock.unlocked, ahoj);
                elevatorList.Add(elevator);
                position.X += 20;
                Thread.Sleep(20);
            }
            MessageBox.Show("Floors: " + NumberOfFloors + "\n" + "Elevators: " + NumberOfElevators + "\n" + "Lenght of one floor: " + floor.LenghtOfOneFloor() + "\n" +  "Total lenght: " + TotalLenght + "\n");
        }
    }
}
