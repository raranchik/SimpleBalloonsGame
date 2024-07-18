using System.Collections;
using Core.Balloons.Kinds;

namespace Core.Balloons.Generator
{
    public interface IBalloonsGeneratorStrategy
    {
        string Id { get; }
        void DisposeBalloon(IBalloon balloon);
        IEnumerator GenerateBalloon();
    }
}