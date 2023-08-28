using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Oculus.Interaction;

#if UNITY_EDITOR
namespace Vanamotion
{
    public class VM_EventSystem : MonoBehaviour
    {
        [UnityEditor.MenuItem("GameObject/Vanamotion/Event System", false, 0)]
        private static void CreateEventsytem()
        {
            if(FindObjectOfType<EventSystem>() == null)
            {
                GameObject newObject = new GameObject("Event System");
                newObject.AddComponent<EventSystem>();
                newObject.AddComponent<PointableCanvasModule>();
            }
            else
            {
                GameObject eventSystem = FindObjectOfType<EventSystem>().gameObject;
                if(eventSystem.GetComponent<PointableCanvasModule>() == null)
                {
                    Destroy(eventSystem);
                    CreateEventsytem();
                }
                else
                {
                    Debug.Log("Pointable Canvas Module already exists in the scene");
                }
            }
        }
    }
}
#endif
