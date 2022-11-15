using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace El_Sabroso_App.Validador
{
    internal class Validador
    {
        public static bool validar(Control.ControlCollection a)
        {
            bool van = true;
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            foreach (Control item in a)
            {
                if (item is GroupBox)
                {
                    foreach (Control item1 in item.Controls)
                    {
                        if (item1.Text.Trim().Equals(""))
                        {
                            van = false;
                        }
                    }


                }
            }
            if (!van)
            {
                MessageBox.Show("Hay campos incorrectos");
            }
            return van;
        }

        internal static DataTable ObtenerDatosVentas(DateTimePicker dtpicdesde, DateTimePicker dtpichasta)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM VENTAS WHERE fecha >= @fechadesde AND fecha <= @fechahasta";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@fechadesde", dtpicdesde.Value);
                cmd.Parameters.AddWithValue("@fechahasta", dtpichasta.Value);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static DataTable ObtenerStock()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT P.nombre as 'Producto',S.stock as 'Cantidad' FROM PRODUCTOS P JOIN STOCK S ON P.Id_producto = S.id_producto";
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;

            }
            catch (Exception)
            {

                throw;
            }
        }

        internal static bool ValidarExistenciaDeVentas(DateTimePicker dateTimePicker1, DateTimePicker dateTimePicker2)
        {
            bool van = false;
            DataTable tabla = ObtenerDatosVentas(dateTimePicker1, dateTimePicker2);
            if (tabla.Rows.Count != 0)
            {
                van = true;
            }
            return van;
        }

        internal static bool validarExistenciaPro(string nombre, string apellido)
        {
            bool van = true;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT * FROM PROVEEDORES WHERE nombre like @nombre AND apellido like @apellido";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                if (tabla.Rows.Count != 0)
                {
                    van = false;
                }
                return van;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    
}
