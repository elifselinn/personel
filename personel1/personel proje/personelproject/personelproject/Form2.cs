using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
namespace personelproject
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0;Data Source=Kullanicilar.accdb");

        private void kullanicilari_göster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter
                    ("select tcno AS[TC KİMLİK NO],ad AS[AD],soyad AS[SOYAD],yetki AS[YETKİ],kullaniciadi AS[KULLANICI ADI],parola AS[PAROLA] from Kullanicilar Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }

        private void personelleri_göster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter
                    ("select tcno AS[TC KİMLİK NO],ad AS[AD],soyad AS[SOYAD],cinsiyet AS[CİNSİYET],mezuniyet AS[MEZUNİYET]," +
                    "dogumtarihi AS[DOĞUM TARİHİ],gorevi AS[GÖREVİ],gorevyeri AS[GÖREV YERİ],maasi AS[MAAŞI]from personeller Order By ad ASC", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView2.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresim\\" + Form1.tcno + ".jpg");
            }
            catch
            {
                string rootPathRoot = (Application.StartupPath + "\\kullaniciresim\\resimyok.jpg");
            }
            this.Text = "Yönetici İşlemleri";
            label12.Text = Form1.adi + " " + Form1.soyadi;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "11 Karakterli Olmalıdır");
            kullanicilari_göster();

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 90; pictureBox2.Height = 90;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;

            DateTime zaman = DateTime.Now;
            int yıl = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gün = int.Parse(zaman.ToString("dd"));





        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)
                errorProvider1.SetError(textBox1, "TC Kimlik No 11 Karakter Olmalı");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'I');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ç', 'c');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');
            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Türkçe Karakter Kullanmamalısınız!");
            }

            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, AZ_karakter_sayisi) * 10;

            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;

            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;
            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük Harf, Küçük Harf, Rakam ve Sembol İçermelidir";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";
            if (parola_skoru < 70)
                parola_seviyesi = "Kabul Edilemez";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";

            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;



        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola Eşleşmiyor");
            else
                errorProvider1.Clear();

        }
        private void topPage1_temizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear(); textBox6.Clear();
        }
        private void topPage2_temizle()
        {
            pictureBox2.Image = null; textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear
                     (); comboBox1.SelectedIndex = -1; comboBox3.SelectedIndex = -1; comboBox4.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from Kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;

            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Orange;
                else
                    label1.ForeColor = Color.Black;

                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label3.ForeColor = Color.Orange;
                else
                    label3.ForeColor = Color.Black;

                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label2.ForeColor = Color.Orange;
                else
                    label2.ForeColor = Color.Black;

                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                    label5.ForeColor = Color.Orange;
                else
                    label5.ForeColor = Color.Black;

                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Orange;
                else
                    label6.ForeColor = Color.Black;

                if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Orange;
                else
                    label7.ForeColor = Color.Black;


                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" &&
                    textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "kullanıcı";

                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into Kullanicilar values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'," +
                            "'" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni Kullanıcı Kaydı Oluşturuldu", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }

                }
                else
                {
                    MessageBox.Show("yazı rengi turuncu alanları gözden geçiriniz",
                        "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası Daha Önce Kayıtlıdır", "Personel Takip Programı", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from Kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan Kayıt Bulunamadı", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();
            }
            else
            {
                MessageBox.Show("Lütfen 11 Haneli bir TC Kimlik No Giriniz", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string yetki = "";

            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Orange;
            else
                label1.ForeColor = Color.Black;

            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label3.ForeColor = Color.Orange;
            else
                label3.ForeColor = Color.Black;

            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label2.ForeColor = Color.Orange;
            else
                label2.ForeColor = Color.Black;

            if (textBox4.Text.Length != 8 || textBox4.Text == "")
                label5.ForeColor = Color.Orange;
            else
                label5.ForeColor = Color.Black;

            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Orange;
            else
                label6.ForeColor = Color.Black;

            if (textBox6.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Orange;
            else
                label7.ForeColor = Color.Black;


            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" &&
                textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "kullanıcı";

                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update Kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi='" + textBox4.Text
                        + "',parola='" + textBox5.Text + "'where tcno='" + textBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı Bilgileri Güncellendi", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_göster();
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }

            }
            else
            {
                MessageBox.Show("yazı rengi turuncu alanları gözden geçiriniz",
                    "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read()) ;
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete *from kullanicilar where tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı Kaydı Silindi!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_göster();
                    topPage1_temizle();

                }
                if (kayit_arama_durumu == false) ;
                MessageBox.Show("Silinecek kayıt bulunamadı!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();
            }
            else
                MessageBox.Show("Lütfen 11 karakterden oluşan bir TC Kimlik No Giriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Personel resmi seçiniz!";
            resimsec.Filter = "JPG Dosyalar (*.jpg) |*.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK) ;
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            bool kayıtkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where tcno='" + textBox7.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayıtkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayıtkontrol == false)
            {
                if (pictureBox2.Image == null)
                    button11.ForeColor = Color.Red;
                else
                    button11.ForeColor = Color.Black;
                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red;
                else
                    label17.ForeColor = Color.Black;
                if (comboBox3.Text == "")
                    label19.ForeColor = Color.Red;
                else
                    label19.ForeColor = Color.Black;
                if (comboBox4.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;
                if (int.Parse(textBox10.Text) < 1000)
                label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;
                if (radioButton3.Checked == true)
                    cinsiyet = "Bay";
                else if (radioButton4.Checked == true)
                    cinsiyet = "Bayan";
                try
                {
                    baglantim.Open();
                    OleDbCommand eklekomutu = new OleDbCommand("insert into personeller values('" + textBox7.Text + "','"
                        + textBox8.Text + "','" + textBox9.Text + "','"
                        + cinsiyet + "','" + comboBox1.Text + "','"
                        + comboBox3.Text + "','" + comboBox4.Text + "','" + textBox10.Text + "'')", baglantim);
                    eklekomutu.ExecuteNonQuery();
                    baglantim.Close();
                    if (!Directory.Exists(Application.StartupPath + "\\personelresim"))
                        Directory.CreateDirectory(Application.StartupPath + "\\personelresim");
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresim\\" + textBox7.Text + ".jpg");
                    MessageBox.Show("Yeni Kullanıcı Oluşturuldu.", "SKY Personel Takip Programı",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    personelleri_göster();
                    topPage2_temizle();
                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }
            }
            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanları yeniden gözden geçiriniz!", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();

            }
        }
    }
}