using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class UsuarioAnexo
    {
        public decimal Id { get; set; }
        public decimal IdUsuario { get; set; }
        public decimal IdAnexo { get; set; }
        public string Valor { get; set; }
        public DateTime Modificacion { get; set; }

    }
}
