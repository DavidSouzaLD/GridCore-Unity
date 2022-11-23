using UnityEngine;
using GridCore.Utilities;

namespace GridCore
{
    public partial class GridCore<T>
    {
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

        public Cell<T> GetCell3D(float x, float y, float z)
        {
            try
            {
                Vector3 pos = (new Vector3(x, y, z) + GridMath.GridHalf() - GridMath.CellHalf()) - offset;

                int finalX = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int finalY = Mathf.RoundToInt(pos.y / Settings.cellSize.y);
                int finalZ = Mathf.RoundToInt(pos.z / Settings.cellSize.z);

                return cells[finalX, finalY, finalZ];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Cell<T> GetCell2D(Vector3 position)
        {
            try
            {
                Vector2 half2D = new Vector2(
                    GridMath.GridHalf().x - GridMath.CellHalf().x,
                    GridMath.GridHalf().z - GridMath.CellHalf().z);

                Vector2 pos = (new Vector2(position.x, position.z) + half2D) - new Vector2(offset.x, offset.z);

                int finalX = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int finalZ = Mathf.RoundToInt(pos.y / Settings.cellSize.z);

                return cells[finalX, 0, finalZ];
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

                int finalX = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int finalZ = Mathf.RoundToInt(pos.y / Settings.cellSize.z);

                return cells[finalX, 0, finalZ];
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public Cell<T> GetCell2D(float x, float z)
        {
            try
            {
                Vector2 half2D = new Vector2(
                    GridMath.GridHalf().x - GridMath.CellHalf().x,
                    GridMath.GridHalf().z - GridMath.CellHalf().z);

                Vector2 pos = (new Vector2(x, z) + half2D) - new Vector2(offset.x, offset.z);

                int finalX = Mathf.RoundToInt(pos.x / Settings.cellSize.x);
                int finalZ = Mathf.RoundToInt(pos.y / Settings.cellSize.z);

                return cells[finalX, 0, finalZ];
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
