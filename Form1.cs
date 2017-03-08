using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPR2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<double> fun = new List<double>();
        public List<double> x1L = new List<double>();
        public List<double> x2L = new List<double>();
        public double funk(double x1, double x2) {
            return Math.Pow(x1, 2) + Math.Pow(x2, 2) - 16 * x1 - 10 * x2;
        }

        public void Setka() {
            dataGridView1.RowCount = x1L.Count+1; dataGridView1.ColumnCount = x2L.Count+1;
            for (int i = 0; i < x1L.Count; i++) {
                dataGridView1.Rows[i + 1].Cells[0].Value = x1L[i];
                dataGridView1.Rows[0].Cells[i + 1].Value = x2L[i]; 
            }
            for (int i = 1; i <= x1L.Count; i++) {
                for (int j = 1; j <= x1L.Count; j++){
                    if (i == j){
                        dataGridView1.Rows[i].Cells[i].Value = Math.Round(fun[i-1],2);
                        if(fun[i-1] == fun.Min()){
                            dataGridView1.Rows[i].Cells[i].Style.BackColor = System.Drawing.Color.Aqua;
                            dataGridView1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.Aqua;
                            dataGridView1.Rows[0].Cells[i].Style.BackColor = System.Drawing.Color.Aqua;
                            label1.Text = Convert.ToString(dataGridView1.Rows[i].Cells[i].Value);
                            label2.Text = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                            label3.Text = Convert.ToString(dataGridView1.Rows[0].Cells[i].Value);
                        }
                    }
                    else dataGridView1.Rows[i].Cells[j].Value = " - ";               
                }            
            }         
        }

        private void button1_Click(object sender, EventArgs e){
            Random r = new Random();
            int imin = -1, imax = 4; double x1 = 0, x2 = 0;
            for (int i = 0; i < 121; i++) {
                x1 = imin + r.NextDouble() * (imax - imin);
                x2 = imin + r.NextDouble() * (imax - imin);
                if (Math.Pow(x1, 2) - 6 * x1 + 4 * x2 - 11 >= 0 && 3 * x2 - x1 * x2 + Math.Exp(x1 - 3) - 1 >= 0){
                    x1L.Add(Math.Round(x1,2)); x2L.Add(Math.Round(x2,2)); fun.Add(Math.Round(funk(x1, x2),5));
                }                   
            }
            int k1 = 0;
            for (int i = 0; i < x1L.Count; i++){
                double az = x2L[i];
                for (int j = i + 1; j < x1L.Count; j++){
                    if (az == x2L[j]){
                        if (k1 == 8) break;
                        chart1.Series[k1].Points.AddXY(x1L[i], x2L[i]);
                        chart1.Series[k1].Points.AddXY(x1L[j], x2L[j]);
                        k1++;
                    }
                }
            }
            double xx = 0, yy = 0;
            for (int i = 0; i < fun.Count; i++) {
                if (fun[i] == fun.Min()) {
                    xx = x1L[i]; yy = x2L[i];
                }            
            }
            Setka();
            chart1.Series[k1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[k1].Points.AddXY(xx,yy);
        }
    }
}
