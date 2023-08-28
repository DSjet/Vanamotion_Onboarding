using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanamotion
{
    public class VMI_CharacterMovementHandTracking : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
    
        [Header("Movement")]
        public float speed = 1f;
    
        private bool isPoseValidFoward = false;
        private bool isPoseValidBackward = false;
    
        
        void Update()
        {
            if (isPoseValidFoward && isPoseValidBackward == false)
            {
                transform.Translate(speed * Time.deltaTime * cameraTransform.forward.normalized, Space.World);
            }
            else if (isPoseValidBackward && isPoseValidFoward == false)
            {
                transform.Translate(speed * Time.deltaTime * -cameraTransform.forward.normalized, Space.World);
            }
        }
    
        public void SetPoseValidFoward(bool valid)
        {
            isPoseValidFoward = valid;
        }
    
        public void SetPoseValidBackward(bool valid)
        {
            isPoseValidBackward = valid;
        }
}
}

