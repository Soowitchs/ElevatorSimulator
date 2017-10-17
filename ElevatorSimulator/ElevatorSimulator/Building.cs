using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSimulator
{
    class Building
    {
        List<Floor> floorList = new List<Floor>();
        List<Elevator> elevatorList = new List<Elevator>();
        Floor floor;
        double totalLenght;
        int numberOfElevators;
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
            for (int i = 1; i <= numberOfElevators; i++)
            {
                Elevator elevator = new Elevator(1000, 10, 10, 1, 1, Human.Direction.nowhere, 1, 1, Elevator.Door.open, Elevator.Lock.unlocked);
                elevatorList.Add(elevator);
            }
            MessageBox.Show("Floors: " + NumberOfFloors + "\n" + "Elevators: " + elevatorList.Count() + "\n" + "Lenght of one floor: " + floor.LenghtOfOneFloor() + "\n" +  "Total lenght: " + TotalLenght + "\n");
        }
    }
}
