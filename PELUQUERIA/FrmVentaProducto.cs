using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PELUQUERIA.Clases;
namespace PELUQUERIA
{
    public partial class FrmVentaProducto : Form
    {
        cFunciones fun;
        cTabla tabla;
        DataTable tVenta;
        public FrmVentaProducto()
        {
            InitializeComponent();
        }

        private void FrmVentaProducto_Load(object sender, EventArgs e)
        {
            tabla = new cTabla();
            fun = new cFunciones();
            //.LlenarCombo(cmbProducto, "Producto", "Nombre", "CodProducto");
            CargarProductosVigentes();
            string Col = "CodProducto;Nombre;Cantidad;Precio;Subtotal";
            tVenta = fun.CrearTabla(Col);
            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargarTarjeta();
            if (Principal.CodigoPrincipalAbm != null)
            {
                BuscarVenta(Convert.ToInt32 (Principal.CodigoPrincipalAbm)); 
            }
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex <1)
            {
                Mensaje ("Debe seleccionar un producto para continuar");
                return ;
            }

            if (txtCantidad.Text =="")
            {
                Mensaje ("Debe ingresar una cantidad para continuar");
                return ;
            }

            if (txtPrecio.Text =="")
            {
                Mensaje ("Debe ingresar un precio para continuar");
                return ;
            }
            
            //string Col = "CodProducto;Nombre;Cantidad;Precio;Subtotal";
            string val ="";
            string CodProducto = cmbProducto.SelectedValue.ToString ();
            if (tabla.Buscar(tVenta, "CodProducto", CodProducto) == true)
            {
                Mensaje("Ya se ha ingresado el producto");
                return;
            }
            string Nombre = cmbProducto.Text ;
            double  Precio =Convert.ToDouble (txtPrecio.Text) ;
            Int32  Cantidad =Convert.ToInt32 (txtCantidad.Text);
            double Subtotal = Precio * Cantidad ;
            val = CodProducto + ";" + Nombre + ";" + Cantidad.ToString ();
            val = val + ";" + Precio.ToString () + ";" + Subtotal.ToString ();
            tVenta = fun.AgregarFilas (tVenta ,val);
            Grilla.DataSource = tVenta ;
            double Total = fun.TotalizarColumna(tVenta, "Subtotal");
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 210;
            AplicarTotal();
            chkTarjeta.Checked = false;
            txtPrecio.Text = "";
            txtCantidad.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro para continuar");
                return;
            }
            double Descontar = Convert.ToDouble(Grilla.CurrentRow.Cells[4].Value.ToString());
            string CodProducto = Grilla.CurrentRow.Cells[0].Value.ToString();
            tVenta = tabla.EliminarFila(tVenta, "CodProducto", CodProducto);
            Grilla.DataSource = tVenta;
            double Total = fun.TotalizarColumna(tVenta, "Subtotal");
            txtTotal.Text = Total.ToString();
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);

            if (chkTarjeta.Checked == true)
            {
                double TotalTarjeta = 0;
                if (txtTotalTarjeta.Text != "")
                {
                    TotalTarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
                    TotalTarjeta = TotalTarjeta - Descontar;
                    txtTotalTarjeta.Text = TotalTarjeta.ToString();
                }

            }
            else
            {
                double TotalEfectivo = 0;
                if (txtEfectivo.Text != "")
                {
                    TotalEfectivo = Convert.ToDouble(txtEfectivo.Text);
                    TotalEfectivo = TotalEfectivo - Descontar;
                    txtEfectivo.Text = TotalEfectivo.ToString();
                }
            }
            chkTarjeta.Checked = false;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (fun.ValidarFecha(txtFecha.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return;
            }
            if (Grilla.Rows.Count < 1)
            {
                Mensaje("Debe ingresar un producto para continuar");
                return;
            }

            if (ValidarCobro() == false)
            {
                Mensaje("No coinciden los totales");
                return;
            }

            DateTime Fecha = Convert.ToDateTime(txtFecha.Text);
            double ImporteEfectivo = 0;
            double ImporteTarjeta = 0;
            Int32? CodTarjeta = null;
            if (txtEfectivo.Text != "")
                ImporteEfectivo = Convert.ToDouble(txtEfectivo.Text);

            if (txtTotalTarjeta.Text != "")
            {
                
                ImporteTarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
                if (ImporteTarjeta >0)
                CodTarjeta = Convert.ToInt32(cmbTarjeta.SelectedValue); 
            }
                
            double Total = ImporteEfectivo + ImporteTarjeta;
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            cVenta venta = new cVenta();
            cMovimiento mov = new cMovimiento();
            Int32 CodVenta = 0;
            
            try
            {
                CodVenta = venta.InsertarVenta(con, Transaccion, Total, Fecha, ImporteEfectivo, ImporteTarjeta, CodTarjeta, null);
                Int32 CodProducto = 0;
                double Cantidad = 0;
                double Precio = 0;
                double Subtotal = 0;
                for (int i = 0; i < tVenta.Rows.Count; i++)
                {
                    CodProducto = Convert.ToInt32(tVenta.Rows[i]["CodProducto"].ToString());
                    Cantidad = Convert.ToDouble(tVenta.Rows[i]["Cantidad"].ToString());
                    Precio = Convert.ToDouble(tVenta.Rows[i]["Precio"].ToString());
                    Subtotal = Convert.ToDouble(tVenta.Rows[i]["Subtotal"].ToString());
                    venta.InsertarDetalleVenta(con, Transaccion, CodVenta, Cantidad, Precio, CodProducto, Subtotal);
                }
                mov.InsertarMovimientoTransaccion(con, Transaccion, "VENTA DE PRODUCTOS", Fecha, ImporteEfectivo, ImporteTarjeta, CodTarjeta, null, null, 0, null, Total, 0, CodVenta);
                Transaccion.Commit();
                con.Close();
                Mensaje("Datos grabados correctamente");
                Limpiar();

            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
        }

        private void CargarTarjeta()
        {
            cTarjeta tarjeta = new cTarjeta();
            DataTable trdo = tarjeta.GetTarjetasActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable(cmbTarjeta, trdo, "Nombre", "CodTarjeta");
        }

        private void CargarProductosVigentes()
        {
            cProducto producto = new cProducto();
            Clases.cFunciones fun = new Clases.cFunciones();
            DataTable trdo = producto.GetProductosActivos();
            fun.LlenarComboDatatable(cmbProducto, trdo, "Nombre", "CodProducto");
        }

        private Boolean ValidarCobro()
        {
            double Total = 0;
            double Efectivo = 0;
            double Tarjeta = 0;
            if (txtTotal.Text != "")
            {
                Total = Convert.ToDouble(txtTotal.Text);
            }

            if (txtEfectivo.Text != "")
            {
                Efectivo = Convert.ToDouble(txtEfectivo.Text);
            }

            if (txtTotalTarjeta.Text != "")
            {
                Tarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
                if (Tarjeta >0)
                if (cmbTarjeta.SelectedIndex < 1)
                {
                    Mensaje("Debe seleccionar una tarjeta");
                    return false;
                }
            }

            double SubTotal = Efectivo + Tarjeta;
            if (Total != SubTotal)
            {
                Mensaje("No coinciden los montos de pago");
                return false;
            }

            return true;
        }

        private void Limpiar()
        {
            txtTotal.Text = "";
            cmbTarjeta.SelectedIndex = 0;
            tVenta.Clear();
            Grilla.DataSource = null;
            cmbProducto.SelectedIndex = 0;
            txtCantidad.Text = "";
            txtEfectivo.Text = "";
            txtTotalTarjeta.Text = ""; 
        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            fun.SoloEnteroConPunto(sender, e);
        }

        private void txtTotalTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            fun.SoloEnteroConPunto(sender, e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BuscarVenta(Int32 CodVenta)
        {
            cVenta venta = new cVenta();
            DataTable trdo = venta.GetVentaxCodigo(CodVenta);
            if (trdo.Rows.Count > 0)
            {
                DateTime Fecha = Convert.ToDateTime (trdo.Rows[0]["Fecha"].ToString ());
                txtTotal.Text = trdo.Rows[0]["Total"].ToString ();
                txtEfectivo.Text = trdo.Rows[0]["ImporteEfectivo"].ToString ();
                txtTotalTarjeta.Text = trdo.Rows[0]["ImporteTarjeta"].ToString ();
                if (trdo.Rows[0]["CodTarjeta"].ToString () !="")
                {
                    cmbTarjeta.SelectedValue = trdo.Rows[0]["CodTarjeta"].ToString ();
                }
                for (int i = 0; i < trdo.Rows.Count; i++)
                {
                    string val = "";
                    val = trdo.Rows[i]["CodProducto"].ToString();
                    val = val + ";" + trdo.Rows[i]["Nombre"].ToString();
                    val = val + ";" + trdo.Rows[i]["Cantidad"].ToString();
                    val = val + ";" + trdo.Rows[i]["Precio"].ToString();
                    val = val + ";" + trdo.Rows[i]["Subtotal"].ToString();
                    tVenta = fun.AgregarFilas(tVenta, val);
                }
                tVenta = fun.TablaaMiles(tVenta, "Cantidad");
                tVenta = fun.TablaaMiles(tVenta, "Subtotal");
                tVenta = fun.TablaaMiles(tVenta, "Precio");
                Grilla.DataSource = tVenta;
                Grilla.Columns[0].Visible = false;
                Grilla.Columns[1].Width = 210;
                if (txtEfectivo.Text != "")
                {
                    txtEfectivo.Text = fun.SepararDecimales(txtEfectivo.Text);
                    txtEfectivo.Text = fun.FormatoEnteroMiles(txtEfectivo.Text);
                }

                if (txtTotalTarjeta.Text != "")
                {
                    txtTotalTarjeta.Text = fun.SepararDecimales(txtTotalTarjeta.Text);
                    txtTotalTarjeta.Text = fun.FormatoEnteroMiles(txtTotalTarjeta.Text);
                }
                btnGrabar.Visible = false;
                btnCancelar.Visible = false;
            }

        }

        private void AplicarTotal()
        {
            double pre = Convert.ToDouble(txtPrecio.Text);
            double Cantidad = Convert.ToDouble(txtCantidad.Text);
            string Total = (pre * Cantidad).ToString(); 
            double Eft = 0;
            double Tarjeta = 0;
            if (txtEfectivo.Text != "")
                Eft = Convert.ToDouble(txtEfectivo.Text);
            if (txtTotalTarjeta.Text != "")
                Tarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
            if (chkTarjeta.Checked == true)
                Tarjeta = Tarjeta + Convert.ToDouble(Total);
            else
                Eft = Eft + Convert.ToDouble(Total);
            txtEfectivo.Text = Eft.ToString();
            txtTotalTarjeta.Text = Tarjeta.ToString();
        }

        

    }
}
