using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Apollo ApolloPrefab;
    private List<Apollo> Apollos = new List<Apollo>();

    [SerializeField]
    private ResultEffect resultEffectPrefab;
    [SerializeField]
    private Vector3 resultEffectPosition = new Vector3(0,-20,0);

    [SerializeField]
    private float initGravity = -0.5f;
    [SerializeField]
    private int gravityLevelupDistance = 120;

    [SerializeField]
    private int initSpawnTempo = 200;
    [SerializeField]
    private int minSpawnTempo = 30;
    [SerializeField]
    private int spawnTempoLevelupDistance = 90;

    [SerializeField]
    private int initSpawnRange = 0;
    [SerializeField]
    private int maxSpawnRange = 120;
    [SerializeField]
    private int spawnRangeLevelupDistance = 60;

    [SerializeField]
    private float targetGroundY = -36;
    [SerializeField]
    private float targetGroundThreshold = 3;

    [SerializeField]
    private float ParfectWidth = 2;
    [SerializeField]
    private float GreatWidth = 8;
    [SerializeField]
    private float GoodWidth = 24;

    [SerializeField]
    private int groundLine = -72;

    [SerializeField]
    private int[] ScoreTable;

    private int spawnTempo = 0;
    private int spawnRange = 0;
    private float gravity = 0;

    private int score = 0;
    private int left = 0;

    private int progress;
    private int spawnProgress;
    
    public void Initialize(int initLeft)
    {
        left = initLeft;
        score = 0;

        OnScoreUpdate(score);
        OnLeftUpdate(left);

        gravity = initGravity;
        spawnTempo = initSpawnTempo;
        spawnRange = initSpawnRange;
    }

    public void FixedUpdate()
    {
        for (int i= Apollos.Count-1; 0 <= i; i--)
        {
            Apollo apo = Apollos[i];

            int screenXEnd = 168;
            apo.x = apo.x < -screenXEnd ? screenXEnd : apo.x;
            apo.x = apo.x > screenXEnd ? -screenXEnd : apo.x;
            apo.y += gravity;

            if (targetGroundY - targetGroundThreshold <= apo.y && apo.y <= targetGroundY)
            {
                if (-ParfectWidth < apo.x && apo.x < ParfectWidth)
                    RandTarget(apo, RandingResult.Parfect);
                else if (-GreatWidth < apo.x && apo.x < GreatWidth)
                    RandTarget(apo, RandingResult.Great);
                else if (-GoodWidth < apo.x && apo.x < GoodWidth)
                    RandTarget(apo, RandingResult.Good);
            }
            else if (apo.y < groundLine)
            {
                apo.Clash();
                Apollos.Remove(apo);

                left--;
                OnLeftUpdate(left);

                if (left == 0)
                    GameEnd();
            }
        }

        progress++;
        spawnProgress--;
        if (spawnProgress <= 0)
        {
            SpawnApollo();
            spawnProgress = spawnTempo;
        }

        if (progress % spawnTempoLevelupDistance == 0)
        {
            spawnTempo = Mathf.Max(minSpawnTempo, spawnTempo - 10);
        }
        if (progress % spawnRangeLevelupDistance == 0)
        {
            spawnRange = Mathf.Min(maxSpawnRange, spawnRange + 10);
        }
        if (progress % gravityLevelupDistance == 0)
        {
            gravity -= 0.1f;
        }
    }

    private void SpawnApollo()
    {
        Apollo apo = Instantiate(ApolloPrefab).GetComponent<Apollo>();
        apo.x = Random.Range(-spawnRange, spawnRange);
        apo.y = 96;
        Apollos.Add(apo);
    }


    private void RandTarget(Apollo apo, RandingResult result)
    {
        apo.y = targetGroundY;
        apo.Randing();
        Apollos.Remove(apo);

        var ef = Instantiate(resultEffectPrefab);
        ef.Initialize(result);
        ef.position = resultEffectPosition;

        switch (result)
        {
            case RandingResult.Parfect:
                score += ScoreTable[0];
                break;
            case RandingResult.Great:
                score += ScoreTable[1];
                break;
            case RandingResult.Good:
                score += ScoreTable[2];
                break;
            case RandingResult.Clash:
                break;
        }
        OnScoreUpdate(score);
    }

    public void GameEnd()
    {
        for (int i= 0; i < Apollos.Count; i++ )
            Apollos[i].Clash();

        OnGameEnd();
        Destroy(gameObject);
    }

    public delegate void ScoreUpdateDelegate(int score);
    public ScoreUpdateDelegate OnScoreUpdate;

    public delegate void LeftUpdateDelegate(int left);
    public LeftUpdateDelegate OnLeftUpdate;

    public delegate void GameEndDelegate();
    public GameEndDelegate OnGameEnd;
}