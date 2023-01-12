using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum targetAxis { x, y, z }
public class RotationOverride : MonoBehaviour
{
    
    public targetAxis targetAxisVar;
    public float minRot;
    public float maxRot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        float targetAxisRot =0f;
        Debug.Log("Rot Start");
        switch (targetAxisVar)
        {
            case targetAxis.x:
                targetAxisRot = transform.rotation.eulerAngles.x;
                break;
            case targetAxis.y:
                targetAxisRot = transform.rotation.eulerAngles.y;
                break;
            case targetAxis.z:
                targetAxisRot = transform.rotation.eulerAngles.z;
                break;
        }
        Debug.Log("Axis: " + targetAxisVar.ToString());
        Debug.Log("Rot: " + targetAxisRot);

        if (targetAxisRot > maxRot)
        {
            Debug.Log("Rot max");
            SetRot(true);
        }
        else if (targetAxisRot < minRot)
        {
            Debug.Log("Rot min");
            SetRot(false);
        }
    }
    
    private void SetRot(bool max)
    {
        Debug.Log("Rot Hit");
        switch (targetAxisVar)
        {
            case targetAxis.x:
                transform.rotation.eulerAngles.Set( max ? maxRot : minRot, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); 
                break;
            case targetAxis.y:
                transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, max ? maxRot : minRot, transform.rotation.eulerAngles.z);
                break;
            case targetAxis.z:
                transform.rotation.eulerAngles.Set(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, max ? maxRot : minRot);
                break;
        }
    }
}
