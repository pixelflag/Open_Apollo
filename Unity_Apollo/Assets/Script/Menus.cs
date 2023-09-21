using UnityEngine;

public class Menus : MonoBehaviour
{
    public PixelObject titleImage;
    public PixelObject gameover;
    public PixelObject pushSpace;
    public PixelObject scoreTitle;
    public ScoreNum scoreNum;
    public LeftCounter leftCounter;

    private int progress;
    private int wait = 30;

    public void Initialize()
    {
        leftCounter.Initialize();
        scoreNum.Initialize();

        SetScore(0);
        progress = 0;
    }

    public void SetScore(int score)
    {
        scoreNum.SetNum(score);
    }

    public void Execute()
    {
        progress++;
        if(progress % wait == 0 )
            pushSpace.visible = !pushSpace.visible;
    }

    public void Title()
    {
        titleImage.visible = true;
        gameover.visible = false;
        pushSpace.visible = true;
    }

    public void Result()
    {
        titleImage.visible = false;
        gameover.visible = true;
        pushSpace.visible = true;
    }

    public void Hide()
    {
        titleImage.visible = false;
        gameover.visible = false;
        pushSpace.visible = false;
    }
}
