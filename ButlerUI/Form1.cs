using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoorButler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ButlerAI.Vision a = new ButlerAI.Vision();
            var i = a.GetFaces();
            pictureBox1.Image = i;

            //i = a.GetFaces();
            //pictureBox1.Image = i;

            //i = a.GetFaces();
            //pictureBox1.Image = i;

        }
    }
}
