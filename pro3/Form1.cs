using System.Text.RegularExpressions;

namespace pro3
{
    public partial class Form1 : Form
    {
        string filepath;
        int originalLines = 0;
        int originalWords = 0;
        int validLines = 0;
        int validWords = 0;
        Dictionary<string, int> allWords = new Dictionary<string, int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文件|*.cs";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filepath = dialog.FileName;
                this.label1.Text = dialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamReader rd = new StreamReader(filepath))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                {
                    originalLines++;
                }

            }
            label2.Text = originalLines.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (StreamReader rd = new StreamReader(filepath))
            {

                string pattern = @"\b\w+\b";
                string line;
                MatchCollection matches;
                while ((line = rd.ReadLine()) != null)
                {
                    matches = Regex.Matches(line, pattern);
                    originalWords += matches.Count;
                }
                label3.Text = originalWords.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (StreamReader rd = new StreamReader(filepath))
            {
                string line;
                while ((line = rd.ReadLine()) != null)
                {
                    if (line.Length >= 2 && line.Substring(0, 2) != "\\")
                    {
                        validLines++;
                    }

                }

            }
            label4.Text = validLines.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (StreamReader rd = new StreamReader(filepath))
            {
                string pattern = @"\b\w+\b";
                string line;
                MatchCollection matches;
                while ((line = rd.ReadLine()) != null)
                {
                    if (line.Length >= 2 && line.Substring(0, 2) != "\\")
                    {
                        matches = Regex.Matches(line, pattern);
                        validWords += matches.Count;

                    }
                }
                label5.Text = validWords.ToString();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (StreamReader rd = new StreamReader(filepath))
            {
                string pattern = @"\b\w+\b";
                string line;
                MatchCollection matches;
                while ((line = rd.ReadLine()) != null)
                {
                    if (line.Length >= 2 && line.Substring(0, 2) != "\\")
                    {
                        matches = Regex.Matches(line, pattern);
                        foreach (Match match in matches)
                        {
                            string temp = match.Value;
                            if (allWords.ContainsKey(temp))
                            {
                                allWords[temp]++;
                            }
                            else
                            {
                                allWords.Add(temp, 1);
                            }
                        }

                    }
                }

            }
            foreach (var kv in allWords)
            {
                string word = kv.Key;
                int number = kv.Value;
                ListViewItem item = new ListViewItem(word);
                item.SubItems.Add(number.ToString());
                listView1.Items.Add(item);
            }
            listView1.Visible = true;
        }
    }
}