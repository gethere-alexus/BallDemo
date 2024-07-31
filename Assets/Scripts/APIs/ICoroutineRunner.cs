using System.Collections;
using UnityEngine;

namespace APIs
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}