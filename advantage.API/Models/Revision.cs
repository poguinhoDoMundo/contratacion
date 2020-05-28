using System;
using System.Collections.Generic;

namespace advantage.API.Models
{
    public partial class Revision
    {
        public decimal Id { get; set; }
        public decimal IdArchivo { get; set; }
        public decimal IdRevisor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal IdEstado { get; set; }

    }
}
