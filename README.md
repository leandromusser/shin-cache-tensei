
# Shin Cache Tensei (2022)

O objetivo deste projeto pessoal foi ganhar mais conhecimento em ferramentas, padrões, etc. Teve como principal objetivo usar cache server side para retornar os resultados. Ele foi finalizado antes do front-end básico ser construído, pois meu foco está mais em back-end, e também o projeto não visou resolver um problema real de mercado, o que foi um erro da minha parte, não valendo a pena o esforço para ir além. Swagger está funcionando para os testes necessários.


## Explicação Técnica

Uma requisição é feita com os critérios dos personagens que quero buscar (ataque, natureza, nível máximo e mínimo, etc.) e é retornada uma lista de ids de personagens que tenham essas características:

GET: /api/v1/demons/search?MinimumLevel=0&MaximumLevel=50&Quantity=2
```
  {
  "demonIds": [
    5,
    7
  ],
  "cameFrom": 2
}
```

Essa requisição, passando MinimumLevel=0, MaximumLevel=50 e Quantity=2, retorna os demonIds 5 e 7, além do cameFrom com valor 1. Ao fazer exatamente essa mesma requisição (com os exatos mesmos parâmetros e respectivos valores), a resposta poderá vir da seguinte forma:

```
  {
  "demonIds": [
    5,
    7
  ],
  "cameFrom": 1
}
```
Observa-se que a única coisa que mudou foi o valor de cameFrom. Isso quer dizer que essa consulta, (com os exatos mesmos parâmetros e respectivos valores) está armazenada em cache. Se mudar apenas 1 único valor desses parâmetros, a consulta já é tratada como diferente e pode vir do banco de dados (se por acaso já não estiver em cache por conta de alguma outra consulta anterior).

cameFrom com número mágico 2 = veio do banco de dados\
cameFrom com número mágico 1 = veio do cache de servidor

Em testes básicos locais, é quase garantido que, após uma requisição, essa mesma requisição já estará em cache.

Essa mesma lógica é aplicada também aos demons (cameFrom no final):

GET: /api/v1/demons?id=5
```
[
  {
    "demon": {
      "id": 5,
      "name": "Leandro",
      "race": {
        "id": 1,
        "name": "Avatar"
      },
      "initialLevel": 50,
      "initialHp": null,
      "initialMp": null,
      "initialStr": null,
      "initialMag": null,
      "initialVit": null,
      "initialAgi": null,
      "initialLck": null,
      "recruitingMethod": {
        "id": 1,
        "description": "Fusion only"
      },
      "demonInitialSkills": [
        {
          "unlockLevel": 67,
          "skill": {
            "id": 5,
            "name": "Megido",
            "description": "Deals 100 sacred damage to all foes",
            "skillType": {
              "id": 2,
              "type": "MP"
            },
            "cost": 80
          }
        }
      ],
      "demonAffinities": [
        {
          "nature": {
            "id": 5,
            "name": "Nerve"
          },
          "affinityType": {
            "id": 5,
            "name": "Weak"
          }
        }
      ]
    },
    "cameFrom": 2
  }
]
```
Realizando a mesma requisição:

```
[
  {
    "demon": {
      "id": 5,
      "name": "Leandro",
      "race": {
        "id": 1,
        "name": "Avatar"
      },
      "initialLevel": 50,
      "initialHp": null,
      "initialMp": null,
      "initialStr": null,
      "initialMag": null,
      "initialVit": null,
      "initialAgi": null,
      "initialLck": null,
      "recruitingMethod": {
        "id": 1,
        "description": "Fusion only"
      },
      "demonInitialSkills": [
        {
          "unlockLevel": 67,
          "skill": {
            "id": 5,
            "name": "Megido",
            "description": "Deals 100 sacred damage to all foes",
            "skillType": {
              "id": 2,
              "type": "MP"
            },
            "cost": 80
          }
        }
      ],
      "demonAffinities": [
        {
          "nature": {
            "id": 5,
            "name": "Nerve"
          },
          "affinityType": {
            "id": 5,
            "name": "Weak"
          }
        }
      ]
    },
    "cameFrom": 1
  }
]
```
Observe a mesma mudança do cameFrom. Agora, o demon de id 5 está em cache de servidor.

## Paginação

A quantidade máxima de demons que são retornados está definida no appsettings.json. É possível usar paginação adicionando o parâmetro AfterId. 
No caso abaixo, ele retornará 5 demons após o que tiver o id 6.

/demons/search?MinimumLevel=40&Quantity=5&AfterId=6


## Algumas tecnologias, técnicas e padrões usados: 

- C#
- ASP .NET Core 5
- Linq
- CLI do .NET
- Injeção de dependência com IServiceCollection, tentando ao máximo respeitar os princípios SOLID
- Cache de memória com IMemoryCache
- Entity Framework Core 5.0
- Migrations
- Paginação
- Modelagem de dados
- Git
- GitHub
- Commit Often
- Swashbuckle | Swagger
- Visual Studio Community 2022
- Diferentes camadas, cada uma com sua responsabilidade: Controller > Service > Repository > Context
- NUnit
- Docker

## Modificações (2023)

- Este README foi parcialmente refeito para facilitar o entendimento
- Removido GitHub Actions e Heroku (plano gratuito não mais disponível e o foco agora está em testes locais)
- Adicionado Sqlite e aplicado a migração diretamente durante a criação da imagem no Dockerfile

## Testes com Docker

Para agilizar os testes, o projeto está disponível no DockerHub e basta apenas poucos comandos (e um pouco de conhecimento do que está fazendo: https://docs.docker.com/get-started/overview/) para testá-lo localmente no Linux.

Push da imagem:
```
docker push leandromusser/shincachetensei:latest
```
Criação do container e inicialização dele:
```
docker run -it --rm -p 8080:80 leandromusser/shincachetensei:latest
```

Após isso, fique à vontade para testar pelo Swagger: http://localhost:8080/swagger/index.html


## Inspiração
Apesar de diferentes, é inspirado em um trabalho que fiz no SENAC em 2017, totalmente pensado e criado do zero. Os mesmos personagens, mas tudo client side e sem a ideia do cache server side. E o código dele está péssimo. [Trabalho antigo de 2017.](https://leandromusser.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html)
