
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("上方限制")]
    public float up;
    [Header("下方限制")]
    public float mp;

    private Transform player;

    private void Start()
    {
        player = GameObject.Find("Fox").transform;
    }

    //  在 Update 之後才執行：攝影機追蹤、物件追蹤
    private void LateUpdate()
    {
        Track();
    }
    /// <summary>
    /// 攝影機追蹤效果
    /// </summary>
    private void Track()
    {
        Vector3 porA = player.position;     //  玩家
        Vector3 porB = transform.position;  //  攝影機

        porA.x = 0;         //  固定 X 軸
        porA.y = 14.78f;    //  固定 Y 軸
        porA.z -= 6;        //  放在玩家後方 -=

        porA.z = Mathf.Clamp(porA.z, mp, up);   // 玩家.Z 夾住 下方限制 ~ 上方限制

        //攝影機.座標 = 三圍插植(A座標,B座標,百分比)
        transform.position = Vector3.Lerp(porB, porA, 0.3f * Time.deltaTime * speed);
    }
}
