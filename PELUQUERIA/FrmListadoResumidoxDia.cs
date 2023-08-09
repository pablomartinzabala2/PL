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
    public partial class FrmListadoResumidoxDia : Form
    {
        cFunciones fun;
        public FrmListadoResumidoxDia()
        {
            InitializeComponent();
        }

        private void FrmListadoResumidoxDia_Load(object sender, EventArgs e)
        {
            fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            //fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            CargarPeluquero();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            Int32? CodPeluquero = null;
            if (cmbPeluquero.SelectedIndex > 0)
                CodPeluquero = Convert.ToInt32(cmbPeluquero.SelectedValue);
            cCorte corte = new cCorte();
            DataTable trdo = corte.GetCortesResumidoxDia(FechaDesde, FechaHasta,CodPeluquero);
            trdo = AplicarPorcentaje(trdo);
            trdo = fun.TablaaMiles(trdo, "Total");
            Grilla.DataSource = trdo;
            Grilla.Columns[1].Width = 232;
            Grilla.Columns[2].Width = 232;
            double Total = fun.TotalizarColumna(trdo, "Total");
            double TotalPorcentaje = fun.TotalizarColumna(trdo, "PorcentajeAplicado");
            txtTotal.Text = Total.ToString();
            txtTotalPorcentaje.Text = TotalPorcentaje.ToString();
            if (txtTotal.Text != "")
            {
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            }
            else
            {
                txtTotal.Text = "0";
            }

            if (txtTotalPorcentaje.Text != "")
            {
                txtTotalPorcentaje.Text = fun.SepararDecimales(txtTotalPorcentaje.Text);
                txtTotalPorcentaje.Text = fun.FormatoEnteroMiles(txtTotalPorcentaje.Text);
            }
            else
            {
                txtTotalPorcentaje.Text = "0";
            }
            Grilla.Columns[4].Visible = false;
            Grilla.Columns[5].HeaderText = "Porcentaje"; 
            
        }

        private DataTable  AplicarPorcentaje(DataTable trdo)
        {
            trdo.Columns.Add("PorcentajeAplicado");
            double Por = 0;
            double Total = 0;
            double PorAplicado = 0;
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                if (trdo.Rows[i]["Porcentaje"].ToString() != "")
                {
                    Por = Convert.ToDouble(trdo.Rows[i]["Porcentaje"].ToString());
                    Total = 0;
                    if (trdo.Rows[i]["Total"].ToString() != "")
                    {
                        Total = Convert.ToDouble(trdo.Rows[i]["Total"].ToString());
                    }
                    PorAplicado = Por * Total / 100;
                    trdo.Rows[i]["PorcentajeAplicado"] = PorAplicado.ToString();
                }
            }
            return trdo;
        }

        private void CargarPeluquero()
        {
            cPeluquero pel = new cPeluquero();
           // DataTable trdo = pel.GetPeluqueros();
            DataTable trdo = pel.GetPeluquerosActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable(cmbPeluquero, trdo, "Apellido", "CodPeluquero");
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            if (Grilla.Rows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un registro");
                return;
            }
            string Campo1 = "";
            string Campo2 = "";
            string Campo3 = "";
            string Campo4 = "";
            string Campo5 = "";
            string Campo6 = "";
            string Campo7 = "";
            string Campo8 = "";
            
            Campo8 = "Periodo " + txtFechaDesde.Text + " - " + txtFechaHasta.Text;
            cReporte objReporte = new cReporte();
            objReporte.Borrar();
            for (int i = 0; i < Grilla.Rows.Count - 1; i++)
            {
                Campo1 = Grilla.Rows[i].Cells[0].Value.ToString();
                if (Campo1.Trim() == "")
                    Campo1 = "Totales";
                if (Campo1.Length > 10)
                    Campo1 = Campo1.Substring(0, 10);
                Campo2 = Grilla.Rows[i].Cells[1].Value.ToString();
                Campo3 = Grilla.Rows[i].Cells[2].Value.ToString();
                Campo4 = Grilla.Rows[i].Cells[3].Value.ToString();
               /// Campo5 = Grilla.Rows[i].Cells[4].Value.ToString();
               // Campo6 = Grilla.Rows[i].Cells[5].Value.ToString();
                Campo8 = Grilla.Rows[i].Cells[5].Value.ToString();
                objReporte.Insertar(Campo1, Campo2, Campo3, Campo4, Campo5, Campo6, Campo7, Campo8);
            }
            objReporte.Insertar("", "", "TOTAL PORCENTAJES", txtTotalPorcentaje.Text , "", "", "", "");
            objReporte.Insertar("", "", "TOTAL ACUMULADO", txtTotal.Text , "", "", "", "");
            FrmReporteResumido frm = new FrmReporteResumido();
            frm.Show();
        }
    }
}
