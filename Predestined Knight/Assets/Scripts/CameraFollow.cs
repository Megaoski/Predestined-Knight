using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;

    //public Transform Obstruction;
    //float zoomSpeed = 2f;
    //public Transform Target;


    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
        //Obstruction = Target;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //ViewObstructed();

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtPlayer)
            transform.LookAt(PlayerTransform);
    }

    //void ViewObstructed()
    //{
    //    RaycastHit hit;

    //    if(Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
    //    {
    //        if(hit.collider.gameObject.tag != "Player")
    //        {
    //            Obstruction = hit.transform;
    //            Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

    //            if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
    //            {
    //                transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
    //            }
    //        }
    //        else
    //        {
    //            Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    //            if(Vector3.Distance(transform.position, Target.position) < 4.5f)
    //            {
    //                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
    //            }
    //        }
    //    }
    //}
}
