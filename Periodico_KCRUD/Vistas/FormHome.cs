using System;
using System.Windows.Forms;

namespace Periodico_KCRUD.Vistas
{
    public partial class FormHome : Form
    {
        // Variable para saber si el que entró es admin o lector
        private bool esAdmin;
        private bool usuarioLogueado = false;

        // Modificamos el constructor para recibir el permiso
        public FormHome(bool permisoAdmin)
        {
            InitializeComponent();
            this.esAdmin = permisoAdmin;
            this.usuarioLogueado = true;

            // Configuración inicial según el rol
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            if (esAdmin)
            {
                this.Text = "Panel de Administración - Periódico";
                // Aquí podrías habilitar botones de edición
            }
            else
            {
                this.Text = "Portal de Noticias - Modo Lector";
                // Aquí podrías ocultar el botón de "Gestionar Noticias"
            }
        }

        private void timerCookies_Tick(object sender, EventArgs e)
        {
            // Si no estuviera logueado, saltaría el mensaje (según tu idea de proyecto)
            if (!usuarioLogueado)
            {
                MessageBox.Show("Aviso: Debes registrarte para eliminar la publicidad y acceder a contenido exclusivo.", "Configuración de Cookies");
            }
        }
    }
}