using El_Sabroso_App.Validador;
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
    public partial class FrmCargarDatosVentas : Form
    {
        public FrmCargarDatosVentas()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool van = Validador.ValidarExistenciaDeVentas(dateTimePicker1, dateTimePicker2);
            if (!van) { 
                MessageBox.Show("No hay ventas en ese periodo");
                return;
            }
            FrmListadoVentas ventana = new FrmListadoVentas();
            ventana.inicializar(dateTimePicker1, dateTimePicker2);
            ventana.ShowDialog();
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
