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
        public Form1()
        {
            InitializeComponent();
            Building building = new Building(10, 10, 1, 3);
            Human human = new Human(Human.Direction.down, 50, 1, 9);
        }
    }
}
