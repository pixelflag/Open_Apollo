using UnityEngine;

public class Rabbit : MonoBehaviour
{
    [SerializeField]
    private Sprite[] idelAnim;

    [SerializeField]
    private Sprite[] sideAnim;

    [SerializeField]
    private int animStep = 2;

    private int frame;
    private int progress;

    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        progress++;
        if (progress % animStep == 0)
            frame++;

        // left
        if (PlayerInput.value < 0)
        {
            render.sprite = sideAnim[frame % sideAnim.Length];
            render.flipX = false;
        }
        // rigth
        else if(0 < PlayerInput.value)
        {
            render.sprite = sideAnim[frame % sideAnim.Length];
            render.flipX = true;
        }
        // center
        else
        {
            render.sprite = idelAnim[frame % idelAnim.Length];
            render.flipX = false;
        }
    }
}
