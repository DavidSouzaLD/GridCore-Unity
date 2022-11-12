using System.Collections.Generic;
using UnityEngine;
using GridCore;
using GridCore.Utilities;

public class World : GridCore<Chunk>
{
    [Header("World Settings")]
    public Transform test;
    public Material atlasMaterial;

    [Header("Noise Settings")]
    public int minHeight;
    public int maxHeight;
    [Space]
    public float threshold;
    public float noiseScale;
    public float noiseOffset;
    private List<Chunk> activeChunks = new List<Chunk>();

    private void Awake()
    {
        Initialize();

        for (int x = 0; x < Settings.gridSize.x; x++)
        {
            for (int y = 0; y < Settings.gridSize.y; y++)
            {
                for (int z = 0; z < Settings.gridSize.z; z++)
                {
                    Chunk chunk = GetChunk(cells[x, y, z].position);
                    cells[x, y, z].value = chunk;
                }
            }
        }
    }

    private void Update()
    {
        Debug.Log(ExistsVoxel(test.position.ConvertToInt()));
    }

    private void LateUpdate()
    {
        for (int i = 0; i < activeChunks.Count; i++)
        {
            activeChunks[i].Render();

            if (activeChunks[i].needUpdate)
            {
                Debug.Log("Update");
                activeChunks[i].Update();
            }
        }
    }

    public Chunk GetChunk(Vector3Int position)
    {
        Cell<Chunk> cell = GetCell(position);

        if (cell != null)
        {
            Chunk chunk = cell.value;

            if (chunk == null)
            {
                chunk = new Chunk(cell, this, atlasMaterial);
                cell.value = chunk;
                activeChunks.Add(chunk);

                return chunk;
            }
            else
            {
                return chunk;
            }
        }
        else
        {
            return null;
        }
    }

    public bool ExistsVoxel(Vector3Int position)
    {
        Chunk chunk = GetChunk(position);

        if (chunk != null)
        {
            return chunk.ExistsVoxel(position);
        }
        else
        {
            return false;
        }
    }

    public void EditWorld(Vector3Int position, byte type)
    {
        Chunk chunk = GetChunk(position);

        if (chunk != null)
        {
            chunk.EditMap(position, type);
            chunk.needUpdate = true;
        }
    }

    public void EditWorld(Vector3 position, byte type)
    {
        Chunk chunk = GetChunk(position.ConvertToInt());

        if (chunk != null)
        {
            chunk.EditMap(position.ConvertToInt(), type);
            chunk.needUpdate = true;
        }
    }

    public byte GetVoxel(Vector3Int position)
    {
        float noise = Noise.Get2DPerlin(position, noiseScale, noiseOffset, Settings.cellSize);

        if (noise >= threshold)
        {
            return 1;
        }

        return 0;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (Application.isPlaying)
        {
            Cell<Chunk> cell = GetCell(test.position);

            if (cell != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(cell.centerPosition, Settings.cellSize);
            }
        }
    }
}
