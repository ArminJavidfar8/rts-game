using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ArrayExtension
    {
        public static T GetRandomItem<T>(this T[] array)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
    }
}