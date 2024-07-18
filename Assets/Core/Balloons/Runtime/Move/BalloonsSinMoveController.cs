using Core.Balloons.Kinds;
using Core.Base.Map;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Balloons.Move
{
    public class BalloonsSinMoveController : ITickable
    {
        [Inject] private readonly BaseMap<IBalloon, BalloonSinMoveArgs> m_MoveArgsMap;

        public void Tick()
        {
            if (!m_MoveArgsMap.HasValues())
            {
                return;
            }

            foreach (var moveArgs in m_MoveArgsMap.GetValues())
            {
                var balloon = moveArgs.Balloon;
                var position = balloon.Position;
                position.x = moveArgs.StartX + Mathf.Sin(Time.time * moveArgs.Frequency) * moveArgs.Amplitude;
                position.y += moveArgs.Speed * Time.deltaTime;
                balloon.MoveToPosition(position);
            }
        }
    }
}