using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store_Client.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientModel> clients = new List<ClientModel>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=clientStore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using(SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientModel client = new ClientModel();
                                client.id = "" + reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.phone = reader.GetString(3);
                                client.address = reader.GetString(4);
                                client.created_at = reader.GetDateTime(5).ToString();

                                clients.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class ClientModel
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;

        public ClientModel()
        {
            id = "";
            name = "";
            email = "";
            phone = "";
            address = "";
            created_at = "";
        }
    }
}
