
using Microsoft.Data.SqlClient;

string connectionString = "server=localhost\\sqlexpress;" +
                            "database=SalesDb;" +
                            "trusted_connection=true;" +
                            "trustServerCertificate=true;";
SqlConnection sqlConn = new SqlConnection(connectionString);

sqlConn.Open();
if(sqlConn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("I screwed up my connection string!");
}
Console.WriteLine("Connection opened successfully!");

///////////

string sql = "SELECT * from Customers where sales > 90000 order by sales desc;";

SqlCommand cmd = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var name = Convert.ToString(reader["Name"]);
    var city = Convert.ToString(reader["City"]);
    var state = Convert.ToString(reader["State"]);
    var sales = Convert.ToDecimal(reader["Sales"]);
    var active = Convert.ToBoolean(reader["Active"]);
    Console.WriteLine($"{id} {name} {city}, {state} {sales} {(active ? "Yes" : "No")}");

}
reader.Close();



///////////////

string sql = "SELECT * from Orders;";

SqlCommand cmdo = new SqlCommand(sql, sqlConn);

SqlDataReader reader = cmdo.ExecuteReader();

while (reader.Read())
{
    var id = Convert.ToInt32(reader["Id"]);
    var customerId = (reader["CustomerId"].Equals(System.DBNull.Value))
                                        ? (int?)null
                                        : Convert.ToInt32(reader["CustomerId"]);
    var date = Convert.ToDateTime(reader["Date"]);
    var desc = Convert.ToString(reader["Description"]);
    
    Console.WriteLine($"{id} | {customerId} {date} {desc}");
}
reader.Close();




//////////////



sqlConn.Close();
