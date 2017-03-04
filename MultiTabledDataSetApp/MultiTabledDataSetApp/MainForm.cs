using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace MultiTabledDataSetApp
{
    public partial class MainForm : Form
    {
        //form
        private DataSet autoLotDS = new DataSet("AUTOLOT");
        private SqlCommandBuilder sqlCBInventory;
        private SqlCommandBuilder sqlCBCustumers;
        private SqlCommandBuilder sqlCBOrders;
        //adaprers for tables
        private SqlDataAdapter invTableAdapter;
        private SqlDataAdapter custTableAdapter;
        private SqlDataAdapter ordersTableAdapter;
        //
        private string cnStr = string.Empty;
        public MainForm()
        {
            
            InitializeComponent();
            cnStr = ConfigurationManager.ConnectionStrings["AutoLotSqlProvider"].ConnectionString;
            //adapter
            invTableAdapter = new SqlDataAdapter("Select * from Inventory", cnStr);
            custTableAdapter = new SqlDataAdapter("Select * from Custumers", cnStr);
            ordersTableAdapter = new SqlDataAdapter("Select * from Orders", cnStr);
            //command
            sqlCBInventory = new SqlCommandBuilder(invTableAdapter);
            sqlCBOrders = new SqlCommandBuilder(ordersTableAdapter);
            sqlCBCustumers = new SqlCommandBuilder(custTableAdapter);
            //
            invTableAdapter.Fill(autoLotDS, "Inventory");
            custTableAdapter.Fill(autoLotDS, "Custumers");
            ordersTableAdapter.Fill(autoLotDS, "Orders");
            //relation
            BuildTableRelationship();
            //grid
            dataGridViewInventory.DataSource = autoLotDS.Tables["Inventory"];
            dataGridViewCustumers.DataSource = autoLotDS.Tables["Custumers"];
            dataGridViewOrders.DataSource = autoLotDS.Tables["Orders"];
        }
        private void BuildTableRelationship()
        {
            DataRelation dr = new DataRelation("CustumerOrder", autoLotDS.Tables["Custumers"].Columns["CustID"],
                autoLotDS.Tables["Orders"].Columns["CustID"]);
            autoLotDS.Relations.Add(dr);
            dr = new DataRelation("InventoryOrder", autoLotDS.Tables["Inventory"].Columns["CarID"],
                autoLotDS.Tables["Orders"].Columns["CarID"]);
            autoLotDS.Relations.Add(dr);
        }

        private void btnUpdateDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                invTableAdapter.Update(autoLotDS, "Inventory");
                custTableAdapter.Update(autoLotDS, "Custumers");
                ordersTableAdapter.Update(autoLotDS, "Orders");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetOrderInfo_Click(object sender, EventArgs e)
        {
            string strOrderInfo = string.Empty;
            DataRow[] drsCust = null;
            DataRow[] drsOrder = null;
            int custID = int.Parse(this.txtCustID.Text);
            //
            drsCust = autoLotDS.Tables["Custumers"].Select(string.Format("CustID={0}", custID));
            strOrderInfo += string.Format("Customer {0} :{1}  {2}\n", drsCust[0]["CustID"].ToString(), drsCust[0]["FirstName"].ToString(),
                drsCust[0]["LastName"].ToString());
            //
            drsOrder = drsCust[0].GetChildRows(autoLotDS.Relations["CustumerOrder"]);
            //
            foreach(DataRow order in drsOrder)
            {
                strOrderInfo += string.Format("------------\nOrder Number: {0}", order["OrderID"]);
                DataRow[] drsInv = order.GetParentRows(autoLotDS.Relations["InventoryOrder"]);

                DataRow car = drsInv[0];
                strOrderInfo += string.Format("Make: {0}\n", car["Make"]);
                strOrderInfo += string.Format("Color: {0}\n", car["Color"]);
                strOrderInfo += string.Format("Pet Name: {0}\n", car["PetName"]);
            }
            MessageBox.Show(strOrderInfo, "Order Details");
        }
    }
}
