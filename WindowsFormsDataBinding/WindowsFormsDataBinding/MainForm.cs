using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsDataBinding
{
    public partial class MainForm : Form
    {
        List<Car> listCars = null;
        DataTable inventoryTable = new DataTable();
        DataView yugosOnlyView;
        public MainForm()
        {
            InitializeComponent();
            listCars = new List<Car>
            {
                new Car { ID=100, PetName="Chucky", Make="BMW", Color="Green"},
                new Car { ID=101, PetName="Tiny",Make="Yugo",Color="White"},
                new Car { ID=102, PetName="Ami",Make="Jeep", Color="Tan"},
                new Car { ID=103, PetName="PAin Inducer", Make="Caravan", Color="Pink"},
                new Car {ID=104, PetName="Fred",Make="BMW", Color="Green" },
                new Car { ID=105, PetName="Sidd", Make="BMW", Color="Black"},
                new Car { ID=106, PetName="Mel", Make="Firebird", Color="Red"},
                new Car {  ID=107, PetName="Sarah", Make="Colt", Color="Black"},
            };
            CreateDataTable();
            CreateDataView();
        }

        private void CreateDataTable()
        {
            DataColumn carIDColumn = new DataColumn("ID", typeof(int));
            DataColumn carMakeColumn = new DataColumn("Make", typeof(string));
            DataColumn carColorColumn = new DataColumn("Color", typeof(string));
            DataColumn carPetNameColumn = new DataColumn("PetName", typeof(string));
            carPetNameColumn.Caption = "Pet Name";
            inventoryTable.Columns.AddRange(new DataColumn[] { carIDColumn, carMakeColumn, carColorColumn, carPetNameColumn });

            foreach(Car c in listCars)
            {
                DataRow newRow = inventoryTable.NewRow();
                newRow["ID"] = c.ID;
                newRow["Make"] = c.Make;
                newRow["Color"] = c.Color;
                newRow["PetName"] = c.PetName;
                inventoryTable.Rows.Add(newRow);
            }
            carInventoryGridView.DataSource = inventoryTable;
        }

        private void CreateDataView()
        {
            yugosOnlyView = new DataView(inventoryTable);
            //
            yugosOnlyView.RowFilter = "Make= 'Yugo'";
            dataGridColtsView.DataSource = yugosOnlyView;
        }


        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rowToDelete = inventoryTable.Select(string.Format("ID={0}", int.Parse(txtRowToRemove.Text)));
                //delete
                rowToDelete[0].Delete();
                inventoryTable.AcceptChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDisplayMakes_Click(object sender, EventArgs e)
        {
            //
            string filterStr = string.Format("Make= '{0}'", txtMakeToView.Text);
            DataRow[] makes = inventoryTable.Select(filterStr);
            if (makes.Length == 0)
                MessageBox.Show("Sorry, no cars...", "Selection error!");
            else
            {
                string strMake = "";
                for(int i=0;i<makes.Length;i++)
                {
                    strMake += makes[i]["PetName"] + "\n";
                }
                MessageBox.Show(strMake, string.Format("We have {0}s named:", txtMakeToView.Text));
            }
        }

        private void btnChangeMakes_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes==MessageBox.Show("Are you sure?? BMWs are much nicer that Yugos!", "Please Confirm!",MessageBoxButtons.YesNo))
            {
                string filterStr = "Make='BMW'";
                string strMake = string.Empty;
                DataRow[] makes = inventoryTable.Select(filterStr);
                for(int i=0;i<makes.Length;i++)
                {
                    makes[i]["Make"] = "Yugo";
                }
            }
        }

        private void btnReturnChange_Click(object sender, EventArgs e)
        {
            string filterStr = "Make='Yugo'";
            string strMake = string.Empty;
            DataRow[] makes = inventoryTable.Select(filterStr);
            for (int i = 0; i < makes.Length; i++)
            {
                makes[i]["Make"] = "BMW";
            }
        }
    }
}
