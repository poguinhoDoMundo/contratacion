using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class Usuario
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public decimal IdPersona { get; set; }
        public decimal IdOrganizacion { get; set; }
        public decimal Id { get; set; }
        public DateTime Modificacion { get; set; }
        public string Pass { get; set; }
    }
}
