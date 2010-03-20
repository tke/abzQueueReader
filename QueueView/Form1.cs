using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QueueView
{
    public partial class Form1 : Form
    {
        abzQueueReader.ABZQueueReader reader;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Queue File|queue.abz"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    reader = new abzQueueReader.ABZQueueReader();
                    reader.loadFile(dialog.FileName);
                    bindingSource1.DataSource = reader;
                }
                catch (Exception ex)
                {
                    Console.Error.Write(ex);
                }
            }
        }
    }
}
