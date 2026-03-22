using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Necesario para la conexión
using Periodico_KCRUD.Datos; // Asegúrate de que coincida con tu carpeta de conexión

namespace Periodico_KCRUD.Vistas
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            ConexionBD bd = new ConexionBD();

            try
            {
                bd.Abrir();
                string sql = "SELECT EsAdmin FROM Usuarios WHERE Username = @user AND Password = @pass";

                SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                cmd.Parameters.AddWithValue("@user", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@pass", txtClave.Text);

                object resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    // 1. Obtenemos si es admin (true/1 o false/0)
                    bool esAdmin = Convert.ToBoolean(resultado);

                    MessageBox.Show("¡Bienvenido al Periódico!");

                    // 2. LA CLAVE: Pasamos 'esAdmin' Y pasamos 'true' (porque ya se logueó)
                    // Esto es lo que apagará el Timer definitivamente
                    FormHome home = new FormHome(esAdmin, true);

                    home.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Intente de nuevo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message);
            }
            finally
            {
                bd.Cerrar();
            }
        }
    }
}