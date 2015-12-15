using UnityEngine;
using System.Collections;

namespace MinMax
{

    public class InvalidRangeException : System.Exception
    {
        public InvalidRangeException(float min, float max)
        {
            Debug.LogError("Invalid Range Specified. " + max + " is less than " + min + ".");
        }
    } 

    public struct Range
    {
        public float min, max;
        public float range
        {
            get { return max - min; }
        }

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;

            if (this.max < this.min)
            {
                throw new InvalidRangeException(min, max);
            }
        }

        public static implicit operator Range(Vector2 vector)
        {
            Range vectorRange = new Range(vector.x, vector.y);
            return vectorRange;
        }
        public static implicit operator Vector2(Range range)
        {
            Vector2 rangeVector = new Vector2(range.min, range.max);
            return rangeVector;
        }
        
        public static bool operator < (Range a, Range b)
        {
            if (a.range < b.range)
                return true;
            else
                return false;
        }
        public static bool operator > (Range a, Range b)
        {
            if (a.range > b.range)
                return true;
            else
                return false;
        }
        public static bool operator <= (Range a, Range b)
        {
            if (a.range <= b.range)
                return true;
            else
                return false;
        }
        public static bool operator >= (Range a, Range b)
        {
            if (a.range >= b.range)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return "Range(" + min.ToString("N1") + ", " + max.ToString("N1") + ")";
        }
    }
}
