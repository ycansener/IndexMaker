using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IndexMaker
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog fbd;
        string path;
        string[] folders;
        string[] files;
        public Form1()
        {
            InitializeComponent();
            toolTip1.SetToolTip(listBox1, "Bulunduğu dizini açmak için çift tıklayınız. İşlem menüsü için sağ tıklayınız.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath.ToString() != "")
            {
                path = fbd.SelectedPath;
                textBox1.Text = path;
                folders = Directory.GetDirectories(path);
                files = Directory.GetFiles(path);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folders != null || files != null)
            {
                if (checkBox1.Checked)
                {
                    yaz(folders, 0);
                    yaz(files, 0);
                }
                else
                {
                    duzYaz(folders);
                    duzYaz(files);
                }
            }
        }

        private void yaz(string[] paths,int degree)
        {
            string newPath;
            string ayirac = "";
            for (int i = 0; i < paths.Length; i++)
            {
                for (int j = 0; j < degree; j++)
                    ayirac += "     ";
                listBox1.Items.Add(ayirac +"-"+ paths[i].ToString());
                newPath = paths[i].ToString();
                if (Directory.Exists(newPath))
                {
                    yaz(Directory.GetDirectories(newPath),degree+1);
                    yaz(Directory.GetFiles(newPath),degree+1);
                }
                ayirac = "";
            }
        }

        private void duzYaz(string[] paths)
        {
            string newPath;
            for (int i = 0; i < paths.Length; i++)
            {
                listBox1.Items.Add(paths[i].ToString());
                newPath = paths[i].ToString();
                if (Directory.Exists(newPath))
                {
                    duzYaz(Directory.GetDirectories(newPath));
                    duzYaz(Directory.GetFiles(newPath));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void temizle()
        {
            listBox1.Items.Clear();
            textBox1.Clear();
            folders = null;
            files = null;
            path = "";
            if(fbd != null)
            fbd.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string aranan;
            ArrayList eslesenler = new ArrayList();
            try
            {
                if (textBox3.Text != null || textBox3.Text != "")
                {
                    aranan = textBox3.Text;

                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i].ToString().Contains(aranan))
                            eslesenler.Add(listBox1.Items[i]);
                    }

                    listBox1.Items.Clear();

                    for (int i = 0; i < eslesenler.Count; i++)
                    {
                        listBox1.Items.Add(eslesenler[i]);
                    }
                }
                else throw new Exception("Aranacak kelimeyi giriniz!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string secilenEleman = "";
            string dizin = "";
            string[] dizi;
            if (listBox1.SelectedIndex > 0)
            {
                secilenEleman = listBox1.SelectedItem.ToString();
                secilenEleman.Replace("-", "");
                dizi = secilenEleman.Split('\\');
                secilenEleman = "";
                dizin = dizi[0];

                for (int i = 1; i < dizi.Count() - 1; i++)
                {
                    secilenEleman += '\\' + dizi[i];
                }
                //secilenEleman.Replace(":", ':');

                System.Diagnostics.Process.Start(dizin + secilenEleman);
            }
            else MessageBox.Show("Öncelikle seçim yapmalısınız.");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void openDirectly_Click(object sender, EventArgs e)
        {
            string secilenEleman = "";
            if (listBox1.SelectedIndex > 0)
            {
                secilenEleman = listBox1.SelectedItem.ToString();
                secilenEleman.Replace("-", "");

                System.Diagnostics.Process.Start(secilenEleman);
            }
            else MessageBox.Show("Öncelikle seçim yapmalısınız.");
        }
    }
}
