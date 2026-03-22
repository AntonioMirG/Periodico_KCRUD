using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periodico_KCRUD.Datos
{
    using Microsoft.Data.SqlClient;

    public class ConexionBD
    {
        
        public string cadena = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PeriodicoDB;Integrated Security=True";
        public SqlConnection conectar = new SqlConnection();

        public ConexionBD()
        {
            conectar.ConnectionString = cadena;
        }

        public void Abrir() => conectar.Open();
        public void Cerrar() => conectar.Close();
    }
}
