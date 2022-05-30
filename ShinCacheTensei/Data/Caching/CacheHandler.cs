using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;
using System;

namespace ShinCacheTensei.Data.Caching
{
    public class CacheHandler : ICacheHandler
    {
        public readonly IMemoryCache _memoryCache;
        public CacheHandler(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool TryGetValue(object key, out object value) => _memoryCache.TryGetValue(key, out value);
        public bool AddDurable(object key, object value) {

            DateTime dt = new DateTime();
            DateTime.UtcNow.AddDays(1);
            //DateTimeOffset.

            _memoryCache.CreateEntry(key).AbsoluteExpiration
            return false;

        }


        _memoryCache.Set(key, demon) == null;

        //public TryObtainDemonWithName(object key, out Demon demon)
        public bool TryObtainDemon(object key, out Demon demon) => _memoryCache.TryGetValue(key, out demon);
        public bool AddDemon(object key, Demon demon) => _memoryCache.Set(key, demon) == null;
        public bool Teste() {
            _memoryCache.GetOrCreate("DSADAS", (p) => p);
            _memoryCache.CreateEntry("DSADSA").pr

                //no serviço, pegar o tempo de expiração do cache por arquivo de configuração
                //Com o CreaqteEntry, eu tenho muito mais controle de tudo o  que eu insiro lá e posso guardar os valores antes de inserir pra passar pra um DaoCache
                //Informações ao front-end serão apenas se veio do cache e nada mais
                //Não colocar CacheableDemonDto no Controller, apenas DemonDto, pois será mais fácil
                //NÃO ESQUECER DA FUNÇÃO QUE VAI RETORNAR A LISTA GIGANTE DE DEMONS COM PAGINAÇÃO, ETC. É ATRAVÉS DELA QUE VAMOS VARRER O CACHE

            //A lógica funciona melhor em caso de paginação. Ex: Usuário digita 'a', o serv faz a consulta no banco e devolve uma lista enorme múltipla de 5
            //Através da lista, você faz outra requisição com os ids que você quer e controla a paginação você mesmo não esquecendo de pegar do cache frontend também
            //Usuário ter que clicar no botão para pesquisar ao invés de só ir digitando ou talvez remover a barra de pesquisa por nome?
            //Criar fraquezas demons. Cache de páginas? Ex: usuário pesquisa demons imunidade a fogo da raça warrior. ter um cache com essas informações e devolver da memória
            //...os ids dos demons com essas características. A princípio, pode ser feito sem isso

            /*
                CacheableDemonDto
                Demon Demon
                bool IsFromCache
             */
        }

    }
}