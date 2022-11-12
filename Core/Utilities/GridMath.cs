using UnityEngine;
using GridCore;

namespace GridCore.Utilities
{
    public class GridMath : MonoBehaviour
    {
        public static Vector3Int CellHalf()
        {
            return (Settings.cellSize / 2);
        }

        public static Vector3Int GridHalf()
        {
            return ((Settings.gridSize * Settings.cellSize) / 2) + (Settings.cellSize / 2);
        }

        public static Vector3Int NativeToWorld(int x, int y, int z)
        {
            return new Vector3Int(x, y, z) * Settings.cellSize;
        }

        public static Vector3Int NativeToWorld(Vector3Int position)
        {
            return new Vector3Int(position.x, position.y, position.z) * Settings.cellSize;
        }

        public static Vector3Int GetPivotCell(int x, int y, int z)
        {
            Vector3Int fix = GridHalf();
            Vector3Int pos = new Vector3Int(x, y, z) * Settings.cellSize;
            Vector3Int result = pos - fix + Settings.offset;

            return result;
        }

        public static Vector3Int GetPivotCell(Vector3Int position)
        {
            Vector3Int fix = GridHalf();
            Vector3Int pos = position * Settings.cellSize;
            Vector3Int result = pos - fix + Settings.offset;

            return result;
        }
        public static Vector3Int GetCenterCell(int x, int y, int z)
        {
            Vector3Int fix = GridHalf();
            Vector3Int pos = (new Vector3Int(x, y, z) * Settings.cellSize) + (Settings.cellSize / 2);
            Vector3Int result = pos - fix + Settings.offset;

            return result;
        }

        public static Vector3Int GetCenterCell(Vector3Int position)
        {
            Vector3Int fix = GridHalf();
            Vector3Int pos = (position * Settings.cellSize) + (Settings.cellSize / 2);
            Vector3Int result = pos - fix + Settings.offset;

            return result;
        }
    }
}
