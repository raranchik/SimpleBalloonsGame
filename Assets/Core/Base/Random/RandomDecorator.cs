namespace Core.Base.Random
{
    public class RandomDecorator
    {
        public float Range(float minInclusive, float maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }

        public int Range(int minInclusive, int maxInclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxInclusive);
        }
    }
}