using System.Collections.Generic;
using UnityEngine;

namespace JGajewski.Common
{
    public static class CollectionsExtensions
    {
        public static TType[] GetRandomItems<TType>(this TType[] collection, int itemCount)
        {
            var collectionLength = collection.Length;
            var randomItems = new TType[itemCount];
            
            for (int i = 0; i < itemCount; i++)
            {
                randomItems[i] = collection[Random.Range(0, collectionLength)];
            }
            
            return randomItems;
        }
        
        public static TType GetRandomItem<TType>(this TType[] collection)
        {
            var collectionLength = collection.Length;
            var randomItem = collection[Random.Range(0, collectionLength)];
            
            return randomItem;
        }
        
        public static TType[] GetRandomItems<TType>(this List<TType> collection, int itemCount)
        {
            var collectionCount = collection.Count;
            var randomItems = new TType[itemCount];
            
            for (int i = 0; i < itemCount; i++)
            {
                randomItems[i] = collection[Random.Range(0, collectionCount)];
            }
            
            return randomItems;
        }
        
        public static TType GetRandomItem<TType>(this List<TType> collection)
        {
            var collectionCount = collection.Count;
            var randomItem = collection[Random.Range(0, collectionCount)];
            
            return randomItem;
        }
        
        public static TType[] GetRandomItems<TType>(this IReadOnlyList<TType> collection, int itemCount)
        {
            var collectionCount = collection.Count;
            var randomItems = new TType[itemCount];
            
            for (int i = 0; i < itemCount; i++)
            {
                randomItems[i] = collection[Random.Range(0, collectionCount)];
            }
            
            return randomItems;
        }
        
        public static TType GetRandomItem<TType>(this IReadOnlyList<TType> collection)
        {
            var collectionCount = collection.Count;
            var randomItem = collection[Random.Range(0, collectionCount)];
            
            return randomItem;
        }
    }
}
