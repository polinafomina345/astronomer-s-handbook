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
    public partial class aftorizaciya : Form
    {
        DataBase dataBase = new DataBase();
        public aftorizaciya()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            registraciya registraciya=new registraciya();
            this.Hide();
            registraciya.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string queryString = $"select  login, password from Register where login = '{login}' and password = '{password}'";

            SqlCommand sqlCommand = new SqlCommand(queryString, dataBase.getConnection());
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы вошли успешно");
                Meny meny = new Meny();
                this.Hide();
                meny.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void aftorizaciya_Load(object sender, EventArgs e)
        {

        }
    }
}
