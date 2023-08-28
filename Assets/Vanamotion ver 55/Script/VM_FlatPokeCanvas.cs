using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Oculus.Interaction;
using UnityEngine.UI;
using Oculus.Interaction.Surfaces;

#if UNITY_EDITOR
namespace Vanamotion
{
    public class VM_FlatPokeCanvas : MonoBehaviour
    {
        [MenuItem("GameObject/Vanamotion/Flat Poke Canvas", false, 0, priority = 1)]
        private static void CreateNewGameObject()
        {
            //Attribute
            List<BoundsClipper> clipper = new List<BoundsClipper>();

            //=======================add pointable canvas========================
            GameObject newObject = new GameObject("Flat Poke Canvas");
            Selection.activeGameObject = newObject;

            //add pointable canvas
            PointableCanvas pc = newObject.AddComponent<PointableCanvas>();

            //add poke interactable
            PokeInteractable pi = newObject.AddComponent<PokeInteractable>();
            
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

            //======================add surface========================
            GameObject surface = new GameObject("Surface");
            surface.transform.parent = newObject.transform;

            //add plane surface
            PlaneSurface ps = surface.AddComponent<PlaneSurface>();
            
            //add bounds clipper
            BoundsClipper bc = surface.AddComponent<BoundsClipper>();
            
            clipper.Add(bc);

            //add clipper bounds
            ClippedPlaneSurface cps = surface.AddComponent<ClippedPlaneSurface>();
            cps.InjectPlaneSurface(ps);
            cps.InjectClippers(clipper);

            //==========================inject element===================
            pc.InjectCanvas(can); //inject canvas
            pi.InjectSurfacePatch(cps); // inject surface patch
            pi.InjectOptionalPointableElement(pc); //inject pointable canvas
        }
    }
}
#endif
