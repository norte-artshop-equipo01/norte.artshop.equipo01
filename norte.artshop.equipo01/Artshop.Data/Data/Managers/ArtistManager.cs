using Artshop.Data.Data;
using Artshop.Data.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _database.Add(artist);
        }

        public void RemoveArtist(Artist artist)
        {
            for (int i = 0; i < artist.Product.Count; i++)
            {
                _database.Remove(artist.Product.ElementAt(i));
            }
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

        public List<Artist> GetAllArtists(bool includeDisabled = false)
        {
            return !includeDisabled
                ? _database.GetAll<Artist>().Where(x => x.Disabled == false).ToList()
                : _database.GetAll<Artist>();
        }
    }
}
