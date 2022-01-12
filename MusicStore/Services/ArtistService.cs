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
    public class ArtistService : IArtistService
    {
        private readonly SqlConnection _connection = new SqlConnection(Properties.Resources.DBConnectionString);

        public ICollection<Artist> GetArtists()
        {
            var artists = new List<Artist>();
            string query = "SELECT Id, Name, Country,Description from Artist;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            SetDataToArtists(table, artists);
            return artists;
        }
        private void SetDataToArtists(DataTable table, List<Artist> artists)
        {
            foreach(DataRow row in table.Rows) {
            var artist = new Artist();
            artist.Id = int.Parse(row["Id"].ToString());
            artist.Name = row["Name"].ToString();
            artist.Description = row["Description"].ToString();
            artist.Country = row["Country"].ToString();
                artists.Add(artist);
            }
        }

        public Artist GetArtist(int? id)
        {
            Artist artist = new Artist();
            string query = "SELECT Id, Name, Country,Description from Artist where id=" + id + ";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                throw new ArgumentNullException();
            }
            SetDataRowToArtist(table, artist);
            return artist;
        }

        private void SetDataRowToArtist(DataTable table, Artist artist)
        {
            artist.Id = int.Parse(table.Rows[0]["Id"].ToString());
            artist.Name = table.Rows[0]["Name"].ToString();
            artist.Description = table.Rows[0]["Description"].ToString();
            artist.Country = table.Rows[0]["Country"].ToString();
        }
        public void AddArtist(Artist artist)
        {
            if (artist != null && CheckIfArtistInDatabase(artist.Id) == 0)
            {
                String addArtistQuery = "INSERT INTO Artist (Name, Country, Description) VALUES('" + artist.Name + "','" + artist.Country + "','" + artist.Description + "');";
                _connection.Open();
                SqlCommand addArtistCommand = new SqlCommand(addArtistQuery, _connection);
                addArtistCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public void DeleteArtist(int id)
        {
            string deleteArtistQuery = "DELETE FROM Artist WHERE Id='" + id + "';";
            _connection.Open();
            SqlCommand deleteArtistCommand = new SqlCommand(deleteArtistQuery, _connection);
            deleteArtistCommand.ExecuteNonQuery();
            _connection.Close();

        }
        public void Edit(Artist artist)
        {
            _connection.Open();
            string updateArtistQuery = "UPDATE Artist SET Name='" + artist.Name + "', Country=" + artist.Country + ", Description=" + artist.Description + ",' WHERE Id=" + artist.Id + ";";
            SqlCommand commandInsertBook = new SqlCommand(updateArtistQuery, _connection);
            commandInsertBook.ExecuteNonQuery();
            _connection.Close();
        }

        public int CheckIfArtistInDatabase(int id)
        {
            string queryCheckArtist = "SELECT COUNT(*) FROM Artist WHERE Artist.Id='" + id + "';";
            _connection.Open();
            SqlCommand commandCheckArtist = new SqlCommand(queryCheckArtist, _connection);
            int numberOfArtists = (int)commandCheckArtist.ExecuteScalar();
            _connection.Close();
            return numberOfArtists;
        }
    }
}// usunac i edytowac 