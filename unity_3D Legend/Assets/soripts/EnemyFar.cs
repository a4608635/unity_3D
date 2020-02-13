﻿using UnityEngine;
using System.Collections;

public class EnemyFar : Enemy
{
    [Header("子彈")]
    public GameObject bullet;
    private Vector3 posBullet;      // 子彈座標

    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(CreateBullet());     // 啟動協程
    }

    private IEnumerator CreateBullet()
    {
        // 子彈座標 = 飛龍.座標 + 飛龍前方 * Z + 飛龍上方 * Y
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        yield return new WaitForSeconds(data.attackDelay);                                  // 等待
        GameObject temp = Instantiate(bullet, posBullet, transform.rotation);               // 區域變數 = 生成(物件，座標，角度)
        temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.bulletPower);      // 取得鋼體.推力(敵人前方 * 力道)

        temp.AddComponent<Bullet>();                                                        // 區域變數.添加物件<任意物件>();
        temp.GetComponent<Bullet>().damage = data.attack;                                   // 區域變數.取得物件<任意物件>().傷害值 = 資料.攻擊力
        temp.GetComponent<Bullet>().player = false;                                   // 區域變數.取得物件<任意物件>().傷害值 = 資料.攻擊力
    }

    private void OnDrawGizmos()
    {
        // 圖示.顏色 = 顏色
        Gizmos.color = Color.red;
        // 子彈座標 = 飛龍.座標 + 飛龍前方 * Z + 飛龍上方 * Y
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        // 圖示.繪製球體(中心點，半徑)
        Gizmos.DrawSphere(posBullet, 0.1f);
    }
}
