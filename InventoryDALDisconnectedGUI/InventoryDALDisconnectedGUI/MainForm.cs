using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoLotDisconnectedLayer;

namespace InventoryDALDisconnectedGUI
{
    public partial class MainForm : Form
    {
        InventoryDALDisLayer dal = null;
        public MainForm()
        {
            InitializeComponent();
            string cnStr = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=AUTOLOT;Integrated Security=True";
            dal = new InventoryDALDisLayer(cnStr);
            //grid
            inventoryGrid.DataSource = dal.GetAllInventory();
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            DataTable changedDT = (DataTable)inventoryGrid.DataSource;
            try
            {
                dal.UpdateInventory(changedDT);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
