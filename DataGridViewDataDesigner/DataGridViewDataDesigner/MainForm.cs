using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewDataDesigner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aUTOLOTDataSet.Inventory". При необходимости она может быть перемещена или удалена.
            this.inventoryTableAdapter.Fill(this.aUTOLOTDataSet.Inventory);

        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            try
            {
                this.inventoryTableAdapter.Update(this.aUTOLOTDataSet.Inventory);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.inventoryTableAdapter.Fill(this.aUTOLOTDataSet.Inventory);
        }
    }
}
