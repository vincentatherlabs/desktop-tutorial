using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    public static System.Action<Drag> OnDragReleaseBall = delegate {};
    Vector3 mousePos;

    //public static Drag Instance;

    public float minPosX;
    public float maxPosX;

    public float speed = 0.1f;
    
    public bool falling = false;

    private bool selected;

    private void Awake()
    {
        //Drag.Instance = this;
    }
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        if (falling) return;

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "ball")
                {
                    selected = true;
                }
            }
        }

        if (selected == true)
        {
            Vector3 v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20);

            mousePos = Camera.main.ScreenToWorldPoint(v3);

            Vector3 movePos = Vector3.Lerp(transform.position, mousePos, speed * Time.deltaTime);
            transform.position = movePos;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPosX, maxPosX), transform.parent.position.y, transform.position.z);

        }

        if (Input.GetMouseButtonUp(0) && selected)
        {
            selected = false;
            falling = true;
            transform.tag = "Untagged";
            this.GetComponent<Rigidbody>().isKinematic = false;

            OnDragReleaseBall(this);
        }

    }
}
