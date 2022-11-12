using UnityEngine;
using GridCore;
using GridCore.Utilities;

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
        map = new byte[Settings.cellSize.x, Settings.cellSize.y, Settings.cellSize.z];

        Populate();
    }

    public void Populate()
    {
        for (int x = 0; x < Settings.cellSize.x; x++)
        {
            for (int y = 0; y < Settings.cellSize.y; y++)
            {
                for (int z = 0; z < Settings.cellSize.z; z++)
                {
                    Vector3Int pos = new Vector3Int(x, y, z);

                    if (ChunkToWorld(pos).y == world.maxHeight)
                    {
                        map[x, y, z] = world.GetVoxel(pos + GridMath.NativeToWorld(cell.nativePosition));
                        continue;
                    }

                    if (ChunkToWorld(pos).y < world.maxHeight && ChunkToWorld(pos).y >= world.minHeight)
                    {
                        map[x, y, z] = 1;
                    }
                }
            }
        }

        Update();
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

        for (int x = 0; x < Settings.cellSize.x; x++)
        {
            for (int y = 0; y < Settings.cellSize.y; y++)
            {
                for (int z = 0; z < Settings.cellSize.z; z++)
                {
                    Vector3Int voxelPos = new Vector3Int(x, y, z);

                    if (map[x, y, z] > 0)
                    {
                        bool[] faceRemoving = new bool[6];

                        // Removing bottom
                        faceRemoving[3] = true;

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

    public bool ExistsVoxel(Vector3Int checkPosition)
    {
        try
        {
            if (PositionInChunk(checkPosition))
            {
                return map[checkPosition.x, checkPosition.y, checkPosition.z] > 0;
            }
            else
            {
                Vector3Int pos = WorldToChunk(checkPosition);
                return map[pos.x, pos.y, pos.z] > 0;
            }
        }
        catch (System.Exception)
        {
            return false;
        }
    }

    public bool PositionInChunk(Vector3Int position)
    {
        if (position.x < 0 || position.x > Settings.cellSize.x - 1 ||
            position.y < 0 || position.y > Settings.cellSize.y - 1 ||
            position.z < 0 || position.z > Settings.cellSize.z - 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public Vector3Int WorldToChunk(int x, int y, int z)
    {
        return new Vector3Int(x, y, z) - position;
    }

    public Vector3Int WorldToChunk(Vector3Int worldPosition)
    {
        return worldPosition - position;
    }

    public Vector3Int ChunkToWorld(int x, int y, int z)
    {
        return new Vector3Int(x, y, z) + position;
    }

    public Vector3Int ChunkToWorld(Vector3Int chunkPosition)
    {
        return chunkPosition + position;
    }
}
