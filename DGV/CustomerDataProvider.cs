using DGV.AdventureWorks2012CustomerDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGV
{
    public class CustomerDataProvider
    {
        private CustomerTableAdapter adapter;

        private AdventureWorks2012CustomerDataSet dataset;

        private List<string> StoreIds = new List<string>();

        public CustomerDataProvider()
        {
            dataset = new AdventureWorks2012CustomerDataSet();
            adapter = new CustomerTableAdapter();
            adapter.Fill(dataset.Customer);
            SqlCommand sqlcom = new SqlCommand("Select DISTINCT StoreID from AdventureWorks2012.Sales.Customer Order By StoreID", adapter.Connection);
            adapter.Connection.Open();
            SqlDataReader dr =  sqlcom.ExecuteReader();//dataset.Customer.CreateDataReader();
            
            while (dr.Read())
            {                
                string item = dr.GetValue(0).ToString();
                StoreIds.Add(item);                
            }

            adapter.Connection.Close();
        }

        public List<string> getStoreIds()
        {
            return StoreIds;
        }
        public DataView GetCustomers()
        {
            return dataset.Customer.DefaultView;
        }

        public DataView GetCustomersForStore(string storeId)
        {

            SqlCommand sqlcom = new SqlCommand("Select * from AdventureWorks2012.Sales.Customer Where StoreId = " + storeId + " Order By CustomerID", adapter.Connection);
            SqlDataAdapter da = new SqlDataAdapter(sqlcom);
            DataSet ds = new DataSet();

            adapter.Connection.Open();
            da.Fill(ds);
            adapter.Connection.Close();
            return ds.Tables[0].DefaultView;
        }
    }
}
