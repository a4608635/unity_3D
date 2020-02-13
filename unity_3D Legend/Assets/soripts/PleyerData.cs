using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "Agfeuft/玩家資料")]
public class PleyerData : ScriptableObject
{
    [Header("血量"), Range(200, 10000000)]
    public float hp;
    [Header("最大血量")]
    public float hpmax;
    [Header("子彈位移")]
    public float attackY;
    public float attackZ;
    [Header("攻擊冷卻時間"), Range(0, 1.5F)]
    public float cd;
    [Header("子彈推力")]
    public float bulletPower;
    [Header("攻擊力")]
    public float attack;
}
