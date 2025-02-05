using Microsoft.Data.SqlClient;
using MvcCoreLinqToSQL.Models;
using System.Data;

namespace MvcCoreLinqToSQL.Repositories
{
    public class RepositoryEnfermos
    {
        private DataTable tablaEnfermos;

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryEnfermos()
        {
            string connectionString = @"Data Source=PCANDREI\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Encrypt=True;Trust Server Certificate=True";

            string sql = "select * from ENFERMO";

            SqlDataAdapter adEmp = new SqlDataAdapter(sql, connectionString);

            this.tablaEnfermos = new DataTable();

            adEmp.Fill(this.tablaEnfermos);

            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Enfermo> GetEnfermos()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable() select datos;

            List<Enfermo> enfermos = new List<Enfermo>();

            foreach (var row in consulta)
            {
                Enfermo enfermo = new Enfermo();
                enfermo.Inscipcion = row.Field<string>("INSCRIPCION");
                enfermo.Apellido = row.Field<string>("APELLIDO");
                enfermo.Direccion = row.Field<string>("DIRECCION");
                enfermo.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
                enfermo.Genero = row.Field<string>("S");
                enfermo.NumeroSeguridadSocial = row.Field<string>("NSS");
                enfermos.Add(enfermo);
            }
            return enfermos;
        }

        public Enfermo FindEnfermo(string inscripccion)
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable() where datos.Field<string>("INSCRIPCION") == inscripccion select datos;

            var row = consulta.First();
            //poner condicional de null
            Enfermo enfermo = new Enfermo();

            enfermo.Inscipcion = row.Field<string>("INSCRIPCION");
            enfermo.Apellido = row.Field<string>("APELLIDO");
            enfermo.Direccion = row.Field<string>("DIRECCION");
            enfermo.FechaNacimiento = row.Field<DateTime>("FECHA_NAC");
            enfermo.Genero = row.Field<string>("S");
            enfermo.NumeroSeguridadSocial = row.Field<string>("NSS");

            return enfermo;
        }

        public void DeleteEnfermo(string inscripcion)
        {
            string sql = "delete from enfermo where INSCRIPCION=@inscripcion";

            this.com.Parameters.AddWithValue("@inscripcion", inscripcion);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;

            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }

    }
}
