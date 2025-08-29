using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite; // Ensure you have the SQLite library referenced

namespace MoneLog
{
    public partial class StartingForm : Form
    {
        private void CenterControls()
        {
            int spacing = 60; // Space between controls

            int totalWidth = dateTimePicker1.Width + spacing + richTextBox1.Width;
            int startX = (this.ClientSize.Width - totalWidth) / 2;
            int centerY = (this.ClientSize.Height - Math.Max(dateTimePicker1.Height, richTextBox1.Height)) / 2;

            dateTimePicker1.Location = new Point(startX, centerY);
            richTextBox1.Location = new Point(startX + dateTimePicker1.Width + spacing, centerY);

            // Center Label above the DateTimePicker
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = dateTimePicker1.Top - label1.Height - 10; // 10px gap above the DateTimePicker

            // Center Button under the DateTimePicker
            int buttonSpacing = 10; // Space between DateTimePicker and Button
            button1.Left = dateTimePicker1.Left + (dateTimePicker1.Width - button1.Width) / 2;
            button1.Top = dateTimePicker1.Bottom + buttonSpacing;

            // Center PictureBox under the Button
            int pictureBoxSpacing = 10; // Space between Button and PictureBox
            pictureBox1.Left = dateTimePicker1.Left + (dateTimePicker1.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = button1.Bottom + pictureBoxSpacing;
        }
        public StartingForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CenterControls();
        }

        private void StartingForm_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedDate = dateTimePicker1.Value.ToString("MM.yyyy.");
            LoadedDate f1 = new LoadedDate(selectedDate);
            f1.StartPosition = FormStartPosition.CenterScreen;
            this.Hide();
            f1.ShowDialog();
            this.Show();
        }
    }
}
