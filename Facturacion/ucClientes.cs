using CapaDeNegocio;
using CapaDeEntidad;
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
    public partial class ucClientes : DevExpress.XtraEditors.XtraUserControl
    {
        private ClienteDAL clienteDAL= new ClienteDAL();
        private clsCliente cliente = new clsCliente();

        bool btnNuevoIsClicked = true;

        private static ucClientes _instance;
        public static ucClientes Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucClientes();
                return _instance;

            }
        }
        private void ShowData()
        {
            gdClientes.DataSource = clienteDAL.GetClientes();
        }

        private void fillData()
        {
            ConvenioDAL convenioDAL = new ConvenioDAL();
            DataTable convenio;

            convenio = convenioDAL.GetConvenio();

            cbTipoCliente.Properties.DataSource = convenio;

            cbTipoCliente.Properties.DisplayMember = "Convenio";
            cbTipoCliente.Properties.ValueMember = "IDConvenio";


            cbTipoCliente.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IDConvenio", "Cod Convenio"));
            cbTipoCliente.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Convenio", "Convenio"));
            cbTipoCliente.DataBindings.Add("EditValue", convenio, "IDConvenio", true, DataSourceUpdateMode.OnPropertyChanged);


        }

        private int selectedRow()
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            return (int)row.ItemArray[0];
        }
        public ucClientes()
        {
            InitializeComponent();
            ShowData();
            fillData();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);


            txtNombre.Text = row.ItemArray[1].ToString();
            txtApellido.Text = row.ItemArray[2].ToString();
            cbTipoCliente.Text = row.ItemArray[3].ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if(!btnNuevoIsClicked)
            {

                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.IDConvenio = int.Parse(cbTipoCliente.EditValue.ToString());

                if (clienteDAL.InsertCliente(cliente))
                    MessageBox.Show("Se guardó el cliente!");

            }
            else
            {
                cliente.IDCliente = selectedRow();
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                cliente.IDConvenio = int.Parse(cbTipoCliente.EditValue.ToString());

                if (clienteDAL.UpdateCliente(cliente))
                    MessageBox.Show("Se actualizó el cliente!");
            }
            ShowData();
            ResetNewButton();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
           
            if (clienteDAL.DeleteCliente(selectedRow()))
                MessageBox.Show("Se eliminó el cliente!");
            ShowData();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevoIsClicked)
            {
                btnNuevoIsClicked = false;
                gdClientes.Enabled = false;
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
            gdClientes.Enabled = true;
            btnNuevo.Text = "Nuevo";
            btnNuevo.ImageOptions.SvgImage = Properties.Resources.newproduct;
        }
    }
}
