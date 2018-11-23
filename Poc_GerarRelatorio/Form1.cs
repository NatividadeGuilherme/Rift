using FastReport;
using System;
using System.Collections.Generic;
using Rift.Models;
using System.Windows.Forms;

namespace Poc_GerarRelatorio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var report = new Report();
            var dados = new List<RelatorioChamados>();
            report.RegisterData(dados, "Dados", 10);
            report.GetDataSource("Dados").Enabled = true;
            report.Design();
            report.Dispose();

        }
    }
}
