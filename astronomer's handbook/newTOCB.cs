using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace astronomer_s_handbook
{
    public partial class @new : Form
    {
        DataBase data = new DataBase();
        public @new()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;


            var addQuare = $"insert into typeOfCelestialBody(Код_НТ, Тип_НТ) values ('{firsNum}','{MidleNum}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Тип небесного тела добавлен");
            data.closeConnection();
        }
    }
}
