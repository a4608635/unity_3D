using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害值
    /// </summary>
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Fox")                            // 如果碰到.名稱 = "Fox"
        {
            other.GetComponent<pleyer>().Hit(damage);       // 取得<玩家>().受傷(傷害值)
        }
    }
}
