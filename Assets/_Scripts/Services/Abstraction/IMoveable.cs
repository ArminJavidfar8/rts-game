using UnityEngine;

namespace Services.Abstraction
{
    public interface IMoveable
    {
        void Move(Vector3 target, float speed);
        void Rotate(Vector3 targetPosition, float speed);
    }
}