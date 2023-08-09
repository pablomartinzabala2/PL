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
    public partial class FrmCumpleanios : Form
    {
        public FrmCumpleanios()
        {
            InitializeComponent();
        }

        private void FrmCumpleanios_Load(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            int Dia = hoy.Day;
            int Mes = hoy.Month;
            Clases.cCliente cli = new Clases.cCliente();
            DataTable trdo = cli.GetCumplenios(Dia, Mes);
            Grilla.DataSource = trdo;
            Grilla.Columns[0].Visible = false;
            Grilla.Columns[1].Width = 100;
            Grilla.Columns[2].Width = 100;
            Grilla.Columns[3].Width = 180;
            Grilla.Columns[4].Width = 50;
            Grilla.Columns[5].Width = 50;
            Grilla.Columns[6].HeaderText = "Teléfono";
            Grilla.Columns[7].Visible = false;
            Grilla.Columns[8].Visible = false;
        }
    }
}
