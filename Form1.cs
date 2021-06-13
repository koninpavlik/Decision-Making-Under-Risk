using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            fillGrid();
        }

        private void fillGrid()
        {
            List<List<double>> list = new List<List<double>> {
                new List<double> { 5, 4, 3, 5, 3, 4, 3, 4, 5, 3, 5 },
                new List<double> { 2, 4, 1, 5, 3, 1, 5, 1, 2, 3, 5},
                new List<double> { 3, 5, 3, 4, 1, 5, 4, 2, 3, 4, 2},
                new List<double> { 4, 1, 3, 5, 2, 4, 2, 2, 5, 3, 2},
                new List<double> { 5, 5, 3, 5, 4, 5, 4, 5, 4, 3, 5}
            };
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
                dataGridView1.Columns[i].DefaultCellStyle.NullValue = "0";
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView3.Columns.Add(i.ToString(), i.ToString());
                dataGridView3.Columns[i].DefaultCellStyle.NullValue = "0";
            }
            for (int i = 0; i < numericUpDownElements.Value; i++)
            {
                dataGridView1.Rows.Add();
            }
            dataGridView3.Rows.Add();
        }

        private void numericUpDownCriteries_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownCriteries.Value)
            {
                dataGridView3.Columns.Add((dataGridView3.Columns.Count).ToString(), (dataGridView3.Columns.Count).ToString());
                dataGridView3.Columns[dataGridView3.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
                dataGridView1.Columns.Add((dataGridView1.Columns.Count).ToString(), (dataGridView1.Columns.Count).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].DefaultCellStyle.NullValue = "0";
            }
            else
            {
                dataGridView3.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
                dataGridView1.Columns.Remove((Convert.ToInt32(((UpDownBase)sender).Text) - 1).ToString());
            }
        }

        private void numericUpDownElements_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((UpDownBase)sender).Text) < numericUpDownElements.Value)
            {
                dataGridView1.Rows.Add();
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
        }

        private static List<List<double>> dgwTo2dListOfDouble(DataGridView dataGridView)
        {
            List<List<double>> list = new List<List<double>>();
            for (int i2 = 0; i2 < dataGridView.Rows.Count; i2++)
            {
                list.Add(new List<double>());
                for (int j2 = 0; j2 < dataGridView.Columns.Count; j2++)
                {
                    list[i2].Add(Convert.ToDouble(dataGridView.Rows[i2].Cells[j2].Value));
                }
            }
            return list;
        }

        private void buttonRandomize_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = rnd.Next(0, 3).ToString();
                }
            }
            for (int i = 0; i < numericUpDownCriteries.Value; i++)
            {
                dataGridView3.Rows[0].Cells[i].Value = rnd.Next(0, 3).ToString();
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs ev)
        {
            var elements = dgwTo2dListOfDouble(dataGridView1);

            var states = dgwTo2dListOfDouble(dataGridView3);

            var matrix = new List<List<double>>();


            for (int i = 0; i < elements.Count; i++)
            {
                matrix.Add(new List<double>());
                for (int j = 0; j < elements[i].Count; j++)
                {
                    matrix[i].Add(elements[i][j] * states[0][j]);
                }
            }

            richTextBox1.Text = "";

            double max = 0, min = 0;
            for (int i = 0; i < matrix.Count; i++)
            {
                double sum = 0;
                matrix[i].ForEach(e => sum += e);
                richTextBox1.Text += $"H(d{i}) = {sum}\n";
                if (i == 0)
                {
                    max = sum;
                    min = sum;
                }
                else
                {
                    if (sum > max)
                    {
                        max = sum;
                    }
                    else if (sum < min)
                    {
                        min = sum;
                    }
                }
            }

            richTextBox1.Text += $"Используя критерий ожидаемой полезности победило значение {max}\n";
            richTextBox1.Text += $"Используя критерий дисперсии полезности победило значение {min}\n";
        }
    }
}
