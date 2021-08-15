using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace Kutuphaneprojem
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand com;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=DESKTOP-QP2F3VH;Initial Catalog=kutuphane;Integrated Security=True");
        SqlConnection baglan=new SqlConnection(constring);
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void Girisyap_Click(object sender, EventArgs e)
        {
            string kullaniciadi = textBox1.Text;
            string sifre = textBox2.Text;
            con = new SqlConnection("Data Source=DESKTOP-QP2F3VH;Initial Catalog=kutuphane;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection=con;
            com.CommandText = "Select * from kayit where kullaniciadi='" + textBox1.Text + "'And sifre='" + textBox2.Text + "'";
            dr=com.ExecuteReader();
            if(dr.Read())
            {
                MessageBox.Show("Giriş Başarılı" + " " +  "Hoşgeldin" + " " + textBox1.Text);
                tabControl1.SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre ");
            }
        }

        private void Kayitol_Click(object sender, EventArgs e)
        {
            try
            {
                if(baglan.State==ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit="insert into kayit (ad,soyad,kullaniciadi,sifre) values(@ad,@soyad,@kullaniciadi,@sifre)";
                    SqlCommand komut = new SqlCommand(kayit,baglan);
                    komut.Parameters.AddWithValue("@ad", textBox3.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox4.Text);
                    komut.Parameters.AddWithValue("@kullaniciadi", textBox5.Text);
                    komut.Parameters.AddWithValue("@sifre", textBox6.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Eklendi");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata var!" + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {

                textBox2.PasswordChar = '\0';
                checkBox1.Text = "Gizle";

            }
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '*';
                checkBox1.Text = "Göster";

            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayit = "insert into kitap (kitapadi,yazari) values(@kitapadi,@yazari)";
                    SqlCommand komut = new SqlCommand(kayit, baglan);
                    komut.Parameters.AddWithValue("@kitapadi", textBox7.Text);
                    komut.Parameters.AddWithValue("@yazari", textBox8.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Eklendi");
                    baglan.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata var!" + ex.Message);
            }
        }
        public void kayitlarigetir()
        {
            
            string getir = "Select * from kitap";
            SqlCommand komut = new SqlCommand(getir,baglan);
            SqlDataAdapter ad = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            kayitlarigetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
                string kayit = "Select * from kitap Where kitapadi=@kitapadi";
                SqlCommand komut = new SqlCommand(kayit, baglan);
                komut.Parameters.AddWithValue("@kitapadi", textBox9.Text);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                              baglan.Close();
           
           


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string kayit = "Delete from kitap Where kitapadi=@kitapadi";
            SqlCommand komut = new SqlCommand(kayit, baglan);
            komut.Parameters.AddWithValue("@kitapadi", textBox9.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            MessageBox.Show(textBox9.Text + " " +  "isimli kitap" + " " + "silindi");
            kayitlarigetir();
            baglan.Close();
        }

        int i = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            string kayitguncelle = ("Update kitap set  kitapadi=@kitapadi,yazari=@yazari where id=@id");
            SqlCommand komut = new SqlCommand(kayitguncelle, baglan);
            komut.Parameters.AddWithValue("@kitapadi",textBox7.Text);
            komut.Parameters.AddWithValue("@yazari",textBox8.Text);
            komut.Parameters.AddWithValue("@id", dataGridView1.Rows[i].Cells[0].Value);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kayıt güncellendi");
            baglan.Close();
            kayitlarigetir();


        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            i = e.RowIndex;
            textBox7.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            textBox8.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();

        }
    }
}
