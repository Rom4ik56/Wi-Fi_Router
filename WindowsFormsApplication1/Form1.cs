using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Media;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
      
           
            InitializeComponent();
          
            // сдвиг окна с любой ее области
            this.MouseDown += delegate
            {
                this.Capture = false;
                var msg = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                this.WndProc(ref msg);
            };

            
            textBox1.Text = Properties.Settings.Default.SaveLabelText;//сохранение значений
            textBox2.Text = Properties.Settings.Default.SaveLabel1Text; //



            notifyIcon1.Visible = false;
            this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);                                  // трей
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = "netsh wlan start hostednetwork";
         
                // создается ProcessStartInfo с использованием "CMD" в качестве программы для запуска
                // и "/c " в качестве параметров.
                // /c говорит CMD, что далее будет следовать команда для запуска
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command );
                // Следующая команды означает, что нужно перенаправить стандартынй вывод
                // на Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // не создавать окно CMD
                procStartInfo.CreateNoWindow = true;

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                // Получение текста в виде кодировки 866 win
                procStartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                //запуск CMD
                proc.StartInfo = procStartInfo;
                proc.Start();
                //чтение результата
                string result = proc.StandardOutput.ReadToEnd();

                textBox3.Text = result; 
            
           }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            //Имя запускаемого приложения
            psi.FileName = "cmd";

            //команда, которую надо выполнить       
            psi.Arguments = @"/c netsh wlan stop hostednetwork";

            //  /c - после выполнения команды консоль закроется 
            //  /к - не закрывать консоль после выполнения команды
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(psi);
            textBox3.Text = ("Раздача прекращена");
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string command = "netsh wlan show hostednetwork";

            // создается ProcessStartInfo с использованием "CMD" в качестве программы для запуска
            // и "/c " в качестве параметров.
            // /c говорит CMD, что далее будет следовать команда для запуска
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
            // Следующая команды означает, что нужно перенаправить стандартынй вывод
            // на Process.StandardOutput StreamReader.
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            // не создавать окно CMD
            procStartInfo.CreateNoWindow = true;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            // Получение текста в виде кодировки 866 win
            procStartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            //запуск CMD
            proc.StartInfo = procStartInfo;
            proc.Start();
            //чтение результата
            string result = proc.StandardOutput.ReadToEnd();

            textBox3.Text = result;

            /* ProcessStartInfo psi = new ProcessStartInfo(); psi.FileName = "cmd";
             psi.Arguments = @"/k netsh wlan show hostednetwork";
             Process.Start(psi) ;*/

  }

        private void button4_Click(object sender, EventArgs e)
        {

            if (textBox1.TextLength < 4)
            {
                MessageBox.Show("Длина имени сети меньше допустимой. Минимальная длина 5 символа.","ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox1.TextLength > 19)
            {
                MessageBox.Show("Длина имени превышает допустимую. Максимальная длина 16 символов.", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox2.TextLength < 7)
            {
                MessageBox.Show("Длина пароля меньше допустимой. Минимальная длина 8 символов.", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox2.TextLength > 19)
            {
                MessageBox.Show("Длина пароля превышает допустимую. Максимальная длина 16 символов.", "ВНИМАНИЕ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (textBox1.TextLength > 4 && textBox1.TextLength < 19 && textBox2.TextLength > 7 && textBox2.TextLength < 19)
            {
                ProcessStartInfo psi = new ProcessStartInfo();

                //Имя запускаемого приложения
                psi.FileName = "cmd";

                //команда, которую надо выполнить       
                psi.Arguments = @"/c netsh wlan set hostednetwork mode=allow ssid=" + textBox1.Text + " key=" + textBox2.Text;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                //  /c - после выполнения команды консоль закроется 
                //  /к - не закрывать консоль после выполнения команды

                Process.Start(psi);


                Properties.Settings.Default.SaveLabelText = textBox1.Text;
                Properties.Settings.Default.Save();

                // label4.Text = textBox2.Text;
                Properties.Settings.Default.SaveLabel1Text = textBox2.Text;
                Properties.Settings.Default.Save();
                textBox3.Text = ("Данные обновлены");
            }

        }

        private void инструкцииToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void какНастроитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void чтоЗдесьПоказаноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Название сети на латинице (английские буквы).\nПароль должен содержать только латиницу и цифры.\nОкно сворачивается в трей (рядом с часами)", "Инструкция", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = new System.Windows.Forms.DialogResult();
            result = MessageBox.Show("Вы уверены что хотите выйти?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = new System.Windows.Forms.DialogResult();
            result = MessageBox.Show("Вы уверены что хотите выйти?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void дизайнToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для переноса окна в другую часть экрана, нажмите в любом месте на фон, удерживайте и переносите.", "Инструкция по дизайну", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[A-z0-9\b]"); //только лат и цифры
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[A-z0-9\b]"); //только лат и цифры
 }

        private void инструкцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
          //  Help.ShowHelp(this, "11.gui");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
    
        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_2(object sender, EventArgs e)
        {

            

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;

            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void разработчикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Инженерно-Технический Институт\nГруппа 13Ис1\nТалпа Роман");
        }
    }
    }



