using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturacion
{
    public partial class frmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public frmMain()
        {
            InitializeComponent();
            ucFacturas.Instance.ProductosActualizados += ucProductos.Instance.ShowData;

        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(ucClientes.Instance))
            {
                container.Controls.Add(ucClientes.Instance);
                ucClientes.Instance.Dock = DockStyle.Fill;
                ucClientes.Instance.BringToFront();

            }
            ucClientes.Instance.BringToFront();
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(ucProveedores.Instance))
            {
                container.Controls.Add(ucProveedores.Instance);
                ucProveedores.Instance.Dock = DockStyle.Fill;
                ucProveedores.Instance.BringToFront();

            }
            ucProveedores.Instance.BringToFront();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            
            if (!container.Controls.Contains(ucProductos.Instance))
            {
                container.Controls.Add(ucProductos.Instance);
                ucProductos.Instance.Dock = DockStyle.Fill;
                ucProductos.Instance.BringToFront();

            }
            ucProductos.Instance.BringToFront();
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(ucFacturas.Instance))
            {
                container.Controls.Add(ucFacturas.Instance);
                ucFacturas.Instance.Dock = DockStyle.Fill;
                ucFacturas.Instance.BringToFront();

            }
            ucFacturas.Instance.BringToFront();
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (!container.Controls.Contains(ucListaFacturas.Instance))
            {
                container.Controls.Add(ucListaFacturas.Instance);
                ucListaFacturas.Instance.Dock = DockStyle.Fill;
                ucListaFacturas.Instance.BringToFront();

            }
            ucListaFacturas.Instance.BringToFront();
        }

        private void accordionControlElement6_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}
