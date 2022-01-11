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
    }
}


