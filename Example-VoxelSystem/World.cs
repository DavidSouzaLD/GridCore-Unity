using System.Collections.Generic;
using UnityEngine;
using GridCore;

public class World : GridCore<Chunk>
{
    public Material atlasMaterial;
    public List<Chunk> currentChunks = new List<Chunk>();

    private void Awake()
    {
        Initialize();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < currentChunks.Count; i++)
        {
            currentChunks[i].Render();

            if (currentChunks[i].needUpdate)
            {
                Debug.Log("Update");
                currentChunks[i].Update();
            }
        }
    }

    public Chunk GetChunk(Vector3Int position)
    {
        Cell<Chunk> cell = GetCell(position);
        Chunk chunk = cell.value;

        if (chunk == null)
        {
            chunk = new Chunk(cell, this, atlasMaterial);
            cell.value = chunk;
            currentChunks.Add(chunk);

            return chunk;
        }
        else
        {
            return chunk;
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

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}