using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ConstraintManager : MonoBehaviour
{
    public MultiAimConstraint[] aimCon = new MultiAimConstraint[10];
    public float[] aimConWeights = new float[10];

    public ChainIKConstraint[] chainCon = new ChainIKConstraint[10];
    public float[] chainConWeights = new float[10];

    private Collider wolfCollider;
    private bool center = false;
    private float min = 4;
    private float max = 8;

    private CharacterController cc;
    private Rigidbody rb;
    public float force = 100;
    public float rotationSpeed = 100;
    private Animator ani;
    public Transform trap;
    public Collider meshColid;
    public float turnThreshold = 0.05f;

    public AudioSource chainPull;
    public AudioSource chainRattle;

    public AudioSource[] wolf = new AudioSource[4];
    private float time;
    private float timeNextCue;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
        ani = gameObject.GetComponent<Animator>();
        timeNextCue = Random.Range(6f, 15f);
        wolfCollider = gameObject.transform.Find("Wolf_Mesh").GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        center = false;
        ani.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        ani.SetFloat("Vertical", Input.GetAxis("Vertical"));
        time += Time.deltaTime;

        if (time > timeNextCue)
        {
            int rand = Random.Range(0, 3);
            if (wolf[rand].time == 0)
            {
                wolf[rand].PlayDelayed(0.1f);
            }
            time = 0;
            timeNextCue = Random.Range(6f, 15f);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Center")
        { 
            gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.Self);

            Debug.Log("Center");
            center = true;
            rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * force * Time.deltaTime));

            /*
            if (Input.GetAxis("Vertical") != 0 && chainRattle.time == 0)
                chainRattle.PlayDelayed(0.1f);
            */
            for (int i = 0; i < aimCon.Length; i++)
            {
                aimCon[i].weight = 0;
            }

            for (int i = 0; i < chainCon.Length; i++)
            {
                chainCon[i].weight = 0;
            }
        }
        if (other.gameObject.tag == "Edge" && center == false)
        {
            Vector3 oldRot = gameObject.transform.eulerAngles;
            gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0), Space.Self);
            if (lookingAway() < turnThreshold)
                gameObject.transform.eulerAngles = oldRot;



            Debug.Log("Edge");
            float mod = Vector3.Distance(gameObject.transform.position, trap.position);
            mod = mod < 0 ? -mod : mod;

            
            rb.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * (force - (10 * mod) ) * Time.deltaTime) );

            if (lookingAway() < turnThreshold/*Vector3.Distance(trap.position, trap.position) > 4*/)
            {
                Vector3 dir = trap.position - transform.position;
                dir.y = 0; // keep the direction strictly horizontal
                Quaternion rot = Quaternion.LookRotation(dir);
                // slerp to the desired rotation over time
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1 * Time.deltaTime);
            }

            for (int i = 0; i < aimCon.Length; i++)
            {
                aimCon[i].weight = aimConWeights[i] * ( (mod -4)/ 4);
            }

            for (int i = 0; i < chainCon.Length; i++)
            {
                chainCon[i].weight = chainConWeights[i] * ((mod - 4) / 4);
            }
            Debug.Log(mod + "MOD/8" + (mod - 4)/4);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Center")
        {
            if (lookingAway() < turnThreshold)
                StartCoroutine(LookAtTrap());

            if( chainPull.time == 0)
                chainPull.PlayDelayed(0.1f);

            if (chainRattle.time == 0)
                chainRattle.PlayDelayed(0.1f);

            if (wolf[3].time == 0)
                wolf[3].PlayDelayed(0.1f);
        }
    }

    public float lookingAway()
    {
        Vector3 dirFromAtoB = (trap.transform.position - gameObject.transform.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, gameObject.transform.forward);

        return dotProd;
        /*
        if (dotProd > 0.9)
        {
            // ObjA is looking mostly towards ObjB
        }
        */
    }

    IEnumerator LookAtTrap()
    {
            Vector3 dir = trap.position - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1 * Time.deltaTime);
       
        if (lookingAway() < turnThreshold)
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(LookAtTrap());
        }
    }
}
