using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害值
    /// </summary>
    public float damage;
    /// <summary>
    /// 儲存武器，true玩家的，false怪物的
    /// </summary>
    public bool player;

    private void OnTriggerEnter(Collider other)
    {
        if (!player && other.name == "Fox")                            // 如果碰到.名稱 = "Fox"
        {
            other.GetComponent<pleyer>().Hit(damage);       // 取得<玩家>().受傷(傷害值)
            Destroy(gameObject);
        }
        else if (player && other.tag == "敵人" && other.GetComponent<Enemy>())                       // 如果碰到.名稱 = "Fox"
        {
            other.GetComponent<Enemy>().Hit(damage);       // 取得<玩家>().受傷(傷害值)
            Destroy(gameObject);
        }
    }
}
