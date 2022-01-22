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

        public Genre GetGenre(int? id)
        {
            var genre = new Genre();
            string query = "select * from Genre where Genre.Id =  " + id +";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                throw new ArgumentNullException();
            }
            SetDataRowToGenre(table, genre);
            return genre;
        }

        private void SetDataRowToGenre(DataTable table, Genre genre)
        {
            genre.Id = int.Parse(table.Rows[0]["Id"].ToString());
            genre.Name = table.Rows[0]["Name"].ToString();
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
        public void Create(Genre genre)
        {
            if (genre != null && CheckIfGenreInDatabase(genre.Id) == 0)
            {
                String addGenreQuery = "INSERT INTO Genre (Name) VALUES('" + genre.Name + "');";
                _connection.Open();
                SqlCommand addGenreCommand = new SqlCommand(addGenreQuery, _connection);
                addGenreCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void Delete(int id)
        {
            string deleteGenreQuery = "DELETE FROM Genre WHERE Id='" + id + "';";
            _connection.Open();
            SqlCommand deleteGenreCommand = new SqlCommand(deleteGenreQuery, _connection);
            deleteGenreCommand.ExecuteNonQuery();
            _connection.Close();

        }
        public void Edit(Genre genre)
        {
            _connection.Open();
            string updateGenreQuery = "UPDATE Genre SET Name='" + genre.Name + "' WHERE Id=" + genre.Id + ";";
            SqlCommand commandEditGenre = new SqlCommand(updateGenreQuery, _connection);
            commandEditGenre.ExecuteNonQuery();
            _connection.Close();
        }

        private int CheckIfGenreInDatabase(int id)
        {
            string queryCheckGenre = "SELECT COUNT(*) FROM Genre WHERE Genre.Id='" + id + "';";
            _connection.Open();
            SqlCommand commandCheckGenre = new SqlCommand(queryCheckGenre, _connection);
            int numberOfGenres = (int)commandCheckGenre.ExecuteScalar();
            _connection.Close();
            return numberOfGenres;
        }
    }
}
