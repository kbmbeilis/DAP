using Login_DAP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;

namespace Login_DAP.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly DapContext _context;
        public UnidadesController(DapContext context)
        {
            _context = context;
        }

        public async Task<List<Unidad>> GetAllAsync()
        {
            var unidades = _context.Unidades
                .Where(f => f.Estatus == 0 && f.IdUnidad != 1)
                .Include(f => f.Dependencia);


            return unidades.ToList();
        }

        public IActionResult CatalogoIdDependencias()
        {
            List<Dependencia> lista = (from d in _context.Dependencias
                                       select new Dependencia
                                       {
                                           IdDependencia = d.IdDependencia,
                                           NombreDependencia = d.NombreDependencia

                                       }).ToList();
            List<SelectListItem> items = lista.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreDependencia.ToString(),
                    Value = d.IdDependencia.ToString(),
                    Selected = false
                };

            });
            ViewBag.IdDependencias = items;
            return View();
        }

        public IActionResult CreateView()
        {
            CatalogoIdDependencias();
            return View("Views/Funcionarios/Unidades/Create.cshtml");
        }

        public IActionResult Create(Unidad unidad)
        {
            var nuevoRegistro = new Unidad();
            if (unidad != null)
            {
                nuevoRegistro.NombreUnidad = unidad.NombreUnidad;
                nuevoRegistro.EmailUnidad = unidad.EmailUnidad;
                nuevoRegistro.Telefonos = unidad.Telefonos;
                nuevoRegistro.Domicilio = unidad.Domicilio;
                nuevoRegistro.Colonia = unidad.Colonia;
                nuevoRegistro.CodigoPostal = unidad.CodigoPostal;
                nuevoRegistro.Ciudad = unidad.Ciudad;
                nuevoRegistro.Entidad = unidad.Entidad;
                nuevoRegistro.Pais = unidad.Pais;
                nuevoRegistro.Estatus = unidad.Estatus;
                nuevoRegistro.IdDependencia = unidad.IdDependencia;
            }
            _context.Unidades.Add(nuevoRegistro);
            _context.SaveChanges();
            return RedirectToAction("Unidades", "Funcionarios");
        }

        public IActionResult EditView(int id)
        {
            CatalogoIdDependencias();
            var idUnidad = _context.Unidades
           .SingleOrDefault(f => f.IdUnidad == id);
            return View("Views/Funcionarios/Unidades/Edit.cshtml", idUnidad);
        }

        public IActionResult Edit(Unidad unidad)
        {

            var ActualizarUnidad = _context.Unidades.FirstOrDefault(e => e.IdUnidad == unidad.IdUnidad);
            if (ActualizarUnidad != null)
            {
                ActualizarUnidad.NombreUnidad = unidad.NombreUnidad;
                ActualizarUnidad.EmailUnidad = unidad.EmailUnidad;
                ActualizarUnidad.Telefonos = unidad.Telefonos;
                ActualizarUnidad.Domicilio = unidad.Domicilio;
                ActualizarUnidad.Colonia = unidad.Colonia;
                ActualizarUnidad.CodigoPostal = unidad.CodigoPostal;
                ActualizarUnidad.Ciudad = unidad.Ciudad;
                ActualizarUnidad.Entidad = unidad.Entidad;
                ActualizarUnidad.Pais = unidad.Pais;
                ActualizarUnidad.Estatus = unidad.Estatus;
                ActualizarUnidad.IdDependencia = unidad.IdDependencia;
            }
            _context.Update(ActualizarUnidad);
            _context.SaveChanges();
            CatalogoIdDependencias();
            var idUnidad = _context.Unidades
            .SingleOrDefault(f => f.IdUnidad == unidad.IdUnidad);
            return View("Views/Funcionarios/Unidades/Edit.cshtml");
        }

        public IActionResult DeleteView(int id)
        {
            var unidad = _context.Unidades.SingleOrDefault(f => f.IdUnidad == id);
            return View("Views/Funcionarios/Unidades/Delete.cshtml", unidad);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Unidad unidad)
        {
            var unidad1 = _context.Unidades.FirstOrDefault(e => e.IdUnidad == unidad.IdUnidad);
            if (unidad1 != null)
            {
                _context.Unidades.Remove(unidad1);
                _context.SaveChanges();
            }
            return RedirectToAction("Unidades", "Funcionarios");
        }

    }
}
