using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Material material;
    public BlockType[] blockTypes;

    void Awake()
    {
        Instance = this;
    }
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public byte RenderType;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int GetTextureID(int faceIndex)
    {
        // tras, frente, cima, baixo, esquerda, direita
        switch (faceIndex)
        {
            case 0: return backFaceTexture;
            case 1: return frontFaceTexture;
            case 2: return topFaceTexture;
            case 3: return bottomFaceTexture;
            case 4: return leftFaceTexture;
            case 5: return rightFaceTexture;
            default: Debug.Log("Erro em GetTextureID: faceIndex Inválido"); return 0;
        }
    }
}