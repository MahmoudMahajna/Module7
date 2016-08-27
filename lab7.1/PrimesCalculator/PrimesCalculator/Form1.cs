using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimesCalculator
{
    //UI is unresponsive.
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            PrimesCalculator pc = new PrimesCalculator();
            try
            {
                var from = int.Parse(txtFrom.Text);
                var to = int.Parse(txtTo.Text);
                IEnumerable<int> primes = null;
                var thread = new Thread(() =>
                {
                    primes = pc.CalcPrimes(from, to);
                });
                thread.Start();

                //This is not good. This is making your UI unresponsive.
                thread.Join();

                //You should have used the same 'Invoke' method for all of the numbers.
                //lstResult.Invoke((Action) (() => primes.ToList().ForEach(prime => lstResult.Items.Add(prime))));
                primes.ToList().ForEach((prime) => lstResult.Invoke((Action)(() => lstResult.Items.Add(prime))));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
