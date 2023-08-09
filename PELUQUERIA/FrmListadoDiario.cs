using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PELUQUERIA.Clases; 
namespace PELUQUERIA
{
    public partial class FrmListadoDiario : Form
    {
        public FrmListadoDiario()
        {
            InitializeComponent();
        }

        private void FrmListadoDiario_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            //fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            CargarPeluquero();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            Int32? CodPeluquero = null;
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            cCorte corte = new cCorte();
            if (cmbPeluquero.SelectedIndex > 0)
                CodPeluquero = Convert.ToInt32(cmbPeluquero.SelectedValue);
            DataTable trdo = corte.GetTareasDiarias(FechaDesde, FechaHasta,CodPeluquero);
            trdo = fun.TablaaMiles(trdo, "Importe");
            double Total = fun.TotalizarColumna(trdo, "Importe");
            Grilla.DataSource = trdo;
            Grilla.Columns[5].DefaultCellStyle.Alignment  = DataGridViewContentAlignment.TopRight;  
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].Width = 180;
            Grilla.Columns[3].Width = 206;
            Grilla.Columns[4].Width = 180;
            txtTotal.Text = Total.ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
        }

        private void CargarPeluquero()
        {
            cPeluquero pel = new cPeluquero();
          //  DataTable trdo = pel.GetPeluqueros();
            DataTable trdo = pel.GetPeluquerosActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable(cmbPeluquero, trdo, "Apellido", "CodPeluquero");
        }

        private void cmbPeluquero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
