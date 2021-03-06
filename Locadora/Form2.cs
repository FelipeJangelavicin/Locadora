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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cadastroCarro();
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Close();

        }

        public void cadastroCarro()
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
            connection.Run($"insert into carro (placa_car, modelo_car, fabri_car, stat_car, dat_ret, dat_dev, prec_alug) values ('{textBox1.Text}', '{textBox3.Text}', '{textBox2.Text}', {alu}, '{dateTimePicker1.Text}', '{dateTimePicker2.Text}', {textBox4.Text});");
            connection.Dispose();
        }
    }
}
