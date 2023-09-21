using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Menus menus;
    [SerializeField]
    private int initLeft = 11;

    [SerializeField]
    private Game gamePrefab;
    private Game currentGame;

    private State state;
    private enum State
    {
        Title,
        Play,
        Result,
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        menus.Initialize();

        state = State.Title;
        menus.Title();
    }

    private void StartGame()
    {
        if (currentGame != null) Destroy(currentGame);
        currentGame = Instantiate(gamePrefab);
        currentGame.OnScoreUpdate = (int score) =>
        {
            menus.SetScore(score);
        };
        currentGame.OnLeftUpdate = (int left) =>
        {
            menus.leftCounter.SetCount(left);
        };
        currentGame.OnGameEnd = () =>
        {
            Sound.I.StopBgm();
            Sound.I.PlayBgm(BgmType.Finish, false);

            menus.Result();
            state = State.Result;
        };
        currentGame.Initialize(initLeft);
    }

    private void FixedUpdate()
    {
        PlayerInput.left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        PlayerInput.right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        switch (state)
        {
            case State.Title:
                menus.Execute();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    menus.Hide();

                    Sound.I.PlayBgm(BgmType.Space, true);
                    Sound.I.PlaySE(SeType.Ok, 0.5f);

                    StartGame();
                    state = State.Play;
                }
                break;
            case State.Play:
                break;
            case State.Result:
                menus.Execute();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    menus.Title();
                    state = State.Title;
                }
                break;
        }
    }
}

public static class PlayerInput
{
    public static bool left;
    public static bool right;
    public static float value => (left ? -1.0f : 0) + (right ? 1.0f : 0);
}