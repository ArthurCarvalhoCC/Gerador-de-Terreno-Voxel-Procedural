using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    public ChunkData chunkData = new ChunkData();
    // Criação
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();
    private int verticesIndex = 0;
    
    // Renderização
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
      
    void Start()
    { 
        chunkData.voxelMap = populateVoxelMap(transform.position);
        //ChunkOptimizer;
        ChunkOptimizer.CalculateMesh(chunkData.voxelMap, vertices, triangles, uvs, ref verticesIndex);
        //ChunkRender;
        meshFilter.mesh = ChunkRender.createMesh(vertices,triangles,uvs);
    }
    byte[,,] populateVoxelMap(Vector3 chunkGlobalPos)
    {
        int chunkSize = WorldManager.chunkSize;
        byte[,,] chunk = new byte[chunkSize, chunkSize, chunkSize];
        for (int z = 0; z < chunkSize; z++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int x = 0; x < chunkSize; x++)
                {
                    int globalX = (int)chunkGlobalPos.x + x;
                    int globalY = (int)chunkGlobalPos.y + y;
                    int globalZ = (int)chunkGlobalPos.z + z;

                    // Preenchimento via Noise externo
                    byte blockID = BiomeManager.GetBlockTypeAtPosition(globalX, globalY, globalZ);

                    chunk[x, y, z] = blockID;

                    // Nota: No futuro, a geração usará mapas de ruído (ex: Perlin Noise) baseado na posição global do 

                }
            }
        }
        return chunk;
    }
}   