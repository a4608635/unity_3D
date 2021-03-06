﻿using UnityEngine;

public class LearnArray : MonoBehaviour
{
    public int a = 50;
    public int b = 60;
    public int c = 72;

    // 陣列:儲存相同類型的資料
    // 陣列語法:類型[]

    public int[] numbers;
    public string[] names;
    public Color[] colors;
    public Vector3[] pos;
    public pleyer[] players;    //可儲存程式

    public float[] damages = new float[5];
    public string[] props = new string[3] { "紅水", "藍水", "黃水" };
    public Vector2[] positions = { new Vector2(1, 2), new Vector2(3, 4) };
    public int[] scores = { 99, 0 };

    private void Start()
    {
        // 取得陣列 陣列名稱[編號]
        print("取得到距第一筆資料:" + props[0]);
        // 存放陣列 陣列名稱[編號] = 值
        damages[1] = 99.9f;

        // 陣列長度(數量)
        print("分數陣列長度:" + scores.Length);
    }
}
