using UnityEngine;

namespace GridCore.Utilities
{
    public static class GridMath
    {
        public static Vector3Int GetFixedPosition(Vector3Int gridSize, Vector3Int cellSize)
        {
            return ((gridSize * cellSize) / 2) + (cellSize / 2);
        }

        public static Vector3Int GetFixedCellPosition(int x, int y, int z, Vector3Int gridSize, Vector3Int cellSize)
        {
            Vector3Int fix = GetFixedPosition(gridSize, cellSize);
            Vector3Int pos = new Vector3Int(x, y, z) * cellSize;
            Vector3Int result = pos - fix;

            return result;
        }

        public static Vector3Int GetFixedCellPosition(Vector3Int position, Vector3Int gridSize, Vector3Int cellSize)
        {
            Vector3Int fix = GetFixedPosition(gridSize, cellSize);
            Vector3Int pos = position * cellSize;
            Vector3Int result = pos - fix;

            return result;
        }
        public static Vector3Int GetFixedCellCenterPosition(int x, int y, int z, Vector3Int gridSize, Vector3Int cellSize)
        {
            Vector3Int fix = GetFixedPosition(gridSize, cellSize);
            Vector3Int pos = (new Vector3Int(x, y, z) * cellSize) + (cellSize / 2);
            Vector3Int result = pos - fix;

            return result;
        }

        public static Vector3Int GetFixedCellCenterPosition(Vector3Int position, Vector3Int gridSize, Vector3Int cellSize)
        {
            Vector3Int fix = GetFixedPosition(gridSize, cellSize);
            Vector3Int pos = (position * cellSize) + (cellSize / 2);
            Vector3Int result = pos - fix;

            return result;
        }
    }
}