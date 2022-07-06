# [EM CONSTRUÇÃO - README temporário apenas para listar o que está sendo feito] ShinCacheTensei

O objetivo aqui é simplesmente ganhar conhecimento desenvolvendo do zero um projeto pessoal onde o foco está no uso de cache de memória.
Até o momento, destaco as seguintes tecnologias, técnicas e padrões usados: 

- C#
- ASP .NET Core 5
- Linq
- CLI do .NET
- Injeção de dependência com IServiceCollection, tentando ao máximo respeitar os princípios SOLID
- Cache de memória com IMemoryCache
- Entity Framework Core 5.0
- Migrations
- Modelagem de dados
- Git
- Github
- Commit Often
- Swashbuckle | Swagger
- Visual Studio Community 2022
- Diferentes camadas, cada uma com sua responsabilidade: Controller > Service > Repository > Context
- Boas práticas, como usar variáveis de ambiente para guardar informações sensíveis como a string de conexão do Postgre
- NUnit
- Docker

# Funcionamento até o momento

Existem três endpoints: ```/demons```, ```/demons/search``` e ```/filteroptions```. Pense nesses Demons como se fossem Pokémons, mas de outra franquia. Inicialmente, uma requisição GET é feita para ```/demons/search``` com os critérios da pesquisa, que podem ser nome, fraqueza, skill que possui, etc. Exemplo: ```/demons/search?MinimumLevel=40&MaximumLevel=60&Quantity=5``` (retorne até 5 ids de Demons que sejam do nível 40 até o nível 60). A quantidade máxima que pode ser retornada está definida no appsettings.json, evitando de retornar tudo de uma vez.

Também é possível usar paginação: ```/demons/search?MinimumLevel=40&Quantity=5&AfterId=6``` (retorne até 5 ids de Demons que sejam no mínimo do nível 40 e que, ao final de toda filtragem, pegue apenas os que estão após o Demon de id 6. A lógica é que se você fez essa mesma busca anteriormente, como por exemplo ```/demons/search?MinimumLevel=40&Quantity=5``` e foi retornado os Demons de ids 7, 2, 3, 9 e 6, caso você queira obter mais do que isso, é só usar o parâmetro AfterId como igual a 6 para buscar mais 5 após o último elemento.

Mas o que eu quero dizer com ids de Demons? Esse endpoint não retorna os dados sobre os Demons, apenas seus respectivos ids, para que possam ser usado no próximo endpoint: ```/demons?id=7&id=2&id=3&id=9``` (obtenha todas as informações sobre os Demons que tenham os ids 2, 3 e 9). O máximo que pode ser retornado atualmente é 5, mas eventualmente essa configuração será movida para o appsettings.json como o do caso acima. O propósito em primeiro buscar os ids é que eu posso armazenar individualmente cada Demon em cache. Desconsiderarei qualquer cache no lado do Cliente aqui. Ao fazer um GET para esse endpoint e com esses mesmos parâmetros do exemplo acima, o servidor verificará se os demons de ids 7, 2, 3 e 9 existem em cache e, caso estejam lá, são retornados. Os que não são encontrados, são trazidos do banco de dados e, após isso, inseridos no cache. Todos os Demons retornados são "marcados" com um atributo chamado "CameFrom", que serve para indicar se o Demon veio do cache (valor 1) ou do banco de dados (valor 2). 

Essa mesma lógica do cache acima é aplicada no caso da busca pelos ids de Demons. Exemplo: ```/demons/search?MinimumLevel=40&MaximumLevel=60&Quantity=5```. Isso não apenas retornará os ids dos Demons, mas também retornará o atributo "CameFrom". As requisições feitas e seus resultados são armazenados em cache. A lógica usada foi a de concatenar todos os parâmetros possíveis da requisição e usar como chave para acessar o cache. Exemplo: ```$"ResistNatureId={ResistNatureId}&WeakNatureId={WeakNatureId}&MinimumLevel={MinimumLevel}&MaximumLevel={MaximumLevel}&DemonRaceId={DemonRaceId}" + $"&SkillId={SkillId}&ContainsThisTextInName={ContainsThisTextInName}&AfterId={AfterId}";``` Desse jeito, cada requisição tem uma chave única que sempre será a mesma se a requisição tiver os mesmos parâmetros com os mesmos valores.

O endpoint ```/filteroptions``` é responsável por retornar as opções de filtro disponíveis (ex: Nomes de todas as raças e seus respectivos ids, que serão usados tanto para exibição no Front-end quanto para o envio da requisição para ```/demons/search```) e, além disso, ele também fica armazenado em cache, embora não tenha paginação, pois a quantidade é bem menor e é limitada.

Apesar de básicos e cobrir apenas alguns casos de uso da camada Repository, o projeto possui alguns testes automatizados com NUnit, bastando um ```dotnet test``` no diretório tests para testar. Docker pode ser usado setando as variáveis de ambiente PORT (porta do container) e DATABASE_URL (string de conexão do PostgreSQL). Futuramente criarei um PostgreSQL local em uma segunda imagem Docker, pois o LocalDB do SQL Server não funciona no Linux, mas no momento o foco está no uso pelo Heroku.

# Próximos objetivos

<<<<<<< HEAD
As funcionalidades principais já estão prontas e basta aplicar as migrations e usar um simples ```dotnet run``` para rodar o projeto e fazer testes com Swagger, Postman ou outro. O foco agora será em melhorias no código, criação de um front-end super simples (meu foco é back-end), popular o banco de dados e parar para estudar cada detalhe a fundo. 

Alguns testes básicos pelo Swagger podem ser feitos rapidamente por aqui: https://shincachetensei.herokuapp.com/swagger/index.html
No endpoint /api/v1/demons/search, colocar o valor 5 ou maior no campo quantity para pegar alguns ids de Demons rapidamente.
No endpoint /api/v1/demons, pode usar os valores 5, 6, 7, 8, 9 e 10 para buscar os demons com esses ids.
No endpoint /api/v1/filteroptions, basta um GET sem parâmetros para pegar os filtros disponíveis.
Esses valores aqui são apenas para testes e são baseados nos poucos dados que já existem no banco de dados.

O deploy pelo Heroku foi feito usando o modelo de imagem Docker: https://devcenter.heroku.com/articles/container-registry-and-runtime Usei conceitos de CI/CD com Github Actions para automatizar todo esse processo.
=======
As funcionalidades principais já estão prontas e basta aplicar as migrations e usar um simples ```dotnet run``` para rodar o projeto e fazer testes com Swagger, Postman ou outro. O foco agora será em melhorias no código, criação de um front-end super simples (meu foco é back-end), popular o banco de dados e parar para estudar cada detalhe a fundo. Alguns testes básicos pelo Swagger pode ser feito rapidamente por aqui: https://tempabc.herokuapp.com/swagger/index.html O deploy foi feito usando o modelo de imagem Docker: https://devcenter.heroku.com/articles/container-registry-and-runtime Tentarei também usar conceitos de CI/CD com Github Actions para automatizar todo esse processo.
>>>>>>> parent of 3186927 (Added more info to README.md (Github Actions, Heroku, updated link for the app, test values...))

Inspirado em: https://leandro-rmc.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html
