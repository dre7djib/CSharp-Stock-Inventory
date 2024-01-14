using System;
using MySql.Data.MySqlClient;

namespace Api
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        public DatabaseConnection()
        {
            Console.WriteLine("Enter your username :");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password :");
            string password = Console.ReadLine();
            string connectionString = "server=localhost;port=3306;database=api_csharp;UID=" + username + ";PASSWORD=" + password;
            connection = new MySqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connexion à la base de données réussie.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur lors de la connexion à la base de données : " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            try
            {
                connection.Close();
                Console.WriteLine("Déconnexion de la base de données réussie.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur lors de la déconnexion de la base de données : " + ex.Message);
            }
        }
    }
}