using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqToSQL.Models;
using MvcCoreLinqToSQL.Repositories;

namespace MvcCoreLinqToSQL.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryEmpleados repo;

        public EmpleadosController()
        {
            this.repo = new RepositoryEmpleados();
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult Details(int idempleado)
        {
            Empleado emp = this.repo.FindEmpleado(idempleado);
            return View(emp);
        }

        public IActionResult BuscadorEmpleados()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BuscadorEmpleados(string oficio, int salario)
        {
            List<Empleado> empleados = this.repo.GetEmpleadosOficioSalario(oficio, salario);

            if (empleados == null)
            {
                ViewData["MENSAJE"] = "No existen empleados con estas condiciones";
                return View();
            }
            else
            {
                return View(empleados);
            }

        }

        public IActionResult EmpleadosOficio()
        {
            ViewData["oficios"] = this.repo.GetOficios();
            
            return View();
        }

        [HttpPost]
        public IActionResult EmpleadosOficio(string oficio)
        {
            ResumenEmpleados resumen = this.repo.GetEmpleadosOficio(oficio);
            ViewData["oficios"] = this.repo.GetOficios();

            return View(resumen);
        }
    }
}
