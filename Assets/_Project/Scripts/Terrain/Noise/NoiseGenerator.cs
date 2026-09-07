using UnityEngine;
using Unity.Mathematics;
public static class NoiseGenerator
{
    // Gera uma altura suavizada usando Perlin Noise
    public static float GetHeight2D(float x, float z, float scale, int octaves, float persistence)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;  // Usado para normalizar o resultado

        for (int i = 0; i < octaves; i++)
        {
            // O sample do Perlin Noise nativo da Unity
            float sampleX = x * scale * frequency;
            float sampleZ = z * scale * frequency;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ);
            total += perlinValue * amplitude;

            maxValue += amplitude;
            amplitude *= persistence;
            frequency *= 2;
        }

        return total / maxValue; // Retorna um valor estritamente entre 0 e 1
    }
}