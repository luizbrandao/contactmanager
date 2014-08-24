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
                        new Album{ id = 4, artist = "Junior", title = "do"},
                        new Album{ id = 5, artist = "Teste", title = "res"}
                    };

                    ctx.Cache[CacheKey] = albuns;
                }
            }
        }

        public Album[] GetAllAlbuns()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Album[])ctx.Cache[CacheKey];
            }

            return new Album[]
            {
                new Album{ id = 0, artist = "Erro ao inserir Artista e Album", title="Placeholder" }
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

                    var ultimo = currentData.OrderBy(x => x.id).Last();

                    foreach (var item in currentData)
                    {


                        if (album.id.Equals(0))
                        {
                            int id = ultimo.id;
                            album.id = id + 1;
                            currentData.Add(album);
                            ctx.Cache[CacheKey] = currentData.ToArray();

                            return true;
                        }
                        else
                        {
                            if (item.id.Equals(album.id))
                            {
                                item.artist = album.artist;
                                item.title = album.title;

                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        public Album[] getAlbum(string id)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Album[])ctx.Cache[CacheKey]).ToList();
                    foreach (var item in currentData)
                    {
                        if (item.id.ToString().Equals(id))
                        {
                            return new Album[]
                            {
                                item
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Album[]
                    {
                        new Album{ id = 0, artist = "Erro ao procurar por Artista", title="Placeholder" }
                    };
                }
            }

            return new Album[]
            {
                new Album{ id = 0, artist = "Artista nao encontrado!", title="Placeholder" }
            };
        }

        public Album[] deleteAlbum(string id)
        {
            var ctx = HttpContext.Current;

            try
            {
                var currentData = ((Album[])ctx.Cache[CacheKey]).ToList();
                foreach (var item in currentData.ToList())
                {
                    if (item.id.ToString().Equals(id))
                    {
                        currentData.Remove(item);
                        ctx.Cache[CacheKey] = currentData.ToArray();
                    }
                }
                return new Album[]
                {
                    new Album { id = Convert.ToInt32(id), artist = "Deletado", title=""} 
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Album[]
                {
                    new Album{ id = 0, artist = "Erro ao procurar por Artista", title="Placeholder" }
                };
            }
        }
    }
}