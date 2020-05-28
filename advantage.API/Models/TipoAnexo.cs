using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class TipoAnexo
    {

        public decimal Id { get; set; }
        public decimal IdOrganizacion { get; set; }
        public decimal IdEntidad { get; set; }
        public string Nombre { get; set; }
        public bool Obligatorio { get; set; }

    }
}
