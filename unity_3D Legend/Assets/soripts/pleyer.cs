using UnityEngine;

public class pleyer : MonoBehaviour
{
    [Header("速度與激情"), Range(0, 1000)]
    public float speed = 1;
    [Header("玩家資料")]
    public PleyerData data;

    private Rigidbody rig;
    private FixedJoystick joystick;
    private Animator ani;                    // 動畫控制器元件
    private Transform tra;                   // 目標物件
    private LevelManager levelManager;       // 關卡管理器
    private HpValueManager hpvalueManager;   // 血條數值管理器

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();// 取得元件<動畫控制器>()
        joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();

        //tra = GameObject.Find("目標").GetComponent<Transform>(); 以下簡寫
        tra = GameObject.Find("目標").transform;

        levelManager = FindObjectOfType<LevelManager>();            // 透過類行尋找物件(場景上只有一個)
        hpvalueManager = GetComponentInChildren<HpValueManager>();  // 取得子物件元件
    }
    // 一秒執行50次-用來處理物理系統
    private void FixedUpdate()
    {
        Move();
        
    }

    // 碰到物件身上有 IsTrigger 碰撞器執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            　StartCoroutine(levelManager.NextLevel());    // 協程辦法必須用啟動協程
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        rig.AddForce(h * speed, 0, v * speed);

        ani.SetBool("跑步開關", v != 0 || h != 0);

        // 動畫控制器.設定布林植("參數名稱",布林植)

        Vector3 por = transform.position;                               // 玩家座標 = 變形.座標
        tra.position = new Vector3(por.x + h, 0.03F, por.z + v);        // 目標.座標 =  新 三圍向量(玩家.X,水平,玩家.Z)

        //transform.LookAt(traAT); 吃土用

        Vector3 porAT = new Vector3(tra.position.x, transform.position.y, tra.position.z);
        transform.LookAt(porAT);
        
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    public void Hit(float damage)
    {
        if (ani.GetBool("死亡開關")) return;                                    // 如果 死亡開關 是勾選 跳出
        data.hp -= damage;
        hpvalueManager.SetHp(data.hp, data.hpmax);                              // 更新血量(目前，最大)
        StartCoroutine(hpvalueManager.Showvalue(damage, "-", Color.white));     // 啟動協程
        if (data.hp <= 0) Death();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        
        ani.SetBool("死亡開關",true);       // 死亡腳本
        enabled = false;                    // 關閉此腳本(this 可省略)

        StartCoroutine(levelManager.ShowRevival());
    }
}
