using MusicStore.IServices;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class GenreService : IGenreService
    {
        private readonly SqlConnection _connection = new SqlConnection(Properties.Resources.DBConnectionString);

        public ICollection<Genre> GetGenres()
        {
            var genres = new List<Genre>();
            string query = "SELECT Id, Name from Genre;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            setDataToGenre(table, genres);
            return genres;
        }
        private void setDataToGenre(DataTable table, List<Genre> genres)
        {
            foreach (DataRow row in table.Rows)
            {
                var genre = new Genre();
                genre.Id = int.Parse(row["Id"].ToString());
                genre.Name = row["Name"].ToString();
                genres.Add(genre);
            }
        }
        public void AddGenre(string genre)
        {
            if (CheckIfGenreInDatabase(genre) == 0)
            {
                String addGenreQuery = "INSERT INTO Genre VALUES('" + genre + "');";
                _connection.Open();
                SqlCommand addGenreCommand = new SqlCommand(addGenreQuery, _connection);
                addGenreCommand.ExecuteNonQuery();
                _connection.Close();
            }

        }
        public int CheckIfGenreInDatabase(String genre)
        {
            string queryCheckGenre = "SELECT COUNT(*) FROM Genre WHERE Genre.Name='" + genre + "';";
            _connection.Open();
            SqlCommand commandCheckGenre = new SqlCommand(queryCheckGenre, _connection);
            int numberOfGenres = (int)commandCheckGenre.ExecuteScalar();
            _connection.Close();
            return numberOfGenres;
        }
    }
}
