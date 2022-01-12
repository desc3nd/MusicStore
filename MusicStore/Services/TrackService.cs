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
    public class TrackService : ITrackService
    {

        private readonly SqlConnection _connection = new SqlConnection(Properties.Resources.DBConnectionString);

        private readonly IArtistService _artistService;
        private readonly IGenreService _genreService;

        public TrackService(IArtistService artistService, IGenreService genreService)
        {
            _artistService = artistService;
            _genreService = genreService;
        }

        public ICollection<Track> GetTracks()
        {
            ICollection<Track> tracks = new List<Track>();
            string query = "select track.Title, Track.AlbumId, Track.ArtistId, Track.GenreId, Track.Id, Track.SwearWords, Track.Time from Track JOIN Artist On Track.ArtistId = Artist.Id JOIN Genre On Track.GenreId = Genre.Id Join Album on Track.AlbumId = Album.Id;";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            setDataRowToTrack(table, tracks);
            return tracks;
        }

        private void setDataRowToTrack(DataTable table, ICollection<Track> tracks)
        {
            foreach (DataRow row in table.Rows)
            {
                var track = new Track();
                track.Id = int.Parse(row["Id"].ToString());
                track.Title = row["Title"].ToString();
                track.ArtistId = int.Parse(row["ArtistId"].ToString());
                track.AlbumId = int.Parse(row["AlbumId"].ToString());
                track.Artist = _artistService.GetArtist(int.Parse(row["ArtistId"].ToString()));
                track.GenreId = int.Parse(row["GenreId"].ToString());
                track.Genre = _genreService.GetGenre(int.Parse(row["GenreId"].ToString()));
                track.Time = DateTime.Parse(row["Time"].ToString());
                track.SwearWords = bool.Parse(row["SwearWords"].ToString());
                tracks.Add(track);
            }
        }

        public Track GetTrack(int? id)
        {
            Track track = new Track();
            string query = "SELECT Id, Title, AlbumId, ArtistId, GenreId, SwearWords, Time from Track where id=" + id + ";";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                throw new ArgumentNullException();
            }
            SetDataRowToTrack(table, track);
            return track;
        }

        private void SetDataRowToTrack(DataTable table, Track track)
        {
            track.Id = int.Parse(table.Rows[0]["Id"].ToString());
            track.Title = table.Rows[0]["Title"].ToString();
            track.ArtistId = int.Parse(table.Rows[0]["ArtistId"].ToString());
            track.AlbumId = int.Parse(table.Rows[0]["AlbumId"].ToString());
            track.Artist = _artistService.GetArtist(int.Parse(table.Rows[0]["ArtistId"].ToString()));
            track.GenreId = int.Parse(table.Rows[0]["GenreId"].ToString());
            track.Genre = _genreService.GetGenre(int.Parse(table.Rows[0]["GenreId"].ToString()));
            track.Time = DateTime.Parse(table.Rows[0]["Time"].ToString());
            track.SwearWords = bool.Parse(table.Rows[0]["SwearWords"].ToString());
        }

        public void AddTrack(Track track)
        {
         
            if (track != null && CheckIfTrackInDatabase(track.Id) == 0)
            {
                String addTrackQuery = "INSERT INTO Track (Title, AlbumId, ArtistId, GenreId, Time, SwearWords) VALUES ('" + track.Title + "','" + track.AlbumId + "','" + track.ArtistId + "','" + track.GenreId + "','" + track.Time + "','" + track.SwearWords + "');";
                _connection.Open();
                SqlCommand addTrackCommand = new SqlCommand(addTrackQuery, _connection);
                addTrackCommand.ExecuteNonQuery();
                _connection.Close();
            }
        }

        private int CheckIfTrackInDatabase(int id)
        {
            string queryCheckTrack = "SELECT COUNT(*) FROM Track WHERE Track.Id='" + id + "';";
            _connection.Open();
            SqlCommand commandCheckTrack = new SqlCommand(queryCheckTrack, _connection);
            int numberOfTracks = (int)commandCheckTrack.ExecuteScalar();
            _connection.Close();
            return numberOfTracks;
        }

        public void DeleteTrack(int id)
        {
            string deleteTrackQuery = "DELETE FROM Track WHERE Id='" + id + "';";
            _connection.Open();
            SqlCommand deleteTrackCommand = new SqlCommand(deleteTrackQuery, _connection);
            deleteTrackCommand.ExecuteNonQuery();
            _connection.Close();

        }

        public void EditTrack(Track track)
        {
            _connection.Open();
            string updateTrackQuery = "UPDATE Track SET Title='" + track.Title + "', AlbumId=" + track.AlbumId + ", ArtistId=" + track.ArtistId + ", GenreId=" + track.GenreId + ", Time='" + track.Time + "', SwearWords='" + track.SwearWords + "' WHERE Id=" + track.Id + ";";
            SqlCommand commandInsertBook = new SqlCommand(updateTrackQuery, _connection);
            commandInsertBook.ExecuteNonQuery();
            _connection.Close();
        }
    }
}


