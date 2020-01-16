using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    // override 複寫:複寫父類別有 virtual 的成員
    protected override void Attack()
    {
        // 父類別原本的敘述或演算法
        base.Attack();

        StartCoroutine(AttackDelay());  
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(data.attackDelay);

        RaycastHit hit;     // 區域變數 碰撞資訊:用來存放射線打到的物件

        // out 參數修飾詞:保存方法的資訊在區域變數內
        // 物理.射線(起點，方向，碰撞資訊，長度)
        if (Physics.Raycast(transform.position + Vector3.up * data.attackY, transform.forward ,out hit, data.attackLength))
        {
            hit.collider.GetComponent<pleyer>().Hit(data.attack);
        } 
    }

    // 繪製圖示:只會在場景內顯示，開發者觀看
    private void OnDrawGizmos()
    {
        // 圖示.顏色 = 顏色
        Gizmos.color = Color.red;
        
        // 前方 Z transform.forward
        // 右方 X transform.right
        // 上方 y transform.up
        
        // 圖示.繪製射線 (起點，方向)
        Gizmos.DrawRay(transform.position+Vector3.up*data.attackY, transform.forward*data.attackLength);
    }
}
