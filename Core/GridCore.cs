using UnityEngine;
using GridCore.Utilities;

namespace GridCore
{
    public partial class GridCore<T> : MonoBehaviour
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

        private void OnValidate()
        {
            if (Settings.gridSize != gridSize)
            {
                Settings.gridSize = gridSize;
            }

            if (Settings.cellSize != cellSize)
            {
                Settings.cellSize = cellSize;
            }
        }

        protected void Initialize()
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

        public Cell<T> GetCell(Vector3Int position)
        {
            Vector3 pos = position + GridMath.GetFixedPosition() - (Settings.cellSize / 2);

            int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
            int y = Mathf.RoundToInt(pos.y / Settings.cellSize.y);
            int z = Mathf.RoundToInt(pos.z / Settings.cellSize.z);

            return cells[x, y, z];
        }

        public Cell<T> GetCell(Vector3 position)
        {
            Vector3 pos = position + GridMath.GetFixedPosition() - (Settings.cellSize / 2);

            int x = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
            int y = Mathf.RoundToInt(pos.y / Settings.cellSize.y);
            int z = Mathf.RoundToInt(pos.z / Settings.cellSize.z);

            return cells[x, y, z];
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
