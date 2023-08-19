using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallControler : MonoBehaviour
{
    protected GameObject SpawnPoint;
    protected GameObject BallPrefab;

    private List<GameObject> balls;
    public int maxBall = 1;
    public float timer = 0;
    public float delay = 2f;
    private bool ReadySpawn = false;


    private void Awake()
    {
        this.SpawnPoint = GameObject.Find("PointSpawn");
        this.BallPrefab = GameObject.Find("BallPrefab");
    }


    // Start is called before the first frame update
    void Start()
    {
        balls = new List<GameObject>();
        BallPrefab.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        this.ReadySpawn = Drag.Instance.falling;
        if (ReadySpawn || balls.Count == 0)
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        timer += Time.deltaTime;
        if (timer < delay) return;
        timer = 0;

        if (balls.Count >= maxBall) return;

        int index = this.balls.Count + 1;
        GameObject ball = Instantiate(BallPrefab, SpawnPoint.transform.position, Quaternion.identity, SpawnPoint.transform) as GameObject;
        ball.name = "Ball" + index;
        ball.SetActive(true);
        balls.Add(ball);
    }

}
