namespace MvcCoreLinqToSQL.Models
{
    public class ResumenEmpleados
    {
        public int Personas { get; set; }
        public int MaximoSalario { get; set; }
        public double MediaSalario { get; set; }
        public List<Empleado> Empleados { get; set; }
    }
}
