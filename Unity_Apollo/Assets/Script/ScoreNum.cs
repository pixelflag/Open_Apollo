using UnityEngine;

public class ScoreNum : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[10];
    [SerializeField]
    private int length = 7;

    private int fontSize = 8;
    private GameObject[] nums;

    public void Initialize()
    {
        nums = new GameObject[length];

        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = new GameObject("n" + i);
            nums[i].AddComponent<SpriteRenderer>();
            nums[i].transform.parent = transform;
            nums[i].transform.localPosition = new Vector3(-i * fontSize, 0, 0);
        }
        SetNum(0);
    }

    public void SetNum(int num)
    {
        int length = num.ToString().Length;

        for (int i = 0; i < nums.Length; i++)
        {
            if(i == 0)
            {
                nums[i].SetActive(true);
                nums[i].GetComponent<SpriteRenderer>().sprite = sprites[num%10];
            }
            else if(i < length)
            {
                nums[i].SetActive(true);
                int n = (int)((num / Mathf.Pow(10,i)) % 10);
                nums[i].GetComponent<SpriteRenderer>().sprite = sprites[n];
            }
            else
            {
                nums[i].SetActive(false);
            }
        }
    }
}
