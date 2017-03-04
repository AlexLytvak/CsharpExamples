using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLotConnectedLayer
{
    public class InventoryDAL
    {
        private SqlConnection sqlCn = null;
        public void OpenConnection(string connectionString)
        {
            sqlCn = new SqlConnection();
            sqlCn.ConnectionString = connectionString;
            sqlCn.Open();
        }
        public void CloseConnection()
        {
            sqlCn.Close();
        }
        //public void InsertAuto(int id, string color, string make, string petName)
        //{
        //    string sql = string.Format("Insert into Inventory" + "(CarID, Make, Color, PetName) Values" + "('{0}','{1}','{2}','{3}')", id, make, color, petName);
        //    using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
        //    {
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public void InsertAuto(NewCar car)
        {
            string sql = string.Format("Insert Into Inventory" + "(CarID,Make,Color,PetName) Values" + "('{0}','{1}',{2}','{3}')", car.CarID, car.Make, car.Color, car.PetName);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }
        
        public void DeleteCar(int id)
        {
            string sql = string.Format("Delete from Inventory where CarID= '{0}'", id);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(SqlException ex)
                {
                    Exception error = new Exception("Sorry! That car is on order", ex);
                    throw error;
                }
            }
        }

        public void UpdateCarPetName(int id, string newPetName)
        {
            string sql = string.Format("Update Inventory Set PetName ='{0}' Where CarID='{1}'", newPetName, id);
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public List<NewCar> GetAllInventoryAsList()
        {
            List<NewCar> inv = new List<NewCar>();
            string sql = "Select * From Inventory";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    inv.Add(new NewCar
                    {
                        CarID = (int)dr["CarID"],
                        
                        Make = (string)dr["Make"],
                        Color = (string)dr["Color"],
                        PetName=(string)dr["PetName"]
                    });
                }
                dr.Close();
            }
            return inv;
        }

        public DataTable GetAllInventoryAsDataTable()
        {
            DataTable inv = new DataTable();
            string sql = "Select * From Inventory";
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                inv.Load(dr);
                dr.Close();
            }
            return inv;
        }
        

        public void InsertAuto(int id, string color, string make, string petName)
        {
            string sql = string.Format("Insert Into Inventory" + "(CarID,Make,Color,PetName) Values" + "(@CarID, @Make, @Color, @PetName)");
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlCn))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CarID";
                param.Value = id;
                param.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Make";
                param.Value = make;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@Color";
                param.Value = color;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);

                param = new SqlParameter();
                param.ParameterName = "@PetName";
                param.Value = petName;
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
            }
        }

        public string LookUpPetName(int carID)
        {
            string carPetName = string.Empty;
            using (SqlCommand cmd = new SqlCommand("GetPetName", this.sqlCn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //enter param
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@carID";
                param.SqlDbType = SqlDbType.Int;
                param.Value = carID;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                //
                param = new SqlParameter();
                param.ParameterName = "@petName";
                param.SqlDbType = SqlDbType.Char;
                param.Size = 10;
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                //
                cmd.ExecuteNonQuery();
                //
                carPetName = (string)cmd.Parameters["@petName"].Value;
            }
            return carPetName;
        }

        public void ProcessCreditRisk(bool throwEx, int custID)
        {
            string fName = string.Empty;
            string lName = string.Empty;
            SqlCommand cmdSelect = new SqlCommand(string.Format("Select * From Custumers where CustID={0}", custID), sqlCn);
            using (SqlDataReader dr = cmdSelect.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    dr.Read();
                    fName = (string)dr["FirstName"];
                    lName = (string)dr["LastName"];
                }
                else
                    return;
            }
            SqlCommand cmdRemove = new SqlCommand(string.Format("Delete From Custumers where CustID ={0}", custID), sqlCn);
            SqlCommand cmdInsert = new SqlCommand(string.Format("Insert Into CreditRisks" + "(CustID, FirstName, LastName) Values" + "({0},'{1}','{2}')", custID,fName, lName), sqlCn);
            SqlTransaction tx = null;
            try
            {
                tx = sqlCn.BeginTransaction();
                cmdInsert.Transaction = tx;
                cmdRemove.Transaction = tx;
                cmdInsert.ExecuteNonQuery();
                cmdRemove.ExecuteNonQuery();
                if(throwEx)
                {
                    throw new Exception("Sorry! Database error! Tx failed...");
                }
                tx.Commit();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                tx.Rollback();
            }
        }


    }
}
