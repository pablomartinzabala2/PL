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
    public partial class FrmResumenTrabajos : Form
    {
        public FrmResumenTrabajos()
        {
            InitializeComponent();
        }

        

        private void FrmResumenTrabajos_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
           // fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            if (chkFechas.Checked == true)
            {
                if (fun.ValidarFecha(txtFechaDesde.Text) == false)
                {
                    Mensaje("La fecha desde es incorrecta");
                    return;
                }

                if (fun.ValidarFecha(txtFechaHasta.Text) == false)
                {
                    Mensaje("La fecha hasta es incorrecta");
                    return;
                }
                FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
                FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            }
            Int32? CodPeluquero = null;
            cCorte corte = new cCorte();
            DataTable trdo = corte.GetCortesResumenxTarea(FechaDesde, FechaHasta, CodPeluquero);
            AgregarPorcentaje(trdo);
            //  double Total = fun.TotalizarColumna(trdo, "Total");
          //  txtTotal.Text = Total.ToString();
         //   txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
         //   txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
         //   Grilla.DataSource = trdo;
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void AgregarPorcentaje(DataTable Trdo)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            double Total = fun.TotalizarColumna(Trdo, "Total");
            Trdo.Columns.Add("Porcentaje");
            double SubTotal = 0;
            double Por = 0;
            for (int i = 0; i < Trdo.Rows.Count; i++)
            {
                SubTotal = Convert.ToDouble(Trdo.Rows[i]["Total"]);
                Por = SubTotal * 100 / Total;
                Trdo.Rows[i]["Porcentaje"] = Math.Round(Por, 2);
            }
            Trdo = fun.TablaaMiles(Trdo, "Total");
            Grilla.DataSource = Trdo;
         //   Grilla.Columns[0].Visible = false;
        //    Grilla.Columns[1].Width = 175;
             Grilla.Columns[0].Width = 320;
        //    Grilla.Columns[3].Width = 140;
        //    Grilla.Columns[4].Width = 140;
        //    Grilla.Columns[5].Width = 140;
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }

        private void chkFechas_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
