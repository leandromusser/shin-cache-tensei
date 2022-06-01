# ShinCacheTensei

O objetivo deste projeto é recriar um outro do SENAC (2017), que era inteiramente Client Side, apenas para ganhar conhecimento.

# Futuro uso

O Client fará um GET para um endpoint usando parâmetros de URL com os filtros que ele quer, algo como /demons/getlist?race=Warrior&Weakness=Fire. Nesse caso, o usuário receberá uma lista de ids de demons (veja-os como versões mais obscuras de Pokémons) que sejam da raça Warrior e tenham como fraqueza Fire. Sim, ele não receberá os demons, mas sim uma lista de ids de demons que estão de acordo com os critérios de pesquisa. O tamanho da lista será perfeitamente controlado por paginação, assim como o que vem a seguir.

O Client varrerá essa lista de ids e fará mais outra requisição ao servidor, um endpoint como /demons?id=4&id=53&id=61 por exemplo.
Dessa vez, o servidor retornará uma lista com todas as características dos demons de id 4, 53 e 61. Mas por que a primeira requisição não envia os demons de uma vez sem precisar da segunda? Porque essa é uma forma de manter as informações de cada Demon em cache Client e Server Side. O Client primeiro verificará se algum Demon com esses ids existem no cache Client Side antes de efetuar a requisição e, caso exista (o id 53 por exemplo), a requisição ao servidor ficará da seguinte forma: /demons?id=4&id=61. Lá ocorrerá a mesma coisa entre o servidor e o banco de dados.

Tenho muitas outras ideias, como manter cache também da lista de ids, mas por enquanto estou construindo outras coisas.
Repetindo, o objetivo aqui é apenas ganhar conhecimento.


Baseado em: https://leandro-rmc.github.io/SENAC/[Basico]_Algoritmos_e_JavaScript/smtNocturne/demons.html
