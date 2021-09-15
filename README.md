# Collect Balls 2
Esse projeto é uma adaptação do [Trabalho 2](https://github.com/mariaeloi/collect-balls) para atender aos requisitos do Trabalho 3 da disciplina Motores de Jogos Digitais (UFRN/2021.1).

## Descrição do jogo
O jogo possui dois níveis e sempre ao concluir qualquer um deles, o jogador ganha uma certa quantidade de moedas que é baseada no tempo gasto para concluir a fase.

### Objetivo
Os dois níveis do jogo possuem o mesmo objetivo (alterando apenas a dificuldade dos obstáculos): coletar a quantidade indicada de bolas, 10, com a menor quantidade de quedas possível, e no menor tempo também.

### Modeas x Checkpoints
As moedas ganhas pelo jogador podem ser utilizadas nos níveis para marcar checkpoints. Cada checkpoint custa 100 moedas e é marcado de acordo com a posição do jogador naquele momento. É possível marcar checkpoint quantas vezes quiser em uma fase, porém somente a última marcação será leveada em cosideração.

### Persistência de dados
Alguns dados são persistidos no jogo automaticamente, dentre eles:
* O nível máximo alcançado, sendo permitido jogar somente até a próxima fase da alcançada
* Melhor jogada de cada nível (menor quantidade de quedas nele, e o menor tempo gasto para essa menor quantidade)
* Total de moedas do jogador
* Total de bolas já coletas no jogo, contadas independente de ter concluído as fases ou não

Também, é possível apagar todos os dados de progresso do jogo.

### Controles
* <kbd>W</kbd><kbd>A</kbd><kbd>S</kbd><kbd>D</kbd> - anda
* <kbd>Space</kbd> - pula
* <kbd>Q</kbd> - segura ou solta **caixa rosa claro** (![#FF75DB](https://via.placeholder.com/9/FF75DB/000000?text=+ "Cor da caixa segurável")) na mão esquerda
* <kbd>E</kbd> - segura ou solta **caixa rosa claro** (![#FF75DB](https://via.placeholder.com/9/FF75DB/000000?text=+ "Cor da caixa segurável")) na mão direita
* <kbd>Tab</kbd> - abre um menu e pausa o cronômetro da partida
* <kbd>C</kbd> - marca um checkpoint, desde que o jogador possua 100 moedas que serão descontadas

## Como utilizar esse projeto
Para utilizar esse projeto é necessário ter instalado em sua máquina o [Unity](https://unity3d.com/pt/get-unity/download) e baixar esse respositório, que pode ser clonado através de:
```
https://github.com/mariaeloi/collect-balls-2.git
```
Após isso, abra o projeto no Unity.

### Intruções de Build
Para compilar esse projeto, abra-o no Unity e vá até **File > Build Settings**. Em seguida, selecione ``PC, Mac & Linux Standalone`` e escolha sua plataforma alvo. É importante certificar-se, também, que as cenas estão na seguinte sequência:
1. Scenes/LevelSelect
2. Scenes/Level1
3. Scenes/Level2

Conforme o exemplo: [janela Build Settings](https://drive.google.com/file/d/1vK1xeqSHsNCDv19s_RNq_iS6c4ZZNqia/view?usp=sharing).
