using UnityEngine;
using GridCore.Utilities;

namespace GridCore
{
    public class Cell<T>
    {
        public GridCore<T> core { get; private set; }

        public int x { get; private set; }
        public int y { get; private set; }
        public int z { get; private set; }

        public T value;

        public Cell(int nX, int nY, int nZ, GridCore<T> gridCore)
        {
            x = nX;
            y = nY;
            z = nZ;
            core = gridCore;
        }

        public Vector3Int nativePosition
        {
            get
            {
                return new Vector3Int(x, y, z);
            }
        }

        public Vector3Int position
        {
            get
            {
                return GridMath.GetPivotCell(x, y, z);
            }
        }

        public Vector3Int centerPosition
        {
            get
            {
                return GridMath.GetCenterCell(new Vector3Int(x, y, z));
            }
        }
    }
}
