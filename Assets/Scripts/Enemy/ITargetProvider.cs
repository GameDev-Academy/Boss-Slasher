using UnityEngine;

namespace Enemy
{
    public interface ITargetProvider
    {
        GameObject GetNearestTarget();

        bool HasAnyTarget();
    }
}