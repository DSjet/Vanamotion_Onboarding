using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Vanamotion
{
    public class VMI_ResetPositionCharacter : MonoBehaviour
    {
        [SerializeField] private Vector3 resetPosition;
        [SerializeField] private Vector3 resetRotation;

        public void ResetPositionandRotation()
        {
            Invoke("resetChar", 0.5f);
        }

        public void cancelReset()
        {
            CancelInvoke("resetChar");
        }

        void resetChar()
        {
            transform.position = resetPosition;
            transform.rotation = Quaternion.Euler(resetRotation);
        }
    }
}

