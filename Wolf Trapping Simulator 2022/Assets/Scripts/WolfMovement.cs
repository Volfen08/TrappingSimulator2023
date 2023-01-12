using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    private CharacterController cc;
    private Rigidbody rb;
    public float force = 100;
    public float rotationSpeed = 100;
    private Animator ani;
    public Transform trap;
    public Collider meshColid;
    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float mag = Mathf.Clamp01(dir.magnitude) * force;
        dir.Normalize();
        cc.SimpleMove( dir * mag);

        if (dir != Vector3.zero)
        {
            Quaternion toRot = Quaternion.LookRotation(dir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, rotationSpeed * Time.deltaTime);
        }
        */
        //gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0),Space.Self);
        
        
        rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * force ));
        //ani.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        //ani.SetFloat("Vertical", Input.GetAxis("Vertical"));
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            Debug.Log("Trap hit");
            gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.Self);
            rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * force));
            ani.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            ani.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }
        else
        {
            gameObject.transform.LookAt(trap, Vector3.forward);
        }

    }
}
