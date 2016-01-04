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

        /// <summary>
        /// Inicializo a classe com um conjunto de Array inicial
        /// </summary>
        public AlbumRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var albuns = new Album[]
                    { 
                        new Album{ id = 1, artist = "Luiz", title = "C#"},
                        new Album{ id = 2, artist = "Cezar", title = "PHP"},
                        new Album{ id = 3, artist = "Brandao", title = "Java"},
                        new Album{ id = 4, artist = "Junior", title = "Python"},
                        new Album{ id = 5, artist = "Teste", title = "Ruby"}
                    };

                    ctx.Cache[CacheKey] = albuns;
                }
            }
        }

        /// <summary>
        /// Listo todos os Albuns
        /// </summary>
        /// <returns>Array de Albuns</returns>
        public Album[] GetAllAlbuns()
        {
            // Recupero o contexto enviado
            var ctx = HttpContext.Current;

            // verifico se é nulo ou não
            if (ctx != null)
            {
                // retorno todos os albuns em caso do contexto nao ser vazio
                return (Album[])ctx.Cache[CacheKey];
            }

            return new Album[]
            {
                new Album{ id = 0, artist = "Erro ao tentar recuperar todos os Artistas e Albuns", title="Placeholder" }
            };
        }

        /// <summary>
        /// Gravo ou atualizo os registro no array
        /// </summary>
        /// <param name="album"></param>
        /// <returns>Verdadeiro em caso de sucesso, falso em caso de falha</returns>
        public bool SaveAlbum(Album album)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    // Recupero uma lista com todos os Albuns
                    var currentData = ((Album[])ctx.Cache[CacheKey]).ToList();

                    // Verifico qual é o ultimo album inserido
                    var ultimo = currentData.OrderBy(x => x.id).Last();

                    // Percorro todos os albuns, que já estao no array
                    foreach (var item in currentData)
                    {
                        // Caso o id do album seja 0, novo album, acao de inserir
                        if (album.id.Equals(0))
                        {
                            // Recupero o ultimo id
                            int id = ultimo.id;
                            // Incremento em um
                            album.id = id + 1;
                            // Adicino na lista
                            currentData.Add(album);
                            // Atualizo o array com todos os albuns
                            ctx.Cache[CacheKey] = currentData.ToArray();

                            return true;
                        }
                        else
                        {
                            // Caso o id nao seja 0, album já existente, acao de atualizacao
                            if (item.id.Equals(album.id))
                            {
                                //Atualiza os dados
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

        /// <summary>
        /// Recupero um album em especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Array de Album</returns>
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
                        // Verifico se o id do item é igual ao id solicitado,
                        // em caso verdadeiro, retorno o item
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

        /// <summary>
        /// removo um album do array
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Array com informacoes do album deletado</returns>
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
                        // Removo o item da lista.
                        currentData.Remove(item);
                        // Atualizo o array com a nova lista
                        ctx.Cache[CacheKey] = currentData.ToArray();
                    }
                }
                // retorno as informacoes do objeto removido
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