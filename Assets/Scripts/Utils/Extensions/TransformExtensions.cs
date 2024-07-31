using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3[] GetPositions(this Transform[] transforms)
        {
            List<Vector3> toReturn = new List<Vector3>();
         
            foreach (var transform in transforms)
            {
                toReturn.Add(transform.position);
            }

            return toReturn.ToArray();
        }

        public static Vector3 Position(this GameObject gameObject) => gameObject.transform.position;
    }
}