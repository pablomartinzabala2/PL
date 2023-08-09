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
    public partial class FrmListadoTarjetas : Form
    {
        public FrmListadoTarjetas()
        {
            InitializeComponent();
        }

        private void FrmListadoTarjetas_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            // fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            fun.LlenarCombo(CmbTarjeta, "Tarjeta", "Nombre", "CodTarjeta");
            Buscar();
        }

        private void Buscar()
        {
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            Int32? CodTarjeta = null;
            if (CmbTarjeta.SelectedIndex > 0)
                CodTarjeta = Convert.ToInt32(CmbTarjeta.SelectedValue);
            cTarjeta tarjeta = new cTarjeta();
            DataTable trdo = tarjeta.GetTrabajosxTarjeta(FechaDesde, FechaHasta, CodTarjeta);
            cFunciones fun = new cFunciones();
            double Total = fun.TotalizarColumna(trdo, "ImporteTarjeta");
            txtTotal.Text = Total.ToString();
            trdo = fun.TablaaMiles(trdo, "ImporteTarjeta");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Width = 240;
            Grilla.Columns[1].Width = 100;
            Grilla.Columns[2].Width = 190;
            Grilla.Columns[3].Width = 190;
            Grilla.Columns[4].HeaderText = "Total"; 
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
