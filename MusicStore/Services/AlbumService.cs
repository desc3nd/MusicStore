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

        public AlbumService(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public ICollection<Album> GetAlbums()
        {
            ICollection<Album> albums = new List<Album>();
            //tworze zapytanie w sqlu query
            string query = "SELECT Album.Title,Album.Id,Artist.Id as ArtistId,Genre.Id as GenreId, SwearWords.SwearWords,Album.YearOfPublish,Album.DateOfPublish,Album.Amount,Album.Description,Album.Price FROM Album JOIN Artist ON Album.ArtistId = Artist.Id JOIN Genre ON Album.GenreId = Genre.Id JOIN SwearWords ON Album.SwearWordsId = SwearWords.Id;";
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
                album.Artist = _artistService.GetArtist(int.Parse(row["ArtistId"].ToString()));
                album.GenreId = int.Parse(row["GenreId"].ToString());
                album.YearOfPublish = int.Parse(row["YearOfPublish"].ToString());
                album.DateOfPublish = DateTime.Parse(row["DateOfPublish"].ToString());
                album.Price = decimal.Parse(row["Price"].ToString());
                album.Amount = int.Parse(row["Amount"].ToString());
                album.Description = row["Description"].ToString();
                albums.Add(album);
            }
        }
    }
}
