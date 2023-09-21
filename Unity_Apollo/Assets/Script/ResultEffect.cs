using UnityEngine;

public class ResultEffect : PixelObject
{
    [SerializeField]
    private Sprite[] Parfect;
    [SerializeField]
    private Sprite[] Great;
    [SerializeField]
    private Sprite[] Good;

    [SerializeField]
    private int showlife = 60;
    [SerializeField]
    private int animStep = 2;
    [SerializeField]
    private int upSpeed = 1;

    private int progress;
    private RandingResult result;

    private SpriteRenderer render;

    public void Initialize(RandingResult result)
    {
        this.result = result;
        render = GetComponent<SpriteRenderer>();
        render.sprite = null;
        progress = showlife;
    }

    public void FixedUpdate()
    {
        switch (result)
        {
            case RandingResult.Parfect:
                render.sprite = Parfect[(int)(progress / animStep) % Parfect.Length];
                break;
            case RandingResult.Great:
                render.sprite = Great[(int)(progress / animStep) % Great.Length];
                break;
            case RandingResult.Good:
                render.sprite = Good[(int)(progress / animStep) % Good.Length];
                break;
            case RandingResult.Clash:
                render.sprite = null;
                break;
        }

        y += upSpeed;

        progress--;
        if (progress == 0)
            Destroy(gameObject);
    }
}
