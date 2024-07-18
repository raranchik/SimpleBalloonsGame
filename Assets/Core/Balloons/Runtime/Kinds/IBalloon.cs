using UnityEngine;

namespace Core.Balloons.Kinds
{
    public interface IBalloon
    {
        bool IsActive { get; }
        GameObject GameObject { get; }
        Vector3 Position { get; }
        void MoveToPosition(Vector3 position);
        void SetActive(bool isActive);
    }
}