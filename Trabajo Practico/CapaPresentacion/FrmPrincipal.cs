using El_Sabroso_App.CapaEntidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabajo_Practico.CapaPresentacion;
using Trabajo_Practico.CapaPresentacion.abmProductos;
using Trabajo_Practico.CapaPresentacion.abmProveedores;
using Trabajo_Practico.CapaPresentacion.ReporteListadoStock;
using Trabajo_Practico.CapaPresentacion.ReporteVentaCategoria;
using Trabajo_Practico.CapaPresentacion.ReporteVentaProveedor;
using Trabajo_Practico.CapaPresentacion.ReporteVentasPorProducto;
using Trabajo_Practico.CapaPresentacion.Reports;

namespace Trabajo_Practico
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            frmLogin Login = new frmLogin();
            Login.ShowDialog();
            lblUsuario.Text = "Usuario: " + Login.retornarNombre();
        }

        private void altaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmAltaProducto(1, new Producto()).ShowDialog();
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsultarProducto consulta = new FrmConsultarProducto();
            consulta.ShowDialog();
        }

        private void altaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAltaPro alta = new frmAltaPro();
            alta.ShowDialog();
        }

        private void consultaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaPro ventana = new frmConsultaPro();
            ventana.ShowDialog();
        }

        private void nuevaVentaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVentas Venta = new FrmVentas();
            Venta.ShowDialog();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCargarDatosVentas ventana = new FrmCargarDatosVentas();
            ventana.ShowDialog();   
        }

        private void stockDisponibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListadoStockPro ventana = new FrmListadoStockPro();
            ventana.ShowDialog();
        }

        private void ventasPorCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentasCategorias ventana = new FrmVentasCategorias();
            ventana.ShowDialog();
        }

        private void ventasPorProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentasPorProductos ventana = new FrmVentasPorProductos();
            ventana.ShowDialog();
        }

        private void ventasPorProveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentasPorProveedor ventana = new FrmVentasPorProveedor();
            ventana.ShowDialog();
        }

        private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayuda vendana = new Ayuda();
            vendana.ShowDialog();
        }
    }
}
