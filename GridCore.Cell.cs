using System;
using UnityEngine;

namespace GridCore
{
    public class Cell<T>
    {
        private GridCore<T> core;
        private int x, y, z;
        public T value;

        public Cell(Vector3Int position, GridCore<T> gridCore)
        {
            x = position.x;
            y = position.y;
            z = position.z;
            core = gridCore;
        }

        public Vector3Int centerPosition
        {
            get
            {
                return new Vector3Int(x, y, z);
            }
        }

        public Vector3Int rightPosition
        {
            get
            {
                return new Vector3Int(x, y, z) - (core.cellSize / 2);
            }
        }
    }
}
