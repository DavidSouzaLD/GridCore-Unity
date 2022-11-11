using System.Collections.Generic;
using UnityEngine;

public static class MeshData
{
    public class Container
    {
        public int vertexIndex = 0;
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangles = new List<int>();
        public List<Vector2> uvs = new List<Vector2>();

        public void Clear()
        {
            vertexIndex = 0;
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
        }
    }

    // Fixed value for creating mesh voxels.
    public static readonly Vector3 fixedPos = new Vector3(0.5f, 0.5f, 0.5f);

    public static readonly float textureAtlasSize = 16;
    public static readonly float textureNormalizedSize = 1 / textureAtlasSize;

    // Vertices values
    public static readonly Vector3[] vertices = new Vector3[8]
    {
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 0.0f, 0.0f),
                new Vector3(1.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 1.0f, 0.0f),
                new Vector3(0.0f, 0.0f, 1.0f),
                new Vector3(1.0f, 0.0f, 1.0f),
                new Vector3(1.0f, 1.0f, 1.0f),
                new Vector3(0.0f, 1.0f, 1.0f),
    };

    // World directions
    public static readonly Vector3Int[] directions = new Vector3Int[6]
    {
                new Vector3Int(0, 0, -1),
                new Vector3Int(0, 0, 1),
                new Vector3Int(0, 1, 0),
                new Vector3Int(0, -1, 0),
                new Vector3Int(-1, 0, 0),
                new Vector3Int(1, 0, 0)
    };

    // Triangles
    public static readonly int[,] triangles = new int[6, 4]
    {
                // Back, Front, Top, Bottom, Left, Right
		        // 0 1 2 2 1 3
		        {0, 3, 1, 2}, // Back Face
		        {5, 6, 4, 7}, // Front Face
		        {3, 7, 2, 6}, // Top Face
		        {1, 5, 0, 4}, // Bottom Face
		        {4, 7, 0, 3}, // Left Face
		        {1, 2, 5, 6} // Right Face
    };

    // List of uv maps
    public static readonly Vector2[] uvs = new Vector2[4] {
                new Vector2 (0.0f, 0.0f),
                new Vector2 (0.0f, 1.0f),
                new Vector2 (1.0f, 0.0f),
                new Vector2 (1.0f, 1.0f)
            };

    public static void CreateVoxel(ref MeshData.Container container, Vector3 position, bool[] faceRemoving)
    {
        for (int i = 0; i < 6; i++)
        {
            if (faceRemoving[i] == false)
            {
                // Vertices
                container.vertices.Add(position - MeshData.fixedPos + MeshData.vertices[MeshData.triangles[i, 0]]);
                container.vertices.Add(position - MeshData.fixedPos + MeshData.vertices[MeshData.triangles[i, 1]]);
                container.vertices.Add(position - MeshData.fixedPos + MeshData.vertices[MeshData.triangles[i, 2]]);
                container.vertices.Add(position - MeshData.fixedPos + MeshData.vertices[MeshData.triangles[i, 3]]);

                // UVS
                AddTexture(ref container, 0);

                // Triangles
                container.triangles.Add(container.vertexIndex);
                container.triangles.Add(container.vertexIndex + 1);
                container.triangles.Add(container.vertexIndex + 2);
                container.triangles.Add(container.vertexIndex + 2);
                container.triangles.Add(container.vertexIndex + 1);
                container.triangles.Add(container.vertexIndex + 3);
                container.vertexIndex += 4;
            }
        }
    }

    // Reworks the UV map based on the Atlas map.
    public static void AddTexture(ref MeshData.Container container, int textureID)
    {
        float y = textureID / MeshData.textureAtlasSize;
        float x = textureID - (y * MeshData.textureAtlasSize);

        x *= MeshData.textureNormalizedSize;
        y *= MeshData.textureNormalizedSize;

        y = 1f - y - MeshData.textureNormalizedSize;

        container.uvs.Add(new Vector2(x, y));
        container.uvs.Add(new Vector2(x, y + MeshData.textureNormalizedSize));
        container.uvs.Add(new Vector2(x + MeshData.textureNormalizedSize, y));
        container.uvs.Add(new Vector2(x + MeshData.textureNormalizedSize, y + MeshData.textureNormalizedSize));
    }
}