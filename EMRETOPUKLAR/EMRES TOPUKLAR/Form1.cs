using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace EMRES_TOPUKLAR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OleDbConnection bag = new OleDbConnection
            ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=emres.accdb");
            bag.Open();

            OleDbDataAdapter kur = new OleDbDataAdapter
            ("select tur from depo", bag);
            DataSet hamal = new DataSet();
            kur.Fill(hamal);
            for (int i = 0; i < hamal.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(hamal.Tables[0].Rows[i].ItemArray[0].ToString());
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OleDbConnection bag = new OleDbConnection
            ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=emres.accdb");
            bag.Open();

            OleDbDataAdapter kur = new OleDbDataAdapter
            ("select fiyat from depo where tur='" + comboBox1.Text + "'", bag);
            DataSet hamal = new DataSet();
            kur.Fill(hamal);
            textBox1.Text = hamal.Tables[0].Rows[0].ItemArray[0].ToString(); 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.Text = (int.Parse(textBox1.Text) * int.Parse(textBox2.Text)).ToString();
            }
            catch (Exception)
            {
                
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection bag = new OleDbConnection
            ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=emres.accdb");
            bag.Open();

            OleDbCommand komut = new OleDbCommand();
            komut.Connection = bag;
            komut.CommandText ="insert into satis(satilan,adet,tutar,tarih)values('" +comboBox1.Text + "',"+int.Parse (textBox2.Text)+","+int.Parse(textBox3.Text)+",'" +DateTime.Now.ToString()+"')";
            komut.ExecuteNonQuery();
            bag.Close();

            comboBox1.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OleDbConnection bag = new OleDbConnection
            ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=emres.accdb");
            bag.Open();

            OleDbDataAdapter kur= new OleDbDataAdapter
            ("select sum (tutar) from satis",bag);
            DataSet hamal = new DataSet();
            kur.Fill(hamal);

            MessageBox.Show(hamal.Tables[0].Rows[0].ItemArray[0].ToString());
        }
    }
}
