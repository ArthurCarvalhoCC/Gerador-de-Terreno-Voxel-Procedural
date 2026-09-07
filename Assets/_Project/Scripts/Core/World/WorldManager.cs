using UnityEngine;
using System.Collections.Generic;
using System;
// using System.Numerics; // O foco do Vector3 é pra cálculos brutos (noise)
public class WorldManager : MonoBehaviour
{
    public GameObject chunkPreFab;
    public static int chunkSize = 16; 
    public const int renderDistance = 5;
    public const int maxHeight = 256;
    public const int minHeight = 0;
    public ChunkData[,,] LoadedChunks = new ChunkData[renderDistance,renderDistance,renderDistance]; // ainda não vou usar
    void Start()
    {
        SpawnBeginChunks(renderDistance, LoadedChunks, 32); // OFF SET PRA TESTE
    }

    void SpawnBeginChunks(int distance, ChunkData[,,] finalChunks, int heightOffSet)
    {
        for (int z = 0; z < distance; z++)
        {
            for (int y = 0; y < distance; y++)
            {
                for (int x = 0; x < distance; x++)
                {
                    finalChunks[x,y,z] = new ChunkData(); // Cria a base de dados da Chunk
                    
                    Vector3 globalPosition = new Vector3(x * chunkSize, y * chunkSize + heightOffSet, z * chunkSize); // Define a posição global da chunk

                    GameObject novoChunk = Instantiate(chunkPreFab, globalPosition, Quaternion.identity);

                    Chunk scriptChunk = novoChunk.GetComponent<Chunk>();

                    scriptChunk.chunkData = finalChunks[x, y, z];
                }
            }
        }
    }
}
