using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core.Base.CoroutineRunner
{
    public class PlainCoroutineRunner : ICoroutineRunner, IDisposable
    {
        private readonly MonoCoroutineRunner m_Mono = new GameObject(nameof(MonoCoroutineRunner))
            .AddComponent<MonoCoroutineRunner>();

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return m_Mono.StartCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            m_Mono.StopCoroutine(routine);
        }

        public void Dispose()
        {
            Object.Destroy(m_Mono);
        }
    }
}