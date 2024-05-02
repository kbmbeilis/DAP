using Login_DAP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;

namespace Login_DAP.Controllers
{
    public class DependenciasController : Controller
    {
        private readonly DapContext _context;
        public DependenciasController(DapContext context) => _context = context;

        public IActionResult CreateView() 
        {
            return View("Views/Funcionarios/Dependencias/Create.cshtml");
        }
        public IActionResult Create(Dependencia dependencia)
        {
            var nuevoRegistro = new Dependencia();
            if (dependencia != null) 
            {
                nuevoRegistro.NombreDependencia = dependencia.NombreDependencia;
                nuevoRegistro.Estatus = dependencia.Estatus;
            }
            _context.Dependencias.Add(nuevoRegistro);
            _context.SaveChanges();
            return RedirectToAction("Dependencias","Funcionarios");
        }

        public IActionResult EditView(int id) 
        {
            var datos = _context.Dependencias.SingleOrDefault(f => f.IdDependencia == id);
            return View("Views/Funcionarios/Dependencias/Edit.cshtml",datos);

        }
        public IActionResult Edit(Dependencia dependencia) 
        {
            var ActualizarDependencia = _context.Dependencias.FirstOrDefault(e => e.IdDependencia == dependencia.IdDependencia);
            if (dependencia != null)
            {
                ActualizarDependencia.NombreDependencia = dependencia.NombreDependencia;
                ActualizarDependencia.Estatus = dependencia.Estatus;
            }
            _context.Update(ActualizarDependencia);
            _context.SaveChanges();
            var datos = _context.Dependencias.SingleOrDefault(f => f.IdDependencia == dependencia.IdDependencia);
            return View("Views/Funcionarios/Dependencias/Edit.cshtml", datos);
        }

        public IActionResult DeleteView(int id) 
        {
            var dependencia = _context.Dependencias.SingleOrDefault(f => f.IdDependencia == id);
            return View("Views/Funcionarios/Dependencias/Delete.cshtml", dependencia);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Dependencia dependencia)
        {
            var dependencia1 = _context.Dependencias.FirstOrDefault(e => e.IdDependencia == dependencia.IdDependencia);
            if (dependencia1 != null)
            {
                _context.Dependencias.Remove(dependencia1);
                _context.SaveChanges();
            }
            return RedirectToAction("Dependencias", "Funcionarios");
        }
    
    }
}

