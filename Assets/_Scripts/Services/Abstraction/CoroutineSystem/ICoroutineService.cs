using System.Collections;
using UnityEngine;

namespace Services.Abstraction.CoroutineSystem
{
    public interface ICoroutineService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(IEnumerator routine);
    }
}