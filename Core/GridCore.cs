using UnityEngine;
using GridCore.Utilities;

namespace GridCore
{
    public partial class GridCore<T> : MonoBehaviour
    {
        [Header("GridCore Settings")]
        public Vector3Int gridSize = new Vector3Int(15, 15, 15);
        public Vector3Int cellSize = new Vector3Int(15, 15, 15);
        public Vector3Int offset = Vector3Int.zero;
        [Space]
        public bool showGizmos;

        protected Cell<T>[,,] cells { get; set; }

        private void OnValidate()
        {
            // Update settings
            if (Settings.gridSize != gridSize)
            {
                Settings.gridSize = gridSize;
            }

            if (Settings.cellSize != cellSize)
            {
                Settings.cellSize = cellSize;
            }

            if (Settings.offset != offset)
            {
                Settings.offset = offset;
            }
        }

        protected virtual void Initialize()
        {
            cells = new Cell<T>[Settings.gridSize.x, Settings.gridSize.y, Settings.gridSize.z];

            for (int x = 0; x < Settings.gridSize.x; x++)
            {
                for (int y = 0; y < Settings.gridSize.y; y++)
                {
                    for (int z = 0; z < Settings.gridSize.z; z++)
                    {
                        cells[x, y, z] = new Cell<T>(x, y, z, this);
                    }
                }
            }
        }

        public Cell<T> GetCell3D(Vector3Int position)
        {
            try
            {
                Vector3 pos = (position + GridMath.GridHalf() - GridMath.CellHalf()) - offset;

                int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int y = Mathf.RoundToInt(pos.y / Settings.cellSize.y);
                int z = Mathf.RoundToInt(pos.z / Settings.cellSize.z);

                return cells[x, y, z];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Cell<T> GetCell3D(Vector3 position)
        {
            try
            {
                Vector3 pos = (position + GridMath.GridHalf() - GridMath.CellHalf()) - offset;

                int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int y = Mathf.RoundToInt(pos.y / Settings.cellSize.y);
                int z = Mathf.RoundToInt(pos.z / Settings.cellSize.z);

                return cells[x, y, z];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Cell<T> GetCell2D(Vector2Int position)
        {
            try
            {
                Vector2Int half2D = new Vector2Int(
                    GridMath.GridHalf().x - GridMath.CellHalf().x,
                    GridMath.GridHalf().z - GridMath.CellHalf().z);

                Vector2Int pos = (position + half2D) - new Vector2Int(offset.x, offset.z);

                int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int z = Mathf.RoundToInt(pos.y / Settings.cellSize.z);

                return cells[x, 0, z];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Cell<T> GetCell2D(Vector2 position)
        {
            try
            {
                Vector2 half2D = new Vector2(
                    GridMath.GridHalf().x - GridMath.CellHalf().x,
                    GridMath.GridHalf().z - GridMath.CellHalf().z);

                Vector2 pos = (position + half2D) - new Vector2(offset.x, offset.z);

                int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int z = Mathf.RoundToInt(pos.y / Settings.cellSize.z);

                return cells[x, 0, z];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (showGizmos)
            {
                if (Application.isPlaying)
                {
                    for (int x = 0; x < Settings.gridSize.x; x++)
                    {
                        for (int y = 0; y < Settings.gridSize.y; y++)
                        {
                            for (int z = 0; z < Settings.gridSize.z; z++)
                            {
                                Gizmos.DrawWireCube(cells[x, y, z].centerPosition, Settings.cellSize);
                            }
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < Settings.gridSize.x; x++)
                    {
                        for (int y = 0; y < Settings.gridSize.y; y++)
                        {
                            for (int z = 0; z < Settings.gridSize.z; z++)
                            {
                                Vector3Int position = GridMath.GetCenterCell(x, y, z);
                                Gizmos.DrawWireCube(position, Settings.cellSize);
                            }
                        }
                    }
                }
            }
        }
    }
}
