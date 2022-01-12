using MusicStore.IServices;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class AccountService : IAccountService
    {
        private readonly SqlConnection _connection = new SqlConnection(Properties.Resources.DBConnectionString);

        public void AddAccuount(Account user)
        {
                var encrypted = sha256_hash(user.Password);
                _connection.Open();
                string insertAccountQuery = "INSERT INTO Account VALUES('" + user.Name + "','" + user.Surname + "','" + user.Login + "','" + encrypted + "');";
                SqlCommand commandInsertBook = new SqlCommand(insertAccountQuery, _connection);
                commandInsertBook.ExecuteNonQuery();
                _connection.Close();

        }

        private static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }       
        
        public bool IsAccountInDB()
        {
            _connection.Open();
            string getNumberOfAccounts = "SELECT count (*) from Account;";
            SqlCommand commandCheckAccount = new SqlCommand(getNumberOfAccounts, _connection);
            int numberOfArtists = (int)commandCheckAccount.ExecuteScalar();
            _connection.Close();
            return numberOfArtists != 0 ? true : false;
        }

        public bool LogIn(Account account)
        {
            if(string.IsNullOrEmpty(account.Login) || string.IsNullOrEmpty(account.Password))
            {
                return false;
            }
            _connection.Open();
            string getNumberOfAccounts = "SELECT count (*) from Account where login ='" + account.Login + "' and password ='" + sha256_hash(account.Password) + "';"; 
            SqlCommand commandCheckAccount = new SqlCommand(getNumberOfAccounts, _connection);
            int numberOfArtists = (int)commandCheckAccount.ExecuteScalar();
            _connection.Close();
            return numberOfArtists != 0 ? true : false;
        }
    }
}
