using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace astronomer_s_handbook
{
    public partial class Meny : Form
    {
        public Meny()
        {
            InitializeComponent();
        }

        private void Meny_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            type_of_celestial_body type_Of = new type_of_celestial_body();
            this.Hide();
            this.Close();
            type_Of.Show();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stars type_Of = new stars();
            this.Hide();
            this.Close();
            type_Of.Show();
            this.Show();
        }
    }
}
