using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "Agfeuft/玩家資料")]
public class PleyerData : ScriptableObject
{
    [Header("血量"), Range(200, 10000000)]
    public float hp;
    [Header("最大血量")]
    public float hpmax;
}
