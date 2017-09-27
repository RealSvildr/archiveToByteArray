using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace archiveToByteArray {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void button2_Click(object sender, EventArgs e) {
            input.Text = getFile("All Files (*.*)|*.*");
        }
        private void input_Click(object sender, EventArgs e) {
            input.Text = getFile("All Files (*.*)|*.*");
        }

        private void output_Click(object sender, EventArgs e) {
            output.Text = getFile("Text Files (*.txt)|*.txt");
        }
        private void button3_Click(object sender, EventArgs e) {
            output.Text = getFile("Text Files (*.txt)|*.txt");
        }

        private void button1_Click(object sender, EventArgs e) {
            if (input.Text == "") { MessageBox.Show("Select an input."); return; }
            if (output.Text == "") { MessageBox.Show("Select an output."); return; }

            StringBuilder sB = new StringBuilder();

            sB.Append("{ ");

            using (FileStream sR = new FileStream(input.Text, FileMode.Open, FileAccess.Read)) {
                int bytePerLine = 20;
                int b = 0, counter = 0;

                while ((b = sR.ReadByte()) > -1) {
                    sB.Append("0x" + Convert.ToString(b, 16) + ", ");

                    if (counter == bytePerLine) {
                        sB.Append(Environment.NewLine);
                        counter = 0;
                    }

                    counter++;
                }

                sR.Dispose();
            }

            sB.Remove(sB.Length - 2, 1);
            sB.Append("}");

            using (StreamWriter sW = new StreamWriter(output.Text)) {
                sW.WriteLine(sB.ToString());
                sW.Dispose();
            }

            MessageBox.Show("Output has been written.");
        }

        private string getFile(string filter) {
            openFileDialog1.Reset();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = filter;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                return openFileDialog1.FileName;
            }

            return null;
        }
    }
}
