using System;
using System.Collections.Generic;
using UnityEngine;

namespace JGajewski.Common
{
    [Serializable]
    public struct Range
    {
        public float min;
        public float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float Clamp(float value) => Mathf.Clamp(value, min, max);

        public float Lerp(float t) => Mathf.Lerp(min, max, t);

        public static Range OffsetRange(Range range, Range offsetRange)
        {
            return new Range(range.min - offsetRange.min, range.max - offsetRange.max);
        }
        
        public static Range OffsetBetweenRanges(Range rangeA, Range rangeB)
        {
            return new Range(rangeA.min - rangeB.min, rangeA.max - rangeB.max);
        }

        public override string ToString()
        {
            return $"Range: Min = {min}, Max = {max}";
        }
    }
    
    [Serializable]
    public struct RangeInt
    {
        public int min;
        public int max;

        public RangeInt(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int Clamp(int value) => Mathf.Clamp(value, min, max);
    }

    public static class RangeExtensions
    {
        public static float Clamp(this float value, Range range)
            => range.Clamp(value);
        
        public static int Clamp(this int value, RangeInt range)
            => range.Clamp(value);

        public static List<float> GetPositionsInRange(this Range range, int positionsCount)
        {
            var positions = new List<float>(positionsCount);
            var positionStep = 1f / (positionsCount - 1); 
            
            positions.Add(range.min);
            for (int i = 1; i < positionsCount - 1; i++)
            {
                var position = Mathf.Lerp(range.min, range.max, positionStep * i);
                positions.Add(position);
            }
            positions.Add(range.max);

            return positions;
        }
    }
}
