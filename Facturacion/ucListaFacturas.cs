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
    public partial class ucListaFacturas : DevExpress.XtraEditors.XtraUserControl
    {
        private static ucListaFacturas _instance;

        public static ucListaFacturas Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucListaFacturas();
                return _instance;

            }
        }
        public ucListaFacturas()
        {
            InitializeComponent();
            ShowData();
        }

        private void ShowData()
        {
            FacturaEncDAL facturaDAL = new FacturaEncDAL();

            gridControl1.DataSource = facturaDAL.GetFacturaEnc();
        }
        
    }
}
