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
    public partial class FrmBalance : Form
    {
        public FrmBalance()
        {
            InitializeComponent();
        }

        private void FrmBalance_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
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
            DateTime Fecha = Convert.ToDateTime(FechaDesde);
            string Fin = "";
            double Importe = 0;
            cCorte Corte = new cCorte();
            cVenta Venta = new cVenta();
            cGasto Gasto = new cGasto();
            cMovimiento mov = new cMovimiento();
            DataTable tbBalance = new DataTable();
            tbBalance.Columns.Add("Fecha");
            tbBalance.Columns.Add("Produccion");
            tbBalance.Columns.Add("Compras");
            tbBalance.Columns.Add("Ventas");
            tbBalance.Columns.Add("Gastos");
            tbBalance.Columns.Add("Personales");
            tbBalance.Columns.Add("Tarjetas");

            while (Fin != "Si")
            {
                Importe = Corte.Balance(Fecha);
                DataRow r = tbBalance.NewRow();
                r["Fecha"] = Fecha.ToShortDateString();
                r["Produccion"] = Importe.ToString();
                Importe = Gasto.Balance(Fecha, 3);
                r["Compras"] = Importe.ToString();
                Importe = Venta.Balance(Fecha);
                r["Ventas"] = Importe.ToString();
                Importe = Gasto.Balance(Fecha, 1);
                r["Gastos"] = Importe.ToString();
                Importe = Gasto.Balance(Fecha, 2);
                r["Personales"] = Importe.ToString();
                Importe = mov.BalanceTarjeta(Fecha);
                r["Tarjetas"] = Importe.ToString();
                tbBalance.Rows.Add(r);
                Fecha = Fecha.AddDays(1);
                if (Fecha > FechaHasta)
                    Fin = "Si";

            }
            double Produccion = fun.TotalizarColumna(tbBalance, "Produccion");
            double Compras = fun.TotalizarColumna(tbBalance, "Compras");
            double Ventas = fun.TotalizarColumna(tbBalance, "Ventas");
            double Gastos = fun.TotalizarColumna(tbBalance, "Gastos");
            double Personales = fun.TotalizarColumna(tbBalance, "Personales");
            double Tarjetas = fun.TotalizarColumna(tbBalance, "Tarjetas");
            DataRow r2 = tbBalance.NewRow();
            r2["Fecha"] = "";
            r2["Produccion"] = Produccion.ToString();
            r2["Compras"] = Compras.ToString();
            r2["Ventas"] = Ventas.ToString();
            r2["Gastos"] = Gastos.ToString();
            r2["Personales"] = Personales.ToString();
            r2["Tarjetas"] = Tarjetas.ToString();
            tbBalance.Rows.Add(r2);
            Grilla.DataSource = tbBalance;
            Grilla.Columns[1].Width = 110;
            Grilla.Columns[2].Width = 110;
            Grilla.Columns[3].Width = 110;
            Grilla.Columns[4].Width = 110;
            Grilla.Columns[5].Width = 110;
            Grilla.Columns[6].Width = 115;
            int can = Grilla.Rows.Count - 2;
            Grilla.Rows[can].DefaultCellStyle.BackColor = Color.LightGreen;

        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Grilla.Rows.Count == 0)
            {
                Mensaje("Debe seleccionar un registro");
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
                Campo2 = Grilla.Rows[i].Cells[1].Value.ToString();
                Campo3 = Grilla.Rows[i].Cells[2].Value.ToString();
                Campo4 = Grilla.Rows[i].Cells[3].Value.ToString();
                Campo5 = Grilla.Rows[i].Cells[4].Value.ToString();
                Campo6 = Grilla.Rows[i].Cells[5].Value.ToString();
                Campo7 = Grilla.Rows[i].Cells[6].Value.ToString();
                objReporte.Insertar(Campo1, Campo2, Campo3, Campo4, Campo5, Campo6, Campo7,Campo8);
            }
            FrmReporte frm = new FrmReporte();
            frm.Show();

        }
    }
}
