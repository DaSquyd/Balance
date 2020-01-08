using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed, 0f, 0f, Space.World);
        transform.Rotate(0f, 0f, Input.GetAxis("Horizontal") * -10, Space.World);
    }
}
