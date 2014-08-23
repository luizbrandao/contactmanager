using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;

namespace ContactManager.Services
{
    public class AlbumRepository 
    {
        private const string CacheKey = "AlbumStore";

        public AlbumRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var albuns = new Album[]
                    { 
                        new Album{ id = 1, artist = "Luiz", title = "Ava"},
                        new Album{ id = 2, artist = "Cezar", title = "ssa"},
                        new Album{ id = 3, artist = "Brandao", title = "la"},
                        new Album{ id = 5, artist = "Junior", title = "do"},
                        new Album { id = 6, artist = "", title = "res"}
                    };

                    ctx.Cache[CacheKey] = albuns;
                }
            }
        }

        public Album[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Album[])ctx.Cache[CacheKey];
            }

            return new Album[]
            {
                new Album{ id = 0, artist = "Placeholder", title="Placeholder" }
            };
        }

        public bool SaveAlbum(Album album)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Album[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(album);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}