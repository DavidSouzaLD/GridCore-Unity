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
            UpdateSettings();
        }

        protected virtual void Initialize()
        {
            UpdateSettings();

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

        protected void UpdateSettings()
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
