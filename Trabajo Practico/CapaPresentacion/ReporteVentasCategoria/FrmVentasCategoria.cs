using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Practico.CapaPresentacion.ReporteVentasCategoria
{
    public partial class FrmVentasCategoria : Form
    {
        public FrmVentasCategoria()
        {
            InitializeComponent();
        }

        private void FrmVentasCategoria_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
