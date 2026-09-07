using UnityEngine;
using System.Collections.Generic;

public class ChunkRender
{
    public static Mesh createMesh(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
    {
        Mesh mesh = new Mesh();

        // Altera o formato de indexação para suportar mais de 65 mil vértices por Chunk (permite chunks maiores)
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        // Converte as listas dinâmicas em Arrays obrigatórios da estrutura Mesh
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        // Calcula automaticamente a iluminação e sombreamento baseados na direção das faces
        mesh.RecalculateNormals();

        // Atribui a malha recém-criada ao filtro de renderização do objeto
        return mesh;
    }
}
