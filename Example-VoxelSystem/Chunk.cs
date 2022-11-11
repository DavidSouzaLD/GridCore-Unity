using UnityEngine;
using GridCore;

public class Chunk
{
    // Core
    private World world;
    private Cell<Chunk> cell;

    // Chunk
    private int x, y, z;
    private byte[,,] map;

    // Mesh
    public Material material { get; private set; }
    public Mesh mesh { get; private set; }

    // Components
    public GameObject gameObject { get; private set; }
    public MeshCollider collider { get; private set; }

    public bool needUpdate;

    public Vector3Int position
    {
        get
        {
            return cell.position;
        }
    }

    public Chunk(Cell<Chunk> chunkCell, World world1, Material atlasMaterial)
    {
        // Setting
        world = world1;
        cell = chunkCell;
        material = atlasMaterial;

        // Creating object
        gameObject = new GameObject();
        gameObject.name = "Chunk [X:" + cell.position.x + " / " + cell.position.y + " / " + cell.position.z + "]";
        gameObject.transform.parent = world.transform;
        gameObject.transform.position = cell.position;
        collider = gameObject.AddComponent<MeshCollider>();

        // Generating map
        map = new byte[world.cellSize.x, world.cellSize.y, world.cellSize.z];
    }

    public void Render()
    {
        if (mesh != null)
        {
            Graphics.DrawMesh(mesh, position, Quaternion.identity, material, 0);
        }
    }

    public void Update()
    {
        // Mesh
        Mesh newMesh = CreateMesh();
        mesh = newMesh;
        collider.sharedMesh = newMesh;
        needUpdate = false;
    }

    private Mesh CreateMesh()
    {
        MeshData.Container container = new MeshData.Container();

        for (int x = 0; x < world.cellSize.x; x++)
        {
            for (int y = 0; y < world.cellSize.y; y++)
            {
                for (int z = 0; z < world.cellSize.z; z++)
                {
                    Vector3Int voxelPos = new Vector3Int(x, y, z);

                    if (map[x, y, z] > 0)
                    {
                        bool[] faceRemoving = new bool[6];

                        for (int f = 0; f < 6; f++)
                        {
                            Vector3Int voxelToCheck = voxelPos + MeshData.directions[f];

                            if (ExistsVoxel(voxelToCheck))
                            {
                                faceRemoving[f] = true;
                            }
                        }

                        MeshData.CreateVoxel(ref container, voxelPos, faceRemoving);
                    }
                }
            }
        }

        // Create mesh
        Mesh mesh1 = new Mesh();
        mesh1.vertices = container.vertices.ToArray();
        mesh1.triangles = container.triangles.ToArray();
        mesh1.uv = container.uvs.ToArray();

        mesh1.RecalculateBounds();
        mesh1.RecalculateTangents();
        mesh1.RecalculateNormals();

        return mesh1;
    }

    public void EditMap(Vector3Int position, byte type)
    {
        Vector3Int pos = WorldToChunk(position);
        map[pos.x, pos.y, pos.z] = type;
    }

    public bool ExistsVoxel(Vector3Int position)
    {
        try
        { return map[position.x, position.y, position.z] > 0; }
        catch (System.Exception)
        { return false; }
    }

    public Vector3Int WorldToChunk(Vector3Int worldPosition)
    {
        return worldPosition - position;
    }
}