using CapaDeEntidad;
using CapaDeNegocio;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Facturacion
{
    public partial class ucFacturas : DevExpress.XtraEditors.XtraUserControl
    {
        /*clsNegocio negocio = new clsNegocio();*/
        private static ucFacturas _instance;

        FacturaEncDAL facturaEnDAL = new FacturaEncDAL();
        ProductoDAL productosDAL = new ProductoDAL();
        clsFacturaEnc factura = new clsFacturaEnc();
        //DataTable productos;

        NoSerieDAL noSerieDAL = new NoSerieDAL();
        DataTable noSerie;

        EstadoDAL estadoDAL = new EstadoDAL();
        DataTable estado;

        MonedaDAL monedaDAL = new MonedaDAL();
        DataTable moneda;

        public static ucFacturas Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ucFacturas();

                }
                return _instance;

            }
        }




        private void FillData()
        {

            txtFactura.Text = facturaEnDAL.GetLastFacturaEncID().ToString();

           

            noSerie = noSerieDAL.GetNoSerie("GetLastNSerie");

            txtSerie.Text = noSerie.Rows[0]["NumeroSerie"].ToString();

            estado = estadoDAL.GetEstados();

            txtEstado.Text = estado.Rows[0]["Estado"].ToString();

            TipoFacDAL tipoFaDAL = new TipoFacDAL();
            DataTable tipoFac;
            tipoFac = tipoFaDAL.GetTiposFactura();

            cbTipo.Properties.DataSource = tipoFac;

            cbTipo.Properties.DisplayMember = "TipoFactura";
            cbTipo.Properties.ValueMember = "IDTipoFac";

            cbTipo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TipoFactura", "Tipo"));
            cbTipo.DataBindings.Add("EditValue", tipoFac, "IDTipoFac", true, DataSourceUpdateMode.OnPropertyChanged);

            ClienteDAL clientesDAL = new ClienteDAL();
            DataTable clientes;
            clientes = clientesDAL.GetClientes();

            cbCliente.Properties.DataSource = clientes;

            cbCliente.Properties.DisplayMember = "Nombre";
            cbCliente.Properties.ValueMember = "IDCliente";

            cbCliente.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDCliente", "Cod Cliente"));
            cbCliente.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Nombre"));
            cbCliente.DataBindings.Add("EditValue", clientes, "IDCliente", true, DataSourceUpdateMode.OnPropertyChanged);

            DescuentoDAL descuentoDAL = new DescuentoDAL();
            DataTable descuentos;
            descuentos = descuentoDAL.GetDescuento();

            cbDescuento.Properties.DataSource = descuentos;

            cbDescuento.Properties.DisplayMember = "Descuento";
            cbDescuento.Properties.ValueMember = "IDDescuento";

            cbDescuento.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDDescuento", "Cod Descuento"));
            cbDescuento.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descuento", "Descuento"));
            cbDescuento.DataBindings.Add("EditValue", descuentos, "IDDescuento", true, DataSourceUpdateMode.OnPropertyChanged);

            
            moneda = monedaDAL.GetMoneda();

            cbMoneda.Properties.DataSource = moneda;

            cbMoneda.Properties.DisplayMember = "Moneda";
            cbMoneda.Properties.ValueMember = "IDMoneda";

            cbMoneda.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Moneda", "Moneda"));

            cbMoneda.DataBindings.Add("EditValue", moneda, "IDMoneda", true, DataSourceUpdateMode.OnPropertyChanged);

        }

        public ucFacturas()
        {
            InitializeComponent();
            FillData();

        }

        private void cbCliente_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cbCliente_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (gdProductos.DataSource == null)
            {
                MessageBox.Show("Debe de agregar un producto");
                return;
            }

            //Factura
            List<clsFacturaDetalle> detallesFactura = new List<clsFacturaDetalle>();
            FacturaDetalleDAL facturaDetalleDAL = new FacturaDetalleDAL();

            FacturaEncDAL facturaDAL = new FacturaEncDAL();


            DataTable gridData = gdProductos.DataSource as DataTable;

            factura.Fecha = DateTime.Now;
            factura.IDCliente = int.Parse(cbCliente.EditValue.ToString());
            factura.Subtotal = gridData.AsEnumerable().Sum(row => row.Field<decimal>("Sub Total"));
            factura.IDDescuento = checkDescuento.Checked ? int.Parse(cbDescuento.EditValue.ToString()) : 0;
            factura.IVA = decimal.Parse(txtIVA.Text);
            factura.Total = (factura.Subtotal - ((factura.IDDescuento / 100) * factura.Subtotal)) + factura.IVA;
            factura.IDEstado = int.Parse(estado.Rows[0]["IDEstado"].ToString());
            factura.IDSerie = int.Parse(noSerie.Rows[0]["IDSerie"].ToString());
            factura.IDMoneda = int.Parse(cbMoneda.EditValue.ToString());
            factura.IDTipoFac = int.Parse(cbTipo.EditValue.ToString());
            factura.NoFactura = facturaDAL.GetLastFacturaEncID() + 1.ToString();
            factura.Observaciones = memoObservaciones.Text;

            facturaDAL.InsertFacturaEnc(factura);

            int ultimaFac = facturaDAL.ObtenerUltimaFacturaPorCliente(factura.IDCliente);
            

            //Detalle factura
            // Recorrer cada fila del gridData para extraer el IdProducto y otros detalles
            if (gridData != null)
            {
                foreach (DataRow row in gridData.Rows)
                {
                    // Crear una instancia de clsDetalleFactura
                    clsFacturaDetalle detalleFactura = new clsFacturaDetalle
                    {
                        IDFacEnc = ultimaFac, // Asigna el IdFactura obtenido previamente
                        IDProducto = Convert.ToInt32(row["IDProducto"]),
                        Cantidad = Convert.ToInt32(row["Cantidad"]), // Asigna la cantidad si está en el grid
                        PrecioUnitario = Convert.ToDecimal(row["PrecioVenta"]), // Asigna el precio si está en el grid
                        Subtotal = Convert.ToDecimal(row["Sub Total"]),
                        Descuento = factura.IDDescuento,
                        IVA = Convert.ToDecimal(row["IVA"]),
                        Total = (Convert.ToDecimal(row["Sub Total"]) - ((factura.IDDescuento / 100) * Convert.ToDecimal(row["Sub Total"])) + Convert.ToDecimal(row["IVA"]))
                    };
                    // Agregar el detalle de la factura a la lista
                    //detallesFactura.Add(detalleFactura);
                    facturaDetalleDAL.InsertFacturaDetalle(detalleFactura);
                }
            }

            MessageBox.Show("Se guardó la factura");

            

         

        }

        

        private decimal CalcularIVA(decimal precioVenta)
        {
                return (decimal.Parse(0.15.ToString()) * precioVenta);
        }
        private void CalcularTotal()
        {
            decimal descuento = (decimal.Parse(cbDescuento.EditValue.ToString()) / 100) * decimal.Parse(txtSubtotal.Text);
            decimal IVA = decimal.Parse(txtIVA.Text);

            txtTotal.Text = (decimal.Parse(txtSubtotal.Text) - descuento + IVA).ToString(); ;
        }

        private void AgregarProducto()
        {
            if (txtProducto.Text != null)
            {
                
                int idProducto = Convert.ToInt32(txtProducto.Text);

                // Obtener el DataTable actual del GridControl
                DataTable newProduct = productosDAL.GetProductoInfoById(idProducto);
                DataTable gridData = gdProductos.DataSource as DataTable;


                // Si el grid está vacío, inicializamos el DataSource con el nuevo DataTable
                if (gridData == null)
                {
                    // Clonar la estructura de newProduct para agregar columnas adicionales
                    gridData = newProduct.Clone();
                    gridData.Columns.Add("Cantidad", typeof(int));
                    gridData.Columns.Add("Sub Total", typeof(decimal));
                    gridData.Columns.Add("IVA", typeof(decimal));



                    // Agregar las filas del nuevo producto con el valor de txtCantidad y el precio seleccionado
                    foreach (DataRow row in newProduct.Rows)
                    {
                        DataRow newRow = gridData.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[]; // Copiar datos de la fila original
                        newRow["Cantidad"] = int.Parse(txtCantidad.Text); // Asigna el valor de txtCantidad
                        newRow["Sub Total"] = Convert.ToDecimal(newProduct.Rows[0]["PrecioVenta"]) * decimal.Parse(txtCantidad.Text); // Asigna el precio seleccionado
                        newRow["IVA"] = CalcularIVA(decimal.Parse(newProduct.Rows[0]["PrecioVenta"].ToString()));

                        gridData.Rows.Add(newRow);
                    }

                    gdProductos.DataSource = gridData;
                }
                else
                {
                    // Si el gridData ya existe, verificar si las columnas "Cantidad" y "Precio" existen; si no, agregarlas
                    if (!gridData.Columns.Contains("Cantidad"))
                    {
                        gridData.Columns.Add("Cantidad", typeof(int));
                    }
                    if (!gridData.Columns.Contains("Sub Total"))
                    {
                        gridData.Columns.Add("Sub Total", typeof(decimal));
                    }
                    if (!gridData.Columns.Contains("IVA"))
                    {
                        gridData.Columns.Add("IVA", typeof(decimal));
                    }
                    // Agregar cada fila del nuevo producto al DataTable actual con el valor de txtCantidad y el precio seleccionado
                    foreach (DataRow row in newProduct.Rows)
                    {
                        DataRow newRow = gridData.NewRow();
                        newRow.ItemArray = row.ItemArray.Clone() as object[]; // Copiar datos de la fila original
                        newRow["Cantidad"] = int.Parse(txtCantidad.Text); // Asigna el valor de txtCantidad
                        newRow["Sub Total"] = Convert.ToDecimal(newProduct.Rows[0]["PrecioVenta"]) * decimal.Parse(txtCantidad.Text); // Asigna el precio seleccionado
                        newRow["IVA"] = CalcularIVA(decimal.Parse(newProduct.Rows[0]["PrecioVenta"].ToString()));

                        gridData.Rows.Add(newRow);
                    }

                    // Actualizar el DataSource del grid con el DataTable actualizado
                    gdProductos.DataSource = gridData;
                }
                txtIVA.Text = gridData.AsEnumerable().Sum(row => row.Field<decimal>("IVA")).ToString();
                txtSubtotal.Text = gridData.AsEnumerable().Sum(row => row.Field<decimal>("Sub Total")).ToString();
                CalcularTotal();

            }
            else
            {
                MessageBox.Show("No se seleccionó un producto válido.");
            }
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {

            AgregarProducto();



        }

        private void txtCantidad_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void AgregarDetalle(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Llamar a Agregar usando BeginInvoke para que se ejecute después del evento KeyDown
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AgregarProducto();
                });

                // Opcionalmente, marcar el evento como manejado para evitar comportamientos adicionales
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void txtProducto_KeyDown(object sender, KeyEventArgs e)
        {
            AgregarDetalle(e);
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            AgregarDetalle(e);
        }



        

        private void checkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDescuento.Checked == true)
            {
                cbDescuento.Enabled = true;
               
            }
            else
                cbDescuento.Enabled = false;
        }

        private void cbDescuento_Properties_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {

        }

        private void cbDescuento_Properties_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cbDescuento_EditValueChanged(object sender, EventArgs e)
        {
            if (txtSubtotal.Text != "" && txtIVA.Text != "" && checkDescuento.Checked != false)
            {

                CalcularTotal();
                
            }

        }
    }
}
