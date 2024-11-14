using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDeEntidad
{
    public class clsProducto
    {

        public int IDProducto { get; set; }
        public string Nombre { get; set; }
        public int IDGrupo { get; set; }
        public int IDMarca { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Costo { get; set; }
        public string CodigoBarra { get; set; }
        public string Referencia { get; set; }
        public int IDProveedor { get; set; }
        public decimal Existencias { get; set; }

    }
}
