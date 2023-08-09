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
    public partial class FrmCorrector : Form
    {
        public FrmCorrector()
        {
            InitializeComponent();
        }

        private void FrmCorrector_Load(object sender, EventArgs e)
        {

        }

        private void btnCorregir_Click(object sender, EventArgs e)
        {
            string sql = "select *";
            sql = sql + " from Corte c,cliente cli";
            sql = sql + " where c.CodCliente = cli.CodCliente";
            sql = sql + " order by c.CodCorte asc";
            DataTable trdo = cDb.ExecuteDataTable(sql);
            SqlTransaction Transaccion;
            SqlConnection con = new SqlConnection(cConexion.Cadenacon());
            con.Open();
            Transaccion = con.BeginTransaction();
            try
            {
                cMovimiento mov = new cMovimiento();
                for (int i = 0; i < trdo.Rows.Count; i++)
                {
                    string Cliente = trdo.Rows[i]["Nombre"].ToString();
                    Cliente = Cliente + " " + trdo.Rows[i]["APELLIDO"].ToString();
                    string DESCRIPCION = "TRABAJO REALIZADO A " + Cliente.ToString ();
                    Int32 CodCorte = Convert.ToInt32(trdo.Rows[i]["CodCorte"].ToString());
                    DateTime fecha = Convert.ToDateTime(trdo.Rows[i]["Fecha"].ToString ());
                    double Total = Convert.ToDouble(trdo.Rows[i]["Total"].ToString());
                    mov.InsertarMovimientoTransaccion(con, Transaccion, DESCRIPCION, fecha, Total, 0, null, CodCorte, null, 0, null, Total, 0,null);
                } 
                Transaccion.Commit();
                con.Close();
                MessageBox.Show("Datos grabados correctamente");
            }
            catch (Exception ex)
            {
                Transaccion.Rollback();
                MessageBox.Show("Hubo un error en el proceso de grabación", Clases.cMensaje.Mensaje());
            }
        }
    }
}
