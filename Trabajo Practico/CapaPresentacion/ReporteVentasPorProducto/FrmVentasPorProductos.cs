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

namespace Trabajo_Practico.CapaPresentacion.ReporteVentasPorProducto
{
    public partial class FrmVentasPorProductos : Form
    {
        public FrmVentasPorProductos()
        {
            InitializeComponent();
        }

        private void FrmVentasPorProductos_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            DataTable tabla = Validador.Buscar_ventas_producto();
            ReportDataSource ds = new ReportDataSource("Producto", tabla);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);
            reportViewer1.LocalReport.Refresh();
        }
    }
}
