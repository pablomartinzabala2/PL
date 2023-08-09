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
    public partial class FrmResumen : Form
    {
        public FrmResumen()
        {
            InitializeComponent();
        }

        private void CargarPeluquero()
        {
            cPeluquero pel = new cPeluquero();
            DataTable trdo = pel.GetPeluquerosActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable(cmbPeluquero, trdo, "Apellido", "CodPeluquero");
        }

        private void FrmResumen_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
          //  fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            CargarPeluquero();
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            Int32? CodPeluquero = null;
            if (cmbPeluquero.SelectedIndex > 0)
                CodPeluquero = Convert.ToInt32(cmbPeluquero.SelectedValue);
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
            cCorte corte = new cCorte();
            DataTable trdo = corte.GetCortesResumen(FechaDesde, FechaHasta,CodPeluquero);
            AgregarPorcentaje(trdo);
            //trdo = fun.TablaaMiles(trdo, "Total");
            //Grilla.DataSource = trdo;
           
        }

        private void AgregarPorcentaje(DataTable Trdo)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            double Total = fun.TotalizarColumna(Trdo, "Total");
            Int32 Porcen = 0;
            double PorAplicado = 0;
            Trdo.Columns.Add("PorcentajeTotal");
            double SubTotal = 0;
            
            for (int i = 0; i < Trdo.Rows.Count; i++)
            {
                SubTotal = Convert.ToDouble(Trdo.Rows[i]["Total"]);
                if (Trdo.Rows[i]["Porcentaje"].ToString() != "")
                {
                    Porcen = Convert.ToInt32(Trdo.Rows[i]["Porcentaje"].ToString());
                    PorAplicado = (double)(SubTotal * Porcen / 100);
                    Trdo.Rows[i]["PorcentajeTotal"] = Math.Round(PorAplicado, 2);
                }
                //Por = SubTotal * 100 / Total;
                
            }
            Trdo = fun.TablaaMiles(Trdo, "Total");
            Trdo = fun.TablaaMiles(Trdo, "PorcentajeTotal");
            Grilla.DataSource = Trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width  = 185;
            Grilla.Columns[2].Width = 185;
            Grilla.Columns[6].HeaderText = "Por. Aplic."; 
          //  Grilla.Columns[3].Width = 140;
          //  Grilla.Columns[4].Width = 140;
          //  Grilla.Columns[5].Width = 140;
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            Grilla.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            Grilla.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
        }
    }
}
