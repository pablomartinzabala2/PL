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
    public partial class FrmConsultaTrabajo : Form
    {

        public FrmConsultaTrabajo()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void CargarPeluquero()
        {
            cPeluquero pel = new cPeluquero();
            DataTable trdo = pel.GetPeluqueros();
            cFunciones fun = new cFunciones();
            
        }

        private void FrmConsultaTrabajo_Load(object sender, EventArgs e)
        {
            CargarPeluquero();
            cFunciones fun = new cFunciones();
            DateTime fecha = DateTime.Now;
            txtFechaHasta.Text = fecha.ToShortDateString();
            //fecha = fecha.AddMonths(-1);
            txtFechaDesde.Text = fecha.ToShortDateString();
            if (Principal.CodidoClientePrincipal !=null)
                if (Principal.CodidoClientePrincipal != null)
                {
                    Int32 CodCliente = Convert.ToInt32(Principal.CodidoClientePrincipal);
                    chkFechas.Checked = false;
                    BuscarxCodCliente(CodCliente);
                }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            cFunciones fun = new cFunciones();
            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            if (chkFechas.Checked == true)
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
                FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
                FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            }
            string ApeCli = txtApellidoCLiente.Text;
            string NomCli = txtNombre.Text;
            Int32? CodPeluquero = null;
            Int32? CodCliente = null;
            Int32? NroDocumento = null;
            if (txtDni.Text != "")
                NroDocumento = Convert.ToInt32(txtDni.Text); 
            cCorte cor = new cCorte();
            DataTable trdo = cor.GetCortes(FechaDesde, FechaHasta, NomCli, ApeCli, CodPeluquero, NroDocumento, CodCliente);
            double Totalizar = fun.TotalizarColumna(trdo, "Total");
            trdo = fun.TablaaMiles(trdo, "Total");
            //traigo peluquero
            Clases.cDetalleCorte detalle = new Clases.cDetalleCorte();
            for (int i = 0; i < trdo.Rows.Count; i++)
            {
                Int32 CodCorte = Convert.ToInt32(trdo.Rows[i]["CodCorte"].ToString());
                string Pel = detalle.GetPeluquerosxCodCorte(CodCorte);
                trdo.Rows[i][3] = Pel.ToString(); 
            }
            trdo.AcceptChanges();
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 120;
            Grilla.Columns[2].Width = 120;
            Grilla.Columns[3].Width = 350;
            Grilla.Columns[4].Width = 80;
            Grilla.Columns[3].HeaderText = "Trabajadores"; 
            txtTotal.Text = Totalizar.ToString();
            txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }

        private void Mensaje(string msj)
        {
            MessageBox.Show(msj, cMensaje.Mensaje());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro para continuar");
                return;
            }
            string CodCorte = Grilla.CurrentRow.Cells[0].Value.ToString();
            Principal.CodigoCorte = CodCorte;
            FrmCorte frm2 = new FrmCorte();
            frm2.FormClosing += new FormClosingEventHandler(form_FormClosing);
            frm2.Show();
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Buscar();
            this.Close();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (Grilla.CurrentRow == null)
            {
                Mensaje("Debe seleccionar un registro para continuar");
                return;
            }

            string msj = "Confirma eliminar el trabajo ";
            var result = MessageBox.Show(msj, "Información",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                return;
            }

            Int32 CodCorte = Convert.ToInt32(Grilla.CurrentRow.Cells[0].Value);
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {

                cCorte obj = new cCorte();
                cMovimiento mov = new cMovimiento();
                obj.BorrarDetalleCorte (con ,Transaccion ,CodCorte);
                obj.BorrarCorte(con, Transaccion, CodCorte);
                mov.BorrarMovimientoxCodCorte(con, Transaccion, CodCorte);
                Transaccion.Commit();
                con.Close();
                Mensaje("Datos borrados correctamente");
                Buscar();
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            cFunciones fun = new cFunciones();
            fun.SoloEnteroConPunto(sender, e);
        }

        private void BuscarxCodCliente(Int32 CodCliente)
        {
            cFunciones fun = new cFunciones();
            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            if (chkFechas.Checked == true)
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
                FechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
                FechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
            }
            string ApeCli = txtApellidoCLiente.Text;
            string NomCli = txtNombre.Text;
            Int32? CodPeluquero = null;
            
            Int32? NroDocumento = null;
            if (txtDni.Text != "")
                NroDocumento = Convert.ToInt32(txtDni.Text);
            cCorte cor = new cCorte();
            DataTable trdo = cor.GetCortes(FechaDesde, FechaHasta, NomCli, ApeCli, CodPeluquero, NroDocumento, CodCliente);
            double Totalizar = fun.TotalizarColumna(trdo, "Total");
            trdo = fun.TablaaMiles(trdo, "Total");
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 210;
            Grilla.Columns[2].Width = 205;
            Grilla.Columns[3].Width = 80;
            Grilla.Columns[4].Width = 170;
            // Grilla.Columns[4].HeaderText = "Teléfono"; 
            txtTotal.Text = Totalizar.ToString();
            txtTotal.Text = fun.SepararDecimales(txtTotal.Text);
            txtTotal.Text = fun.FormatoEnteroMiles(txtTotal.Text);
        }
    }
}
