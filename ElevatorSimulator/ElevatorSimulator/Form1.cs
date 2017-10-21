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
        Graphics kp;
        List<Building> buildingList = new List<Building>();
        Building building;
        Human human;
        Random rnd = new Random();
        public Form1()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            for (int i = 1; i <= 1; i++)
            {
                buildingList.Add(new Building(10, 20, 1, 1, i));
            }
            timer1.Start();
            timer2.Start();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void ElevatorTick(Elevator elev, Building building)
        {
            /*richTextBox1.Text = "";
            string output = "";
            foreach (Elevator elevator in building.elevatorList)
            {
                output += elevator.FloorCheck(building) + "       " + building.queue + "\n";
            }
            richTextBox1.Text = output;*/
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            richTextBox1.Text = "";
            kp = e.Graphics;
            foreach (Building building in buildingList)
            {
                this.building = building;
                foreach (Floor floors in building.floorList)
                {
                    Rectangle floor = new Rectangle(floors.Position.Y, floors.Position.X, building.Width, (int)building.LenghtOfOneFloor);
                    kp.DrawRectangle(Pens.Black, floor);
                    //richTextBox1.Text += floor.Location.ToString();
                }
                foreach (Elevator elev in building.elevatorList)
                {
                    Rectangle elevator = new Rectangle(building.Position.X+1, elev.Position.Y+1, building.Width-1, (int)building.LenghtOfOneFloor-1);
                    kp.FillRectangle(Brushes.Red, elevator);
                    richTextBox1.Text += elev.FloorCheck(building) + "\n";
                    foreach (Human human in elev.humanList)
                    {
                        Rectangle Human = new Rectangle(human.Position.X, human.Position.Y, 5, 5);
                        kp.DrawEllipse(Pens.Black, Human);
                        richTextBox1.Text += "Pozice: " + human.Position.ToString() + ", aktuální patro: " + human.StartFloor + ", cílové patro: " + human.EndFloor + "\n";
                    }
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Elevator elevator in building.elevatorList)
            {
                if (elevator.humanList.Count < 4)
                {
                    building.GenerateHuman();
                } 
            }
        }
    }
}
