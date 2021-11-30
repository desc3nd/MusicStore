﻿using MusicStore.IServices;
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
            setDataRowToArtist(table, artist);
            return artist;
        }
        private void setDataRowToArtist(DataTable table, Artist artist)
        {
            artist.Id = int.Parse(table.Rows[0]["Id"].ToString());
            artist.Name = table.Rows[0]["Name"].ToString();
            artist.Description = table.Rows[0]["Description"].ToString();
            artist.Country = table.Rows[0]["Country"].ToString();
        }
        }
    }
