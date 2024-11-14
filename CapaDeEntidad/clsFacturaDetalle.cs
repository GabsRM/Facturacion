using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    public class clsFacturaDetalle
    {
        public int IDFacDet { get; set; }
        public int IDFacEnc { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal PrecioCompra { get; set; }
    }
}
