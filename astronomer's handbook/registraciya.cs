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
    public partial class registraciya : Form
    {
        DataBase dataBase = new DataBase();
        private Boolean Сheckгser()
        {
            var login = textBox1.Text;
            var password = textBox2.Text;


            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            string quertString = $"select  login, password from Register where login = '{login}' and password = '{password}'";
            SqlCommand command = new SqlCommand(quertString, dataBase.getConnection());
            adapter.SelectCommand = command;

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
        public registraciya()
        {
            InitializeComponent();

        }

        private void registraciya_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Сheckгser())

            {
                return;
            }
            var login = textBox1.Text;
            var password = textBox2.Text;
            string quer = $"insert into register(login, password) values('{login}', '{password}')";
            SqlCommand command = new SqlCommand(quer, dataBase.getConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт создан");
                aftorizaciya aftorizaciya = new aftorizaciya();
                this.Hide();
                aftorizaciya.ShowDialog();

            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            dataBase.closeConnection();
        }
    }
}
