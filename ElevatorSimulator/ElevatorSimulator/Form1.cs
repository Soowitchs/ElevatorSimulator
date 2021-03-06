﻿using System;
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
        int numberOfBuildings = 5;
        int numberOfFloors = 5;
        int j = 0;
        bool log = true;
        bool fps = true;
        public Form1()
        {
            //Maximalizace programu, odstranění horního panelu
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            InitializeComponent();
            //Inicializace Budov a jejich uložení do listu
            KokotFixus(numberOfBuildings, numberOfFloors);
            //nastavení vstupu na list
            comboBox1.DataSource = buildingList;
            timer1.Start();
            timer2.Start();
            button1.BackColor = Color.Green;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //timer na běh celého programu
            this.Refresh();
            j++;
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
                    if (elev.selected)
                    {
                        brush2.Color = Color.Blue;
                    }
                    kp.FillRectangle(brush2, elevator);
                    //posouvání výtahu
                    elev.FloorCheck(building);
                    //výpis do richtextboxu
                    if (log)
                    {
                        richTextBox1.Text += elev.Output() + "\n"; 
                    }
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
        private async void Timer2_Tick(object sender, EventArgs e)
        {
            //fps počítadlo
            if (fps)
            {
                label1.Text = (j / 2).ToString();
                j = 0; 
            }
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
        private void Button1_Click(object sender, EventArgs e)
        {
            //vypínání a zapínání timeru
            if (timer1.Enabled == false)
            {
                timer1.Start();
                button1.BackColor = Color.Green;
            }
            else if (timer1.Enabled == true)
            {
                timer1.Stop();
                button1.BackColor = Color.Red;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //zvětšování intervatu timeru
            timer1.Interval += 20;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //kontrola a zmenšování intervalu timeru
            if (timer1.Interval > 10)
            {
                timer1.Interval -= 10;
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            //ukončení programu
            this.Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            //zvětšení intervalu generování
            timer2.Interval += 100;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            //zmenšení intervalu generování
            if (timer2.Interval > 100)
            {
                timer2.Interval -= 100;
            }

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            // start/stop timeru2(generování)
            if (timer2.Enabled == false)
            {
                timer2.Start();
            }
            else if (timer2.Enabled == true)
            {
                timer2.Stop();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //načítání výtahů do comboboxu2 po vybrání v comboboxu1
            foreach (Building item in buildingList)
            {
                item.Selected = false;
            }
            comboBox2.DataSource = (comboBox1.SelectedItem as Building).elevatorList;
            (comboBox1.SelectedItem as Building).Selected = true;
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            //přidání budovy
            numberOfBuildings++;
            KokotFixus(numberOfBuildings, numberOfFloors);
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            //odebrání budovy
            numberOfBuildings--;
            KokotFixus(numberOfBuildings, numberOfFloors);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            //přidání podlaží
            numberOfFloors++;
            KokotFixus(numberOfBuildings, numberOfFloors);
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            //odebrání podlaží
            numberOfFloors--;
            KokotFixus(numberOfBuildings, numberOfFloors);
        }

        public void KokotFixus(int numberOfBuildings, int numberOfFloors)
        {
            buildingList = new List<Building>();
            for (int i = 1; i <= numberOfBuildings; i++)
            {
                buildingList.Add(new Building(numberOfFloors, 20, 10, 1, i, false));
            }
            comboBox1.DataSource = buildingList;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            // restart
            KokotFixus(numberOfBuildings, numberOfFloors);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            // zobrazení logu
            if (log)
            {
                log = false;
            }
            else if (!log)
            {
                log = true;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            // zobrazení FPS
            label1.Text = "";
            if (fps)
            {
                fps = false;
            }
            else if (!fps)
            {
                fps = true;
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Building item in buildingList)
            {
                foreach (Elevator item2 in item.elevatorList)
                {
                    item2.selected = false;
                }
            }
            (comboBox2.SelectedItem as Elevator).selected = true;
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            // zamknutí/odemknutí výtahu
            if ((comboBox2.SelectedItem as Elevator).Locked == Elevator.Lock.locked)
            {
                (comboBox2.SelectedItem as Elevator).Locked = Elevator.Lock.unlocked;
            }
            else if ((comboBox2.SelectedItem as Elevator).Locked == Elevator.Lock.unlocked)
            {
                (comboBox2.SelectedItem as Elevator).Locked = Elevator.Lock.locked;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //about
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
