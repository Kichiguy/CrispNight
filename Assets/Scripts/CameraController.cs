using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController pc;
    public GameObject house;

    private float scrollSpeed;
    public bool isPanning;
    private float startOfHold;
    private bool atHouse;
    public float holdTime = 2.0f;
    public float holdDist = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = pc.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPanning)
        {
            this.transform.position = Vector3.Lerp(
                this.transform.position,
                pc.transform.position,
                Time.deltaTime
            );
        }
        else 
        {
            Vector3 diff = house.transform.position - this.transform.position;

            this.transform.position = Vector3.Lerp(
                this.transform.position,
                house.transform.position,
                Time.deltaTime
            );

            if (diff.sqrMagnitude < holdDist * holdDist && !atHouse)
            {
                atHouse = true;
                startOfHold = Time.time;
            }

            if (atHouse && (Time.time - startOfHold) > holdTime) 
            {
                atHouse = false;
                isPanning = false;
            }
        }
    }
}
