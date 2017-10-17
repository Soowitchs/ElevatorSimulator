using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulator
{
    class Human
    {
        public enum Direction { up, nowhere, down };
        public Direction direction;
        int weight;
        int startFloor;
        int endFloor;
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
        public Human (Direction direction, int weight, int startFloot, int endFloor)
        {
            this.direction = direction;
            this.Weight = weight;
            this.StartFloor = startFloor;
            this.EndFloor = endFloor;
        }
    }
}
