using UnityEngine;

namespace GridCore
{
    public class GridCore<T> : MonoBehaviour
    {
        [Header("GridCore Settings")]
        public Vector3Int gridSize = new Vector3Int(15, 15, 15);
        public Vector3Int cellSize = new Vector3Int(15, 15, 15);
        public bool showGizmos;

        protected Cell<T>[,,] cells
        {
            get;
            private set;
        }

        protected void Initialize()
        {
            cells = new Cell<T>[gridSize.x, gridSize.y, gridSize.z];

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    for (int z = 0; z < gridSize.z; z++)
                    {
                        Vector3Int pos = GridMath.GetFixedCellPosition(x, y, z, gridSize, cellSize);
                        cells[x, y, z] = new Cell<T>(pos, this);
                    }
                }
            }
        }

        public Cell<T> GetCell(Vector3Int position)
        {
            Vector3 pos = position + GridMath.GetFixedPosition(gridSize, cellSize) - (cellSize / 2);

            int x = Mathf.RoundToInt(pos.x / cellSize.x);
            int y = Mathf.RoundToInt(pos.y / cellSize.y);
            int z = Mathf.RoundToInt(pos.z / cellSize.z);

            return cells[x, y, z];
        }

        public Cell<T> GetCell(Vector3 position)
        {
            Vector3 pos = position + GridMath.GetFixedPosition(gridSize, cellSize) - (cellSize / 2);

            int x = Mathf.RoundToInt(pos.x / cellSize.x);
            int y = Mathf.RoundToInt(pos.y / cellSize.y);
            int z = Mathf.RoundToInt(pos.z / cellSize.z);

            return cells[x, y, z];
        }

        protected virtual void OnDrawGizmos()
        {
            if (showGizmos)
            {
                if (Application.isPlaying)
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        for (int y = 0; y < gridSize.y; y++)
                        {
                            for (int z = 0; z < gridSize.z; z++)
                            {
                                Gizmos.DrawWireCube(cells[x, y, z].centerPosition, cellSize);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < gridSize.x; x++)
                    {
                        for (int y = 0; y < gridSize.y; y++)
                        {
                            for (int z = 0; z < gridSize.z; z++)
                            {
                                Vector3Int position = GridMath.GetFixedCellCenterPosition(x, y, z, gridSize, cellSize);
                                Gizmos.DrawWireCube(position, cellSize);
                            }
                        }
                    }
                }
            }
        }
    }
}
