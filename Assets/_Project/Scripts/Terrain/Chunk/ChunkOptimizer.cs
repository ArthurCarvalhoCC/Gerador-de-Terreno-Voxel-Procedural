using UnityEngine;
using System.Collections.Generic;

public static class ChunkOptimizer
{
    public static void CalculateMesh(byte[,,] chunk, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, ref int verticesIndex)
    {
        createMeshData(chunk, vertices, triangles, uvs, ref verticesIndex);
    }

    private static void createMeshData(byte[,,] chunk, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, ref int verticesIndex)
    {
        int chunkSize = WorldManager.chunkSize;
        for (int z = 0; z < chunkSize; z++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int x = 0; x < chunkSize; x++)
                {
                    // Se o bloco possuir um ID diferente de zero (0 costuma ser Ar), calcula suas faces
                    if (chunk[x, y, z] != 0) 
                        addVoxelDataToChunk(new Vector3(x, y, z), vertices, triangles, uvs, ref verticesIndex, chunk);
                }
            }
        }
    }
    private static void addVoxelDataToChunk(Vector3 pos, List<Vector3> vertices, List<int> triangles, List<Vector2> uvs, ref int verticesIndex, byte[,,] chunk)
    {
        // Loop pelas 6 direções possíveis de um cubo (Trás, Frente, Cima, Baixo, Esquerda, Direita)
        for (int f = 0; f < 6; f++)
        { 
            // Se o voxel vizinho nessa direção NÃO for sólido, precisamos desenhar esta face
            if (checkVoxel(pos + VoxelData.FaceChecks[f], chunk) == false)
            { 
                byte blockID = chunk[(int)pos.x, (int)pos.y, (int)pos.z];

                // Adiciona os 4 vértices correspondentes à face atual, transladados para a posição correta do bloco (pos)
                vertices.Add(pos + VoxelData.VoxelVerts[VoxelData.VoxelTriang[f, 0]]);
                vertices.Add(pos + VoxelData.VoxelVerts[VoxelData.VoxelTriang[f, 1]]);
                vertices.Add(pos + VoxelData.VoxelVerts[VoxelData.VoxelTriang[f, 2]]);
                vertices.Add(pos + VoxelData.VoxelVerts[VoxelData.VoxelTriang[f, 3]]);

                // Aplica o mapeamento de textura correto para esta face do bloco
                addTexture(GameManager.Instance.blockTypes[blockID].GetTextureID(f), uvs);

                // Monta os dois triângulos necessários para renderizar o quadrado desta face (padrão horário)
                // Primeiro triângulo:
                triangles.Add(verticesIndex);
                triangles.Add(verticesIndex + 1);
                triangles.Add(verticesIndex + 2);
                
                // Segundo triângulo:
                triangles.Add(verticesIndex + 2);
                triangles.Add(verticesIndex + 1);
                triangles.Add(verticesIndex + 3);
                
                // Incrementa o índice em 4, pois processamos um quadrado (4 vértices adicionados)
                verticesIndex += 4;
            }
        }  
    }
    private static bool checkVoxel(Vector3 position, byte[,,] chunk)
    {
        int chunkSize = WorldManager.chunkSize;
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);
        
        // Se a posição estiver fora dos limites deste Chunk específico, assume que não é sólido 
        // (Nota: Em sistemas avançados, aqui checaríamos os limites do chunk vizinho).
        if (x < 0 || x >= chunkSize || y < 0 || y >= chunkSize || z < 0 || z >= chunkSize) return false;
        
        // Retorna se o tipo de bloco na coordenada é configurado como sólido (bloqueia visão)
        return GameManager.Instance.blockTypes[chunk[x, y, z]].RenderType == 1;
    }
    private static void addTexture(int textureID, List<Vector2> uvs)
    {
        // Calcula a linha (y) e a coluna (x) em que o ID da textura se encontra dentro do Atlas
        float y = textureID / VoxelData.textureAtlasSizeInBlocks;
        float x = textureID - (y * VoxelData.textureAtlasSizeInBlocks);

        // Normaliza os valores brutos transformando-os em escalas fracionadas de 0f a 1f
        x *= VoxelData.normalizedBlockTextureSize;
        y *= VoxelData.normalizedBlockTextureSize;

        // Inverte o eixo Y porque o espaço de coordenadas da Unity começa de baixo, 
        // mas o Atlas geralmente é lido de cima para baixo
        y = 1f - y - VoxelData.normalizedBlockTextureSize;
        
        // Mapeia os 4 cantos da textura quadrada e os adiciona na lista de UVs do Chunk
        uvs.Add(new Vector2(x, y));                                                            // Canto Inferior Esquerdo
        uvs.Add(new Vector2(x, y + VoxelData.normalizedBlockTextureSize));                    // Canto Superior Esquerdo
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y));                    // Canto Inferior Direito
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y + VoxelData.normalizedBlockTextureSize)); // Canto Superior Direito
    }
}
