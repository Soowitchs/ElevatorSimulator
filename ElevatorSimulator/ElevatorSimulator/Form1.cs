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
        //Vytvoření globálních proměnných
        Graphics kp;
        List<Building> buildingList = new List<Building>();
        Building building;
        Random rnd = new Random();
        Pen pen = new Pen(Color.Black, 2);
        SolidBrush brush = new SolidBrush(Color.Black);
        SolidBrush brush2 = new SolidBrush(Color.Red);
        int i = 0;
        bool test = true;
        public Form1()
        {
            //Maximalizace programu, odstranění horního panelu
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            //Inicializace Budov a jejich uložení do listu
            for (int i = 1; i <= 1; i++)
            {
                buildingList.Add(new Building(20, 20, 10, 1, i, false));
            }
            //nastavení vstupu na list
            comboBox1.DataSource = buildingList;
            timer1.Start();
            timer2.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        { 
            //timer na běh celého programu
            this.Refresh();
            i++;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //vykreslování
            richTextBox1.Text = "";
            kp = e.Graphics;
            foreach (Building building in buildingList)
            {
                //pokud je budova vybraná v comboboxu, změní se její barva na modrou
                if (building.Selected)
                {
                    pen.Color = Color.Blue;
                }
                else
                {
                    pen.Color = Color.Black;
                }
                this.building = building;
                //Pro každou budovu se vykreslí její patra
                foreach (Floor floors in building.floorList)
                {
                    Rectangle floor = new Rectangle(floors.Position.Y, floors.Position.X, building.Width, (int)building.LenghtOfOneFloor);
                    kp.DrawRectangle(pen, floor);
                }
                //pro každou budovu se vykreslí výtahy
                foreach (Elevator elev in building.elevatorList)
                {
                    Rectangle elevator = new Rectangle(building.Position.X + 1, elev.Position.Y + 1, elev.Width - 1, (int)building.LenghtOfOneFloor - 1);
                    //pokud jsou zavřené dveře u výtahu, výtah je červený
                    if (elev.door == Elevator.Door.closed)
                    {
                        brush2.Color = Color.Red;
                    }
                    if (elev.door == Elevator.Door.open)
                    {
                        brush2.Color = Color.Green;
                    }
                    kp.FillRectangle(brush2, elevator);
                    //posouvání výtahu
                    elev.FloorCheck(building);
                    //výpis do richtextboxu
                    richTextBox1.Text += elev.Output() + "\n";
                    //vykreslení lidí
                    foreach (Human human in elev.humanList)
                    {
                        //Pokud je vybraný, změní se barva zelenou
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
                    }
                }
            }
        }
        private async void timer2_Tick(object sender, EventArgs e)
        {
            //fps počítadlo
            label1.Text = i.ToString();
            i = 0;
            //generování lidí
            foreach (Building building in buildingList)
            {
                foreach (Elevator elevator in building.elevatorList)
                {
                    //pokud má v seznamu míň jak 4 lidi, vygeneruje se další
                    if (elevator.humanList.Count < 4)
                    {
                        building.GenerateHuman();
                        await Task.Delay(10);
                    }
                } 
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //vypínání a zapínání timeru
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
            //zvětšování intervatu timeru
            timer1.Interval += 5;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kontrola a zmenšování intervalu timeru
            if (timer1.Interval > 5)
            {
                timer1.Interval -= 5;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //ukončení programu
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
            //načítání výtahů do comboboxu2 po vybrání v comboboxu1
            foreach (Building item in buildingList)
            {
                item.Selected = false;
            }
            comboBox2.DataSource = (comboBox1.SelectedItem as Building).elevatorList;
            (comboBox1.SelectedItem as Building).Selected = true;
        }
    }
}
