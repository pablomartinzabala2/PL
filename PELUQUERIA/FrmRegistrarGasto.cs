using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using PELUQUERIA.Clases;

namespace PELUQUERIA
{
    public partial class FrmRegistrarGasto : Form
    {
        cFunciones fun;
        public FrmRegistrarGasto()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                Mensaje("Ingresar una descripciñon");
                return;
            }

            if (txtFechaAltaOrden.Text =="")
            {
                Mensaje ("Ingresar una fecha");
                return ;
            }

            if (CmbCategoriaGasto.SelectedIndex < 1)
            {
                Mensaje("Debe seleccionar una categoría de gasto");
                return;
            }

            if (fun.ValidarFecha (txtFechaAltaOrden.Text)==false)
            {
                Mensaje ("La fecha ingresada es incorrecta");
                return ;
            }

            if (CmbCategoriaGasto.SelectedValue.ToString() == "3")
            {
                if (CmbProveedor.SelectedIndex < 1)
                {
                    Mensaje("Debe seleccionar un proveedor para continuar");
                    return;
                }
            }

            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {
                cMovimiento mov = new cMovimiento();
                DateTime Fecha = Convert.ToDateTime(txtFechaAltaOrden.Text);
                Int32? CodTipoGasto = null;
                Int32? CodProveedor = null;
                if (CmbCategoriaGasto.SelectedIndex > 0)
                    CodTipoGasto = Convert.ToInt32(CmbCategoriaGasto.SelectedValue);
                if (CodTipoGasto == 3)
                {
                    CodProveedor = Convert.ToInt32(CmbProveedor.SelectedValue);
                }
                string Descripcion = txt_Nombre.Text;
                double Total = Convert.ToDouble(txtImporte.Text);
                cGasto gasto = new cGasto ();
                Int32 CodGasto = gasto.InsertarGastoTransaccion(con, Transaccion, Descripcion, Fecha, Total, CodTipoGasto, CodProveedor);
                mov.InsertarMovimientoTransaccion(con, Transaccion, Descripcion, Fecha, 0, 0, null, null, CodGasto, Total, null, 0, Total, null);
                Mensaje ("Datos grabados correctamente");
                Transaccion.Commit();
                con.Close();
                txt_Nombre.Text = "";
                txtImporte.Text = "";
                CmbProveedor.SelectedIndex = 0;
                if (CmbCategoriaGasto.Items.Count > 0)
                    CmbCategoriaGasto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void FrmRegistrarGasto_Load(object sender, EventArgs e)
        {
            cAperturaCaja aper = new cAperturaCaja();
            if (aper.EstaAbierta() == false)
            {
                Mensaje("La caja esta cerrada, debe abrir la caja para continuar");
                btnGrabar.Enabled = false;
            }
            fun = new cFunciones();
            txtFechaAltaOrden.Text = DateTime.Now.ToShortDateString();
            fun.LlenarCombo(CmbCategoriaGasto, "TipoGasto", "Nombre", "CodTipoGasto");
            fun.LlenarCombo(CmbProveedor, "Proveedor", "Nombre", "CodProveedor");
        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            fun.SoloEnteroConPunto(sender, e);
        }
    }
}
