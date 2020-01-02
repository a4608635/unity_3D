﻿using UnityEngine;
using System.Collections;

public class LearnCoroutine : MonoBehaviour
{
    public IEnumerator Test()
    {
        print("執行協成方法");

        yield return new WaitForSeconds(2); // 等待兩秒

        print("兩秒後~");

        yield return new WaitForSeconds(3); // 等待三秒

        print("三秒後~");
    }

    public Transform mouse;

    public IEnumerator Big()
    {
        for (int i = 0; i < 10; i++)
        {
            mouse.localScale += Vector3.one;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        StartCoroutine(Test()); // 啟動協程(協程名稱())
    }
}
