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

namespace Trabajo_Practico.CapaPresentacion
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            cargarProductoInicial();
            string productoSelect = comboProducto.SelectedText.ToString();
        }

        private void cargarProductoInicial()
        {
            String consultaSql = string.Concat("" +
              "SELECT * from PRODUCTOS " +
              " ");

            DataTable resultado = DataManager.GetInstance().ConsultaSQL(consultaSql);
            comboProducto.DataSource = resultado;
            comboProducto.DisplayMember = "nombre";
            comboProducto.ValueMember = "id_producto";
            comboProducto.Text = "";

        }

        private void cargarComboCategoria()
        {
            String consultaSql = string.Concat("" +
                "SELECT * from CATEGORIAS_PROD ");

            DataTable resultado = DataManager.GetInstance().ConsultaSQL(consultaSql);
            comboCategoria.DataSource = resultado;
            comboCategoria.DisplayMember = "nombre";
            comboCategoria.ValueMember = "id_categoria";

        }

        private void cargarComboProveedores()
        {

            String consultaSql = string.Concat("" +
                "SELECT * from PROVEEDORES ");

            DataTable resultado = DataManager.GetInstance().ConsultaSQL(consultaSql);
            comboProveedor.DataSource = resultado;
            comboProveedor.DisplayMember = "nombre";
            comboProveedor.ValueMember = "Id_proveedor";
        }
        private void cargarComboProductos(string productoSelect)
        {

            DataTable tabla = cargarTabla(productoSelect);
            //carga proveedor
            comboProveedor.DataSource = tabla;
            comboProveedor.DisplayMember = "proveedor";
            comboProveedor.ValueMember = "Id_proveedor";
            //carga categoria
            comboCategoria.DataSource = tabla;
            comboCategoria.DisplayMember = "categoria";
            comboCategoria.ValueMember = "c.id_categoria";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtNombreCliente.Enabled= false;
            textBox2.Enabled= false;
            textBox3.Enabled= false;
            textBox4.Enabled= false;
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Fecha ingresada incorrecta");
                return;

            }

            if (comboProducto.Text.Trim() == "")
            {
                MessageBox.Show("Se debe ingresar un producto");
                return;
            }

            if (CantProd.Value == 0)
            {
                MessageBox.Show("Se debe ingresar una cantidad mayor a 0");
                return;
            }

            if (comboProveedor.Text.Trim() == "")
            {
                MessageBox.Show("Se debe elegir un proveedor");
                return;
            }

            //controla que no ingrese dos veces el mismo dato en la gridviwv intermedia
            if ((dataGridView1.RowCount) > 0)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[0].Value != null && item.Cells[0].Value.ToString() == comboProducto.Text.ToString())
                    {

                        MessageBox.Show("Ya se agrego este producto");
                        return;
                    }
                }
            }

            dataGridView1.Rows.Add(comboProducto.Text, comboProveedor.Text, comboCategoria.Text, CantProd.Value, txtNombreCliente.Text, textBox2.Text, textBox4.Text, textBox3.Text);
            int resultado = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    int a = Int32.Parse(row.Cells[3].Value.ToString()); //cantidad
                    DataTable tabla = cargarTabla2(row.Cells[0].Value.ToString());
                    int b = Int32.Parse(tabla.Rows[0][2].ToString());
                    resultado += a * b;
                }
            }
            MontoTotal.Text = resultado.ToString();

        }

        private DataTable cargarTabla2(string productoSelect)
        {

            String consultasql2 =
                "SELECT pr.Id_producto, pr.nombre,pr.precio,c.id_categoria,c.nombre as categoria, p.Id_proveedor ,p.nombre as proveedor" +
                " FROM PRODUCTOS pr" +
                " LEFT JOIN PROVEEDORES p" +
                " ON pr.id_proveedor = p.Id_proveedor" +
                " LEFT JOIN CATEGORIAS_PROD c" +
                " ON pr.id_categoria = c.id_categoria" +
                " WHERE pr.nombre = " + "'" + productoSelect + "'";
            DataTable resultado2 = DataManager.GetInstance().ConsultaSQL(consultasql2);
            return resultado2;

        }

        private void comboProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboProducto.SelectedValue != null)
            {
                string productoSelect = comboProducto.SelectedValue.ToString();

                if (productoSelect != "" && productoSelect != "System.Data.DataRowView")
                {
                    cargarComboProductos(productoSelect);
                }
            }
        }

        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComboProveedores();
        }

        private void comboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarComboCategoria();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlTransaction objTransaction = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO[dbo].[VENTAS]" +
                                    "([fecha]" +
                                    ",[cantidad]"+
                                    ",[monto]" +
                                    ",[nombre_cliente]" +
                                    ",[apellido_cliente]" +
                                    ",[email_cliente]" +
                                    ",[telefono_cliente])" +
                                 " VALUES " +
                                    "(@fecha" +
                                    ",@cantidad"+
                                    ",@monto" +
                                    ",@nombreCliente" +
                                    ",@ApellidoCliente" +
                                    ",@emailCliente" +
                                    ",@telefono)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cantidad",buscar_cantidad());
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@monto", Int32.Parse(MontoTotal.Text));
                cmd.Parameters.AddWithValue("@ApellidoCliente", textBox2.Text);
                cmd.Parameters.AddWithValue("@emailCliente", textBox3.Text);
                cmd.Parameters.AddWithValue("@nombreCliente", (txtNombreCliente.Text));
                cmd.Parameters.AddWithValue("@telefono", (textBox4.Text));
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                objTransaction = cn.BeginTransaction("ventas");

                cmd.Transaction = objTransaction;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                int numerofactura = buscar_venta();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Cells[1].Value == null) { break; }
                    string consultaDetalleFactura = "INSERT INTO[dbo].[DETALLE_VENTAS]"
                                                        + "([Id_venta]"
                                                        + ",[cantidad]"
                                                        + ",[Id_proveedor]"
                                                        + ",[id_categoria]"
                                                        + ",[Id_producto])"
                                                    + " VALUES "
                                                        + "(@idventa"
                                                        + ",@cantidad"
                                                        + ",@id_proveedor"
                                                        + ",@categoria"
                                                        + ",@id_producto)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idventa", numerofactura);
                    cmd.Parameters.AddWithValue("@cantidad", Decimal.ToInt32(CantProd.Value));
                    cmd.Parameters.AddWithValue("@id_proveedor", comboProveedor.SelectedValue);
                    cmd.Parameters.AddWithValue("@categoria", comboCategoria.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_producto", comboProducto.SelectedValue);
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = consultaDetalleFactura;
                    cmd.ExecuteNonQuery();
                    //buscar stock
                    int stock = buscarstock(comboProducto.SelectedValue);
                    if (stock - Decimal.ToInt32(CantProd.Value) < 0) {
                        MessageBox.Show("No hay suficiente stock");
                        return;
                    }
                    string consultaEliminarStock = "UPDATE [dbo].[STOCK]" +
                                                    "SET [stock] = @cantidadnueva " +
                                                    "WHERE id_producto = @id_producto";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id_producto", comboProducto.SelectedValue);
                    cmd.Parameters.AddWithValue("@cantidadnueva", stock - Decimal.ToInt32(CantProd.Value));
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = consultaEliminarStock;
                    cmd.ExecuteNonQuery();

                }
                objTransaction.Commit();
                MessageBox.Show("Se genero la venta con sus detalles");

            }
            catch (Exception ex)
            {
                objTransaction.Rollback();
                MessageBox.Show("La transaccion no se pudo completar");

                throw;
            }
        }

        private int buscarstock(object selectedValue)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT stock FROM STOCK WHERE id_producto = @id_producto";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_producto", selectedValue);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return (int)tabla.Rows[0][0];

            }
            catch (Exception)
            {

                throw;
            }
        }

        private int buscar_cantidad()
        {
            int cantidad = 0;
            foreach (DataGridViewRow fila in dataGridView1.Rows) {
                if (fila.Cells[3].Value != null) {
                    cantidad += Int32.Parse(fila.Cells[3].Value.ToString()); 
                }
            }
            return cantidad;
        }

        private int buscar_venta()
        {
            bool vandera = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "Select IDENT_CURRENT('Ventas') as 'venta'";
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                if (tabla.Rows.Count > 0) { return Int32.Parse(tabla.Rows[0]["venta"].ToString()); }
                else { return 0; }


            }
            catch (Exception)
            {

                throw;
            }
        }
        private DataTable cargarTabla(string productoSelect)
        {

            String consultasql2 =
                "SELECT pr.Id_producto, pr.nombre,pr.precio,c.id_categoria,c.nombre as categoria, p.Id_proveedor ,p.nombre as proveedor" +
                " FROM PRODUCTOS pr" +
                " LEFT JOIN PROVEEDORES p" +
                " ON pr.id_proveedor = p.Id_proveedor" +
                " LEFT JOIN CATEGORIAS_PROD c" +
                " ON pr.id_categoria = c.id_categoria" +
                " WHERE pr.Id_producto = " + "'" + productoSelect + "'";
            DataTable resultado2 = DataManager.GetInstance().ConsultaSQL(consultasql2);
            return resultado2;

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
            else {
                MessageBox.Show("No hay prductos agregados");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}