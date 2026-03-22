using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Periodico_KCRUD.Datos;
using Periodico_KCRUD.Vistas;

namespace Periodico_KCRUD.Vistas
{
    public partial class FormHome : Form
    {
        private bool esAdmin;
        private bool usuarioLogueado;

        public FormHome(bool permisoAdmin, bool estaLogueado = false)
        {
            InitializeComponent();

            this.esAdmin = permisoAdmin;
            this.usuarioLogueado = estaLogueado;

            // 1. ASIGNACIÓN DE TÍTULOS (Crucial para que el Timer sepa si debe morir)
            if (this.usuarioLogueado)
            {
                this.Text = esAdmin ? "Panel de Administración - K-CRUD" : "Bienvenido Lector - K-CRUD";
                timerCookies.Stop();
                timerCookies.Enabled = false;
            }
            else
            {
                this.Text = "K-CRUD NEWS - Portal de Noticias";
                timerCookies.Interval = 10000; // 10 segundos
                timerCookies.Start();
            }

            // 2. Control de interfaz
            if (administracionToolStripMenuItem != null)
                administracionToolStripMenuItem.Visible = esAdmin;

            this.BackColor = Color.White;
            CargarNoticiasEstiloCards();
        }

        private void CargarNoticiasEstiloCards()
        {
            try
            {
                if (panelNoticias != null)
                {
                    panelNoticias.Controls.Clear();
                    ConexionBD bd = new ConexionBD();
                    bd.Abrir();

                    string sql = "SELECT Titulo, Contenido, FechaPublicacion FROM Noticias ORDER BY FechaPublicacion DESC";
                    SqlCommand cmd = new SqlCommand(sql, bd.conectar);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CrearTarjetaNoticia(
                            reader["Titulo"].ToString(),
                            reader["Contenido"].ToString(),
                            Convert.ToDateTime(reader["FechaPublicacion"]).ToString("dd/MM/yyyy")
                        );
                    }
                    bd.Cerrar();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar noticias: " + ex.Message); }
        }



        private void CrearTarjetaNoticia(string titulo, string contenido, string fecha)
        {
            // 1. Contenedor de la tarjeta
            Panel card = new Panel
            {
                Size = new Size(panelNoticias.Width - 40, 160),
                BackColor = Color.White,
                Margin = new Padding(20, 10, 20, 10)
            };

            // 2. DIBUJAR BORDE (Versión simplificada e infalible)
            card.Paint += (s, e) => {
                Rectangle rect = card.ClientRectangle;
                // Dibujamos el borde usando 5 parámetros claros
                ControlPaint.DrawBorder(e.Graphics, rect, Color.LightGray, ButtonBorderStyle.Solid);
            };

            // 3. Etiquetas (Meta, Titular y Cuerpo)
            Label lblMeta = new Label
            {
                Text = "ACTUALIDAD | " + fecha,
                ForeColor = Color.SteelBlue,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(15, 10),
                AutoSize = true
            };

            Label lblTit = new Label
            {
                Text = titulo,
                Font = new Font("Georgia", 16, FontStyle.Bold),
                Location = new Point(15, 35),
                Size = new Size(card.Width - 30, 60)
            };

            Label lblCuerpo = new Label
            {
                Text = contenido.Length > 150 ? contenido.Substring(0, 150) + "..." : contenido,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.DimGray,
                Location = new Point(15, 100),
                Size = new Size(card.Width - 30, 45)
            };

            // 4. Montaje de la tarjeta
            card.Controls.Add(lblMeta);
            card.Controls.Add(lblTit);
            card.Controls.Add(lblCuerpo);
            panelNoticias.Controls.Add(card);
        }

        // --- EVENTOS CORREGIDOS ---

        private void gestionarNoticiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGestionNoticias ventana = new FormGestionNoticias();
            ventana.ShowDialog();
            CargarNoticiasEstiloCards();
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MATAMOS EL TIMER ANTES DE OCULTAR LA VENTANA (Evita el fantasma)
            timerCookies.Stop();
            timerCookies.Enabled = false;

            FormLogin login = new FormLogin();
            login.Show();
            this.Hide();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerCookies.Stop(); // IMPORTANTE
            FormHome volverAEmpezar = new FormHome(false, false); // Volver como visitante
            volverAEmpezar.Show();
            this.Dispose(); // Matar la sesión actual
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerCookies_Tick(object sender, EventArgs e)
        {
            // FILTRO DE SEGURIDAD: Si el título no es de visitante, el timer se suicida
            if (this.Text != "K-CRUD NEWS - Portal de Noticias")
            {
                timerCookies.Stop();
                timerCookies.Enabled = false;
                return;
            }

            timerCookies.Stop();
            MessageBox.Show("Regístrate para eliminar estos anuncios.", "Aviso");

            // Solo reiniciamos si sigue siendo visitante
            if (this.Text == "K-CRUD NEWS - Portal de Noticias")
                timerCookies.Start();
        }
    }
}