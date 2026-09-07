using UnityEngine;

public static class BiomeManager
{
    private static float scale = 0.005f;
    private static int octaves = 3;
    private static float persistence = 0.5f;
    private static int maxWorldHeight = 128;

    // Retorna qual bloco deve existir nessa coordenada Y específica do mundo
    public static byte GetBlockTypeAtPosition(int globalX, int globalY, int globalZ)
    {
        // 1. Pega a altura do terreno (0.0 a 1.0) e escala para a altura máxima do mundo
        float noiseValue = NoiseGenerator.GetHeight2D(globalX, globalZ, scale, octaves, persistence);
        int surfaceHeight = Mathf.FloorToInt(noiseValue * maxWorldHeight);

        // 2. Decide o bloco baseado na altura (Y)
        if (globalY > surfaceHeight)
        {
            return 0; // Ar
        }
        if (globalY == surfaceHeight)
        {
            return 1; // Grama (ID fictício)
        }
        if (globalY < surfaceHeight && globalY > surfaceHeight - 10)
        {
            return 2; // Terra (ID fictício)
        }

        return 3; // Pedra Profunda (ID fictício)
    }
}