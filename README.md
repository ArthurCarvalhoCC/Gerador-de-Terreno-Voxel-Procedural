# Voxel Engine: Gerador de Terreno Procedural

> Status do Projeto: Em Desenvolvimento

## Sobre o Projeto
Este projeto é um motor voxel desenvolvido na Unity focado na geração procedural de cenários e otimização de malhas 3D. O sistema cria mundos dinâmicos em blocos divididos em "Chunks", utilizando algoritmos de ruído para simular relevos naturais e otimização de renderização de faces.

---

## Funcionalidades e Sistemas

* **Geração de Terreno Procedural:** Utiliza o algoritmo Perlin Noise (Mathf.PerlinNoise) com suporte a múltiplas oitavas (octaves), escala e persistência para gerar mapas de altura dinâmicos.
* **Sistema de Biomas Baseado em Altura:** O motor define automaticamente os tipos de blocos (como Ar, Grama, Terra e Pedra Profunda) dependendo da elevação do terreno gerada pelo ruído no eixo Y.
* **Otimização de Malha (Meshing):** O algoritmo de renderização verifica os blocos adjacentes e desenha apenas as faces visíveis (externas) dos voxels. Isso reduz drasticamente a carga de processamento geométrico.
* **Mapeamento UV e Texture Atlas:** Calcula matematicamente as coordenadas UV de cada face do cubo para ler texturas de um único Atlas 2D. O sistema permite aplicar texturas diferentes para o topo, base e laterais de um mesmo bloco.
* **Suporte a Alta Densidade de Vértices:** O renderizador de malha foi configurado com IndexFormat.UInt32, permitindo gerar Chunks robustos que ultrapassam o limite padrão de 65 mil vértices da Unity.
* **Gerenciamento em Grid:** O mundo é dividido em partições de 16x16x16 blocos (chunkSize = 16), gerados organizadamente dentro de uma distância de renderização configurável.

---

## Arquitetura do Código

O projeto foi estruturado com forte separação de responsabilidades para manter o desempenho e facilitar a manutenção:

* **WorldManager.cs:** Responsável por inicializar o mundo, calculando as posições globais e instanciando os Chunks necessários.
* **Chunk.cs e ChunkData.cs:** Gerenciam a matriz de dados 3D (byte[,,]) de cada setor do mapa e orquestram a construção da malha.
* **ChunkOptimizer.cs:** O núcleo da performance gráfica. Contém a lógica matemática para calcular vértices, triângulos e UVs apenas para os blocos que estão expostos ao jogador.
* **VoxelData.cs:** Uma base de dados estática que armazena os vértices brutos de um cubo geométrico, a ordem de desenho das 6 faces e cálculos do Atlas de textura.
* **BiomeGenerator.cs e NoiseGenerator.cs:** Isola toda a lógica de relevo, separando a matemática do ruído das regras de tipo de material de cada bioma.
* **ChunkRender.cs:** Pega os dados brutos calculados e os converte em um objeto Mesh nativo da Unity, recalculando as normais para a iluminação.

---

## Como Executar

Para rodar este projeto localmente, você precisará da Unity instalada. 

1. Clone o repositório utilizando o terminal (via GitHub CLI):
   ```bash
   gh repo clone seu-usuario/nome-do-repositorio
   ```
2. Abra o Unity Hub e adicione o projeto a partir da pasta clonada.
3. Abra a cena principal e execute o projeto para visualizar a geração procedural ocorrendo em tempo real.

---

## Licença
Este projeto é de cunho educacional.