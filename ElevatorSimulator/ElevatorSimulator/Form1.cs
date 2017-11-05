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
        Pen pen = new Pen(Color.Black, 2);
        SolidBrush brush = new SolidBrush(Color.Black);
        int i = 0;
        bool test = true;
        public Form1()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            for (int i = 1; i <= 10; i++)
            {
                buildingList.Add(new Building(20, 20, 10, 1, i, false));
            }
            comboBox1.DataSource = buildingList;
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
            i++;
        }

        private async void Form1_Paint(object sender, PaintEventArgs e)
        {
            richTextBox1.Text = "";
            kp = e.Graphics;
            foreach (Building building in buildingList)
            {
                if (building.Selected)
                {
                    pen.Color = Color.Blue;
                }
                else
                {
                    pen.Color = Color.Black;
                }
                this.building = building;
                foreach (Floor floors in building.floorList)
                {
                    Rectangle floor = new Rectangle(floors.Position.Y, floors.Position.X, building.Width, (int)building.LenghtOfOneFloor);
                    kp.DrawRectangle(pen, floor);
                    //richTextBox1.Text += floor.Location.ToString();
                }
                foreach (Elevator elev in building.elevatorList)
                {
                    Rectangle elevator = new Rectangle(building.Position.X + 1, elev.Position.Y + 1, elev.Width - 1, (int)building.LenghtOfOneFloor - 1);
                    if (elev.door == Elevator.Door.closed)
                    {
                        kp.FillRectangle(Brushes.Red, elevator);
                    }
                    else if (elev.door == Elevator.Door.open)
                    {
                            if (elev.Width >= 0)
                            {
                                elev.Width--;
                            }
                            else if(elev.Width == 0)
                            {
                                test = false;
                            }
                        while (!test)
                        {
                            if (elev.Width <= 20)
                            {
                                elev.Width++;
                            }
                            else if (elev.Width == 20)
                            {
                                elev.door = Elevator.Door.closed;
                                test = true;
                            }
                        }
                        kp.FillRectangle(Brushes.Green, elevator);
                    }
                    elev.FloorCheck(building);
                    richTextBox1.Text += elev.Output() + "\n";
                    foreach (Human human in elev.humanList)
                    {
                        if (human.Selected)
                        {
                            brush.Color = Color.Green;
                        }
                        else
                        {
                            brush.Color = Color.Black;
                        }
                        Rectangle Human = new Rectangle(human.Position.X + 5, human.Position.Y + 5, 10, 10);
                        kp.FillEllipse(brush, Human);
                        //richTextBox1.Text += "Pozice: " + human.Position.ToString() + ", aktuální patro: " + human.StartFloor + ", cílové patro: " + human.EndFloor + "\n";
                    }
                }
            }
        }
        private async void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = (i/2).ToString();
            i = 0;
            foreach (Building building in buildingList)
            {
                foreach (Elevator elevator in building.elevatorList)
                {
                    if (elevator.humanList.Count < 4)
                    {
                        building.GenerateHuman();
                        await Task.Delay(10);
                    }
                } 
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                timer1.Start();
            }
            else if (timer1.Enabled == true)
            {
                timer1.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval += 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 5)
            {
                timer1.Interval -= 5;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Interval += 500;
        }

        private void button7_Click(object sender, EventArgs e)
        {


            if (timer2.Interval > 100)
            {
                timer2.Interval -= 100;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled == false)
            {
                timer2.Start();
            }
            else if (timer2.Enabled == true)
            {
                timer2.Stop();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Building item in buildingList)
            {
                item.Selected = false;
            }
            comboBox2.DataSource = (comboBox1.SelectedItem as Building).elevatorList;
            (comboBox1.SelectedItem as Building).Selected = true;
        }
    }
}
