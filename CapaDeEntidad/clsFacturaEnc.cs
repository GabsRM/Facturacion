using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
   public class clsFacturaEnc
    {
        public int IDFacEnc { get; set; }
        public DateTime Fecha { get; set; }
        public int IDCliente { get; set; }
        public decimal Subtotal { get; set; }
        public int IDDescuento { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public int IDEstado { get; set; }
        public int IDSerie { get; set; }
        public int IDMoneda { get; set; }
        public int IDTipoFac { get; set; }
        public string NoFactura { get; set; }
        public string Observaciones { get; set; }

    }
}
