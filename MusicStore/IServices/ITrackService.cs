using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.IServices
{
    public interface ITrackService
    {
        /// <summary>
        /// zwraca kolekcje tracków
        /// </summary>
        /// <returns></returns>
        public ICollection<Track> GetTracks();

        /// <summary>
        /// zwraca track po id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Track GetTrack(int? id);

        /// <summary>
        /// dodaje track
        /// </summary>
        /// <param name="track"></param>
        public void AddTrack(Track track);

        /// <summary>
        /// usuwa track
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTrack(int id);

        /// <summary>
        /// edytuje track
        /// </summary>
        /// <param name="track"></param>
        public void EditTrack(Track track);
    }
}
