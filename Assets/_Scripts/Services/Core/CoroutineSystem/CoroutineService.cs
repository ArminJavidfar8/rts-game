using Services.Abstraction.CoroutineSystem;
using System.Collections;
using UnityEngine;

namespace Services.Core.CoroutineSystem
{
    public class CoroutineService : ICoroutineService
    {
        private CoroutinesHolder _coroutinesHolder;
        
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (_coroutinesHolder == null)
            {
                _coroutinesHolder = new GameObject("CoroutinesHolder").AddComponent<CoroutinesHolder>();
            }
            return _coroutinesHolder.StartCoroutine(routine);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            _coroutinesHolder.StopCoroutine(routine);
        }
    }
}