using Microsoft.Extensions.Caching.Memory;
using ShinCacheTensei.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShinCacheTensei.Data.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        public readonly IMemoryCache _memoryCache;
        public CacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool GetByKey(object key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public bool GetByKeys(object[] keys, out IEnumerable<object> values) {

            values = new List<object>();
            var tempValue = values;

            keys.ToList().ForEach(k =>
            {
                if (_memoryCache.TryGetValue(k, out object tempObj))
                    tempValue.ToList().Add(tempObj);
            });
            values = tempValue;

            return values.Any();
        }

        public void AddDurable(object key, object value)
        {

            //Obter valor da duração do arquivo de configuração para não ficar algo Hard Coded
            using (var entry = _memoryCache.CreateEntry(key))
            {
                entry.Value = value;
                entry.SetValue(value);
                //entry.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddSeconds(5));
            }
        }

        public bool Teste()
        {
            return false;
            //_memoryCache.GetOrCreate("DSADAS", (p) => p);
            //_memoryCache.CreateEntry("DSADSA").pr

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