using UnityEngine;

public static class VoxelData
{
    public const int chunkSize = 16;

    public static readonly int textureAtlasSizeInBlocks = 4; // Muda de acordo com o tamanho da do Atlas/Textura final
    public static float normalizedBlockTextureSize
    {
        get { return 1f / (float)textureAtlasSizeInBlocks; }
    }
    #region Simple Block Vertices, Triangles and Face Order
    #region Full Block
    public static readonly Vector3[] VoxelVerts = new Vector3[8] // Vértices de um cubo completo
    {
        new Vector3(0f, 0f, 0f), // Ponto 0 
        new Vector3(1f, 0f, 0f), // Ponto 1
        new Vector3(1f, 1f, 0f), // Ponto 2
        new Vector3(0f, 1f, 0f), // Ponto 3
        new Vector3(0f, 0f, 1f), // Ponto 4
        new Vector3(1f, 0f, 1f), // Ponto 5
        new Vector3(1f, 1f, 1f), // Ponto 6
        new Vector3(0f, 1f, 1f)  // Ponto 7
    };

    public static readonly Vector3[] FaceChecks = new Vector3[6]
    {
        new Vector3(0f, 0f, -1f), // Ponto 0
        new Vector3(0f, 0f, 1f), // Ponto 0
        new Vector3(0f, 1f, 0f), // Ponto 0
        new Vector3(0f, -1f, 0f), // Ponto 0
        new Vector3(-1f, 0f, 0f), // Ponto 0
        new Vector3(1f, 0f, 0f), // Ponto 0
    };

    public static readonly int[,] VoxelTriang = new int[6,4]
    { 
        {0, 3, 1, 2}, // Face de Trás 
        {5, 6, 4, 7}, // Face de Frente 
        {3, 7, 2, 6}, // Face de Cima 
        {1, 5, 0, 4}, // Face de Baixo 
        {4, 7, 0, 3}, // Face da Esquerda 
        {1, 2, 5, 6} // Face da Direita
        
    };

    public static readonly Vector2[] VoxelUvs = new Vector2[4]
    {
        new Vector2(0f,0f),
        new Vector2(0f,1f),
        new Vector2(1f,0f),
        new Vector2(1f,1f)
    };
    #endregion
    #endregion
    public enum BlockRenderType // no futuro, isso vai dividir os vértices de renderização em malhas diferentes
    {
        Air,
        Opaque,
        Chutout,
        Transparent,
        Liquid,

    }
}
