using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // El paquete que instalamos antes
using Periodico_KCRUD.Datos;    // Tu clase de conexión

namespace Periodico_KCRUD.Vistas
{
    public partial class FormGestionNoticias : Form
    {
        ConexionBD bd = new ConexionBD();

        public FormGestionNoticias()
        {
            InitializeComponent();
            CargarTabla(); // Al abrir, mostramos lo que hay en SQL
        }

        // MÉTODO LEER (Punto 9 de la rúbrica)
        private void CargarTabla()
        {
            try
            {
                bd.Abrir();
                string query = "SELECT * FROM Noticias ORDER BY FechaPublicacion DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, bd.conectar);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvNoticias.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar: " + ex.Message); }
            finally { bd.Cerrar(); }
        }

        // MÉTODO CREAR (Punto 3: CRUD)
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // 1. Limpiamos espacios en blanco y validamos
            string titulo = txtTitulo.Text.Trim();
            string contenido = txtContenido.Text.Trim();

            if (titulo == "" || contenido == "")
            {
                MessageBox.Show("Debes escribir algo en el título y el contenido.");
                return;
            }

            try
            {
                // 2. Usamos la conexión 'bd' que ya declaraste arriba de la clase
                bd.Abrir();
                string sql = "INSERT INTO Noticias (Titulo, Contenido, FechaPublicacion) VALUES (@t, @c, GETDATE())";

                SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                cmd.Parameters.AddWithValue("@t", titulo);
                cmd.Parameters.AddWithValue("@c", contenido);

                int resultado = cmd.ExecuteNonQuery();
                bd.Cerrar(); // Cerramos inmediatamente después de ejecutar

                if (resultado > 0)
                {
                    MessageBox.Show("¡Guardado correctamente!");
                    txtTitulo.Clear();
                    txtContenido.Clear();

                    // 3. ¡ESTO ES VITAL! Volvemos a cargar la tabla para ver la nueva noticia
                    CargarTabla();
                }
            }
            catch (Exception ex)
            {
                // Si entra aquí, el error es de SQL. Lee lo que dice el mensaje.
                MessageBox.Show("ERROR DE SQL: " + ex.Message);
                if (bd.conectar.State == ConnectionState.Open) bd.Cerrar();
            }
        }

        // MÉTODO BORRAR (Punto 3: CRUD)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvNoticias.SelectedRows.Count > 0)
            {
                // Obtenemos el ID de la fila seleccionada
                int id = Convert.ToInt32(dgvNoticias.CurrentRow.Cells["NoticiaID"].Value);

                bd.Abrir();
                string sql = "DELETE FROM Noticias WHERE NoticiaID = @id";
                SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                bd.Cerrar();

                MessageBox.Show("Noticia eliminada.");
                CargarTabla();
            }
            else { MessageBox.Show("Selecciona una fila completa en la tabla para borrar."); }
        }
    }
}