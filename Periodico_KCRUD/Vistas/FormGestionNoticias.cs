using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Periodico_KCRUD.Datos;

namespace Periodico_KCRUD.Vistas
{
    public partial class FormGestionNoticias : Form
    {
        ConexionBD bd = new ConexionBD();
        // VARIABLE GLOBAL para saber qué noticia estamos editando
        private int idSeleccionado = -1;

        public FormGestionNoticias()
        {
            InitializeComponent();
            CargarTabla();
        }

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

        // 1. EVENTO PARA CARGAR DATOS EN LOS TEXTBOX AL HACER CLIC
        // Ve al rayo amarillo del DataGridView y busca "CellClick"
        private void dgvNoticias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Guardamos el ID para usarlo luego en el Update o Delete
                idSeleccionado = Convert.ToInt32(dgvNoticias.CurrentRow.Cells["NoticiaID"].Value);

                // Pasamos el texto de la tabla a los cuadros de texto
                txtTitulo.Text = dgvNoticias.CurrentRow.Cells["Titulo"].Value.ToString();
                txtContenido.Text = dgvNoticias.CurrentRow.Cells["Contenido"].Value.ToString();
            }
        }

        // 2. MÉTODO ACTUALIZAR (El que te faltaba)
        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Por favor, selecciona una noticia de la tabla primero.");
                return;
            }

            try
            {
                bd.Abrir();
                // Usamos UPDATE para modificar la noticia que coincida con el ID
                string sql = "UPDATE Noticias SET Titulo = @t, Contenido = @c WHERE NoticiaID = @id";

                SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                cmd.Parameters.AddWithValue("@t", txtTitulo.Text.Trim());
                cmd.Parameters.AddWithValue("@c", txtContenido.Text.Trim());
                cmd.Parameters.AddWithValue("@id", idSeleccionado);

                int resultado = cmd.ExecuteNonQuery();
                bd.Cerrar();

                if (resultado > 0)
                {
                    MessageBox.Show("¡Noticia actualizada correctamente!");
                    LimpiarCampos();
                    CargarTabla();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al actualizar: " + ex.Message); }
        }

        // MÉTODO CREAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string titulo = txtTitulo.Text.Trim();
            string contenido = txtContenido.Text.Trim();

            if (titulo == "" || contenido == "")
            {
                MessageBox.Show("Debes escribir algo en el título y el contenido.");
                return;
            }

            try
            {
                bd.Abrir();
                string sql = "INSERT INTO Noticias (Titulo, Contenido, FechaPublicacion) VALUES (@t, @c, GETDATE())";
                SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                cmd.Parameters.AddWithValue("@t", titulo);
                cmd.Parameters.AddWithValue("@c", contenido);
                cmd.ExecuteNonQuery();
                bd.Cerrar();

                MessageBox.Show("¡Guardado correctamente!");
                LimpiarCampos();
                CargarTabla();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        // MÉTODO BORRAR (Actualizado para usar la variable idSeleccionado)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado != -1)
            {
                if (MessageBox.Show("¿Seguro que quieres borrarla?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bd.Abrir();
                    string sql = "DELETE FROM Noticias WHERE NoticiaID = @id";
                    SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);
                    cmd.ExecuteNonQuery();
                    bd.Cerrar();

                    MessageBox.Show("Noticia eliminada.");
                    LimpiarCampos();
                    CargarTabla();
                }
            }
            else { MessageBox.Show("Selecciona una noticia primero."); }
        }

        // Método extra para tener el código limpio
        private void LimpiarCampos()
        {
            txtTitulo.Clear();
            txtContenido.Clear();
            idSeleccionado = -1;
        }

       
    }
}