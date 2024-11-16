using CapaDeNegocio;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
        private int SelectedRow()
        {
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            return (int)row.ItemArray[0];
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Report1 reporte = new Report1();

            reporte.Parameters["IDFacturaEnc"].Value = SelectedRow();
            reporte.Parameters["IDFacturaEnc"].Visible = false;
            

            ReportPrintTool printTool = new ReportPrintTool(reporte);
            printTool.ShowPreview();
        }
    }
}
