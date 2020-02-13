using UnityEngine;
using System.Linq;      // 引用 查詢API - Min、Max 與 ToList 

public class pleyer : MonoBehaviour
{
    [Header("速度與激情"), Range(0, 1000)]
    public float speed = 1;
    [Header("玩家資料")]
    public PleyerData data;
    [Header("子彈")]
    public GameObject bullet;

    private Rigidbody rig;
    private FixedJoystick joystick;
    private Animator ani;                    // 動畫控制器元件
    private Transform tra;                   // 目標物件
    private LevelManager levelManager;       // 關卡管理器
    private HpValueManager hpvalueManager;   // 血條數值管理器
    private Vector3 posBullet;               // 子彈座標
    private float timer;                     // 計時器
    private Enemy[]enemys;                   // 敵人陣列[]:存放所有敵人
    private float[] enemysDis;               // 距離震裂[]:存放所有敵人距離

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

        if (v == 0 && h == 0) Attack();
        
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

    /// <summary>
    /// 復活
    /// </summary>
    public void Revival()
    {
        enabled = true;                                     // 開起此腳本(this 可省略)
        ani.SetBool("死亡開關", false);                     // 死亡腳本
        data.hp = data.hpmax;                               // 恢復血量
        hpvalueManager.SetHp(data.hp, data.hpmax);          // 更新血量(目前，最大)
        levelManager.HideRevival();
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    public void Attack()
    {
        if (timer < data.cd)                // 如果 計時器 < CD
        {
            timer += Time.deltaTime;        // 計時器累加
        }
        else
        {
            timer = 0;                      // 計時器歸零
            ani.SetBool("攻擊觸發", true);  // 攻擊

            // 1.取得所有敵人
            enemys = FindObjectsOfType<Enemy>();
            // 2.取的所有敵人的距離
            enemysDis = new float[enemys.Length];       // 距離陣列 = 新的 浮點數陣列[敵人陣列.數量]

            for (int i = 0; i < enemys.Length; i++)
            {
                enemysDis[i] = Vector3.Distance(transform.position, enemys[i].transform.position);
            }
            // 3.判斷誰最近與面相
            float min = enemysDis.Min();                    // 距離陣列.最小值()
            int index = enemysDis.ToList().IndexOf(min);    // 距離陣列.轉為清單().取得資料的編號(資料) - 清單才能使用
            Vector3 enemyPos = enemys[index].transform.position;
            enemyPos.y = transform.position.y;
            transform.LookAt(enemyPos);
            //發射武器
            posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;    // 子彈座標 = 飛龍.座標 + 飛龍前方 * Z + 飛龍上方 * Y
            Vector3 angle = transform.eulerAngles;                                                              // 三維向量 玩家角度 = 變型.歐拉角度(0-360度)
            Quaternion qua = Quaternion.Euler(angle.x + 0, angle.y, angle.z);                                   // 四元角度 = 四元.歐拉() - 歐拉轉為四元角度
            GameObject temp = Instantiate(bullet, posBullet, qua);                               // 區域變數 = 生成(物件，座標，角度)
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.bulletPower);                      // 取得鋼體.推力(敵人前方 * 力道)
            temp.AddComponent<Bullet>();                                                                        // 區域變數.添加物件<任意物件>();
            temp.GetComponent<Bullet>().damage = data.attack;                                                   // 區域變數.取得物件<任意物件>().傷害值 = 資料.攻擊力
            temp.GetComponent<Bullet>().player = true;                                                   // 區域變數.取得物件<任意物件>().傷害值 = 資料.攻擊力
        }
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
