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
    public partial class type_of_celestial_body : Form
    {

        DataBase dataBase = new DataBase();
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;

            dataGridView1.Rows[index].Visible = false;

            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[2].Value = RowState.Deleted;
        }
        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var brig = textBox1.Text;
            var job = textBox2.Text;


            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRowIndex].SetValues(brig, job);
                dataGridView1.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }

        }
        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string search = $"select * from typeOfCelestialBody where concat(Код_НТ, Тип_НТ) like '%" + textBox1.Text + "%'";
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

            string queryString = $"select *from typeOfCelestialBody";
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

            dataGridView1.Columns.Add("Код_НТ", "Код_НТ");
            dataGridView1.Columns.Add("Тип_НТ", "Тип_НТ");
            dataGridView1.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);

        }
        public type_of_celestial_body()
        {
            InitializeComponent();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void type_of_celestial_body_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefresDataGrid(dataGridView1);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectRow];

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();


            }
        }
        private void Update()
        {
            dataBase.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existet)
                    continue;

                if (rowState == RowState.Deleted)
                {
                    var Name = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"delete from typeOfCelestialBody where Код_НТ = {Name}";

                    var command = new SqlCommand(deleteQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

                if (rowState == RowState.Modified)
                {

                    var fam = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var name = dataGridView1.Rows[index].Cells[1].Value.ToString();


                    var changeQuery = $"update typeOfCelestialBody set Тип_НТ = '{name}' where Код_НТ = '{fam}' ";

                    var command = new SqlCommand(changeQuery, dataBase.getConnection());
                    command.ExecuteNonQuery();
                }

            }

            dataBase.closeConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Update();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            @new @new = new @new();
            @new.Show();
        }
    }

}
