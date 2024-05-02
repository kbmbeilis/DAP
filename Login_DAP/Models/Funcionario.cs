using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Login_DAP.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string NombreFuncionario { get; set; } = null!;

    public string FechaCargo { get; set; } = null!;

    public string PuestoOficial { get; set; } = null!;

    public string PuestoFuncional { get; set; } = null!;

    public int IdUnidad { get; set; } 

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public byte[] Foto { get; set; }
    [NotMapped]
    public IFormFile imagenFile { get; set; }
    public int Estatus { get; set; }

    public  Unidad Unidad { get; set; } = null!;
}
