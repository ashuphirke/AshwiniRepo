using ContactManagementSystem;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ContactManagementSystem
{
    public class SqlContactRepository : IContactRepository
    {
        // private readonly string _connectionString;
        private string _connectionString;
        //"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDatabase;Integrated Security=True;Connect Timeout=30;";
        // private readonly AppContext _context;

        public SqlContactRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public int Add(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Contacts (FirstName, LastName, Mobile, Email) VALUES (@FirstName, @LastName, @Mobile, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@Mobile", contact.Mobile);
                cmd.Parameters.AddWithValue("@Email", contact.Email);

                conn.Open();
               cmd.ExecuteNonQuery();
                return 1;
            }
        }
        public Contact Get(string mobile)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts WHERE Mobile = @Mobile";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Mobile", mobile);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Contact
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            CountryCode= reader["CountryCode"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Contacts";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contacts.Add(new Contact
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Mobile = reader["Mobile"].ToString(),
                            CountryCode = reader["CountryCode"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return contacts;
        }
        public int Update(Contact contact)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName,CountryCode=@CountryCode, Email = @Email WHERE Mobile = @Mobile";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
                cmd.Parameters.AddWithValue("@LastName", contact.LastName);
                cmd.Parameters.AddWithValue("@CountryCode", contact.CountryCode);
                cmd.Parameters.AddWithValue("@Email", contact.Email);
                cmd.Parameters.AddWithValue("@Mobile", contact.Mobile);

                conn.Open();
                cmd.ExecuteNonQuery();
                return 1;
            }
        }

        public int Delete(string mobile)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Contacts WHERE Mobile = @Mobile";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Mobile", mobile);

                conn.Open();
                cmd.ExecuteNonQuery();
                return 1;
            }
        }
    }
}

