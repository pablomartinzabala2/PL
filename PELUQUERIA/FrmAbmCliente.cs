using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PELUQUERIA.Clases;
using System.Windows.Forms;

namespace PELUQUERIA
{
    public partial class FrmAbmCliente : Form
    {
        public FrmAbmCliente()
        {
            InitializeComponent();
        }

        private void FrmAbmCliente_Load(object sender, EventArgs e)
        {
            Botonera(1);
            Grupo.Enabled = false;
            CargarCombo();
        }

        private void Botonera(int Jugada)
        {
            switch (Jugada)
            {
                //estado inicial
                case 1:
                    btnNuevo.Enabled = true;
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnAceptar.Enabled = false;
                    btnCancelar.Enabled = false;

                    break;
                case 2:
                    btnNuevo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnEliminar.Enabled = true;
                    btnAceptar.Enabled = true;
                    btnCancelar.Enabled = true;

                    break;
                case 3:
                    //viene del buscador
                    btnNuevo.Enabled = true;
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnAceptar.Enabled = false;
                    btnCancelar.Enabled = false;


                    break;
            }


        }

        private void CargarCombo()
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            string Col = "Dia;Nombre";
            DataTable trdo = fun.CrearTabla(Col);
            string Val ="1;1";
            
            for (int i = 1; i < 32; i++)
            {
                Val = i.ToString() + ";" + i.ToString();
                fun.AgregarFilas(trdo, Val);
            }
            fun.LlenarComboDatatable(cmb_Dia, trdo, "Dia", "Nombre");

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
            fun.LlenarComboDatatable(cmb_Mes, tmes, "Nombre", "Mes");
            fun.LlenarCombo(cmb_CodTipoDoc, "TipoDocumento", "Nombre", "CodTipoDoc");
            if (cmb_CodTipoDoc.Items.Count > 1)
            {
                cmb_CodTipoDoc.SelectedIndex = 1; 
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = true;
            txt_Nombre.Focus();
           if (cmb_CodTipoDoc.Items.Count > 1)
            {
                cmb_CodTipoDoc.SelectedIndex = 1; 
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            Clases.cFunciones fun = new Clases.cFunciones();

            if (txt_Email.Text  != "")
            {
                if (fun.validarEmail(txt_Email.Text) == false)
                {
                    MessageBox.Show("Formato incorrecto del e-mail", Clases.cMensaje.Mensaje());
                    return;
                }
            }         
            if (txtCodigo.Text == "")
                fun.GuardarNuevoGenerico(this, "Cliente");
            else
                fun.ModificarGenerico(this, "Cliente", "CodCliente", txtCodigo.Text);
            MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
            Botonera(1);
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = false;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            //nombre de los camposa buscar, se llaman igual que en la base de datos
            //exepcional para este abm
            Principal.CodigoSenia = "1";
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
            Clases.cFunciones fun = new Clases.cFunciones();
            //CargarJugador(Convert.ToInt32(PRINCIPAL.CDOGIO_JUGADOR));
            if (Principal.CodigoPrincipalAbm != null)
            {
                if (Principal.CodigoPrincipalAbm != "")
                {
                    Botonera(3);
                    txtCodigo.Text = Principal.CodigoPrincipalAbm.ToString();

                    if (Principal.CodigoPrincipalAbm != "")
                        fun.CargarControles(this, "Cliente", "CodCliente", txtCodigo.Text);
                    Grupo.Enabled = false;
                    //return;
                }

            }
            if (txtCodigo.Text != "")
            {
                cCliente cli = new cCliente();
                DataTable trdo = cli.GetClientexCodigo(Convert.ToInt32(txtCodigo.Text));
                if (trdo.Rows.Count > 0)
                    if (trdo.Rows[0]["ClienteNulo"].ToString() == "1")
                        Botonera(1);
            }
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Grupo.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Botonera(1);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Grupo_Enter(object sender, EventArgs e)
        {

        }

       

        private void GetClientexCodigo(Int32 CodCliente)
        {
            cCliente cli = new cCliente();
            DataTable trdo = cli.GetClientexCodigo(CodCliente);
            if (trdo.Rows.Count > 0)
            {
                txtCodigo.Text = trdo.Rows[0]["CodCliente"].ToString();
                txt_NroDocumento.Text = trdo.Rows[0]["NroDocumento"].ToString();
                txt_Nombre.Text = trdo.Rows[0]["Nombre"].ToString();
                txt_Apellido.Text = trdo.Rows[0]["Apellido"].ToString();
                txt_Telefono.Text = trdo.Rows[0]["Telefono"].ToString();
                txt_Email.Text = trdo.Rows[0]["Email"].ToString();
                if (trdo.Rows[0]["Mes"].ToString() != "")
                {
                    cmb_Mes.SelectedValue = trdo.Rows[0]["Mes"].ToString();
                }

                if (trdo.Rows[0]["Dia"].ToString() != "")
                {
                    cmb_Dia.SelectedValue = trdo.Rows[0]["Dia"].ToString();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string msj = "Confirma eliminar el cliente ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }
            cCorte corte = new cCorte();
            Int32 CodCliente = Convert.ToInt32(txtCodigo.Text);
            cCliente cli = new cCliente();
            cli.BorrarCliente(CodCliente);
            corte.BorrarCortexCliente(CodCliente);
            MessageBox.Show("Cliente Eliminado", "Sistema");
            Botonera(1);
            cFunciones fun = new cFunciones();
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = false;
        }

        
    }
}
