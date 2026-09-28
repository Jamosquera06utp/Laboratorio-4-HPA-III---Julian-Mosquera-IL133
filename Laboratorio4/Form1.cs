using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Laboratorio4
{
    public partial class Form1 : Form
    {
        ConexionBD conexion = new ConexionBD();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            CargarProductos();
        }

        // Manejador de errores para evitar excepciones si hay valores nulos o incompatibles
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        // Funciones auxiliares para la conversión de imágenes
        public static byte[] ImageToByteArray(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bmp = new Bitmap(image))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                return ms.ToArray();
            }
        }

        public static Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        // Cargar productos en el DataGridView
        private void CargarProductos()
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id AS ID, folio AS Folio, nombre AS Nombre, precio AS Precio, cantidad AS Cantidad, imagen AS Imagen FROM productos";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos: " + ex.Message);
                }
            }
        }

        // BOTÓN AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string folio = txtFolio.Text;
            string nombre = txtNombre.Text;
            string precioText = txtPrecio.Text;
            string cantidadText = txtCantidad.Text;
            byte[] imagenBytes = ImageToByteArray(pctbImagen.Image);

            if (string.IsNullOrEmpty(folio) || string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(precioText) || string.IsNullOrEmpty(cantidadText))
            {
                MessageBox.Show("Por favor, llena todos los campos requeridos.");
                return;
            }

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO productos (folio, nombre, precio, cantidad, imagen) VALUES (@folio, @nombre, @precio, @cantidad, @imagen)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@folio", folio);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(precioText));
                        cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(cantidadText));
                        cmd.Parameters.AddWithValue("@imagen", imagenBytes);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado correctamente.");

                        CargarProductos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
            }
        }

        // BOTÓN MODIFICAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un producto de la tabla para modificar.");
                return;
            }

            byte[] imagenBytes = ImageToByteArray(pctbImagen.Image);

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE productos SET folio=@folio, nombre=@nombre, precio=@precio, cantidad=@cantidad, imagen=@imagen WHERE id=@id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                        cmd.Parameters.AddWithValue("@folio", txtFolio.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                        cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                        cmd.Parameters.AddWithValue("@imagen", imagenBytes);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto modificado correctamente.");

                        CargarProductos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message);
                }
            }
        }

        // BOTÓN ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un producto de la tabla para eliminar.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                using (MySqlConnection conn = conexion.ObtenerConexion())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM productos WHERE id=@id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Producto eliminado correctamente.");

                            CargarProductos();
                            LimpiarCampos();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message);
                    }
                }
            }
        }

        // BUSCADOR (Filtra en tiempo real al escribir en el TextBox)
        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id AS ID, folio AS Folio, nombre AS Nombre, precio AS Precio, cantidad AS Cantidad, imagen AS Imagen FROM productos WHERE folio LIKE @filtro OR nombre LIKE @filtro";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + txtBuscador.Text + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }
            }
        }

        // Clic en la tabla para seleccionar fila y cargar la imagen en el PictureBox
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = row.Cells["ID"].Value.ToString();
                txtFolio.Text = row.Cells["Folio"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtPrecio.Text = row.Cells["Precio"].Value.ToString();
                txtCantidad.Text = row.Cells["Cantidad"].Value.ToString();

                if (row.Cells["Imagen"].Value != DBNull.Value && row.Cells["Imagen"].Value != null)
                {
                    byte[] bytes = (byte[])row.Cells["Imagen"].Value;
                    pctbImagen.Image = ByteArrayToImage(bytes);
                    pctbImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pctbImagen.Image = null;
                }
            }
        }

        // Limpiar formulario
        private void LimpiarCampos()
        {
            txtId.Clear();
            txtFolio.Clear();
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCantidad.Clear();
            pctbImagen.Image = null;
            pctbImagen.ImageLocation = null;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void pctbImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog buscarImagen = new OpenFileDialog())
            {
                buscarImagen.Title = "Seleccionar Imagen del Producto";
                buscarImagen.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (buscarImagen.ShowDialog() == DialogResult.OK)
                {
                    pctbImagen.Image = Image.FromFile(buscarImagen.FileName);
                    pctbImagen.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBuscador_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id AS ID, folio AS Folio, nombre AS Nombre, precio AS Precio, cantidad AS Cantidad, imagen AS Imagen FROM productos WHERE folio LIKE @filtro OR nombre LIKE @filtro";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + txtBuscador.Text.Trim() + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar: " + ex.Message);
                }
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            // 1. Validar que se haya seleccionado un registro de la tabla
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Por favor, selecciona un producto de la tabla para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Confirmar si el usuario realmente desea eliminar
            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (confirmacion == DialogResult.Yes)
            {
                using (MySqlConnection conn = conexion.ObtenerConexion())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM productos WHERE id = @id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                            int filasAfectadas = cmd.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Producto eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Recargar el DataGridView y limpiar el formulario
                                CargarProductos();
                                LimpiarCampos();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el registro a eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }

    // Clase de conexión
    public class ConexionBD
    {
        private string cadenaConexion = "Server=localhost; Database=tienda_db; Uid=root; Pwd=Isayalejo1.;";

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}