using System;
using UnityEngine;

namespace Services.Abstraction
{
    public interface IMoveable
    {
        void Move(Vector3 target);
        void Rotate(Vector3 targetPosition);
    }
}