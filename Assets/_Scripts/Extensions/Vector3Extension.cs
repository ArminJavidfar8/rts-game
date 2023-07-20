using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extension
    {
        public static Vector3 GetRandomPositionOnGround(float range)
        {
            return new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        }
    }
}