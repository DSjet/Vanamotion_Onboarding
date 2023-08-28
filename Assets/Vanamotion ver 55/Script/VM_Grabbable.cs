using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class VM_Grabbable : Grabbable
{
    [ContextMenu("Initialize")]
    void Initialize()
    {
        if (GetComponent<VM_Grabbable>() == null)
        {
            gameObject.AddComponent<VM_Grabbable>();
        }

        //Add rigidbody
        Rigidbody rb =  gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        //add pointable unity event
        PointableUnityEventWrapper Puew = gameObject.AddComponent<PointableUnityEventWrapper>();
        Puew.InjectPointable(this);

        //Add child name HandGrabInteractable
        GameObject HGInteractable = new GameObject("HandGrabInteractable");
        HGInteractable.transform.parent = transform;
        HGInteractable.AddComponent<HandGrabInteractable>();

        
        
    }
}
