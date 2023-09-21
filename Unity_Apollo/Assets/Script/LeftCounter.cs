using UnityEngine;

public class LeftCounter : MonoBehaviour
{
    [SerializeField]
    private PixelObject FlagPrefab;
    [SerializeField]
    private int max = 30;
    [SerializeField]
    private int colmun = 10;
    [SerializeField]
    private Vector2Int size = new Vector2Int(8,8);

    private PixelObject[] icons;

    public void Initialize()
    {
        icons = new PixelObject[max];
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = Instantiate(FlagPrefab, transform).GetComponent<PixelObject>();

            int xx = i % colmun;
            int yy = (int)(i / colmun);

            icons[i].transform.localPosition = new Vector3(xx * size.x, -yy * size.y, 0);
            icons[i].visible = false;
        }
    }

    public void SetCount(int count)
    {
        count = Mathf.Clamp(count, 0, icons.Length);
        for (int i = 0; i < icons.Length; i++)
            icons[i].visible = i < count;
    }
}
