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
    public partial class FrmListadoCajaHIstorica : Form
    {
        public FrmListadoCajaHIstorica()
        {
            InitializeComponent();
        }

        private void FrmListadoCajaHIstorica_Load(object sender, EventArgs e)
        {   
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechahasta.Text = fecha.ToShortDateString();
            fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            if (fun.ValidarFecha(txtFechaDesde.Text) == false)
            {
                Mensaje("La fecha desde es incorrecta");
                return;
            }

            if (fun.ValidarFecha(txtFechahasta.Text) == false)
            {
                Mensaje("La fecha hasta es incorrecta");
                return;
            }

            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechahasta.Text);
            cCajaHistorica caja = new cCajaHistorica();
            DataTable trdo = caja.GetCajaHistorica(FechaDesde, FechaHasta);
            trdo =  fun.TablaaMiles(trdo, "MontoInicial");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 105;
            Grilla.Columns[1].HeaderText = "Inicial";
            Grilla.Columns[2].Width = 110;
            Grilla.Columns[2].HeaderText = "Fecha"; 
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un elemento para continuar");
                return;
            }

            Principal.CodigoPrincipalAbm = Grilla.CurrentRow.Cells[0].Value.ToString();
            this.Close();
        }
    }
}
