using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PELUQUERIA.Clases;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace PELUQUERIA
{
    public partial class FrmListadoGastos : Form
    {
        public FrmListadoGastos()
        {
            InitializeComponent();
        }

        private void FrmListadoGastos_Load(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
           // fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            IniciarBusqueda();
            fun.LlenarCombo(CmbProveedores, "Proveedor", "Nombre", "CodProveedor");
        }

        private void IniciarBusqueda()
        {
            cFunciones fun = new cFunciones();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            string Descripcion = txtDescripcion.Text;
            int TipoGastoParticular=0;
            int TipoGastoGeneral=0;
            int TipoGastoRetiro = 0;
            int Proveedor=0;   
            Int32? CodProveedor = null;
            if (ChkGenerales.Checked == true)
                TipoGastoGeneral = 1;
            if (ChkParticulares.Checked == true)
                TipoGastoParticular = 2;
            if (chkRetiros.Checked == true)
                TipoGastoRetiro = 4;
            if (ChkProveedores.Checked == true)
                Proveedor = 3;
            if (CmbProveedores.SelectedIndex >0)
                CodProveedor = Convert.ToInt32 (CmbProveedores.SelectedValue);
            cGasto gasto = new cGasto();
            DataTable trdo = gasto.GetGastos(FechaDesde, FechaHasta, Descripcion, TipoGastoParticular, TipoGastoGeneral, Proveedor, CodProveedor,TipoGastoRetiro);
            trdo = fun.TablaaMiles(trdo, "Total");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[4].Visible = false;
            Grilla.Columns[1].Width = 430;
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[3].Width = 100;
            Grilla.Columns[5].Width = 198;
            txtTotal.Text = fun.TotalizarColumna(trdo, "Total").ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            double TotalProveedor = 0;
            double TotalGeneral = 0;
            double TotalParticulares = 0;
            double TotalRetiros = 0;
            if (TipoGastoGeneral == 0 && TipoGastoParticular == 0 && Proveedor == 0 && TipoGastoRetiro ==0)
            {
                TotalProveedor = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 3, CodProveedor);
                TotalParticulares = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 2, null);
                TotalGeneral = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 1, null);
                TotalRetiros = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 4, null);
            }
            else
            {
                if (TipoGastoGeneral ==1)
                    TotalGeneral = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 1, null);
                if (TipoGastoParticular ==2)
                    TotalParticulares = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 2, null);
               if (Proveedor ==3)
                   TotalProveedor = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 3, CodProveedor);
            if (TipoGastoRetiro ==4)
                TotalRetiros = gasto.GetTotalxCodTipoGasto(FechaDesde, FechaHasta, 4, null);
            }
            txtTotalProveedor.Text = TotalProveedor.ToString();
            txtTotalGastosGenerales.Text = TotalGeneral.ToString();
            textBox1.Text = TotalParticulares.ToString();
            txtTotalRetiros.Text = TotalRetiros.ToString();
            Formato(txtTotalProveedor);
            Formato(txtTotalGastosGenerales);
           Formato(textBox1);
           Formato(txtTotalRetiros);
        }

        private void Formato(TextBox t)
        {
            cFunciones fun = new cFunciones ();
            t.Text = fun.SepararDecimales(t.Text);
            t.Text = fun.FormatoEnteroMiles(t.Text);
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cFunciones fun = new cFunciones();

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
            IniciarBusqueda();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmRegistrarGasto frm = new FrmRegistrarGasto();
            
            frm.FormClosing += new FormClosingEventHandler(form_FormClosing);
            
            frm.ShowDialog();
            //form.FormClosing += new FormClosingEventHandler(form_FormClosing);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro");
                return;
            }
            string msj = "Confirma eliminar el gasto";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            Int32 CodGasto = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());   
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {  
                cMovimiento mov = new cMovimiento();
                cGasto gasto = new cGasto();
                gasto.BorrarGasto(con, Transaccion, CodGasto);
                mov.BorrarMovimientoxCodGasto(con, Transaccion, CodGasto);
                Mensaje("Datos borrados  correctamente");
                Transaccion.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
            IniciarBusqueda();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniciarBusqueda();
        }
    }
}
