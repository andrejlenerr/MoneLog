using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO; // Ensure you have the SQLite library referenced

namespace MoneLog
{
    public partial class LoadedDate : Form
    {
        SQLiteConnection con = new SQLiteConnection(@"Data Source=monelog.db;Version=3;"); // Update with your actual database file
        private string _selectedDate;

        public LoadedDate(string selectedDate)
        {
            InitializeComponent();
            _selectedDate = selectedDate;
        }

        public void LoadMonth()
        {
            SQLiteCommand cmd = new SQLiteCommand("select MonthYear FROM Budgeting where MonthYear = @p1", con);
            cmd.Parameters.AddWithValue("@p1", _selectedDate);
            con.Open();
            label1.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        public void LoadData()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT Id, Date, Description, Source, Amount FROM Budgeting WHERE MonthYear = @p1 ORDER BY substr(Date, 4, 2), substr(Date, 1, 2);", con);
            cmd.Parameters.AddWithValue("@p1", label1.Text);

            con.Open();
            using (SQLiteDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    ListViewItem item = new ListViewItem(rdr["Id"].ToString());
                    item.SubItems.Add(rdr["Date"].ToString());
                    item.SubItems.Add(rdr["Description"].ToString());
                    item.SubItems.Add(rdr["Source"].ToString());
                    item.SubItems.Add(rdr["Amount"].ToString());

                    listView1.Items.Add(item);
                }
            }
            con.Close();
        }

        private void LoadedDate_Load(object sender, EventArgs e)
        {
            LoadMonth();
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand("insert into Budgeting(Date,Description,Source,Amount,MonthYear) values (@p1,@p2,@p3,@p4,@p5)",con);
                cmd.Parameters.AddWithValue("@p1",textBox1.Text);
                cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                cmd.Parameters.AddWithValue("@p4", textBox4.Text);
                cmd.Parameters.AddWithValue("@p5",label1.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                listView1.Items.Clear();
                LoadData();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error!" + ex.Message,"Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                try
                {
                    if (listView1.SelectedItems.Count > 0)
                    {
                        textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                        textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;
                        textBox3.Text = listView1.SelectedItems[0].SubItems[3].Text;
                        textBox4.Text = listView1.SelectedItems[0].SubItems[4].Text;
                        textBox5.Text = listView1.SelectedItems[0].SubItems[0].Text;
                    }
                else
                {
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                }
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Error!" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void LoadedDate_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand("update Budgeting set Date = @p1, Description = @p2, Source = @p3, Amount = @p4 where Id = @p5", con);
                cmd.Parameters.AddWithValue("@p1", textBox1.Text);
                cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                cmd.Parameters.AddWithValue("@p4", textBox4.Text);
                cmd.Parameters.AddWithValue("@p5", textBox5.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                listView1.Items.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
