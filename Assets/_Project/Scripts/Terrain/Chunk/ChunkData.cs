using UnityEngine;

public class ChunkData
{
    private static int chunkLength = WorldManager.chunkSize; // futuramente, pode conter altura e profundidade
    public Vector3Int chunkPosition = new Vector3Int(0,0,0);
    public byte[,,] voxelMap = new byte[chunkLength, chunkLength, chunkLength];

}