using El_Sabroso_App.CapaEntidades;
using El_Sabroso_App.Validador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Practico.CapaPresentacion.abmProveedores
{
    public partial class frmEditarPro : Form
    {
        public frmEditarPro()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool van = Validador.validar(Controls);
            if (!van)
            {
                return;
            }
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE PROVEEDORES SET nombre = @nombre, apellido = @apellido, telefono = @telefono,email = @mail,direccion = @direccion,ciudad = @ciudad, fecha_alta = @fechaalta WHERE Id_proveedor = @codigo";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@codigo", Int32.Parse(txtId.Text));
                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@telefono", Int32.Parse(txtTelefono.Text));
                cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd.Parameters.AddWithValue("@ciudad", txtCiudad.Text);
                cmd.Parameters.AddWithValue("@fechaalta", dtpFachaAlta.Value);


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Se modifico con exito el proveedor");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditarPro_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
        }
        internal void inicializar(Proveedor prov)
        {
            txtId.Text = prov.IdProveedor.ToString();
            txtNombre.Text = prov.Nombre.ToString();
            txtApellido.Text = prov.Apellido.ToString();
            txtCiudad.Text = prov.Ciudad.ToString();
            txtDireccion.Text = prov.Direccion.ToString();
            txtMail.Text = prov.Email.ToString();
            txtTelefono.Text = prov.Telefono.ToString();
            dtpFachaAlta.Value = prov.Fecha_Alta;
        }
    }
}
