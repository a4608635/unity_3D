
using UnityEngine;

public class pleyer : MonoBehaviour
{
    [Header("速度與激情"), Range(0, 1000)]
    public float speed = 1;

    public Rigidbody rig;
    public FixedJoystick joystick;
    public Animator ani;// 動畫控制器元件

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();// 取得元件<動畫控制器>()
        joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
    }
    // 一秒執行50次-用來處理物理系統
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        rig.AddForce(h * speed, 0, v * speed);

        ani.SetBool("跑步開關", v != 0 || h != 0);
        // 動畫控制器.設定布林植("參數名稱",布林植)
        
    }
}
