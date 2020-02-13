using UnityEngine;
using UnityEngine.AI;       // 引用 人工智慧 API

// 內容收起 ctrl+m+o
// 內容展開 ctrl+m+l

public class Enemy : MonoBehaviour
{
    [Header("怪物資料")]
    public EnemyData data;

    private Animator ani;                    // 動畫控制器       
    private NavMeshAgent nav;                // 導覽網隔代理器
    private Transform tra;                   // 玩家變形
    private float timer;                     // 計時器
    private HpValueManager hpvalueManager;   // 血條數值管理器


    private void Start()
    {
        ani = GetComponent<Animator>();        
        nav = GetComponent<NavMeshAgent>();     
        nav.speed = data.speed;                 // 調整 代理器.速度
        nav.stoppingDistance = data.stopDistance;

        hpvalueManager = GetComponentInChildren<HpValueManager>();  // 取得子物件元件
        tra = GameObject.Find("Fox").GetComponent<Transform>();     // 取得玩家變形
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 等待
    /// </summary>
    private void Wait()
    {
        ani.SetBool("跑步開關",false);
        timer += Time.deltaTime;
        // print("計時器" + timer);
        if (timer>=data.cd)
        {
            Attack();
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        Vector3 posTra = tra.position;          // 區域三維向量 = 目標.座標
        posTra.y = transform.position.y;        // 三維向量.y = 本身.y
        transform.LookAt(posTra);               // 變形.看著(三維向量)

        nav.SetDestination(tra.position);       // 代理器.設定目的地(玩家)

        // print("剩餘距離" + nav.remainingDistance);

        if (nav.remainingDistance<data.stopDistance)        // 剩餘距離<資料.停止距離
        {
            Wait();
        }
        else
        {
            ani.SetBool("跑步開關", true);
        }
    }

    // protected 保護:允許子類別存取，禁止外部類別存取
    // virtual 虛擬:允許子類別複寫
    /// <summary>
    /// 攻擊
    /// </summary>
    protected  virtual void Attack()
    {
        ani.SetBool("攻擊觸發", true);
        timer = 0;
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收玩家給予的傷害直</param>
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
    private void Death()
    {

    }
}
