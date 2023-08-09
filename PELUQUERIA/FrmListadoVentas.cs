using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PELUQUERIA.Clases;
using System.Data.SqlClient;
namespace PELUQUERIA
{
    public partial class FrmListadoVentas : Form
    {
        cFunciones fun;
        public FrmListadoVentas()
        {
            InitializeComponent();
        }

        private void FrmListadoVentas_Load(object sender, EventArgs e)
        {
            fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
           // fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
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
            cVenta venta = new cVenta();
            DateTime FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
            DateTime FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            DataTable trdo = venta.GetVentasxFecha(FechaDesde, FechaHasta);
            trdo = fun.TablaaMiles(trdo, "Total");
            trdo = fun.TablaaMiles(trdo, "ImporteEfectivo");
            trdo = fun.TablaaMiles(trdo, "ImporteTarjeta");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[2].HeaderText = "Efectivo";
            Grilla.Columns[3].HeaderText = "Tarjeta";
            Grilla.Columns[4].Width = 110;
            txtTotal.Text = fun.TotalizarColumna(trdo, "Total").ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("seleccione un registro ");
                return;
            }
            Principal.CodigoPrincipalAbm = Grilla.CurrentRow.Cells[0].Value.ToString();
            FrmVentaProducto frm = new FrmVentaProducto();
            frm.ShowDialog();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string msj = "Confirma eliminar la venta ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro para continuar");
                return;
            }
            Int32 CodVenta = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value.ToString());
            cVenta venta = new cVenta();
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {
                venta.BorrarVenta(con ,Transaccion , CodVenta);
                Transaccion.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
            Buscar();
        }
    }
}
