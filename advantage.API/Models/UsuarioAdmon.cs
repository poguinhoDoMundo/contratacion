using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class UsuarioAdmon
    {
        public decimal Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Phone { get; set; }
    }
}
