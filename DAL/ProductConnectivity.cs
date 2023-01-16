namespace DAL;
using BOL;
using System.Data;
using MySql.Data.MySqlClient;

public class ProductConnectivity
{
    public static string conString = @"server=localhost;port=3306;user=root; password=root123;database=ECommerce";
    public static List<Product> GetAllProducts()
    {
        List<Product> prod = new List<Product>();
        string querry = "select * from Product";
        MySqlConnection con = new MySqlConnection();
        con.ConnectionString = conString;
        try
        {
            con.Open();
            DataSet db = new DataSet();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = querry;
            MySqlDataAdapter adt = new MySqlDataAdapter();
            adt.SelectCommand = cmd;
            adt.Fill(db);
            DataTable dtable = db.Tables[0];
            DataRowCollection rows = dtable.Rows;
            foreach (DataRow row in rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                int quantity = int.Parse(row["quantity"].ToString());
                int price = int.Parse(row["price"].ToString());
                Product p = new Product
                {
                    ID = id,
                    Name = name,
                    Quantity = quantity,
                    Price = price
                };
                prod.Add(p);


            }
        }
        catch (Exception e)
        {
            Console.WriteLine("error", e.Message);

        }
        finally
        {
            con.Close();
        }
        return prod;
    }
    public static void Insertion(Product prod){
        MySqlConnection con =new MySqlConnection(conString);
        try{
        con.Open();    
        string querry=$"insert into Product(id,name,quantity,price)values('{prod.ID}','{prod.Name}','{prod.Quantity}','{prod.Price}')";
        MySqlCommand cmd=new MySqlCommand(querry,con);
        cmd.ExecuteNonQuery();
        }
        catch(Exception e){
        Console.WriteLine("error-->"+e.Message);
        }
        finally{
            con.Close();
        }
    }

    public static void DeleteByID(int id){
        MySqlConnection con =new MySqlConnection(conString);
        string querry="delete from product where id="+id;
        try{
            con.Open();
            MySqlCommand cmd=new MySqlCommand(querry,con);
            cmd.ExecuteNonQuery();
        }
        catch(Exception e){
            Console.WriteLine("error-->"+e.Message);

        }
        finally{
            con.Close();
        }



    }

}
