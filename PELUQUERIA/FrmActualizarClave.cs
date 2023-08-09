using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using PELUQUERIA.Clases;

namespace PELUQUERIA
{
    public partial class FrmActualizarClave : Form
    {
        string ClaveActual;
        public FrmActualizarClave()
        {
            InitializeComponent();
        }

        private void FrmActualizarClave_Load(object sender, EventArgs e)
        {
            Int32 CodUsuario = Principal.CodUsuarioLogueado;
            cUsuario usu = new cUsuario();
            DataTable trdo = usu.GetUsuarioxCodigo(CodUsuario);
            if (trdo.Rows.Count > 0)
            {
                ClaveActual = trdo.Rows[0]["Clave"].ToString(); 
            }
        }
        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtClave.Text == "")
            {
                Mensaje("Ingresar la clave actual");
                return;
            }

            if (txtClave.Text.ToUpper() != ClaveActual.ToString().ToUpper())
            {
                Mensaje("La clave ingresada es diferente a la clave registrada");
                return;
            }
            if (txtContraseniaNueva.Text == "")
            {
                Mensaje("Debe ingresar una clave nueva para continuar");
                return;
            }

            if (txtReingresoContrasenia.Text == "")
            {
                Mensaje("Debe reingresar una clave nueva para continuar");
                return;
            }

            if (txtReingresoContrasenia.Text.ToUpper() != txtContraseniaNueva.Text.ToUpper())
            {
                Mensaje ("la contraseña nueva ingresada es diferente a la contraseña reingresada");
                return ;
            }
            cUsuario usu = new cUsuario ();
            usu.ActualizarContrasenia (Principal.CodUsuarioLogueado,txtContraseniaNueva.Text);
            Mensaje ("Contraseña actualizada correctamente");


        }
    }
}
