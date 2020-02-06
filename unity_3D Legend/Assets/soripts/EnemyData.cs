using UnityEngine;

// ScriptableObject 腳本化物件:將腳本的資存放在專案內(不需要掛在物件上)

[CreateAssetMenu(fileName = "怪物資料", menuName = "Agfeuft/怪物資料")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度"),Range(0,100)]
    public float speed;
    [Header("生命"), Range(100, 10000)]
    public float hp;
    [Header("攻擊"), Range(1, 100)]
    public float attack;
    [Header("冷卻時間"), Range(1, 10)]
    public float cd;
    [Header("停止距離"), Range(0.1F, 100)]
    public float stopDistance;

    [Header("近距離單位")]
    public float attackY;
    public float attackLength;
    public float attackDelay;

    [Header("遠距離子彈位子")]
    public float attackZ;
    [Header("子彈速度")]
    public int bulletPower;
}
