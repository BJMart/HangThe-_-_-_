using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace HangThe______
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string path = "words.txt";

            FileStream fs = File.OpenRead(path);
            byte[] encodedString = new byte[1024];


            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
