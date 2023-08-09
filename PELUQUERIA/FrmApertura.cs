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
    public partial class FrmApertura : Form
    {
        cFunciones fun;
        public FrmApertura()
        {
            InitializeComponent();
        }

        private void FrmApertura_Load(object sender, EventArgs e)
        {
            Inicio();
        }

        private void Inicio()
        {
            fun = new cFunciones();
            cAperturaCaja ap = new cAperturaCaja();
            DataTable trdo = ap.GetUltimaApertura();
            if (trdo.Rows.Count > 0)
                if (trdo.Rows[0]["FechaAbierta"].ToString() != "")
                {
                    DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["FechaAbierta"].ToString());
                    txtFechaApertura.Text = Fecha.ToShortDateString();
                    btnGrabar.Enabled = false;
                    btnCerrar.Enabled = true;
                    Buscar(Fecha, Fecha);
                }
                else
                {
                    btnGrabar.Enabled = true;
                    btnCerrar.Enabled = false;
                }
            else
            {
                btnGrabar.Enabled = true;
                btnCerrar.Enabled = false;
                DateTime Fecha = DateTime.Now;
                txtFechaApertura.Text = Fecha.ToShortDateString();
            }
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            if (fun.ValidarFecha(txtFechaApertura.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }

            if (txtTotal.Text.Trim ()  == "")
            {
                Mensaje("Debe ingresar un importe");
                return;
            }
            DateTime FechaAp = Convert.ToDateTime(txtFechaApertura.Text);
            double Importe = Convert.ToDouble(txtTotal.Text);
            cAperturaCaja aper = new cAperturaCaja();
            aper.AbrirCaja(Importe, FechaAp);
            Inicio();
            Mensaje("Datos grabados correctamente");
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (fun.ValidarFecha(txtFechaApertura.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }
            Principal.VieneDeCaja = true;
            FrmListadoCajaDiario form = new FrmListadoCajaDiario();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            DateTime FechaAp = Convert.ToDateTime(txtFechaApertura.Text);
            Int32 CodApertura = 0;
            cMovimiento mov = new cMovimiento();
            cAperturaCaja AP = new cAperturaCaja();
            DataTable trdo = AP.GetUltimaApertura();
            if (trdo.Rows.Count > 0)
            {
                CodApertura = Convert.ToInt32(trdo.Rows[0]["CodApertura"].ToString());
            }
            AP.CierreCaja(CodApertura, FechaAp);
            mov.ActualizarProceso();
            Inicio();
            Mensaje("Datos grabados correctamente");
        }

        private void Buscar(DateTime FechaDesde, DateTime FechaHasta)
        {
            cMovimiento mov = new cMovimiento();
            double TotalEfectivo = mov.GetImporteEfectivos(FechaDesde, FechaHasta, true);
            double TotalTarjeta = mov.GetImporteTarjeta(FechaDesde, FechaHasta, true);
            double TotalTrabajos = TotalEfectivo + TotalTarjeta;
            cAperturaCaja ap = new cAperturaCaja();
            double TotalApertura = ap.GetUltimoImporte(FechaDesde);
            double TotalEfectivoVenta = mov.GetImporteEfectivosVenta(FechaDesde, FechaHasta, true);
            double TotalTarjetaVenta = mov.GetImporteTarjetaVenta(FechaDesde, FechaHasta, true);
            double totalventa = TotalEfectivoVenta + TotalTarjetaVenta;
            double Gastos = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 1);
            double GastosPersonales = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 2);
            double PagosProv = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 3);
            double Retiros = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 4);
            double TotalCaja = 0;

            TotalCaja = TotalEfectivo + TotalEfectivoVenta - Gastos + TotalApertura - GastosPersonales - PagosProv - Retiros;
            txtTotalCaja.Text = TotalCaja.ToString();
            Formato(txtTotalCaja);
        }

        private void Formato(TextBox t)
        {
            if (t.Text != "")
            {
                t.Text = fun.FormatoEnteroMiles(t.Text);
            }
        }
    }
}
