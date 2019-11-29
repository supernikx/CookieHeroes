using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineController : MonoBehaviour
{
    private static CoroutineController i;

    private void Awake()
    {
        i = this;
    }

    public static IEnumerator StartRoutine(Action _callback, float _time)
    {
        IEnumerator routine = i.DelayCoroutine(_callback, _time);
        i.StartCoroutine(routine);
        return routine;
    }

    private static void StopRoutine(IEnumerator _routine)
    {
        if (_routine != null)
            i.StopCoroutine(_routine);
    }

    private IEnumerator DelayCoroutine(Action _callback, float _time)
    {
        yield return new WaitForSeconds(_time);
        _callback?.Invoke();
    }
}
