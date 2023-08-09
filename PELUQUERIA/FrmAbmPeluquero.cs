using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PELUQUERIA
{
    public partial class FrmAbmPeluquero : Form
    {
        public FrmAbmPeluquero()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = true;
            txt_Nombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Botonera(2);
            Grupo.Enabled = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txt_Nombre.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txt_Apellido.Text  == "")
            {
                MessageBox.Show("Debe ingresar un apellido para continuar", Clases.cMensaje.Mensaje());
                return;
            }

            if (txt_Porcentaje.Text != "")
            {
                Int32 Por = Convert.ToInt32(txt_Porcentaje.Text);
                if (Por > 100)
                {
                    MessageBox.Show ("El importe no debe superar el 100");
                    return ;
                }
            }
            if (txt_Porcentaje.Text == "")
                txt_Porcentaje.Text = "10";


            Clases.cFunciones fun = new Clases.cFunciones();
            if (txtCodigo.Text == "")
                fun.GuardarNuevoGenerico(this, "Peluquero");
            else
                fun.ModificarGenerico(this, "Peluquero", "CodPeluquero", txtCodigo.Text);
            MessageBox.Show("Datos grabados Correctamente", Clases.cMensaje.Mensaje());
            Botonera(1);
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = false;
         
    }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Botonera(1);
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.LimpiarGenerico(this);
            txtCodigo.Text = "";
            Grupo.Enabled = false;
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

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            //nombre de los camposa buscar, se llaman igual que en la base de datos
            
          
            Principal.OpcionesdeBusqueda = "Apellido;Nombre";
            //nombre de la tabla, 
            Principal.TablaPrincipal = "Peluquero";

            Principal.OpcionesColumnasGrilla = "CodPeluquero; Nombre;Apellido;Telefono";
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
                        fun.CargarControles(this, "Peluquero", "CodPeluquero", txtCodigo.Text);
                    Grupo.Enabled = false;
                    return;
                }

            }
        }

        private void FrmAbmPeluquero_Load(object sender, EventArgs e)
        {
            Botonera(1);
            Grupo.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            Clases.cFunciones fun = new Clases.cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Debe seleccionar un registro");
                return;
            }
            try
            {
                Clases.cFunciones fun = new Clases.cFunciones();
                fun.EliminarGenerico("Peluquero", "CodPeluquero", txtCodigo.Text);
                MessageBox.Show("Datos borrados correctamente", Clases.cMensaje.Mensaje());
                Botonera(1);
                Grupo.Enabled = false;
                //Clases.cFunciones fun = new Clases.cFunciones();
                fun.LimpiarGenerico(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede borrar el registro, tiene datos asociaados", Clases.cMensaje.Mensaje());
            }
        }

}
}
