# [EM CONSTRUÇÃO] ShinCacheTensei

O objetivo aqui é simplesmente ganhar conhecimento desenvolvendo do zero um projeto pessoal.
Até o momento, destaco as seguintes tecnologias, técnicas e padrões usados: 

C#
Linq
ASP .NET Core 5
CLI do .NET
Injeção de dependência com IServiceCollection, tentando ao máximo respeitar os princípios SOLID
Cache de memória com IMemoryCache
Entity Framework Core 5.0
Migrations
Modelagem de dados
Git
Github
Commit Often
Swashbuckle | Swagger
Visual Studio Community 2022
Diferentes camadas, cada uma com sua responsabilidade: Controller > Service > Repository > Context

# Funcionamento até o momento

Existem dois endpoints: ```/demons``` e ```/demons/search```. Pense nesses Demons como se fossem Pokémons, mas de outra franquia. Inicialmente, uma requisição GET é feita para /demons/search com os critérios da pesquisa, que podem ser nome, fraqueza, skill que possui, etc. Exemplo: ```/demons/search?MinimumLevel=40&MaximumLevel=60&Quantity=5``` (retorne até 5 ids de Demons que sejam do nível 40 até o nível 50). A quantidade máxima que pode ser retornada está definida no appsettings.json, evitando de retornar tudo de uma vez.

Também é possível usar paginação: ```/demons/search?MinimumLevel=40&Quantity=5&AfterId=6``` (retorne até 5 ids de Demons que sejam no mínimo do nível 40 e que, ao final de toda filtragem, pegue apenas os que estão após o Demon de id 6. A lógica é que se você fez essa mesma busca anteriormente, como por exemplo ```/demons/search?MinimumLevel=40&Quantity=5``` e foi retornado os Demons de ids 7, 2, 3, 9 e 6, caso você queira obter mais do que isso, é só usar o parâmetro AfterId como igual a 6 para buscar mais 5 após o último elemento.

Mas o que eu quero dizer com ids de Demons? Esse endpoint não retorna os dados sobre os Demons, apenas seus respectivos ids, para que possam ser usado no próximo endpoint: ```/demons?id=7&id=2&id=3&id=9``` (obtenha todas as informações sobre os Demons que tenham os ids 2, 3 e 9). O máximo que pode ser retornado atualmente é 5, mas eventualmente essa configuração será movida para o appsettings.json como o do caso acima. O propósito em primeiro buscar os ids é que eu posso armazenar individualmente cada Demon em cache. Desconsiderarei qualquer cache no lado do Cliente aqui. Ao fazer um GET para esse endpoint e com esses mesmos parâmetros do exemplo acima, o servidor verificará se os demons de ids 7, 2, 3 e 9 existem em cache e, caso estejam lá, são retornados. Os que não são encontrados, são trazidos do banco de dados e, após isso, inseridos no cache. Todos os Demons retornados são "marcados" com um atributo chamado "CameFrom", que serve para indicar se o Demon veio do cache (valor 1) ou do banco de dados (valor 2). 

# Próximos objetivos

O próximo objetivo é fazer com a lista de ids fique armazenada em cache após a requisição ao primeiro endpoint, sendo a chave o conjunto de parâmetros. Usando a mesma consulta lá de cima como exemplo: ```/demons/search?MinimumLevel=40&Quantity=5&AfterId=6```. Nesse caso, os parâmetros MinimumLevel, Quantity, AfterId e todos os outros possíveis (mesmo nulos ou vazios por não terem sido considerados nessa consulta) serão combinados para gerar um Hash OU convertidos em Strings e concatenados em uma certa ordem. Em ambos os casos, serão usados como chaves para acessar o item contendo os ids dessa consulta no cache.

No fim, usarei este projeto como base para meus estudos com NUnit e CI/CD. Após essa etapa, será feito o deploy na Heroku usando o modelo de deploy com imagem Docker.

Inspirado em: https://leandro-rmc.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html
