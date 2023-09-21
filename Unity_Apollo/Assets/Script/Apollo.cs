using UnityEngine;

public class Apollo : PixelObject
{
    [SerializeField]
    private float accele = 0.1f;
    [SerializeField]
    private float friction = 0.98f;

    private Vector2 speed = new Vector2();
    private int progress = 0;

    private State state;
    private enum State
    {
        Fall,
        Randing,
        Launch,
        Clash,
    }

    private void Start()
    {
        Boost(false, false, false);
        progress = 0;
        state = State.Fall;
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case State.Fall:
                Boost(PlayerInput.left, PlayerInput.right, false);

                speed.x += PlayerInput.value * accele;
                speed.x *= friction;
                break;

            case State.Randing:
                if (progress == 10)
                {
                    Sound.I.PlaySE(SeType.Launch, 0.5f);
                    render.sprite = defaultSprite;
                    Boost(false, false, true);

                    progress = 0;
                    state = State.Launch;
                }
                progress++;
                break;

            case State.Launch:
                speed.y += 1;

                if( y > 128)
                    Destroy(gameObject);
                break;

            case State.Clash:
                speed.y -= 0.5f;

                render.sprite = GetRolingSprite();

                if (y < -128)
                    Destroy(gameObject);
                break;
        }

        x += speed.x;
        y += speed.y;
    }

    public void Randing()
    {
        Sound.I.PlaySE(SeType.Randing, 0.5f);
        render.sprite = randingSprite;
        Boost(false, false, false);

        speed.x = 0;
        speed.y = 0;
        state = State.Randing;
    }

    public void Clash()
    {
        Sound.I.PlaySE(SeType.Clash, 0.5f);
        Boost(false, false, false);

        speed.x = 0;
        speed.y = 5;
        state = State.Clash;
    }

    // View ----------

    [SerializeField]
    private GameObject upFire;
    [SerializeField]
    private GameObject rightFire;
    [SerializeField]
    private GameObject leftFire;

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite randingSprite;
    [SerializeField]
    private Sprite[] rolingSprites;

    [SerializeField]
    private SpriteRenderer render;

    private void Boost(bool left, bool right, bool up)
    {
        leftFire.SetActive(left);
        rightFire.SetActive(right);
        upFire.SetActive(up);
    }

    private int rolingFrame = 0;
    private int rolingCount = 0;
    private int rolingStep = 2;

    private Sprite GetRolingSprite()
    {
        rolingCount++;

        if(rolingCount % rolingStep == 0)
            rolingFrame++;

        return rolingSprites[rolingFrame % rolingSprites.Length];
    }
}
