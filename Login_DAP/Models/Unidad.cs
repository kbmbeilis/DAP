using System;
using System.Collections.Generic;

namespace Login_DAP.Models;

public partial class Unidad
{
    public int IdUnidad { get; set; }

    public string NombreUnidad { get; set; } = null!;

    public string EmailUnidad { get; set; } = null!;

    public string Telefonos { get; set; } = null!;

    public string Domicilio { get; set; } = null!;

    public string Colonia { get; set; } = null!;

    public int? CodigoPostal { get; set; } 

    public string Ciudad { get; set; } = null!;

    public string Entidad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public int Estatus { get; set; }

    public int IdDependencia { get; set; }

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();

    public virtual Dependencia Dependencia { get; set; } = null!;
}
