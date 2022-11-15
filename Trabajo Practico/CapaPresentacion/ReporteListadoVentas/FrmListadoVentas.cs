using El_Sabroso_App.Validador;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Practico.CapaPresentacion.Reports
{
    public partial class FrmListadoVentas : Form
    {
        DateTimePicker desde;
        DateTimePicker hasta;
        public FrmListadoVentas()
        {
            InitializeComponent();
        }

        internal void inicializar(DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2)
        {
            desde = dateTimePicker1;
            hasta = dateTimePicker2;
        }

        private void FrmListadoVentas_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable tabla = Validador.ObtenerDatosVentas(desde, hasta);
            ReportDataSource ds = new ReportDataSource("Ventas",tabla);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }
    }
}
