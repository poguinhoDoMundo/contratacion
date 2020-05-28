using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class Carga
    {

        public decimal Id { get; set; }
        public decimal IdDocumento { get; set; }
        public string Path { get; set; }
        public DateTime Fecha { get; set; }
        public decimal IdEstado { get; set; }
        public decimal IdUsuario { get; set; }

    }
}
