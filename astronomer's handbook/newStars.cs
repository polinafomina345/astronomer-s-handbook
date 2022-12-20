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
    public partial class newStars : Form
    {
        DataBase data = new DataBase();
        public newStars()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.openConnection();

            var firsNum = textBox1.Text;
            var MidleNum = textBox2.Text;
            var fufNum = textBox3.Text;
            var rurNum = textBox4.Text;
            var eueNum = textBox5.Text;

            var addQuare = $"insert into stars(Код_звезды, Название, Обозначение, Блеск, Код_типа_НТ) values ('{firsNum}','{MidleNum}', '{fufNum}', '{rurNum}', '{eueNum}')";

            var command = new SqlCommand(addQuare, data.getConnection());
            command.ExecuteNonQuery();

            MessageBox.Show("Звезда добавлена");
            data.closeConnection();
        }
    }
}
