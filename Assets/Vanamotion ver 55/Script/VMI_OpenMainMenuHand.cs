using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanamotion
{
    public class VMI_OpenMainMenuHand : MonoBehaviour
    {
        [SerializeField]
        private OVRCameraRig cameraRig;

        [SerializeField]
        private GameObject targetHand;

        [SerializeField]
        private Vector3 offsetFromHand = new Vector3(0.5f, 0.5f, 0.5f);

        [SerializeField] public GameObject HandMainMenu;

        private void OpenMainMenu()
        {
            HandMainMenu.SetActive(true);
        }

        private void CloseMainMenu()
        {
            HandMainMenu.SetActive(false);
        }

        public void delayOpenMainMenu()
        {
            Invoke("OpenMainMenu", 0.5f);
        }

        public void delayCloseMainMenu()
        {
            CancelInvoke("OpenMainMenu");
            CloseMainMenu();
        }

        private void Update()
        {
            HandMainMenu.transform.position = targetHand.transform.position + offsetFromHand;
            HandMainMenu.transform.rotation = Quaternion.LookRotation(HandMainMenu.transform.position - cameraRig.centerEyeAnchor.transform.position, Vector3.up);
        }
    }
}