using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace BackgroundPrimeCalc
{
    public partial class Form1 : Form
    {
        private int _from;
        private int _to;
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var worker = sender as BackgroundWorker;
                var tmp = Enumerable.Range(_from, _to - _from + 1);
                var i = 0;
                foreach (var num in tmp)
                {
                    if ((i++) % 10 == 0)
                    {
                        worker?.ReportProgress(i);
                    }
                    if (worker != null && worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    if (!IsPrime(num)) continue;
                    Invoke((Action)(() => lstResult.Items.Add(num)));
                }
                worker?.ReportProgress(i);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Invoke((Action)(() => progressBar1.Value = e.ProgressPercentage));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(e.Cancelled ? "Canceled" : "Calculated");
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                _from = int.Parse(txtFrom.Text);
                _to = int.Parse(txtTo.Text);
                Invoke((Action)(() =>
                {
                    lstResult.Items.Clear();
                    progressBar1.Maximum = _to - _from + 1;
                    progressBar1.Value = 0;

                }));

                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private static bool IsPrime(int num)
        {
            if (num <= 1)
            {
                return false;
            }
            if (num == 2)
            {
                return true;
            }
            var list = Enumerable.Range(2, (int)(Math.Sqrt(num))).Where((number) => num % number == 0).ToList();
            return list.Count == 0;
        }
    }
}
