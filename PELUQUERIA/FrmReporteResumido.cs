using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PELUQUERIA
{
    public partial class FrmReporteResumido : Form
    {
        public FrmReporteResumido()
        {
            InitializeComponent();
        }

        private void FrmReporteResumido_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'PELUQUERIADataSet.Reporte' table. You can move, or remove it, as needed.
            this.ReporteTableAdapter.Fill(this.PELUQUERIADataSet.Reporte);

            this.reportViewer1.RefreshReport();
        }
    }
}
