using System;
using System.Collections.Generic;

namespace Login_DAP.Models;

public partial class Dependencia
{
    public int IdDependencia { get; set; }
    public string NombreDependencia { get; set; } = null!;
    public int Estatus { get; set; }
    public  ICollection<Unidad> Unidades { get; set; } = new List<Unidad>();
}
