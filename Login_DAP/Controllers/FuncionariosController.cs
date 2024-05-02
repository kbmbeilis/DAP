using Login_DAP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;

namespace Login_DAP.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly DapContext _context;
        public FuncionariosController(DapContext context) => _context = context;

        public IActionResult Index()
        {
            var datos = _context.Funcionarios.Where(g => g.Estatus == 0 && g.IdFuncionario != 1)
                .Include(f => f.Unidad)
                .Include(f => f.Unidad.Dependencia)
                .ToList();

            foreach (var funcionario in datos)
            {
                var imageBase64 = Convert.ToBase64String(funcionario.Foto);
                var imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";
            }
            return View("Views/Funcionarios/Index.cshtml", datos);
        }

        public IActionResult Buscar(string busqueda)
        {
            var resultados = _context.Funcionarios
                .Where(e => e.NombreFuncionario.Contains(busqueda))
                .Include(f => f.Unidad)
                .Include(f => f.Unidad.Dependencia)
                .ToList();
            return View("Views/Funcionarios/Index.cshtml", resultados);
        }
        public IActionResult CatalogoIdUnidades()
        {
            List<Unidad> lista = (from d in _context.Unidades

                                  select new Unidad
                                  {
                                      IdUnidad = d.IdUnidad,
                                      NombreUnidad = d.NombreUnidad

                                  }).ToList();
            List<SelectListItem> items = lista.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.NombreUnidad.ToString(),
                    Value = d.IdUnidad.ToString(),
                    Selected = false
                };

            });
            ViewBag.IdUnidades = items;
            return View();
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

        public IActionResult GetUnidades(int IdDependencia)
        {
            List<Unidad> lista = (from d in _context.Unidades
                            .Where(p => p.IdDependencia == IdDependencia)
                                  select new Unidad
                                  {
                                      IdUnidad = d.IdUnidad,
                                      NombreUnidad = d.NombreUnidad

                                  }).ToList();
            return Json(lista);
        }

        public IActionResult CreateView()
        {
            CatalogoIdUnidades();
            CatalogoIdDependencias();
            return View("Views/Funcionarios/Create.cshtml");

        }
        [HttpPost]
        public IActionResult Create(Funcionario funcionario)
        {

            if (funcionario.imagenFile != null && funcionario.imagenFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    funcionario.imagenFile.CopyTo(ms);
                    var datosImagen = ms.ToArray();
                    var nuevoRgistro = new Funcionario();
                    nuevoRgistro.NombreFuncionario = funcionario.NombreFuncionario;
                    nuevoRgistro.FechaCargo = funcionario.FechaCargo;
                    nuevoRgistro.PuestoOficial = funcionario.PuestoOficial;
                    nuevoRgistro.PuestoFuncional = funcionario.PuestoFuncional;
                    nuevoRgistro.Email = funcionario.Email;
                    nuevoRgistro.Telefono = funcionario.Telefono;
                    nuevoRgistro.Foto = datosImagen;
                    _context.Funcionarios.Add(nuevoRgistro);
                    _context.SaveChanges();

                }
            }

            var datos = _context.Funcionarios.Where(g => g.Estatus == 0 && g.IdFuncionario != 1)
               .Include(f => f.Unidad)
               .Include(f => f.Unidad.Dependencia)
               .ToList();
            CatalogoIdUnidades();
            CatalogoIdDependencias();
            return View("Views/Funcionarios/Index.cshtml", datos);
        }
        public IActionResult EditView(int id)
        {
            var idFuncionario = _context.Funcionarios.Include(f => f.Unidad)
            .Include(f => f.Unidad.Dependencia)
            .SingleOrDefault(f => f.IdFuncionario == id);
            var imageBase64 = Convert.ToBase64String(idFuncionario.Foto);
            var imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";
            CatalogoIdUnidades();
            CatalogoIdDependencias();
            return View("Views/Funcionarios/Edit.cshtml", idFuncionario);
        }

        [BindProperty]

        public Funcionario f { get; set; }
        public IActionResult Edit(Funcionario funcionario) 
        {
            if (f.imagenFile != null && f.imagenFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    f.imagenFile.CopyTo(ms);
                    var datosImagen = ms.ToArray();
                    var ActualizarFuncionario = _context.Funcionarios.FirstOrDefault(e => e.IdFuncionario == funcionario.IdFuncionario);
                    if (funcionario != null)
                    {
                        ActualizarFuncionario.NombreFuncionario = funcionario.NombreFuncionario;
                        ActualizarFuncionario.FechaCargo = funcionario.FechaCargo;
                        ActualizarFuncionario.PuestoFuncional = funcionario.PuestoFuncional;
                        ActualizarFuncionario.IdUnidad = funcionario.IdUnidad;
                        ActualizarFuncionario.Email = funcionario.Email;
                        ActualizarFuncionario.Telefono = funcionario.Telefono;
                        ActualizarFuncionario.Foto = datosImagen;
                        ActualizarFuncionario.Estatus = funcionario.Estatus;
                    }
                    _context.Update(ActualizarFuncionario);
                    _context.SaveChanges();
                }
            }
            var funcionario1 = _context.Funcionarios.Include(f => f.Unidad)
            .Include(f => f.Unidad.Dependencia)
            .SingleOrDefault(f => f.IdFuncionario == funcionario.IdFuncionario);
            var imageBase64 = Convert.ToBase64String(funcionario1.Foto);
            var imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";
            CatalogoIdUnidades();
            CatalogoIdDependencias();
            return View("Views/Funcionarios/Edit.cshtml", funcionario1);
        }
   
        public IActionResult DeleteView(int id)
        {
            var funcionario = _context.Funcionarios.Include(f => f.Unidad)
            .Include(f => f.Unidad.Dependencia)
            .SingleOrDefault(f => f.IdFuncionario == id & f.Estatus == 0);
            return View("Views/Funcionarios/Delete.cshtml", funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Funcionario funcionario)
        {
            var funcionario1 = _context.Funcionarios.FirstOrDefault(e => e.IdFuncionario == funcionario.IdFuncionario);
            if (funcionario1 != null)
            {
                _context.Funcionarios.Remove(funcionario1);
                _context.SaveChangesAsync();
            }
            return RedirectToAction("Index","Funcionarios");
        }

        public IActionResult DetailsView(int id)
        {
            var idFuncionario = _context.Funcionarios.Include(f => f.Unidad)
               .Include(f => f.Unidad.Dependencia)
               .SingleOrDefault(f => f.IdFuncionario == id & f.Estatus == 0);
            return View("Views/Funcionarios/Details.cshtml", idFuncionario);
        }

        public IActionResult DetailsViewEdit() 
        {
            var datos = _context.Funcionarios.Where(g => g.Estatus == 0 && g.IdFuncionario != 1)
            .Include(f => f.Unidad)
            .Include(f => f.Unidad.Dependencia).FirstOrDefault();
            var imageBase64 = Convert.ToBase64String(datos.Foto);
            var imageDataUrl = $"data:image/jpeg;base64,{imageBase64}";
            CatalogoIdUnidades();
            CatalogoIdDependencias();
            return RedirectToAction("Index","Funcionarios");
        }

        public IActionResult Dependencias()
        {
            var datos = _context.Dependencias.ToList();
            return View("Views/Funcionarios/Dependencias/List.cshtml", datos);
        }

        public IActionResult Unidades()
        {
            var datos = _context.Unidades.ToList();
            return View("Views/Funcionarios/Unidades/List.cshtml", datos);
        }

        public IActionResult Guardar()
        {
            return RedirectToAction("Index", "Funcionarios");
        }

        public IActionResult Cancelar()
        {
            return RedirectToAction("Index", "Funcionarios");
        }

        public IActionResult Inicio()
        {
            return RedirectToAction("Index", "Funcionarios");
        }

        public IActionResult Funcionarios()
        {
            return RedirectToAction("Index", "Funcionarios");
        }
       
    }
}

