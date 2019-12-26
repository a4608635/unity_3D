
using UnityEngine;

public class SAO : MonoBehaviour
{
    public float a = 0, b = 10,numbA=0,numbB=100;
    public Vector2 v2A = new Vector2(0, 0), v2B = new Vector2(100, 100);

    public Color cA, cB, cC;

    public Transform cube, sphere;

    public int hp = 50;

    private void Start()
    {
        print(Mathf.Lerp(a, b, 0.8f));

        print(Vector2.Lerp(v2A, v2B, 0.5f));

        cC = Color.Lerp(cA, cB, 0.7F);

        print(Mathf.Clamp(hp, 0, 100));
    }

    private void Update()
    {
        numbA = Mathf.Lerp(numbA, numbB, 0.3f * Time.deltaTime);

        cube.position = Vector3.Lerp(cube.position, sphere.position, 0.3f * Time.deltaTime);
    }

}
