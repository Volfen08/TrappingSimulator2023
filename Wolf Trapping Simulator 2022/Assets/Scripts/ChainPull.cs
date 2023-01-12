using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPull : MonoBehaviour
{
    public Transform target;
    public float moveFactor;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist >= 0.2 || dist <= -0.2)
            transform.position = transform.position + transform.forward * moveFactor;
    }
}
