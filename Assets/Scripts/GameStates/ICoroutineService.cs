using System.Collections;
using UnityEngine;

public interface ICoroutineService : IService
{
    Coroutine StartCoroutine(IEnumerator routine);
}