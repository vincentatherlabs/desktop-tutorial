using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColl : MonoBehaviour
{
    private Rigidbody rb;
    public float impactForce = 6f;

    public float duration = 0.2f;
    public float magnitude = 0.4f;
    private bool Shock = false;
    private Coroutine moveCoroutine;
    public Camera cameraMain;
    Vector3 originalPos;

    public Drag drag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = cameraMain.transform.position;
    }

    private void StopAllCoroutines(IEnumerator enumerator)
    {
        throw new System.NotImplementedException();
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "BG")
        {
            Debug.Log("Yes");
            Shock = true;

            if (rb.velocity.magnitude >= impactForce && Shock)
            {
                if (moveCoroutine != null) StopCoroutine(Shake(duration, magnitude));

                moveCoroutine = StartCoroutine(Shake(duration, magnitude));
            }
        } else if (col.gameObject.tag == "Destroy")
        {
            Destroy(gameObject);
            BallControler.Instance.currentBall = null;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "BG")
        {
            Debug.Log("No");
            Shock = false;
        }
    }



    public float minR = 1f;
    public float maxR = 1f;
    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(minR, maxR) * magnitude + originalPos.x;
            float y = Random.Range(minR, maxR) * magnitude + originalPos.y;

            cameraMain.transform.position = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        Debug.Log("run");
        cameraMain.transform.position = originalPos;
    }
}
