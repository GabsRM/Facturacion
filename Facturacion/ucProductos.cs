using CapaDeEntidad;
using CapaDeNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace Facturacion
{
    public partial class ucProductos : DevExpress.XtraEditors.XtraUserControl
    {
        private ProductoDAL productosDAL = new ProductoDAL();
        private clsProducto producto= new clsProducto();

        private bool btnNuevoIsClicked = true;


        private void FillData()
        {
            ProveedorDAL proveedoresDAL = new ProveedorDAL();
            DataTable proveedores;

            proveedores = proveedoresDAL.GetProveedores();

            cbProveedores.Properties.DataSource = proveedores;

            cbProveedores.Properties.DisplayMember = "Nombre";
            cbProveedores.Properties.ValueMember = "IDProveedor";


            cbProveedores.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDProveedor", "Cod Proveedor"));
            cbProveedores.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Nombre", "Proveedor"));
            cbProveedores.DataBindings.Add("EditValue", proveedores, "IDProveedor", true, DataSourceUpdateMode.OnPropertyChanged);


            MarcaDAL marcaDAL = new MarcaDAL();
            DataTable marca;

            marca = marcaDAL.GetMarcas();

            cbMarca.Properties.DataSource = marca;

            cbMarca.Properties.DisplayMember = "Descripcion";
            cbMarca.Properties.ValueMember = "IDMarca";


            cbMarca.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDMarca", "Cod Marca"));
            cbMarca.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Marca"));
            cbMarca.DataBindings.Add("EditValue", marca, "IDMarca", true, DataSourceUpdateMode.OnPropertyChanged);

            GrupoDAL grupoDAL = new GrupoDAL();
            DataTable grupo;

            grupo = grupoDAL.GetGrupos();

            cbGrupo.Properties.DataSource = grupo;

            cbGrupo.Properties.DisplayMember = "Descripcion";
            cbGrupo.Properties.ValueMember = "IDGrupo";


            cbGrupo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDGrupo", "Cod Grupo"));
            cbGrupo.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Descripcion", "Grupo"));
            cbGrupo.DataBindings.Add("EditValue", grupo, "IDGrupo", true, DataSourceUpdateMode.OnPropertyChanged);


        }

        private static ucProductos _instance;
        public static ucProductos Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucProductos();
                return _instance;

            }
        }
        public ucProductos()
        {
            InitializeComponent();
            FillData();
            ShowData();
           

        }

        
        public void ShowData()
        {
            gdProductos.DataSource = null; 
            gdProductos.DataSource = productosDAL.GetProductos(); 
            gdProductos.RefreshDataSource();

        }
        private int selectedRow()
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            return (int)row.ItemArray[0];
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);


            txtNombre.Text = row.ItemArray[1].ToString();
            txtPrecioCompra.Text = row.ItemArray[4].ToString();
            txtPrecioVenta.Text = row.ItemArray[5].ToString();
            txtCosto.Text = row.ItemArray[6].ToString();
            txtCodBarra.Text = row.ItemArray[7].ToString();
            txtReferencia.Text = row.ItemArray[8].ToString();

            txtCantidad.Text = row.ItemArray[10].ToString();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(!btnNuevoIsClicked)
            {
                producto.Nombre = txtNombre.Text;
                producto.IDGrupo = int.Parse(cbGrupo.EditValue.ToString());
                producto.IDMarca = int.Parse(cbMarca.EditValue.ToString());
                producto.PrecioCompra = decimal.Parse(txtPrecioCompra.Text);
                producto.PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                producto.Costo = decimal.Parse(txtCosto.Text);
                producto.CodigoBarra = txtCodBarra.Text;
                producto.Referencia = txtReferencia.Text;
                producto.IDProveedor = int.Parse(cbProveedores.EditValue.ToString());
                producto.Existencias = decimal.Parse(txtCantidad.Text);
                if (productosDAL.InsertProducto(producto))
                    MessageBox.Show("Se guardó el producto!");
            }
            else
            {

              
                producto.IDProducto = selectedRow();
                producto.Nombre = txtNombre.Text;
                producto.IDGrupo = int.Parse(cbGrupo.EditValue.ToString());
                producto.IDMarca = int.Parse(cbMarca.EditValue.ToString());
                producto.PrecioCompra = decimal.Parse(txtPrecioCompra.Text);
                producto.PrecioVenta = decimal.Parse(txtPrecioVenta.Text);
                producto.Costo = decimal.Parse(txtCosto.Text);
                producto.CodigoBarra = txtCodBarra.Text;
                producto.Referencia = txtReferencia.Text;
                producto.IDProveedor = int.Parse(cbProveedores.EditValue.ToString());
                producto.Existencias = decimal.Parse(txtCantidad.Text);
                if (productosDAL.UpdateProducto(producto))
                    MessageBox.Show("Se actualizó el producto!");

            }
            ShowData();
            ResetNewButton();

        }

       

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            if (productosDAL.DeleteProducto(selectedRow()))
                MessageBox.Show("Se eliminó el Producto!");
            ShowData();
        }

        private void cbProveedores_Click(object sender, EventArgs e)
        {

        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {

            if (btnNuevoIsClicked)
            {
                btnNuevoIsClicked = false;
                gdProductos.Enabled = false;
                btnNuevo.Text = "Cancelar";
                btnNuevo.ImageOptions.SvgImage = Properties.Resources.cancel;
            }
            else
            {
                ResetNewButton();
            }

        }

        private void ResetNewButton()
        {
            btnNuevoIsClicked = true;
            gdProductos.Enabled = true;
            btnNuevo.Text = "Nuevo";
            btnNuevo.ImageOptions.SvgImage = Properties.Resources.newproduct;
        }
    }
}
