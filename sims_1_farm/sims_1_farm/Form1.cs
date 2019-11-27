using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sims_1_farm
{
    public partial class Form1 : Form
    {
        Dictionary<CheckBox, Cell> field = new Dictionary<CheckBox, Cell>();
        //Dictionary<CellState, int> price = new Dictionary<CellState, int>
        ////{
        ////    { CellState.Green, 10},
        ////    { CellState.Yellow, 20},
        ////    { CellState.Red, 30}
        ////};
        public Form1()
        {
            InitializeComponent();
            foreach (CheckBox cb in flowLayoutPanel1.Controls)
            {
                field.Add(cb, new Cell());
            }
        }

        bool flag_pause = false;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (flag_pause == false)
                {
                    CheckBox cb = sender as CheckBox;
                    if (cb.Checked) StartGrow(cb);
                    else Cut(cb);
                }
        }

       public int money = 100;

        private void Cut(CheckBox cb)
        {
            GiveMoney(cb);
            field[cb].Cut();
            UpdateBox(cb);
        }


        private void StartGrow(CheckBox cb)
        {
            if (money>=10)
            {
                field[cb].StartGrowing();
                UpdateBox(cb);
                Change(-10);
                GhangeLabel();
            }
        }

        private void GhangeLabel()
        {
            label2.Text = "Money: " + money;
        }

        //private void Minus()
        //{
        //    money -= 10;
        //}

        private void Change(int v)
        {
            money += v;
        }

        private void GiveMoney(CheckBox cb)
        {
            switch (field[cb].state)
            {
                case CellState.Green:
                    Change(10);
                    GhangeLabel();
                    break;
                case CellState.Yellow:
                    Change(15);
                    GhangeLabel();
                    break;
                case CellState.Red:
                    Change(20);
                    GhangeLabel();
                    break;
            }
        }

        int date = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

            foreach (CheckBox cb in flowLayoutPanel1.Controls)
            {
                field[cb].Step();
                UpdateBox(cb);
            }

            date++;
            label1.Text = "Day: " + date;
        }

        private void UpdateBox(CheckBox cb)
        {
            switch (field[cb].state)
            {
                case CellState.Empty: cb.BackColor = Color.White; break;
                case CellState.Growing: cb.BackColor = Color.Black; break;
                case CellState.Green:cb.BackColor = Color.Green;break;
                case CellState.Yellow:cb.BackColor = Color.Yellow;break;
                case CellState.Red:cb.BackColor = Color.Red;break;
                case CellState.Brown: cb.BackColor = Color.Brown; break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag_pause==false)
            {
                timer1.Enabled = false;
                flag_pause = true;
            }
            else
            {
                timer1.Enabled = true;
                flag_pause = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
        }
    }

    

    enum CellState { Empty, Growing, Green, Yellow, Red, Brown };

    class Cell
    {
        public int money = 100;

        private int progress = 0;
        const int prGrowing = 15;
        const int prGreen = 65;
        const int prYellow = 85;
        const int prRed = 100;

        public CellState state = CellState.Empty;

        public void Step()
        {
            if (state != CellState.Empty && state != CellState.Brown)
            {
                progress++;
                if (progress < prGrowing) state = CellState.Growing;
                else if (progress < prGreen) state = CellState.Green;
                else if (progress < prYellow) state = CellState.Yellow;
                else if (progress < prRed) state = CellState.Red;
                else state = CellState.Brown;
            }
        }

        public void StartGrowing()
        {
            state = CellState.Growing;
        }

        public void Cut()
        {
            state = CellState.Empty;
            progress = 0;
        }
    }

    
}
