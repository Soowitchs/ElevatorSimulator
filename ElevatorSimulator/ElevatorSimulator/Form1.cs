using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSimulator
{

    public partial class Form1 : Form
    {
        public void log(string logText)
        {
        }
        bool firstStart = true;
        Building building;
        Human human;
        public Form1()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            building = new Building(30, 20, 1, 5);
            human = new Human(Human.Direction.down, 50, 1, 9);
            timer1.Start();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ElevatorTick(Elevator elev)
        {
            richTextBox1.Text = "";
            string output = "";
            foreach (Elevator elevator in building.elevatorList)
            {
                output += elevator.FloorCheck(building) + "       " + building.queue + "\n";
            }
            richTextBox1.Text = output;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics kp = e.Graphics;
            Rectangle floor = new Rectangle(10, 10, 20, (int)building.LenghtOfOneFloor);
            for (int i = 0; i < building.NumberOfElevators; i++)
            {
                for (int j = 0; j < building.NumberOfFloors; j++)
                {
                    kp.DrawRectangle(Pens.Black, floor);
                    floor.Y += floor.Height;
                }
                floor.X += floor.Width;
                floor.Y -= floor.Height * building.NumberOfFloors;
            }
            richTextBox1.Text = "";
            foreach (Elevator elev in building.elevatorList)
            {
                Rectangle elevator = new Rectangle(elev.Position.X, elev.Position.Y, 20, (int)building.LenghtOfOneFloor);
                kp.DrawRectangle(Pens.Red, elevator);
                //ElevatorTick(elev);
                richTextBox1.Text += elev.FloorCheck(building) + "\n";
            }
        }
    }
}
