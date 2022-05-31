# ShinCacheTensei

O objetivo deste projeto é recriar um outro do SENAC (2017), que era inteiramente Client Side. O objetivo aqui é apenas ganhar conhecimento.

# Futuro uso

O Client fará uma requisição GET para algum endpoint e usando Query Params, algo como /demons/getlist?race=Warrior&Weakness=Fire.
Nesse caso, o usuário quer uma lista de ids de demons (veja-os como versões mais obscuras de Pokémons) que seja da raça Warrior e tenha como fraqueza Fire.
OBS: Como nesse caso não foi explicitado os parâmetros relacionados à paginação, o servidor retornará uma lista do primeiro elemento até elemento X, sem retornar tudo do banco.

O Client olhará essa lista e fará mais outra requisição ao servidor, um endpoint como /demons?id=4&id=53&id=61.
Dessa vez, o servidor retornará uma lista com todas as características dos demons de id 4, 53 e 61.
Mas por que a primeira requisição não envia tudo de uma vez sem precisar da segunda? Porque essa é uma forma de manter as informações de cada Demon em cache Client e Server Side. O client primeiro verificará se algum demon com esses ids existem no cash client side e, caso exista (o id 53 por exemplo), a requisição real ao servidor ficará: /demons?id=4&id=61. Lá ocorrerá a mesma coisa entre o servidor e o banco de dados.

Tenho muitas outras ideias, como manter cache também da lista de ids, mas por enquanto estou construindo outras coisas.
Repetindo, o objetivo aqui é apenas ganhar conhecimento.


Baseado em: https://leandro-rmc.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html
