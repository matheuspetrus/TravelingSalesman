# TravelingSalesman

#### Apresentação ####

Grupo: Matheus Petrus, Gabriel pedroso, Leonardo

Neste trabalho  é apresentado uma solução para o famoso problema do caixeiro viajante, usando algoritmos genéticos.

O Problema do Caixeiro Viajante é um problema que tenta determinar a menor rota para percorrer uma série de cidades (visitando uma única vez cada uma delas), retornando à cidade de origem. Ele é um problema de otimização difícil inspirado na necessidade dos vendedores em realizar entregas em diversos locais (as cidades) percorrendo o menor caminho possível, reduzindo o tempo necessário para a viagem e os possíveis custos com transporte e combustível.

![Imagem1](/ArquivosReadme/4.mp4?raw=true)

Primeiramente é gerado uma sequência de caminhos aleatórios, após esse sorteio de caminhos, ele pega o primeiro e o segundo caminhos com a menor distância e gera uma um novo caminho juntando os dois, usando a primeira metade das cidades da lista do melhor caminho e completando o restante de acordo com a lista do segundo melhor caminho.

![Imagem1](/ArquivosReadme/2.jpg?raw=true)

As mutações ocorrem de acordo com o que foi pré-definido pelo usuário e ela funciona fazendo a troca da posição das cidades na lista, e se essa mutação gerar uma nova melhor distância ele vai passar para a nova rodada de testes, se não vai ser descartada.

![Imagem1](/ArquivosReadme/3.jpg?raw=true)

O projeto tem uma interface simples que possibilita diversos  cenários para testes 


![Imagem1](/ArquivosReadme/1.png?raw=true)


Link Download Build:
https://drive.google.com/file/d/1_cR-zBIvPPUtYKZUJHzblWCN9oFuXmVo/view?usp=share_link
