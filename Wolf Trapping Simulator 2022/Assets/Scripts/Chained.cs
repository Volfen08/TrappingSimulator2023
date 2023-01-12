using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chained : MonoBehaviour
{
    public Transform target;
    public float xOffset, yOffset, zOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(target.position.x + xOffset, target.position.y  + yOffset, target.position.z + zOffset);

        //gameObject.transform.position = pos;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pos,10f);
    }
}
