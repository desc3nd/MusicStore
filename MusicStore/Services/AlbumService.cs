using MusicStore.IServices;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MusicStore.Services
{
    public class AlbumService : IAlbumService
    {
        /// <summary>
        /// Connection string do db
        /// </summary>
        private readonly SqlConnection _connection = new SqlConnection(Properties.Resources.DBConnectionString);

        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;

        public AlbumService(IArtistService artistService, IGenreService genreService)
        {
            _artistService = artistService;
            _genreService = genreService;
        }

        public ICollection<Album> GetAlbums()
        {
            ICollection<Album> albums = new List<Album>();
            //tworze zapytanie w sqlu query
            string query = "SELECT Album.Title,Album.Id,Artist.Id as ArtistId,Genre.Id as GenreId, Album.SwearWords,Album.YearOfPublish,Album.DateOfPublish,Album.Amount,Album.Description,Album.Price FROM Album JOIN Artist ON Album.ArtistId = Artist.Id JOIN Genre ON Album.GenreId = Genre.Id;";
            // adapter służy do wykonania komendy sqlowej
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            //tworze nowa tabele DataTable  służy do przechowywania danych tabelranych z sqla
            DataTable table = new DataTable();
            // zasilam table nowymi danymi z sql adaptera i zwracam table
            adapter.Fill(table);
            //przypisuje dane do modelu - w tym wypadku jest to album
            setDataRowToAlbum(table, albums);
            //zwracam kolekcje albumów
            return albums;
        }
        private void setDataRowToAlbum(DataTable table, ICollection<Album> albums)
        {
            foreach (DataRow row in table.Rows)
            {
                var album = new Album();
                album.Id = int.Parse(row["Id"].ToString());
                album.Title = row["Title"].ToString();
                album.ArtistId = int.Parse(row["ArtistId"].ToString());
                album.Artist = _artistService.GetArtist(int.Parse(row["ArtistId"].ToString()));
                album.GenreId = int.Parse(row["GenreId"].ToString());
                album.Genre = _genreService.GetGenre(int.Parse(row["GenreId"].ToString()));
                album.YearOfPublish = int.Parse(row["YearOfPublish"].ToString());
                album.DateOfPublish = DateTime.Parse(row["DateOfPublish"].ToString());
                album.Price = decimal.Parse(row["Price"].ToString());
                album.Amount = int.Parse(row["Amount"].ToString());
                album.Description = row["Description"].ToString();
                album.SwearWords = bool.Parse(row["SwearWords"].ToString());
                albums.Add(album);
            }
        }

        public void Edit(Album album)
        {
            _connection.Open();
            string updateBookQuery = "UPDATE Album SET Title='" + album.Title + "', ArtistId=" + album.ArtistId + ", GenreId=" + album.GenreId + ", YearOfPublish=" + album.YearOfPublish + ", Price = " + album.Price + ", SwearWords='" + album.SwearWords + "', Description='" + album.Description + "', Amount='" + album.Amount + "', DateOfPublish='" + album.DateOfPublish.ToString("yyyy-MM-dd") + "' WHERE Id=" + album.Id + ";";
            SqlCommand commandInsertBook = new SqlCommand(updateBookQuery, _connection);
            commandInsertBook.ExecuteNonQuery();
            _connection.Close();
        }

        public void AddAlbumWithoutSpotifyAPI(Album album)
        {
            _connection.Open();
            string addAlbumQuery = "INSERT INTO Album (Title,ArtistId,GenreId,YearOfPublish,Price,SwearWords,Description,Amount,DateOfPublish) VALUES ('" + album.Title + "', " + album.ArtistId + ", " + album.GenreId + ", " + album.YearOfPublish + ", " + album.Price + ", '" + album.SwearWords + "', '" + album.Description + "', " + album.Amount + ", '" + album.DateOfPublish.ToString("yyyy-MM-dd") + "');";
            SqlCommand commandInsertAlbum = new(addAlbumQuery, _connection);
            commandInsertAlbum.ExecuteNonQuery();
            _connection.Close();

        }

        public void DeleteAlbum(string title)
        {
            //tu jeszcze dopisac usuwanie artystow i gatunkow jesli nie ma albumu z nimi
            string deleteAlbumQuery = "DELETE FROM Album WHERE Title='" + title + "';";
            _connection.Open();
            SqlCommand deleteAlbumCommand = new SqlCommand(deleteAlbumQuery, _connection);
            deleteAlbumCommand.ExecuteNonQuery();
            _connection.Close();

        }

        public ICollection<Album> SearchAlbumsByTitle(string title)
        {
            ICollection<Album> albums = new List<Album>();
            string query = "SELECT Album.Title,Album.Id,Artist.Id as ArtistId,Genre.Id as GenreId, Album.SwearWords,Album.YearOfPublish,Album.DateOfPublish,Album.Amount,Album.Description,Album.Price FROM Album JOIN Artist ON Album.ArtistId = Artist.Id JOIN Genre ON Album.GenreId = Genre.Id WHERE Album.Title=" + title + ";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            setDataRowToAlbum(table, albums);
            return albums;
        }

        public ICollection<Album> SearchAlbumsByArtist(string artist)
        {
            ICollection<Album> albums = new List<Album>();
            string query = "SELECT Album.Title,Album.Id,Artist.Id as ArtistId,Genre.Id as GenreId, Album.SwearWords,Album.YearOfPublish,Album.DateOfPublish,Album.Amount,Album.Description,Album.Price FROM Album JOIN Artist ON Album.ArtistId = Artist.Id JOIN Genre ON Album.GenreId = Genre.Id WHERE Album.Title=" + artist + ";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            setDataRowToAlbum(table, albums);
            return albums;
        }
    }
}

