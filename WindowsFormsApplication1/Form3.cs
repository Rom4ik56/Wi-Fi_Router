using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Timer t = new Timer();
            t.Interval = 5100; // 5 секунд
            t.Start();
            t.Tick += new EventHandler(t_Tick); //через 5 сек генерируется событие, обработчик фкц t_Tick

            SoundPlayer sp = new SoundPlayer(WindowsFormsApplication1.Properties.Resources.sms_zvuk);
            sp.Play();
        }
        void t_Tick(object sender, EventArgs e) // тут просто закрываю форму логотип
        {
            Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
