using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;

namespace Artshop.Data.Managers
{
    public class ArtistManager
    {
        private readonly IDatabaseData _database;

        public ArtistManager(IDatabaseData database)
        {
            _database = database;
        }

        public void AddNewArtist(Artist artist)
        {
            arti
            _database.Add(artist);
        }

        public void RemoveArtist(Artist artist)
        {
            _database.Remove(artist);
        }

        public void UpdateArtist(Artist artist)
        {
            _database.Update(artist);
        }

        public List<Artist> FindArtist(Func<Artist, bool> filter)
        {
            return _database.Find(filter);
        }

        public List<Artist> GetAllArtists()
        {
            return _database.GetAll<Artist>();
        }
    }
}
