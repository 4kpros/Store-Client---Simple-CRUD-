using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Store_Client.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientModel client = new ClientModel();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            client.name = Request.Form["name"];
            client.email = Request.Form["email"];
            client.phone = Request.Form["phone"];
            client.address = Request.Form["address"];

            if(client.name.Length <= 0 || client.email.Length <= 0 || client.phone.Length <= 0 || client.address.Length <= 0)
            {
                errorMessage = "Tous les champs doivent etre remplies !";
                return;
            }

            //Save data to the data base
            try
            {
                //Create the connection string
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=clientStore;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", client.name);
                        command.Parameters.AddWithValue("@email", client.email);
                        command.Parameters.AddWithValue("@phone", client.phone);
                        command.Parameters.AddWithValue("@address", client.address);

                        command.ExecuteNonQuery();
                    }
                }
                
            }catch(Exception ex)
            {
                errorMessage = ex.Message;
            }

            client.id = "";
            client.name = "";
            client.email = "";
            client.phone = "";
            client.address = "";
            successMessage = "Nouveau client ajoute !";

            Response.Redirect("/Clients/Index");
        }
    }
}
