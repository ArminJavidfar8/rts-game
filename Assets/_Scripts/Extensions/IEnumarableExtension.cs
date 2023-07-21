using System;
using System.Collections;
using System.Collections.Generic;

namespace Extensions
{
    public static class IEnumarableExtension
    {
        public static T GetRandomItem<T>(this IEnumerable items)
        {
            Random random = new Random();
            int count = items.Count();
            int randomIndex = random.Next(count);
            T randomItem = items.GetItemAt<T>(randomIndex);
            return randomItem;
        }

        public static int Count(this IEnumerable objects)
        {
            int count = 0;
            foreach (var item in objects)
            {
                count++;
            }
            return count;
        }

        public static T GetItemAt<T>(this IEnumerable objects, int index)
        {
            int i = 0;
            foreach (T item in objects)
            {
                if (i == index)
                {
                    return item;
                }
                i++;
            }
            return default(T);
        }
    }
}