using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ElevatorSimulator
{
    class Human
    {
        public enum Direction { up, nowhere, down };
        public Direction direction;
        int weight;
        int startFloor;
        int endFloor;
        int buildingNumber;
        bool selected;
        Point position;
        Floor floor;
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
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value > 0)
                {
                    weight = value;
                }
            }
        }
        public int StartFloor
        {
            get
            {
                return startFloor;
            }
            set
            {
                if (value > 0)
                {
                    startFloor = value;
                }
            }
        }
        public int EndFloor
        {
            get
            {
                return endFloor;
            }
            set
            {
                if (value > 0)
                {
                    endFloor = value;
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
        public Point Position
        {
            get
            {
                return new Point(buildingNumber*20, startFloor * 20);
            }
            set
            {
                position = value;
            }
        }
        public Human (Direction direction, int weight, int startFloor, int endFloor, int buildingNumber, Floor floor, bool selected)
        {
            this.Selected = selected;
            this.floor = floor;
            this.direction = direction;
            this.Weight = weight;
            this.StartFloor = startFloor;
            this.EndFloor = endFloor;
            this.BuildingNumber = buildingNumber;
        }
    }
}
