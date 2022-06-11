# [EM CONSTRUÇÃO] ShinCacheTensei

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

# Funcionamento até o momento

Existem dois endpoints: ```/demons``` e ```/demons/search```. Pense nesses Demons como se fossem Pokémons, mas de outra franquia. Inicialmente, uma requisição GET é feita para /demons/search com os critérios da pesquisa, que podem ser nome, fraqueza, skill que possui, etc. Exemplo: ```/demons/search?MinimumLevel=40&MaximumLevel=60&Quantity=5``` (retorne até 5 ids de Demons que sejam do nível 40 até o nível 50). A quantidade máxima que pode ser retornada está definida no appsettings.json, evitando de retornar tudo de uma vez.

Também é possível usar paginação: ```/demons/search?MinimumLevel=40&Quantity=5&AfterId=6``` (retorne até 5 ids de Demons que sejam no mínimo do nível 40 e que, ao final de toda filtragem, pegue apenas os que estão após o Demon de id 6. A lógica é que se você fez essa mesma busca anteriormente, como por exemplo ```/demons/search?MinimumLevel=40&Quantity=5``` e foi retornado os Demons de ids 7, 2, 3, 9 e 6, caso você queira obter mais do que isso, é só usar o parâmetro AfterId como igual a 6 para buscar mais 5 após o último elemento.

Mas o que eu quero dizer com ids de Demons? Esse endpoint não retorna os dados sobre os Demons, apenas seus respectivos ids, para que possam ser usado no próximo endpoint: ```/demons?id=7&id=2&id=3&id=9``` (obtenha todas as informações sobre os Demons que tenham os ids 2, 3 e 9). O máximo que pode ser retornado atualmente é 5, mas eventualmente essa configuração será movida para o appsettings.json como o do caso acima. O propósito em primeiro buscar os ids é que eu posso armazenar individualmente cada Demon em cache. Desconsiderarei qualquer cache no lado do Cliente aqui. Ao fazer um GET para esse endpoint e com esses mesmos parâmetros do exemplo acima, o servidor verificará se os demons de ids 7, 2, 3 e 9 existem em cache e, caso estejam lá, são retornados. Os que não são encontrados, são trazidos do banco de dados e, após isso, inseridos no cache. Todos os Demons retornados são "marcados" com um atributo chamado "CameFrom", que serve para indicar se o Demon veio do cache (valor 1) ou do banco de dados (valor 2). 

Essa mesma lógica do cache acima é aplicada no caso da busca pelos ids de Demons. Exemplo: ```/demons/search?MinimumLevel=40&MaximumLevel=60&Quantity=5```. Isso não apenas retornará os ids dos Demons, mas também retornará o atributo "CameFrom". As requisições feitas e seus resultados são armazenados em cache. A lógica usada foi a de concatenar todos os parâmetros possíveis da requisição e usar como chave para acessar o cache. Exemplo: ```$"ResistNatureId={ResistNatureId}&WeakNatureId={WeakNatureId}&MinimumLevel={MinimumLevel}&MaximumLevel={MaximumLevel}&DemonRaceId={DemonRaceId}" + $"&SkillId={SkillId}&ContainsThisTextInName={ContainsThisTextInName}&AfterId={AfterId}";``` Desse jeito, cada requisição tem uma chave única que sempre será a mesma se a requisição tiver os mesmos parâmetros com os mesmos valores.

# Próximos objetivos

As funcionalidades principais já estão prontas e basta um simples ```dotnet run``` e aplicação das migrations para rodar o projeto e fazer testes com Swagger, Postman ou outro. O foco agora será em melhorias no código, criação de funções para buscar determinadas listas de coisas (Raça, Natureza, etc. Qual o id da raça Fool? Tudo isso precisa ser retornado para usar nas buscas, mas atualmente estou usando ids aleatórios), inserção dos dados no banco de dados (pois só tem alguns Demons salvos com informações aleatórias, feito de forma manual para poder testar) e criação de um front-end super simples, pois meu foco é back-end. No fim, usarei este projeto como base para meus estudos com NUnit e CI/CD. Após essa etapa, será feito o deploy na Heroku usando o modelo de deploy com imagem Docker.

Inspirado em: https://leandro-rmc.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html
