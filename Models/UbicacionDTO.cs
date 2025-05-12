using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba_Geolocalizacion.Models
{
    public class UbicacionDTO
    {
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public int EmpleadoId { get; set; }
    }
}