using art_shop_core.EntityFramework;
using System;
using System.Collections.Generic;

namespace art_shop_core.DAL
{
    public class ArtistManager
    {
        private readonly IDatabaseConnection database;

        public ArtistManager(IDatabaseConnection database)
        {
            this.database = database;
        }

        public void AddNewArtist(Artist artist)
        {
            database.Add(artist);
        }

        public void RemoveArtist(Artist artist)
        {
            database.Remove(artist);
        }

        public void UpdateArtist(Artist artist)
        {
            database.Update(artist);
        }

        public List<Artist> FindArtist(Func<Artist, bool> filter)
        {
            return database.Find(filter);
        }

        public List<Artist> GetAllArtists()
        {
            return database.GetAll<Artist>();
        }
    }
}
