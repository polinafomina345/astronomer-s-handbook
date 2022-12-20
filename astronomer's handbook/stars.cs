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
    public partial class stars : Form
    {
        DataBase dataBase = new DataBase();
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Name = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from stars where Код_звезды = {Name}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {

                    var fam = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var fuf = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var rur = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var eue = dataGridView1.Rows[index].Cells[4].Value.ToString();


                    var changeQuery = $"update stars set Название = '{name}',Обозначение = '{fuf}', Блеск = '{rur}', Код_типа_НТ = '{eue}' where Код_звезды = '{fam}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[5].Value = RowState.Deleted;
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var brig = textBox1.Text;
            var job = textBox2.Text;
            var fuf = textBox3.Text;
            var rur = textBox4.Text;
            var eue = textBox5.Text;

            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(brig, job, fuf, rur, eue);
                dataGridView1.Rows[selectedRowIndex].Cells[5].Value = RowState.Modified;
            }

        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from stars where concat(Код_звезды, Название, Обозначение, Блеск, Код_типа_НТ) like '%" + textBox1.Text + "%'";
            SqlCommand com = new SqlCommand(search, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();


        }

        private void RefresDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string queryString = $"select *from stars";
            SqlCommand command = new SqlCommand(queryString, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);

            }
            reader.Close();

        }
        enum RowState
        {
            Existet,
            New,
            Modified,
            ModifiedNew,
            Deleted

        }
        int selectRow;
        private void CreateColumns()
        {


            dataGridView1.Columns.Add("Код_звезды", "Код_звезды");
            dataGridView1.Columns.Add("Название", "Название");
            dataGridView1.Columns.Add("Обозначение", "Обозначение");
            dataGridView1.Columns.Add("Блеск", "Блеск");
            dataGridView1.Columns.Add("Код_типа_НТ", "Код_типа_НТ");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetInt32(3), record.GetInt32(4), RowState.ModifiedNew);

        }
        public stars()
        {
            InitializeComponent();
        }

        private void stars_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView1);
        }
      


        private void button4_Click_1(object sender, EventArgs e)
        {
            newStars newStars = new newStars();
            newStars.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Update();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Change();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefresDataGrid(dataGridView1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);   
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();

            }
        }
    }
}
