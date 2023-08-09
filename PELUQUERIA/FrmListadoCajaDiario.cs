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
    public partial class FrmListadoCajaDiario : Form
    {
        cFunciones fun;
        public FrmListadoCajaDiario()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmListadoCajaDiario_Load(object sender, EventArgs e)
        {
            fun = new cFunciones();
            txtFechaDesde.Text = DateTime.Now.ToShortDateString();
            txtFechahasta.Text = DateTime.Now.ToShortDateString();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaDesde.Text);
            Buscar(FechaDesde, FechaHasta);
            btnGrabar.Focus();
            if (Principal.VieneDeCaja == true)
                this.Close();
        }

        private void Buscar(DateTime FechaDesde, DateTime FechaHasta)
        {
            GetUltimaApertura();
            cCajaHistorica cajaHis = new cCajaHistorica();
            cMovimiento mov = new cMovimiento();
            double TotalEfectivo = mov.GetImporteEfectivos(FechaDesde, FechaHasta,true);
            double TotalTarjeta = mov.GetImporteTarjeta(FechaDesde, FechaHasta,true );
            double TotalTrabajos = TotalEfectivo + TotalTarjeta;
            txtEfectivo.Text = TotalEfectivo.ToString();
            txtTotalTarjeta.Text = TotalTarjeta.ToString();
            txtTotalTrabajos.Text = TotalTrabajos.ToString();
           
            
            cAperturaCaja ap = new cAperturaCaja();
            double TotalApertura = ap.GetUltimoImporte(FechaDesde);
            txtAperturaCaja.Text = TotalApertura.ToString();
           
            double TotalEfectivoVenta = mov.GetImporteEfectivosVenta(FechaDesde, FechaHasta,true);
            txtTotalVentaEfectivo.Text = TotalEfectivoVenta.ToString();
            
            double TotalTarjetaVenta = mov.GetImporteTarjetaVenta(FechaDesde, FechaHasta,true );
            txtTotalTarjetaVenta.Text = TotalTarjetaVenta.ToString();
            
            double totalventa = TotalEfectivoVenta + TotalTarjetaVenta;
            txtTotalVenta.Text = totalventa.ToString();
            
            double Gastos = mov.GetImporteGasto(FechaDesde, FechaHasta,true,1);
            double GastosPersonales = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 2);
            double PagosProv = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 3);
            txtGastos.Text = Gastos.ToString ();
            double Retiro = mov.GetImporteGasto(FechaDesde, FechaHasta, true, 4);
            txtRetiros.Text = Retiro.ToString(); 
            txtGastosParticulares.Text = GastosPersonales.ToString();
           
            txtProveedores.Text = PagosProv.ToString();
            
            double TotalCaja = 0;

            TotalCaja = TotalEfectivo + TotalEfectivoVenta - Gastos + TotalApertura - GastosPersonales - PagosProv - Retiro;
            txtTotalCaja.Text = TotalCaja.ToString();
             if (txtCodApertura.Text != "")
            {
                cajaHis.InsertarCajaHistorica(Convert.ToInt32(txtCodApertura.Text),
                    TotalEfectivo, TotalTarjeta, TotalEfectivoVenta, TotalTarjetaVenta,
                    Gastos, GastosPersonales, PagosProv,TotalApertura,Retiro); 
            }
             
            Formato(txtGastos);
            Formato(txtTotalTrabajos);
            Formato(txtTotalTarjeta);
            Formato(txtEfectivo);
            Formato(txtTotalTarjetaVenta);
            Formato(txtTotalVentaEfectivo);
            Formato(txtTotalVenta);
            Formato(txtAperturaCaja);
             Formato(txtGastosParticulares);
            Formato(txtProveedores);
            Formato(txtAperturaCaja);
            Formato(txtRetiros); 
        }

        private void Formato(TextBox t)
        {
            if (t.Text != "")
            {
                t.Text = fun.SepararDecimales(t.Text);
                t.Text = fun.FormatoEnteroMiles(t.Text);
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaDesde.Text);
            Buscar(FechaDesde, FechaHasta);
        }

        private void GetUltimaApertura()
        {
            cAperturaCaja ap = new cAperturaCaja();
            DataTable trdo = ap.GetUltimaApertura();
            if (trdo.Rows.Count > 0)
            {
                if (trdo.Rows[0]["CodApertura"].ToString() != "")
                {
                    txtCodApertura.Text = trdo.Rows[0]["CodApertura"].ToString();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FrmListadoCajaHIstorica form = new FrmListadoCajaHIstorica();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodApertura = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                double MontoInicial = 0;
                double EfectivoPr = 0;
                double EfectivoVentas = 0;
                double GastosParticulares = 0;
                double GastosGenerales = 0;
                double Proveedores = 0;
                double Retiro = 0;
                double Total = 0;
                cCajaHistorica caja = new cCajaHistorica();
                DataTable trdo = caja.GetMontosCajaHistorica(CodApertura);
                if (trdo.Rows.Count > 0)
                {
                    txtAperturaCaja.Text = trdo.Rows[0]["MontoInicial"].ToString();
                    if (trdo.Rows[0]["MontoInicial"].ToString() != "")
                    {
                        MontoInicial = Convert.ToDouble(trdo.Rows[0]["MontoInicial"].ToString());
                    }

                    if (trdo.Rows[0]["EfectivoPr"].ToString() != "")
                    {
                        EfectivoPr = Convert.ToDouble(trdo.Rows[0]["EfectivoPr"].ToString());
                    }

                    if (trdo.Rows[0]["EfectivoVentas"].ToString() != "")
                    {
                        EfectivoVentas = Convert.ToDouble(trdo.Rows[0]["EfectivoVentas"].ToString());
                    }
                     
                    if (trdo.Rows[0]["GastosGenerales"].ToString() != "")
                    {
                        GastosGenerales = Convert.ToDouble(trdo.Rows[0]["GastosGenerales"].ToString());
                    }

                    if (trdo.Rows[0]["GastosParticulares"].ToString() != "")
                    {
                        GastosParticulares = Convert.ToDouble(trdo.Rows[0]["GastosParticulares"].ToString());
                    }
                     
                    if (trdo.Rows[0]["Proveedores"].ToString() != "")
                    {
                        Proveedores = Convert.ToDouble(trdo.Rows[0]["Proveedores"].ToString());
                    }
                    if (trdo.Rows[0]["Retiro"].ToString() != "")
                    {
                        Retiro = Convert.ToDouble(trdo.Rows[0]["Retiro"].ToString());
                    }

                    txtTotalTrabajos.Text = trdo.Rows[0]["MontoInicial"].ToString();
                    txtEfectivo.Text = trdo.Rows[0]["EfectivoPr"].ToString();
                    txtTotalTarjeta.Text = trdo.Rows[0]["TarjetaPr"].ToString();
                    txtTotalTrabajos.Text = trdo.Rows[0]["TotalPr"].ToString();
                    txtRetiros.Text = Retiro.ToString();
                    txtTotalVenta.Text = trdo.Rows[0]["TotalVentas"].ToString();
                    txtTotalVentaEfectivo.Text = trdo.Rows[0]["EfectivoVentas"].ToString();
                    txtTotalTarjetaVenta.Text = trdo.Rows[0]["TarjetaVentas"].ToString();
                    txtGastos.Text = trdo.Rows[0]["GastosGenerales"].ToString();
                    txtGastosParticulares.Text = trdo.Rows[0]["GastosParticulares"].ToString();
                    txtProveedores.Text = trdo.Rows[0]["Proveedores"].ToString();
                    Total = EfectivoPr + EfectivoVentas + MontoInicial;
                    Total = Total - GastosGenerales - GastosParticulares - Proveedores - Retiro;
                    txtTotalCaja.Text = Total.ToString();
                    Formato(txtTotalTrabajos);
                    Formato(txtRetiros);
                    Formato(txtEfectivo);
                    Formato(txtTotalTarjeta);
                    Formato(txtTotalTarjetaVenta);
                    Formato(txtGastos);
                    Formato(txtGastosParticulares);
                    Formato(txtProveedores);
                    Formato(txtTotalCaja);       
                    Formato(txtTotalVentaEfectivo);
                    Formato(txtTotalVenta);
                    Formato(txtAperturaCaja);
                }

            }
        }

    }
}
