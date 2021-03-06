using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Locadora;

namespace Locadora
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Update_List();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            Form2.Show();
            this.Hide();
        }

        public void Update_List()
        {
            var cars = new List<KeyValuePair<string, string>>();
            var connection = new databaseconnection();
            var rows = connection.Query("select modelo_car, id_car from carro "+((textBox1.Text=="")?"":$"where modelo_car LIKE '%{textBox1.Text.ToLower()}%';"));
            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i];
                cars.Add(new KeyValuePair<string, string> ( row["id_car"].ToString(), row["modelo_car"].ToString()));
            }
            connection.Dispose();
            listBox1.DataSource = cars;
            listBox1.ValueMember = "Key";
            listBox1.DisplayMember = "Value";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Visible = true;
            var connection = new databaseconnection();
            var rows = connection.Query($"select * from carro where id_car = '{((KeyValuePair<string, string>)listBox1.SelectedItem).Key}'");
            for (int i = 0; i < rows.Count; i++) {
                var row = rows[i];
                label1.Text = row["modelo_car"].ToString();
                label9.Text = row["placa_car"].ToString();
                label10.Text = row["fabri_car"].ToString();
                var alu = row["stat_car"].ToString();
                button5.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                textBox5.Visible = false;
                label14.Visible = false;
                label1.Visible = true;
                checkBox1.Enabled = false;
                if (alu == "1")
                {
                    label11.Visible = true;
                    label12.Visible = true;
                    label5.Visible = true;
                    label6.Visible = true;
                    checkBox1.Checked = true;
                    label11.Text = row["dat_ret"].ToString();
                    label12.Text = row["dat_dev"].ToString();
                }
                else{
                    label11.Visible = false;
                    label12.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    checkBox1.Checked = false;
                }
                connection.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var connection = new databaseconnection();
            connection.Run($"delete from carro where id_car = '{((KeyValuePair<string, string>)listBox1.SelectedItem).Key}'");
            Update_List();
            connection.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            label1.Visible = false;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            checkBox1.Enabled = true;
            label5.Visible = true;
            label6.Visible = true;
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = true;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            textBox2.Text = label9.Text;
            textBox3.Text = label10.Text;
            textBox4.Text = label1.Text;
            var connection = new databaseconnection();
            var rows = connection.Query($"select prec_alug from carro where id_car={((KeyValuePair<string, string>)listBox1.SelectedItem).Key}");
            var row = rows[0];
            textBox5.Text = row["prec_alug"].ToString();
            connection.Dispose();
        }

        public void UpdateCarro()
        {
            var connection = new databaseconnection();
            int alu;
            if (checkBox1.Checked == true)
            {
                alu = 1;
            }
            else
            {
                alu = 0;
            }
            connection.Run($"update carro set placa_car='{textBox2.Text}', modelo_car='{textBox4.Text}', fabri_car='{textBox3.Text}', stat_car={alu},  dat_ret='{dateTimePicker1.Text}', dat_dev='{dateTimePicker2.Text}', prec_alug={textBox1.Text} where id_car='{((KeyValuePair<string, string>)listBox1.SelectedItem).Key}'");
            connection.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateCarro();
            Update_List();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Update_List();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label5.Visible = true;
                label6.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
            }
        }
    }
}
