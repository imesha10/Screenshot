using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screenshot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setComboBox();
            setDefaultValues();
        }

        private void setDefaultValues()
        {
            textBox1.Text = Screenshot.Properties.Settings.Default.x;
            textBox2.Text = Screenshot.Properties.Settings.Default.y;
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == null) return;
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.DeviceName.Substring(4) + (screen.Primary ? "(Primary)" : "")== comboBox1.Text)
                {
                    Bitmap b1 = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
                    Graphics shot = Graphics.FromImage(b1);
                    shot.CopyFromScreen(screen.Bounds.X, screen.Bounds.Y, 0,0,screen.Bounds.Size, CopyPixelOperation.SourceCopy);

                    Clipboard.SetImage(ResizeBitmap(b1, Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text)));
                }
            }
        }
        // From: http://csharpexamples.com/c-resize-bitmap-example/
        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        private void setComboBox()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                comboBox1.Items.Add(screen.DeviceName.Substring(4) + (screen.Primary?"(Primary)":""));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Screenshot.Properties.Settings.Default.x = textBox1.Text;
            Screenshot.Properties.Settings.Default.y = textBox2.Text;
            Screenshot.Properties.Settings.Default.Save();

        }
    }
}
