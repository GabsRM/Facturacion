﻿using CapaDeEntidad;
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
    public partial class ucProveedores : DevExpress.XtraEditors.XtraUserControl
    {
        private ProveedorDAL proveedoresDAL = new ProveedorDAL();
        private clsProveedor proveedor = new clsProveedor();


        private static ucProveedores _instance;
        public static ucProveedores Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucProveedores();
                return _instance;

            }
        }

        private void ShowData()
        {
            gridControl1.DataSource = proveedoresDAL.GetProveedores();
        }

        private int selectedRow()
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            return (int)row.ItemArray[0];
        }

        public ucProveedores()
        {
            InitializeComponent();
            ShowData();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            proveedor.Nombre = txtNombre.Text;
            proveedor.Direccion = txtDireccion.Text;
            proveedor.Telefono = txtTelefono.Text;

            if (proveedoresDAL.InsertProveedor(proveedor))
                MessageBox.Show("Se guardó el proveedor!");
            ShowData();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            proveedor.IDProveedor = selectedRow();
            proveedor.Nombre = txtNombre.Text;
            proveedor.Direccion = txtDireccion.Text;
            proveedor.Telefono = txtTelefono.Text;

            if (proveedoresDAL.UpdateProveedor(proveedor))
                MessageBox.Show("Se actualizó el proveedor!");
            ShowData();

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);


            txtNombre.Text = row.ItemArray[1].ToString();
            txtDireccion.Text = row.ItemArray[2].ToString();
            txtTelefono.Text = row.ItemArray[3].ToString();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (proveedoresDAL.DeleteProveedor(selectedRow()))
                MessageBox.Show("Se eliminó el proveedor!");
            ShowData();
        }
    }
}