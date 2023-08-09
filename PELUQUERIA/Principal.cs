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
    public partial class Principal : Form
    {
        //nombre del campo id
        public static string CampoIdSecundario;
        //nombre del campo descripcion
        public static string CampoNombreSecundario;
        //nombre de la tabla donde se realiza el grabado
        public static string NombreTablaSecundario;
        public static string NombreLabelSecundario;
        //valor del id que genera al insertar
        public static string CampoIdSecundarioGenerado;
        public static Int32 CodUsuarioLogueado;
        public static string NombreUsuarioLogueado;
        public static string CodigoPrincipalAbm;
        public static string  CodigoCorte;
        public static string CodigoSenia;
        public static string CodidoClientePrincipal;
        private int childFormNumber = 0;
        public static string OpcionesdeBusqueda;
        public static string TablaPrincipal;
        public static string OpcionesColumnasGrilla;
        public static string ColumnasVisibles;
        public static string ColumnasAncho;
        public static Boolean EsAdministrador;
        public static Boolean VieneDeCaja;
       

        public Principal()
        {
            InitializeComponent();
        }

       
        private void trabajosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmTrabajo childForm = new FrmAbmTrabajo();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de altas, bajas y modificación de trabajos";
            childForm.Show();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            VerificarCumpleanios();
            VerificarUsuario();
            
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FrmAbmCliente childForm = new FrmAbmCliente();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de altas, bajas y modificación de clientes";
            childForm.Show();
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void realizarTareasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cAperturaCaja aper = new cAperturaCaja();
            if (aper.EstaAbierta() == false)
            {
                Mensaje("La caja esta cerrada, debe abrir la caja para continuar");
                return;
            }
            Principal.CodigoCorte = "0";
            FrmCorte childForm = new FrmCorte();
            childForm.MdiParent = this;
            childForm.Text = "Formulario  de Cortes";
            childForm.Show();
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.CodidoClientePrincipal = null;
            FrmConsultaTrabajo childForm = new FrmConsultaTrabajo();
            childForm.MdiParent = this;
            childForm.Text = "Formulario  de trabajos";
            childForm.Show();
        }

        private void trabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmPeluquero childForm = new FrmAbmPeluquero();
            childForm.MdiParent = this;
            childForm.Text = "Formulario  de trabajadores";
            childForm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clases.cAperturaCaja ap = new Clases.cAperturaCaja();
            if (ap.EstaAbierta() == true)
            {
                MessageBox.Show ("Debe cerrar la caja antes de cerrar sesión");
                return;
            }
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmAbmCliente childForm = new FrmAbmCliente();
            childForm.MdiParent = this;
            childForm.Text = "Formulario de altas, bajas y modificación de clientes";
            childForm.Show();
 
        }

        private void VerificarCumpleanios()
        {
            DateTime hoy = DateTime.Now;
            int Dia = hoy.Day;
            int Mes = hoy.Month;
            Clases.cCliente cli = new Clases.cCliente();
            DataTable trdo = cli.GetCumplenios(Dia, Mes);
            if (trdo.Rows.Count > 0)
            {
                helpToolStripButton.Visible = true;
                FrmCumpleanios childForm = new FrmCumpleanios();
                childForm.MdiParent = this;
                childForm.Text = "Listado  de cumpleaños";
                childForm.Show();
            }
            else
            {
                helpToolStripButton.Visible = false;
            }
            
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            FrmCumpleanios childForm = new FrmCumpleanios();
            childForm.MdiParent = this;
            childForm.Text = "Listado  de cumpleaños";
            childForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmResumen childForm = new FrmResumen();
            childForm.MdiParent = this;
            childForm.Text = "Listado resumido por trabajadores";
            childForm.Show();
        }

        private void resumenDeTareasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumenTrabajos childForm = new FrmResumenTrabajos();
            childForm.MdiParent = this;
            childForm.Text = "Listado resumido por tareas";
            childForm.Show();
        }

        private void resumenPorTrabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumen childForm = new FrmResumen();
            childForm.MdiParent = this;
            childForm.Text = "Listado resumido por trabajadores";
            childForm.Show();
        }

        private void BtnCopia_Click(object sender, EventArgs e)
        {
            FrmCopia childForm = new FrmCopia();
            childForm.MdiParent = this;
            childForm.Text = "Generación de copia de seguridad";
            childForm.Show();
        }

        private void tarjetasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmTarjeta childForm = new FrmAbmTarjeta();
            childForm.MdiParent = this;
            
            childForm.Show();
        }

        private void cajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Principal.VieneDeCaja = false;
            FrmListadoCajaDiario frm = new FrmListadoCajaDiario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void aperturaCierreCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmApertura frm = new FrmApertura();
            frm.MdiParent = this;
            frm.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmProducto frm = new FrmAbmProducto();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cAperturaCaja aper = new cAperturaCaja();
            if (aper.EstaAbierta() == false)
            {
                Mensaje("La caja esta cerrada, debe abrir la caja para continuar");
                return;
            }
            Principal.CodigoPrincipalAbm = null;
            FrmVentaProducto frm = new FrmVentaProducto();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Principal.CodigoPrincipalAbm = null;
            FrmListadoVentas frm = new FrmListadoVentas();
            frm.MdiParent = this;
            frm.Show();
        }

        private void retirosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoGastos frm = new FrmListadoGastos();
            frm.Show();
        }

        private void actualizarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmActualizarClave frm = new FrmActualizarClave();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            cAperturaCaja aper = new cAperturaCaja();
            if (aper.EstaAbierta() == false)
            {
                Mensaje("La caja esta cerrada, debe abrir la caja para continuar");
                return;
            }
            Principal.CodigoCorte = "0";
            FrmCorte frm = new FrmCorte();
            frm.Show();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmUsuario usu = new FrmAbmUsuario();
            usu.MdiParent = this;
            usu.Show();
        }

        private void tareasDiariasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoDiario frm = new FrmListadoDiario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbmProveedores frm = new FrmAbmProveedores();
            frm.MdiParent = this;
            frm.Show();
        }

        private void resumenDiarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoResumidoxDia frm = new FrmListadoResumidoxDia();
            frm.Show();
        }

        private void balanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBalance frm = new FrmBalance();
            frm.MdiParent = this;
            frm.Show();
        }

        private void registrarGastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegistrarGasto frm = new FrmRegistrarGasto();
            frm.MdiParent = this;
            frm.Show();
        }

        private void VerificarUsuario()
        {
            Boolean Opcion = true ;
            if (Principal.EsAdministrador == false)
            {
                Opcion = false;
            }
            archivoToolStripMenuItem.Enabled = Opcion;
            listaoToolStripMenuItem.Enabled = Opcion;
            accionesToolStripMenuItem.Enabled = Opcion;
            toolStripButton2.Enabled = Opcion;
            helpToolStripButton.Enabled = Opcion;
            toolStripButton1.Enabled = Opcion;
            BtnCopia.Enabled = Opcion;
        }

        private void tarjetasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmListadoTarjetas frm = new FrmListadoTarjetas();
            frm.MdiParent = this;
            frm.Show();
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        { /*
            Clases.cAperturaCaja ap = new Clases.cAperturaCaja();
            if (ap.EstaAbierta() == true)
            {
                MessageBox.Show("La caja esta abierta, a continuación se va cerrar");
                DateTime Fecha = DateTime.Now;
                
                Int32 CodApertura = 0;
                Clases.cMovimiento mov = new Clases.cMovimiento();

                DataTable trdo = ap.GetUltimaApertura();
                if (trdo.Rows.Count > 0)
                {
                    CodApertura = Convert.ToInt32(trdo.Rows[0]["CodApertura"].ToString());
                    ap.CierreCaja(CodApertura, Fecha);
                    mov.ActualizarProceso();
                }
                
                
            }
            */
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clases.cAperturaCaja ap = new Clases.cAperturaCaja();
            if (ap.EstaAbierta() == true)
            {
                MessageBox.Show("Debe cerrar la caja antes de cerrar sesión");
                e.Cancel = true;
                return;

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FrmApertura frm = new FrmApertura();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
