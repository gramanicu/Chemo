using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Chemo.Properties;
using System.IO;
using System.Reflection;

using System.Runtime.InteropServices;

namespace Chemo
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        string default_sym = "Scrieti simbolul";
        string default_num = "Scrieti numarul";
        Color default_col = Color.FromArgb(200,200,200);
        int check = 1;

        private void Form1_Load(object sender, EventArgs e)
        {

            last_text = default_num;
            this.ActiveControl = textBox2;
            textBox2.Visible = false;

            label5.Hide();
            label6.Hide();
            label7.Hide();
            label8.Hide();
            label9.Hide();
            label10.Hide();
            label11.Hide();
            label12.Hide();
            label13.Hide();
            label14.Hide();
            label15.Hide();
            label16.Hide();
            label17.Hide();

            toolTip1.SetToolTip(pictureBox7, "Incepe cautarea");
            toolTip1.SetToolTip(pictureBox6, " - Chemo -" + Environment.NewLine + " Programator : Grama Nicolae " + Environment.NewLine + " Design si baza de date : Botea Bogdan");
            toolTip1.SetToolTip(pictureBox2, "Ascundere ferestra");
            toolTip1.SetToolTip(pictureBox1, "Iesire");
            toolTip1.SetToolTip(label4, "Scrieti simbolul/numarul atomic al elementului cautat");
            toolTip1.SetToolTip(textBox1, "Scrieti simbolul/numarul atomic al elementului cautat");
            toolTip1.SetToolTip(label1, "Cautare dupa numarul atomic al elementului");
            toolTip1.SetToolTip(label2, "Cautare dupa simbolul elementului");
            toolTip1.SetToolTip(label3, "Selectati criteriul de cautare");
            toolTip1.SetToolTip(pictureBox4, "Cautare dupa simbolul elementului");
            toolTip1.SetToolTip(pictureBox5, "Cautare dupa numarul atomic al elementului");
        }

        Element el = new Element();

        private void Cautare()
        {
            if (check == 1)
            {
                try
                {
                    label5.Hide();
                    label6.Hide();
                    label7.Hide();
                    label8.Hide();
                    label9.Hide();
                    label10.Hide();
                    label11.Hide();
                    label12.Hide();
                    label13.Hide();
                    label14.Hide();
                    label15.Hide();
                    label16.Hide();
                    label17.Hide();

                    textBox1.Text = el.Autocorect(textBox1.Text);
                    int electroni;
                    electroni = el.Identificare(textBox1.Text);

                    if (electroni <= 0 || electroni >= 119)
                    {
                        MessageBox.Show("Element inexistent", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Scriere(electroni);
                    }
                }
                catch
                {
                    MessageBox.Show("Element Inexistent", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    label5.Hide();
                    label6.Hide();
                    label7.Hide();
                    label8.Hide();
                    label9.Hide();
                    label10.Hide();
                    label11.Hide();
                    label12.Hide();
                    label13.Hide();
                    label14.Hide();
                    label15.Hide();
                    label16.Hide();
                    label17.Hide();

                    int electroni;
                    electroni = Convert.ToInt32(textBox1.Text);
                    if (electroni <= 0 || electroni >= 119)
                    {
                        MessageBox.Show("Element inexistent", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Scriere(electroni);
                    }
                }
                catch
                {
                    MessageBox.Show("Element inexistent sau nu ati introdus un numar valid", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Scriere(int i)
        {
            label8.Text = el.nume(i) + " ( " + el.simbol(i) + " )";

            label10.Text = el.Configuratie(i);
            label11.Text = el.grupa(i);
            label12.Text = el.masa(i);
            label13.Text = el.numar(i);
            label14.Text = el.perioada(i);
            label16.Text = el.Serie(i);
            if (label14.Text == "Actinide")
            {
                string aux = "";
                aux = label14.Text;
                label14.Text = label11.Text;
                label11.Text = aux;
            }
            else if (label14.Text == "Lantanide")
            {
                string aux = "";
                aux = label14.Text;
                label14.Text = label11.Text;
                label11.Text = aux;
            }

            label5.Show();
            label6.Show();
            label7.Show();
            label8.Show();
            label9.Show();
            label10.Show();
            label11.Show();
            label12.Show();
            label13.Show();
            label14.Show();
            label15.Show();
            label16.Show();
            label17.Show();


            this.ActiveControl = textBox2;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (check == 0)
            {
                Image img;
                img = pictureBox4.Image;
                pictureBox4.Image = pictureBox5.Image;
                pictureBox5.Image = img;
                check = 1;
                textBox1.Text = default_sym;
                textBox1.ForeColor = default_col;

                label5.Hide();
                label6.Hide();
                label7.Hide();
                label8.Hide();
                label9.Hide();
                label10.Hide();
                label11.Hide();
                label12.Hide();
                label13.Hide();
                label14.Hide();
                label15.Hide();
                label16.Hide();
                label17.Hide();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (check == 1)
            {
                Image img;
                img = pictureBox4.Image;
                pictureBox4.Image = pictureBox5.Image;
                pictureBox5.Image = img;
                check = 0;
                textBox1.Text = default_num;
                textBox1.ForeColor = default_col;

                label5.Hide();
                label6.Hide();
                label7.Hide();
                label8.Hide();
                label9.Hide();
                label10.Hide();
                label11.Hide();
                label12.Hide();
                label13.Hide();
                label14.Hide();
                label15.Hide();
                label16.Hide();
                label17.Hide();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Cautare();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == default_num || textBox1.Text == default_sym || label10.Visible == true)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.FromArgb(234, 234, 234);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState =  FormWindowState.Minimized;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.minimize2;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exit2;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exit1;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.minimize1;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cautare();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Cautare();
            }
            if (last_text == default_num || last_text == default_sym)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.FromArgb(234, 234, 234);
            }
        }

        string last_text;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            last_text = textBox1.Text;
        }
        

    }

    public class Element
    {
        private string[] Nume = new string[200];
        private int[] Numar = new int[200];
        private string[] Simbol = new string[200];
        private string[] Config = new string[200];
        private string[] Perioada = new string[200];
        private string[] Grupa = new string[200];
        private string[] Masa = new string[200];
        private int numar_Numare;

        public Element()
        {
            DataSet data = new DataSet();
            XmlDocument xdoc = new XmlDocument();

            xdoc.Load("Tabel (nu modificati datele dinauntru).xml");

            XmlNodeList xnList = xdoc.SelectNodes("/Tabel/element");

            int i = 1;

            foreach (XmlNode a in xnList)
            {
                Nume[i] = a["Nume"].InnerText;
                Simbol[i] = a["Simbol"].InnerText;
                Config[i] = a["Configuratie"].InnerText;
                Perioada[i] = a["Perioada"].InnerText;
                Grupa[i] = a["Grupa"].InnerText;
                Masa[i] = a["Masa_Atomica"].InnerText;
                Numar[i] = Convert.ToInt32(a["Numar"].InnerText);
                i++;
            }
            numar_Numare = i;
        }

        public string masa(int e)
        {
            return Masa[e];
        }
        public string nume(int e)
        {
            return Nume[e];
        }

        public string simbol(int e)
        {
            return Simbol[e];
        }

        public string grupa(int e)
        {
            return Grupa[e];
        }

        public string perioada(int e)
        {
            string g = Perioada[e];
            if (g == "1") g = "I A";
            else if (g == "2") g = "II A";
            else if (g == "3") g = "III B";
            else if (g == "4") g = "IV B";
            else if (g == "5") g = "V B";
            else if (g == "6") g = "VI B";
            else if (g == "7") g = "VII B";
            else if (g == "8") g = "VIII B";
            else if (g == "9") g = "VIII B";
            else if (g == "10") g = "VII B";
            else if (g == "11") g = "I B";
            else if (g == "12") g = "II B";
            else if (g == "13") g = "III A";
            else if (g == "14") g = "IV A";
            else if (g == "15") g = "V A";
            else if (g == "16") g = "VI A";
            else if (g == "17") g = "VII A";
            else if (g == "18") g = "VIII A";
            return g;
            
        }

        public string numar(int e)
        {
            return Numar[e].ToString();
        }

        public int Identificare(string formula)
        {
            int e = 0;
            try
            {
                for (int i = 1; i <= numar_Numare; i++)
                    if (formula.ToLower() == Simbol[i].ToLower())
                    {
                        e = Numar[i];
                        break;
                    }
            }
            catch { }

            return e;
        }

        public string Autocorect(string formula)
        {
            string final = null;
            try
            {
                for (int i = 1; i <= numar_Numare; i++)
                    if (formula.ToLower() == Simbol[i].ToLower())
                    {
                        final = Simbol[i];
                        break;
                    }
            }
            catch { }
            return final;
        }

        private string Superscore(string i)
        {
            string a="";
            int l = i.Length-1;
            for (int j = 0; j <= l; j++)
            {
                if (i[j] == '1') a += '\u00B9'.ToString();
                else if (i[j] == '2') a += '\u00B2'.ToString();
                else if (i[j] == '3') a += '\u00B3'.ToString();
                else if (i[j] == '4') a += '\u2074'.ToString();
                else if (i[j] == '5') a += '\u2075'.ToString();
                else if (i[j] == '6') a += '\u2076'.ToString();
                else if (i[j] == '7') a += '\u2077'.ToString();
                else if (i[j] == '8') a += '\u2078'.ToString();
                else if (i[j] == '9') a += '\u2079'.ToString();
                else if (i[j] == '0') a += '\u2070'.ToString();
            }
            return a;
        }

        private string Prefix(int i)
        {
            string pref = "";
            if (i == 1) pref = "1s";
            else if (i == 2) pref = "2s";
            else if (i == 3) pref = "2p";
            else if (i == 4) pref = "3s";
            else if (i == 5) pref = "3p";
            else if (i == 6) pref = "4s";
            else if (i == 7) pref = "3d";
            else if (i == 8) pref = "4p";
            else if (i == 9) pref = "5s";
            else if (i == 10) pref = "4d";
            else if (i == 11) pref = "5p";
            else if (i == 12) pref = "6s";
            else if (i == 13) pref = "4f";
            else if (i == 14) pref = "5d";
            else if (i == 15) pref = "6p";
            else if (i == 16) pref = "7s";
            else if (i == 17) pref = "5f";
            else if (i == 18) pref = "6d";
            else if (i == 19) pref = "7d";
            return pref;
        }

        private string Strat(int i)
        {
            string pref = "";
            if (i == 1) pref = "1 ( k )";
            else if (i == 2) pref = "2 ( l )";
            else if (i == 3) pref = "3 ( m )";
            else if (i == 4) pref = "4 ( n )";
            else if (i == 5) pref = "5 ( o )";
            else if (i == 6) pref = "6 ( p )";
            else if (i == 7) pref = "7 ( q )";
            return pref;
        }

        public string Serie(int i)
        {
            string pref = "";
            if (simbol(i) == "H") pref = "Nemetal";
            else if (perioada(i) == "I A") pref = "Metal alcalin";
            else if (perioada(i) == "II A") pref = "Metal alcalino-pamantos";
            else if (simbol(i) == "B") pref = "Metaloid";
            else if (perioada(i) == "III A") pref = "Metal de post-tranzitie";
            else if (simbol(i) == "C") pref = "Nemetal";
            else if (simbol(i) == "Si") pref = "Metaloid";
            else if (simbol(i) == "Ge") pref = "Metaloid";
            else if (perioada(i) == "IV A") pref = "Metal de post-tranzitie";
            else if (simbol(i) == "Sb") pref = "Metaloid";
            else if (simbol(i) == "As") pref = "Metaloid";
            else if (simbol(i) == "UUp") pref = "Metal de post-tranzitie";
            else if (simbol(i) == "Bi") pref = "Metal de post-tranzitie";
            else if (perioada(i) == "V A") pref = "Nemetal";
            else if (simbol(i) == "Te") pref = "Metaloid";
            else if (simbol(i) == "Lv") pref = "Metal de post-tranzitie";
            else if (simbol(i) == "Po") pref = "Metaloid";
            else if (perioada(i) == "VI A") pref = "Nemetal";
            else if (perioada(i) == "VII A") pref = "Halogen";
            else if (perioada(i) == "VIII A") pref = "Gaz nobil";
            else if (perioada(i) == "Lantanide") pref = "Lantanid";
            else if (perioada(i) == "Actinide") pref = "Actinid";
            else pref = "Metal de tranzitie";
            return pref;
        }
        
        public string Configuratie(int electroni)
        {
            /*
            string config = Config[electroni];
            int lungime = config.Length-1;
            int[] numere = new int[200];
            int picked = 1;
            int nr = 0;

            for (int i = 1; i <= lungime; i++)
            {
                if (config[i] == '-')
                {
                    nr++;
                    if (picked == 1)
                    {
                        numere[nr] = Convert.ToInt32(config[i - 1]) - 48;
                    }
                    else if (picked == 2)
                    {
                        numere[nr] = Convert.ToInt32(String.Concat(config[i - 2],config[i - 1]));          
                    }
                    picked = 0;
                }
                else
                    if (i == lungime)
                    {
                        nr++;
                        if (picked == 0)
                        {
                            numere[nr] = Convert.ToInt32(config[i]) - 48;
                        }
                        else if (picked == 1)
                        {
                            numere[nr] = Convert.ToInt32(String.Concat(config[i - 1],config[i]));
                        }
                        picked = 0;
                    }
                    else picked++;
            }
             */
           int rez = electroni;
           string config = "";
           int[] a = new int[20];
           int[] b = new int[20];
           a[1] = 2;    // 1s
           a[2] = 2;    // 2s
           a[3] = 6;    // 2p
           a[4] = 2;    // 3s
           a[5] = 6;    // 3p
           a[6] = 2;    // 4s
           a[7] = 10;   // 3d
           a[8] = 6;    // 4p
           a[9] = 2;    // 5s
           a[10] = 10;  // 4d
           a[11] = 6;   // 5p
           a[12] = 2;   // 6s
           a[13] = 14;  // 4f
           a[14] = 10;  // 5d
           a[15] = 6;   // 6p
           a[16] = 2;   // 7s
           a[17] = 14;  // 5f
           a[18] = 10;  // 6d
           a[19] = 6;   // 7p

           int x = 1;
           while (electroni != 0)
           {
               if (b[x] < a[x])
               {
                   b[x]++;
                   electroni--;
               }
               else x++;
           }
           int subs = 1;

           if (rez > 86) { config = "[Rn]"; subs = 16; }
           else if (rez > 54) { config = "[Xe]"; subs = 12; }
           else if (rez > 38) { config = "[Kr]"; subs = 10; }
           else if (rez > 18) { config = "[Ar]"; subs = 6; }
           else if (rez > 10) { config = "[Ne]"; subs = 4; }
           else if (rez > 2) { config = "[He]"; subs = 2; }

           for (int i = subs; i <= x; i++)
               config += Prefix(i) + Superscore(b[i].ToString()) + " ";
           /*
           for (int i = 1; i <= nr; i++)
               config = config + numere[i].ToString()+ " ";
           if (nr < 4)
           {
               for (int i = 1; i <= nr; i++)
                   config = config + Strat(i) + " = " + numere[i].ToString() + Environment.NewLine;
           }
           else
           {
               if (nr % 2 == 0)
               {
                   nr = nr / 2;
                   for (int i = 1; i <= nr; i++)
                       config = config + Strat(i) + " = " + numere[i].ToString() + "               " + Strat(i + 2) + " = " + numere[i + 2].ToString() + Environment.NewLine;
               }
               else
               {
                   for (int i = 1; i <= (nr-1)/2; i++)
                       config = config + Strat(i) + " = " + numere[i].ToString() + "              " + Strat(i + 2) + " = " + numere[i + 2].ToString() + Environment.NewLine;
                   config = config + "            " + Strat(nr) + " = " + numere[nr].ToString() + Environment.NewLine;
               }
           }
            */
            return config;
        }
  
    }
}
