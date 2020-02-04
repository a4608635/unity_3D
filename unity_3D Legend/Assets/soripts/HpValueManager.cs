using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpValueManager : MonoBehaviour
{
    private Image hpBar;
    private Text hptext;
    private RectTransform hpRect;

    private void Start()
    {
        hpBar = transform.GetChild(1).GetComponent<Image>();
        hptext = transform.GetChild(2).GetComponent<Text>();
        hpRect = transform.GetChild(2).GetComponent<RectTransform>();
    }

    private void Update()
    {
        FixedAngle();
        
    }

    /// <summary>
    /// 固定角度
    /// </summary>
    private void FixedAngle()
    {
        transform.eulerAngles = new Vector3(55, 0, 0);
    }

    /// <summary>
    /// 更新血量
    /// </summary>
    /// <param name="hpCurrent">目前血量</param>
    /// <param name="hpMax">最大血量</param>
    public void SetHp(float hpCurrent,float hpMax)
    {
        hpBar.fillAmount = hpCurrent / hpMax;
    }

    public IEnumerator Showvalue(float value, string mark, Color color)
    {
        hptext.text = mark + value;                     // 更新文字
        color.a = 0;                                
        hptext.color = color;
        hpRect.anchoredPosition = Vector2.up * 70;

        for (int i = 0; i < 20; i++)
        {
            hptext.color += new Color(0, 0, 0, 0.05f);
            hpRect.anchoredPosition += Vector2.up * 5;
            yield return new WaitForSeconds(0.05f);
        }

        hptext.color = new Color(0, 0, 0, 0);
    }
}
