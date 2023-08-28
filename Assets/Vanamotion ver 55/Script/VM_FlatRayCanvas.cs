using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Oculus.Interaction;
using UnityEngine.UI;
using Oculus.Interaction.Surfaces;

#if UNITY_EDITOR
namespace Vanamotion
{
    public class VM_FlatRayCanvas : MonoBehaviour
    {
        [MenuItem("GameObject/Vanamotion/Flat Ray Canvas", false, 0, priority = 1)]
        private static void CreateNewGameObject()
        {
            //=======================add pointable canvas========================
            GameObject newObject = new GameObject("Flat Ray Canvas");
            Selection.activeGameObject = newObject;

            //add pointable canvas
            PointableCanvas pc = newObject.AddComponent<PointableCanvas>();

            //add poke interactable
            RayInteractable ri = newObject.AddComponent<RayInteractable>();
            
            //=======================add canvas========================
            GameObject canvas = new GameObject("Canvas");
            canvas.transform.parent = newObject.transform;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            //setting render mode to world space
            Canvas can = canvas.GetComponent<Canvas>();
            can.renderMode = RenderMode.WorldSpace;

            //setup canvas rect transform
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(100, 60);
            canvasRect.localPosition = new Vector3(0, 0, 0);
            canvasRect.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            //add  image
            GameObject image = new GameObject("Image");
            image.transform.parent = canvas.transform;
            image.AddComponent<Image>();
            image.GetComponent<Image>().color = Color.black;

            //change image rect transform  to stretch
            RectTransform imageRect = image.GetComponent<RectTransform>();
            imageRect.anchorMin = new Vector2(0, 0);
            imageRect.anchorMax = new Vector2(1, 1);
            imageRect.sizeDelta = new Vector2(0, 0);
            imageRect.localPosition = new Vector3(0, 0, 0);
            imageRect.localScale = new Vector3(1, 1, 1);

            //add child with collider
            GameObject coll = new GameObject("Collider");
            coll.transform.parent = newObject.transform;
            coll.AddComponent<BoxCollider>();
            ColliderSurface cs = coll.AddComponent<ColliderSurface>();
            cs.InjectCollider(coll.GetComponent<BoxCollider>()); //inject collider to cs

            //add child plane surface
            GameObject surface = new GameObject("Surface");
            surface.transform.parent = newObject.transform;
            PlaneSurface ps = surface.AddComponent<PlaneSurface>();

            //==========================Inject ELement=========================

            //pointable canvas inject
            pc.InjectCanvas(can);

            //ray interactable inject
            ri.InjectOptionalPointableElement(pc);
            ri.InjectSurface(cs);
            ri.InjectOptionalSelectSurface(ps);
        }
    }
}
#endif