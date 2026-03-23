using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows.Forms; 

namespace Periodico_KCRUD.Datos
{
    public class ConexionBD
    {
        public string cadena = @"Data Source=.\SQLEXPRESS;Initial Catalog=PeriodicoDB;Integrated Security=True;TrustServerCertificate=True";
        public SqlConnection conectar;

        public ConexionBD()
        {
            conectar = new SqlConnection(cadena);
        }

        public void Abrir()
        {
            try
            {
                if (conectar.State == ConnectionState.Closed)
                {
                    conectar.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión a la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}