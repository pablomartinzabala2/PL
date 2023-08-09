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
using System.Net.Mail;
namespace PELUQUERIA
{
    public partial class FrmCorte : Form
    {
        cTabla tabla;
        DataTable tbTrabajos;
        DataTable tVenta;
        public FrmCorte()
        {
            InitializeComponent();
        } 

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            //nombre de los camposa buscar, se llaman igual que en la base de datos
            Principal.OpcionesdeBusqueda = "Apellido;Nombre";
            //nombre de la tabla, 
            Principal.TablaPrincipal = "Cliente";

            Principal.OpcionesColumnasGrilla = "CodCliente; Nombre;Apellido;Telefono";
            Principal.ColumnasVisibles = "0;1;1;1";
            Principal.ColumnasAncho = "100;194;194;192";
            FrmBuscadorGenerico form = new FrmBuscadorGenerico();
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.ShowDialog();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            cCliente cli = new cCliente();
            if (Principal.CodigoPrincipalAbm != null)
            {
                Int32 CodCliente = Convert.ToInt32(Principal.CodigoPrincipalAbm);
                DataTable trdo = cli.GetClientexCodigo(CodCliente);
                if (trdo.Rows.Count > 0)
                {
                    txtCodCliente.Text = trdo.Rows[0]["CodCliente"].ToString();
                    txtNroDocumento.Text = trdo.Rows[0]["NroDocumento"].ToString();
                    txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                    txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                    txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                    txtEmail.Text = trdo.Rows[0]["Email"].ToString();
                    if (trdo.Rows[0]["Mes"].ToString() != "")
                    {
                        cmbMes.SelectedValue = trdo.Rows[0]["Mes"].ToString();
                    }

                    if (trdo.Rows[0]["Dia"].ToString() != "")
                    {
                        cmbDia.SelectedValue = trdo.Rows[0]["Dia"].ToString();
                    }
                }
            }
            else
            {
                txtCodCliente.Text = "";
                txtNroDocumento.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtTelefono.Text = "";
                txtEmail.Text = "";
                if (cmbMes.Items.Count >0) 
                    cmbMes.SelectedIndex = 0;
                if (cmbDia.Items.Count >0)
                    cmbDia.SelectedIndex = 0;
            }
        }

        private void FrmCorte_Load(object sender, EventArgs e)
        {
            CargarTrabajos();
            CargarTarjeta();
            CargarCombo();
            tabla = new cTabla();
            string ColTareas = "CodTrabajo;Nombre;CodPeluquero;Peluquero;Subtotal";
            tbTrabajos = tabla.CrearTabla(ColTareas);
            CargarPeluquero();
            txtFechaAltaOrden.Text = DateTime.Now.ToShortDateString();
            cFunciones fun = new cFunciones();
            fun.LlenarCombo(cmb_CodTipoDoc, "TipoDocumento", "Nombre", "CodTipoDoc");
            //fun.LlenarCombo(cmbProducto, "Producto", "Nombre", "CodProducto");
            CargarProductosVigentes();
            if (cmb_CodTipoDoc.Items.Count > 1)
            {
                cmb_CodTipoDoc.SelectedIndex = 1;
                cmb_CodTipoDoc.Enabled = false; ;
            }
            if (Principal.EsAdministrador == false)
                button1.Enabled = false;
           
            string Col = "CodProducto;Nombre;Cantidad;Precio;Subtotal";
            tVenta = fun.CrearTabla(Col);
            if (Principal.CodigoCorte !=null)
            if (Principal.CodigoCorte != "0")
            {
                BuscarCorte(Convert.ToInt32(Principal.CodigoCorte));
                txtCodCorte.Text = Principal.CodigoCorte.ToString();
                btnBuscar.Visible = false;
            }
        }

        private void CargarProductosVigentes()
        { 
            cProducto producto = new cProducto ();
            Clases.cFunciones fun = new Clases.cFunciones();
            DataTable trdo = producto.GetProductosActivos();
            fun.LlenarComboDatatable(cmbProducto,trdo , "Nombre", "CodProducto");
        }

        private void CargarTrabajos()
        {
            cTrabajo trabajo = new cTrabajo();
            DataTable trdo = trabajo.GetTrabajosActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable  (CmbTrabajo,trdo , "Nombre", "CodTrabajo");
        }

        private void CargarCombo()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            string Col = "Dia;Nombre";
            DataTable trdo = fun.CrearTabla(Col);
            string Val = "1;1";

            for (int i = 1; i < 32; i++)
            {
                Val = i.ToString() + ";" + i.ToString();
                fun.AgregarFilas(trdo, Val);
            }
            fun.LlenarComboDatatable(cmbDia , trdo, "Dia", "Nombre");

            Col = "Mes;Nombre";
            DataTable tmes = fun.CrearTabla(Col);
            Val = "1;Enero";
            fun.AgregarFilas(tmes, Val);
            Val = "2;Febrero";
            fun.AgregarFilas(tmes, Val);
            Val = "3;Marzo";
            fun.AgregarFilas(tmes, Val);
            Val = "4;Abril";
            fun.AgregarFilas(tmes, Val);
            Val = "5;Mayo";
            fun.AgregarFilas(tmes, Val);
            Val = "6;Junio";
            fun.AgregarFilas(tmes, Val);
            Val = "7;Julio";
            fun.AgregarFilas(tmes, Val);
            Val = "8;Agosto";
            fun.AgregarFilas(tmes, Val);
            Val = "9;Septiembre";
            fun.AgregarFilas(tmes, Val);
            Val = "10;Octubre";
            fun.AgregarFilas(tmes, Val);
            Val = "11;Noviembre";
            fun.AgregarFilas(tmes, Val);
            Val = "12;Diciembre";
            fun.AgregarFilas(tmes, Val);
            fun.LlenarComboDatatable(cmbMes , tmes, "Nombre", "Mes");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void CargarPeluquero()
        {
            cPeluquero pel = new cPeluquero();
            DataTable trdo = pel.GetPeluquerosActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable (cmbPeluquero, trdo, "Apellido", "CodPeluquero");
        }

        private bool Validar()
        {
            Boolean ValidaCliente = true;
            if (chkSinCliente.Checked == true)
                ValidaCliente = false;
            if (ValidaCliente == true)
            if (txtNombre.Text == "")
            {
                Mensaje("Debe ingresar el nombre del cliente para continuar");
                return false;
            }
            cFunciones fun = new cFunciones();
            if (ValidaCliente ==true)
            if (txtApellido.Text  == "")
            {
                Mensaje("Debe ingresar el apellido del cliente para continuar");
                return false;
            }
            if (ValidaCliente == true )
            if (txtEmail.Text != "")
            {
                if (fun.validarEmail(txtEmail.Text) == false)
                {
                    Mensaje("El email ingresado es incorrecto");
                    return false;
                }
            }

            if (Grilla.Rows.Count < 1)
            {
                Mensaje("Debe ingresar al menos un trabajo para continuar");
                return false;
            }

            if (txtTotal.Text == "")
            {
                Mensaje("Debe ingresar el total para para continuar");
                return false;
            }
            
            if (fun.ValidarFecha(txtFechaAltaOrden.Text) == false)
            {
                Mensaje("La fecha ingresada es incorrecta");
                return false;
            }

            return true;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (Validar() == false)
            {
                return;
            }

            if (ValidarCobro() == false)
                return;

            Boolean ValidaCLiente = true;
            if (chkSinCliente.Checked == true)
                ValidaCLiente = false;
            Int32 CodigoClienteNulo = 0;
            if (ValidaCLiente == false)
            {
                cCliente clienteNulo = new cCliente();
                CodigoClienteNulo = clienteNulo.GetCodClienteNulo();
            }
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {
                if (txtCodCorte.Text != "")
                { 
                    Int32 CodCorteDel = Convert.ToInt32 (txtCodCorte.Text);
                    cCorte CorteDel = new cCorte ();
                    cMovimiento mov = new cMovimiento();
                    CorteDel.BorrarCorte(con ,Transaccion ,CodCorteDel);
                    CorteDel.BorrarDetalleCorte (con ,Transaccion ,CodCorteDel);
                    mov.BorrarMovimientoxCodCorte(con, Transaccion, CodCorteDel);
                }

                if (txtCodVenta.Text != "")
                {
                    cVenta venta = new cVenta();
                    venta.BorrarVenta(con, Transaccion, Convert.ToInt32(txtCodVenta.Text));
                }
                
                Int32 CodCliente = 0;
                string Nombre = txtNombre.Text;
                string Apellido = txtApellido.Text;
                string Email = txtEmail.Text;
                string Telefono = txtTelefono.Text;
                DateTime fecha = Convert.ToDateTime(txtFechaAltaOrden.Text);
                Int32? CodPeluquero = null;
                double Total = 0;
                string Detalle = txtDetalle.Text;
                int? Dia = null;
                int? Mes = null;
                int? CodTipoDoc= null ;
                string NroDocumento = txtNroDocumento.Text;
                if (cmbDia.SelectedIndex > 0)
                    Dia = Convert.ToInt32(cmbDia.SelectedValue);
                if (cmbMes.SelectedIndex > 0)
                    Mes = Convert.ToInt32(cmbMes.SelectedValue);
                if (cmb_CodTipoDoc.SelectedIndex >0)
                    CodTipoDoc  = Convert.ToInt16 (cmb_CodTipoDoc.SelectedValue);
                cCliente cli = new cCliente();
                
                if (ValidaCLiente == true)
                {
                    if (txtCodCliente.Text == "")
                    {
                        CodCliente = cli.InsertarClienteTran(con, Transaccion, Apellido, Nombre, Email, Telefono, Dia, Mes, CodTipoDoc, NroDocumento);
                    }
                    else
                    {
                        CodCliente = Convert.ToInt32(txtCodCliente.Text);
                        cli.ModificarClienteTran(con, Transaccion, CodCliente, Apellido, Nombre, Email, Telefono, Dia, Mes, CodTipoDoc, NroDocumento);
                    }
                }
                else
                {
                    CodCliente = CodigoClienteNulo;
                }
                
                if (txtTotal.Text != "")
                    Total = Convert.ToDouble(txtTotal.Text);
                if (cmbPeluquero.SelectedIndex > 0)
                    CodPeluquero = Convert.ToInt32(cmbPeluquero.SelectedValue);
                cCorte corte = new cCorte();
                Int32 CodCorte = corte.InsertarCorteTransaccion(con, Transaccion, fecha, CodCliente, Detalle, Total);
                for (int i = 0; i < tbTrabajos.Rows.Count; i++)
                {
                    Int32 CodTrabajoRealizado = Convert.ToInt32(tbTrabajos.Rows[i]["CodTrabajo"].ToString());
                    Int32 CodPelu = Convert.ToInt32(tbTrabajos.Rows[i]["CodPeluquero"].ToString());
                    double Subtotal = Convert.ToDouble(tbTrabajos.Rows[i]["Subtotal"].ToString());
                    corte.InsertarDetalleCorte(con, Transaccion, CodCorte, CodTrabajoRealizado, Subtotal,CodPelu);
                }
                GrabarMovimiento(con ,Transaccion ,CodCorte);
                Mensaje("Datos grabados correctamente");
                Transaccion.Commit();
                con.Close();
                Limpiar();
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                con.Close();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
        }

        private void Limpiar()
        {
            tVenta.Clear();
            chkSinCliente.Checked = false;
            GrillaVentas = null;
            txtCodCliente.Text = "";
            txtCodCorte.Text = "";
            txtCodVenta.Text = "";
            txtCodCliente.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtNroDocumento.Text = "";
            txtEmail.Text = "";
            cmbDia.SelectedIndex = 0;
            cmbMes.SelectedIndex = 0;
            tbTrabajos.Rows.Clear();
            txtTotal.Text = "";
            txtTotalVentas.Text = "";
            txtTelefono.Text = "";
            txtDetalle.Text = "";
            if (cmbPeluquero.Items.Count > 0)
                cmbPeluquero.SelectedIndex = 0;
            if (cmbTarjeta.Items.Count > 0)
                cmbTarjeta.SelectedIndex = 0;
            txtEfectivo.Text = "";
            txtTotalTarjeta.Text = "";
            txtTotalEfectivoVenta.Text = "";
            txtTotalTarjetaVentas.Text = "";
            if (CmbTarjetaVentas.Items.Count >0) 
                CmbTarjetaVentas.SelectedIndex = 0;
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BuscarCorte(Int32 CodCorte)
        {
            cFunciones fun = new cFunciones ();
            cCorte objCorte = new cCorte();
            DataTable tcorte = objCorte.GetCortexCodigo(CodCorte);
           

            if (tcorte.Rows[0]["Total"].ToString() != "")
            {
                txtTotal.Text = tcorte.Rows[0]["Total"].ToString();
                txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
                txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
               // btnGrabar.Visible = false;
              //  btnCancelar.Visible = false;
                DateTime fecha = Convert.ToDateTime (tcorte.Rows[0]["Fecha"].ToString());
                txtFechaAltaOrden.Text = fecha.ToShortDateString();

            }

            DataTable tDetalle = objCorte.BuscarDetalleCorte(CodCorte);
            tDetalle = fun.TablaaMiles(tDetalle, "Subtotal");
            Grilla.DataSource = tDetalle;

            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 205;
            Grilla.Columns[2].Visible = false;
            Grilla.Columns[3].Width = 200;
            Grilla.Columns[1].HeaderText = "Trabajo realizado";
            Grilla.Columns[3].HeaderText = "Trabajador";
            Grilla.Columns[4].HeaderText = "Subtotal";
           // cFunciones fun = new cFunciones();
          //  double Total = fun.TotalizarColumna(tbTrabajos, "Subtotal");
         //   txtTotal.Text = Total.ToString();

            cMovimiento mov = new cMovimiento();
            DataTable tmov = mov.GetMovimientoxCodCorte(CodCorte);
            if (tmov.Rows.Count > 0)
            {
                txtEfectivo.Text = tmov.Rows[0]["ImporteEfectivo"].ToString();
                txtTotalTarjeta.Text = tmov.Rows[0]["ImporteTarjeta"].ToString();
                if (tmov.Rows[0]["CodTarjeta"].ToString() != "")
                {
                    cmbTarjeta.SelectedValue = tmov.Rows[0]["CodTarjeta"].ToString();
                }
            }
            
            Formato(txtEfectivo);
            Formato(txtTotalTarjeta);
            Int32 CodCli =Convert.ToInt32 ( tcorte.Rows[0]["CodCliente"].ToString()); 
            cCliente cli = new cCliente();
            if (CodCli >0)
            {

                DataTable trdo = cli.GetClientexCodigo(CodCli);
                if (trdo.Rows.Count > 0)
                {
                    txtNroDocumento.Text  = trdo.Rows[0]["NroDocumento"].ToString();
                    txtCodCliente.Text = trdo.Rows[0]["CodCliente"].ToString();
                    txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                    txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                    txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                    txtEmail.Text = trdo.Rows[0]["Email"].ToString();
                    if (trdo.Rows[0]["Mes"].ToString() != "")
                    {
                        cmbMes.SelectedValue = trdo.Rows[0]["Mes"].ToString();
                    }
                    if (txtNombre.Text == "" && txtApellido.Text == "")
                        chkSinCliente.Checked = true;
                    else
                        chkSinCliente.Checked = false; 


                    if (trdo.Rows[0]["Dia"].ToString() != "")
                    {
                        cmbDia.SelectedValue = trdo.Rows[0]["Dia"].ToString();
                    }
                }
            }
            cVenta venta = new cVenta ();
            DataTable tRdoventa = venta.GetVentasxCodCorte(CodCorte);
            if (tRdoventa.Rows.Count > 0)
                if (tRdoventa.Rows[0]["CodVenta"].ToString() != "")
                {
                    Int32 CodVenta = Convert.ToInt32(tRdoventa.Rows[0]["CodVenta"].ToString());
                    txtCodVenta.Text = CodVenta.ToString();
                    BuscarVenta(CodVenta);
                }
        }

        private void Formato(TextBox t)
        {
            cFunciones fun = new cFunciones();
            if (t.Text != "")
            {
                t.Text = fun.SepararDecimales(t.Text);
                t.Text = fun.FormatoEnteroMiles(t.Text);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new cFunciones();
            fun.SoloNumerosEnteros(e);
        }

  
        private void txtNroDocumento_TextChanged(object sender, EventArgs e)
        {
            int b = 0;
            string NroDoc = txtNroDocumento.Text;
            if (NroDoc.Length < 3)
                return;
            cCliente cli = new cCliente();
            DataTable trdo = cli.GetClientesxNroDocumento(NroDoc);
            if (trdo.Rows.Count >0)
                if (trdo.Rows[0]["CodCliente"].ToString() != "")
                {
                    Int32 CodCliente = Convert.ToInt32(trdo.Rows[0]["CodCliente"].ToString());
                    GetClientexCodigo(CodCliente);
                    b = 1;
                }
            if (b == 0)
            {
                txtCodCliente.Text = "";
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtEmail.Text = "";
                cmbDia.SelectedIndex = 0;
                cmbMes.SelectedIndex = 0;
            }
        }

        private void GetClientexCodigo(Int32 CodCliente)
        {
            cCliente cli = new cCliente();
            DataTable trdo = cli.GetClientexCodigo(CodCliente);
            if (trdo.Rows.Count > 0)
            {
                txtCodCliente.Text = trdo.Rows[0]["CodCliente"].ToString();
                txtNroDocumento.Text = trdo.Rows[0]["NroDocumento"].ToString();
                txtNombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txtApellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txtTelefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txtEmail.Text = trdo.Rows[0]["Email"].ToString();
                if (trdo.Rows[0]["Mes"].ToString() != "")
                {
                    cmbMes.SelectedValue = trdo.Rows[0]["Mes"].ToString();
                }

                if (trdo.Rows[0]["Dia"].ToString() != "")
                {
                    cmbDia.SelectedValue = trdo.Rows[0]["Dia"].ToString();
                }
            }
        }

        private void txtApellido_Leave(object sender, EventArgs e)
        {
            Int32 CodCliente =0;
            if (txtNombre.Text != "" && txtApellido.Text != "")
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                cCliente cli = new cCliente();
                DataTable trdo = cli.GetClientexNomApe(nombre, apellido);
                if (trdo.Rows.Count > 0)
                    if (trdo.Rows[0]["Nombre"].ToString() != "")
                    {
                        CodCliente = Convert.ToInt32 (trdo.Rows[0]["CodCliente"].ToString());
                        string nomApe = nombre + " " + apellido;
                        string msj = "Ya existe un cliente con ese nombre " + nomApe;
                        msj = msj + ", desea cargar los datos";
                        var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                        {
                            txtCodCliente.Text = "";
                            txtTelefono.Text = "";
                            txtNroDocumento.Text = "";
                            cmbDia.SelectedIndex = 0;
                            cmbMes.SelectedIndex = 0;
                            txtEmail.Text = "";
                        }
                        else
                        {
                            GetClientexCodigo(CodCliente);
                        }
                    }

                
            }
        }

        private void GrabarMovimiento(SqlConnection con, SqlTransaction Transaccion,Int32 CodCorte)
        {
            double ImporteEfectivo = 0;
            double ImporteTarjeta = 0;
            Int32? CodTarjeta = null;
            string Descripcion ="TRABAJO REALIZADO A " + txtNombre.Text + " " + txtApellido.Text ;

            if (txtEfectivo.Text != "")
            {
                ImporteEfectivo = Convert.ToDouble(txtEfectivo.Text);
            }

            if (txtTotalTarjeta.Text != "")
            {
                ImporteTarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
            }
            DateTime Fecha = Convert.ToDateTime(txtFechaAltaOrden.Text);
            double Total = ImporteTarjeta + ImporteEfectivo ;
            if (cmbTarjeta.SelectedIndex > 0)
                CodTarjeta = Convert.ToInt32(cmbTarjeta.SelectedValue);
            cMovimiento mov = new cMovimiento();
            mov.InsertarMovimientoTransaccion(con ,Transaccion ,Descripcion,Fecha ,ImporteEfectivo ,ImporteTarjeta ,CodTarjeta ,CodCorte ,null ,0,null ,Total,0,null);
            if (txtTotalVentas.Text !="")
                if (txtTotalVentas.Text != "0")
                {
                    GrabarVentaProducto(con, Transaccion, CodCorte);
                }
        }

        private void CargarTarjeta()
        {
            cTarjeta tarjeta = new cTarjeta();
            DataTable trdo = tarjeta.GetTarjetasActivos();
            cFunciones fun = new cFunciones();
            fun.LlenarComboDatatable (cmbTarjeta, trdo , "Nombre", "CodTarjeta");
            fun.LlenarComboDatatable(CmbTarjetaVentas, trdo, "Nombre", "CodTarjeta");
        }

         private Boolean ValidarCobro()
        {
            double Total = 0;
            double Efectivo = 0;
            double Tarjeta = 0;
            double TotalVenta = 0;
            if (txtTotal.Text != "")
            {
                Total = Convert.ToDouble(txtTotal.Text);
            }

            if (txtEfectivo.Text != "")
            {
                Efectivo  = Convert.ToDouble(txtEfectivo.Text);
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
                Mensaje("No coinciden los montos de pago de producción");
                return false;
            }

            double EfectivoVentas = 0;
            double TarjetaVentas = 0;
            double TotalVentas = 0;
            double SubtotalVentas = 0;
            if (txtTotalVentas.Text != "")
                TotalVentas = Convert.ToDouble(txtTotalVentas.Text);
            if (txtTotalEfectivoVenta.Text != "")
                EfectivoVentas = Convert.ToDouble(txtTotalEfectivoVenta.Text);
            if (txtTotalTarjetaVentas.Text != "")
                TarjetaVentas = Convert.ToDouble(txtTotalTarjetaVentas.Text);
            SubtotalVentas = EfectivoVentas + TarjetaVentas;
            if (TotalVentas != SubtotalVentas)
            {
                Mensaje("No coinciden los montos de pago de venta de productos");
                return false;
            }
            
             if (txtTotalTarjetaVentas.Text !="")
                 if (txtTotalTarjetaVentas.Text != "0")
                 {
                     if (CmbTarjetaVentas.SelectedIndex < 1)
                     {
                         Mensaje("Debe seleccionar una tarjeta para la venta de producto");
                         return false;
                     }
                 }

            return true;
        }

         private void btnAgregar_Click_1(object sender, EventArgs e)
         {
             if (CmbTrabajo.SelectedIndex < 1)
             {
                 Mensaje("Debe seleccionar un trabajo");
                 return;
             }

             if (cmbPeluquero.SelectedIndex < 1)
             {
                 Mensaje("Debe seleccionar un trabajador");
                 return;
             }

             if (txtPrecio.Text == "")
             {
                 Mensaje("Debe ingresar un precio");
                 return;
             }

             string Cod = CmbTrabajo.SelectedValue.ToString();
             string Precio = txtPrecio.Text;
             if (tabla.Buscar(tbTrabajos, "CodTrabajo", Cod) == true)
             {
                 Mensaje("Ya se ingreso la tarea");
                 return;
             }
             string Nom = CmbTrabajo.Text;
             string CodPel = cmbPeluquero.SelectedValue.ToString();
             string Peluquero = cmbPeluquero.Text;
             string Valores = Cod + ";" + Nom + ";" + CodPel + ";" + Peluquero + ";" + Precio;
             tbTrabajos = tabla.AgregarFilas(tbTrabajos, Valores);
             Grilla.DataSource = tbTrabajos;
             Grilla.Columns[0].Visible = false;
             Grilla.Columns[1].Width = 205;
             Grilla.Columns[2].Visible = false;
             Grilla.Columns[3].Width = 200;
             Grilla.Columns[1].HeaderText = "Trabajo realizado";
             Grilla.Columns[3].HeaderText = "Trabajador";
             Grilla.Columns[4].HeaderText = "Subtotal";
             cFunciones fun = new cFunciones();
             double Total = fun.TotalizarColumna(tbTrabajos, "Subtotal");
             txtTotal.Text = Total.ToString();
             txtPrecio.Text = "";
             double Eft = 0;
             double Tarjeta = 0;
             if (txtEfectivo.Text != "")
                 Eft = Convert.ToDouble(txtEfectivo.Text);
             if (txtTotalTarjeta.Text != "")
                 Tarjeta = Convert.ToDouble(txtTotalTarjeta.Text);
             if (chktarjeta.Checked == true)
                 Tarjeta = Tarjeta + Convert.ToDouble(Precio);
             else
                 Eft = Eft + Convert.ToDouble(Precio);
             txtEfectivo.Text = Eft.ToString();
             txtTotalTarjeta.Text = Tarjeta.ToString();
             chktarjeta.Checked = false; 
         }

         private void btnQuitar_Click(object sender, EventArgs e)
         {
             if (Grilla.CurrentRow == null)
             {
                 Mensaje("Debe seleccionar un registro para continuar");
                 return;
             }
             double Descontar = Convert.ToDouble(Grilla.CurrentRow.Cells[4].Value.ToString());
             string CodTrabajo = Grilla.CurrentRow.Cells[0].Value.ToString();
             tabla.EliminarFila(tbTrabajos, "CodTrabajo", CodTrabajo);
             Grilla.DataSource = tbTrabajos;
             cFunciones fun = new cFunciones();
             double Total = fun.TotalizarColumna(tbTrabajos, "Subtotal");
             txtTotal.Text = Total.ToString();
             
             if (chktarjeta.Checked == true)
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
             chktarjeta.Checked = false;
         }

         private void btnAgregarVenta_Click(object sender, EventArgs e)
         {
             cFunciones fun = new cFunciones();
             if (cmbProducto.SelectedIndex < 1)
             {
                 Mensaje("Debe seleccionar un producto para continuar");
                 return;
             }

             if (txtCantidad.Text == "")
             {
                 Mensaje("Debe ingresar una cantidad para continuar");
                 return;
             }

             if (txtPrecioVenta.Text == "")
             {
                 Mensaje("Debe ingresar un precio para continuar");
                 return;
             }
              
             //string Col = "CodProducto;Nombre;Cantidad;Precio;Subtotal";
             string val = "";
             string CodProducto = cmbProducto.SelectedValue.ToString();
             if (tabla.Buscar(tVenta, "CodProducto", CodProducto) == true)
             {
                 Mensaje("Ya se ha ingresado el producto");
                 return;
             }  
             string Nombre = cmbProducto.Text;
             double Precio = Convert.ToDouble(txtPrecioVenta.Text);
             Int32 Cantidad = Convert.ToInt32(txtCantidad.Text);
             double Subtotal = Precio * Cantidad;
             val = CodProducto + ";" + Nombre + ";" + Cantidad.ToString();
             val = val + ";" + Precio.ToString() + ";" + Subtotal.ToString();
             tVenta = fun.AgregarFilas(tVenta, val);
             GrillaVentas.DataSource = tVenta;
             double Total = fun.TotalizarColumna(tVenta, "Subtotal");
             txtTotalVentas.Text = Total.ToString();
             txtTotalVentas.Text = fun.FormatoEnteroMiles(txtTotalVentas.Text);
             GrillaVentas.Columns[0].Visible = false;
             GrillaVentas.Columns[1].Width = 210;
              
             double Eft = 0;
             double Tarjeta = 0;
             if (txtTotalEfectivoVenta.Text != "")
                 Eft = Convert.ToDouble(txtTotalEfectivoVenta.Text);
             if (txtTotalTarjetaVentas.Text != "")
                 Tarjeta = Convert.ToDouble(txtTotalTarjetaVentas.Text);
             if (chkTarjetaVentas.Checked == true)
                 Tarjeta = Tarjeta + Convert.ToDouble(Subtotal);
             else
                 Eft = Eft + Convert.ToDouble(Subtotal);
             txtTotalEfectivoVenta.Text = Eft.ToString();
             txtTotalTarjetaVentas.Text = Tarjeta.ToString();
             txtCantidad.Text = "";
             txtPrecioVenta.Text = "";
             chkTarjetaVentas.Checked = false; 
         }

         private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
         {
             cFunciones fun = new cFunciones();
             fun.SoloEnteroConPunto(txtPrecioVenta, e);
         }

         private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
         {  
             cFunciones fun = new cFunciones();
             fun.SoloEnteroConPunto(txtCantidad, e);
         }

         private void btnQuitarVenta_Click(object sender, EventArgs e)
         {
             if (GrillaVentas.CurrentRow == null)
             {
                 Mensaje("Debe seleccionar un registro para continuar");
                 return;
             }
             double Descontar = Convert.ToDouble(GrillaVentas.CurrentRow.Cells[4].Value.ToString());
             cFunciones fun = new cFunciones();
             string CodProducto = GrillaVentas.CurrentRow.Cells[0].Value.ToString();
             tVenta = tabla.EliminarFila(tVenta, "CodProducto", CodProducto);
             GrillaVentas.DataSource = tVenta;
             double Total = fun.TotalizarColumna(tVenta, "Subtotal");

             txtTotalVentas.Text = Total.ToString();
             txtTotalVentas.Text = fun.FormatoEnteroMiles(txtTotalVentas.Text);

             if (chkTarjetaVentas.Checked == true)
             {  
                 double TotalTarjeta = 0;
                 if (txtTotalTarjetaVentas.Text != "")
                 {
                     TotalTarjeta = Convert.ToDouble(txtTotalTarjetaVentas.Text);
                     TotalTarjeta = TotalTarjeta - Descontar;
                     txtTotalTarjetaVentas.Text = TotalTarjeta.ToString();
                 }

             }
             else
             {  
                 double TotalEfectivo = 0;
                 if (txtTotalEfectivoVenta.Text != "")
                 {
                     TotalEfectivo = Convert.ToDouble(txtTotalEfectivoVenta.Text);
                     TotalEfectivo = TotalEfectivo - Descontar;
                     txtTotalEfectivoVenta.Text = TotalEfectivo.ToString();
                 }
             }
             chkTarjetaVentas.Checked = false;
         }

         private void GrabarVentaProducto(SqlConnection con, SqlTransaction Transaccion,Int32 CodCorte)
         {
             DateTime Fecha = Convert.ToDateTime(txtFechaAltaOrden.Text);
             double ImporteEfectivo = 0;
             double ImporteTarjeta = 0;
             Int32? CodTarjeta = null;
             if (txtTotalEfectivoVenta.Text != "")
                 ImporteEfectivo = Convert.ToDouble(txtTotalEfectivoVenta.Text);

             if (txtTotalTarjetaVentas.Text != "")
             {
                 ImporteTarjeta = Convert.ToDouble(txtTotalTarjetaVentas.Text);
                 CodTarjeta = Convert.ToInt32(cmbTarjeta.SelectedValue);
             }
             double Total = ImporteEfectivo + ImporteTarjeta;
             cVenta venta = new cVenta();
             cMovimiento mov = new cMovimiento();
             Int32 CodVenta = 0;
             Int32 CodProducto = 0;
             double Cantidad = 0;
             double Precio = 0;
             double Subtotal = 0;
             CodVenta = venta.InsertarVenta(con, Transaccion, Total, Fecha, ImporteEfectivo, ImporteTarjeta, CodTarjeta, CodCorte);
             for (int i = 0; i < tVenta.Rows.Count; i++)
             {
                 CodProducto = Convert.ToInt32(tVenta.Rows[i]["CodProducto"].ToString());
                 Cantidad = Convert.ToDouble(tVenta.Rows[i]["Cantidad"].ToString());
                 Precio = Convert.ToDouble(tVenta.Rows[i]["Precio"].ToString());
                 Subtotal = Convert.ToDouble(tVenta.Rows[i]["Subtotal"].ToString());
                 venta.InsertarDetalleVenta(con, Transaccion, CodVenta, Cantidad, Precio, CodProducto, Subtotal);
             }
             mov.InsertarMovimientoTransaccion(con, Transaccion, "VENTA DE PRODUCTOS", Fecha, ImporteEfectivo, ImporteTarjeta, CodTarjeta, null, null, 0, null, Total, 0, CodVenta);
          }

         private void BuscarVenta(Int32 CodVenta)
         {
             cFunciones fun = new cFunciones();
             cVenta venta = new cVenta();
             DataTable trdo = venta.GetVentaxCodigo(CodVenta);
             if (trdo.Rows.Count > 0)
             {
                 DateTime Fecha = Convert.ToDateTime(trdo.Rows[0]["Fecha"].ToString());
                 txtTotalVentas.Text = trdo.Rows[0]["Total"].ToString();
                 txtTotalEfectivoVenta.Text = trdo.Rows[0]["ImporteEfectivo"].ToString();
                 txtTotalTarjetaVentas.Text = trdo.Rows[0]["ImporteTarjeta"].ToString();
                 if (trdo.Rows[0]["CodTarjeta"].ToString() != "")
                 {
                     CmbTarjetaVentas.SelectedValue = trdo.Rows[0]["CodTarjeta"].ToString();
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
                 GrillaVentas.DataSource = tVenta;
                 GrillaVentas.Columns[0].Visible = false;
                 GrillaVentas.Columns[1].Width = 210;
                 if (txtTotalEfectivoVenta.Text != "")
                 {
                     txtTotalEfectivoVenta.Text = fun.SepararDecimales(txtTotalEfectivoVenta.Text);
                     txtTotalEfectivoVenta.Text = fun.FormatoEnteroMiles(txtTotalEfectivoVenta.Text);
                 }

                 if (txtTotalTarjetaVentas.Text != "")
                 {
                     txtTotalTarjetaVentas.Text = fun.SepararDecimales(txtTotalTarjetaVentas.Text);
                     txtTotalTarjetaVentas.Text = fun.FormatoEnteroMiles(txtTotalTarjetaVentas.Text);
                 }
                
             }

         }

         private void button1_Click_1(object sender, EventArgs e)
         {
             if (txtCodCliente.Text != "")
             {
                 Principal.CodidoClientePrincipal = txtCodCliente.Text;
                 FrmConsultaTrabajo frm = new FrmConsultaTrabajo();
                 frm.Show();
             }
             else
             {
                 Mensaje ("Debe ingresar un cliente existente");
             }
         }

       
    }
}
