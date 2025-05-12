using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Geolocalizacion.Models
{
    public class Empleado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int SedeId { get; set; }
        [ForeignKey("SedeId")]
        public Sede Sede { get; set; } // Relación con la sede
    }
}