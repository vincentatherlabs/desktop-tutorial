using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallControler : MonoBehaviour
{
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] BallColl BallPrefab;

    private List<GameObject> balls;
    public int maxBall = 1;
    public float timer = 0;
    public float delay = 2f;

    public BallColl currentBall = null;
    public static BallControler Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentBall == null)
        {
            currentBall = SpawnBall();
        }
    }

    BallColl SpawnBall()
    {
        timer += Time.deltaTime;
        if (timer < delay) return null;
        timer = 0;

        if (balls.Count >= maxBall) return null;

        int index = this.balls.Count + 1;
        BallColl ball = Instantiate(BallPrefab, SpawnPoint.transform.position, Quaternion.identity, SpawnPoint.transform);
        ball.name = "Ball" + index;
        ball.gameObject.SetActive(true);

        return ball;
    }

}
