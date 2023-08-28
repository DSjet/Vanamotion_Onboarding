using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Oculus.Interaction;
using UnityEngine.UI;
using Oculus.Interaction.Surfaces;
using Oculus.Interaction.UnityCanvas;

#if UNITY_EDITOR
namespace Vanamotion
{
    public class VM_CurvedPokeCanvas : MonoBehaviour
    {
        [MenuItem("GameObject/Vanamotion/Curved Poke Canvas", false, 0)]
        private static void CreateCurvedPokeCanvas()
        {
            //Attribute
            List<CanvasCylinder> clipper = new List<CanvasCylinder>();

            //=======================add pointable canvas========================
            GameObject newObject = new GameObject("Curved Poke Canvas");
            Selection.activeGameObject = newObject;

            //add pointable canvas
            PointableCanvas pc = newObject.AddComponent<PointableCanvas>();

            //add poke interactable
            PokeInteractable pi = newObject.AddComponent<PokeInteractable>();

            //add pointable canvas mesh
            PointableCanvasMesh pcm = newObject.AddComponent<PointableCanvasMesh>();
            
            //=======================add canvas========================
            GameObject canvas = new GameObject("Canvas");
            canvas.transform.parent = newObject.transform;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            //setting render mode to world space
            Canvas can = canvas.GetComponent<Canvas>();
            can.renderMode = RenderMode.WorldSpace;

            //add canvas render texture
            CanvasRenderTexture crt = canvas.AddComponent<CanvasRenderTexture>();
            crt.InjectCanvas(can);

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

            //======================add Canvas Mesh========================
            GameObject canvasMesh = new GameObject("Canvas Mesh");
            canvasMesh.transform.parent = newObject.transform;
            CanvasCylinder canvasCylinder = canvasMesh.AddComponent<CanvasCylinder>();
            OVRCanvasMeshRenderer ovrCanvasR = canvasMesh.AddComponent<OVRCanvasMeshRenderer>();
            canvasMesh.AddComponent<CanvasMesh>();
            canvasMesh.AddComponent<MeshRenderer>();
            canvasMesh.AddComponent<MeshFilter>();

            //======================add Cylinder Surface========================
            GameObject cSurface = new GameObject("Cylinder Surface");
            cSurface.transform.parent = newObject.transform;
            cSurface.transform.localPosition = new Vector3(0, 0, -1f);

            //add cylinder component
            Cylinder cylinder = cSurface.AddComponent<Cylinder>();

            //add cylinder surface
            CylinderSurface cs = cSurface.AddComponent<CylinderSurface>();
            cs.InjectNormalFacing(CylinderSurface.NormalFacing.In);
            cs.InjectHeight(0f);

            //add clipped cylinder surface
            ClippedCylinderSurface ccs = cSurface.AddComponent<ClippedCylinderSurface>();

            //=======================add collider========================
            GameObject collider = new GameObject("Collider");
            collider.transform.parent = newObject.transform;
            collider.AddComponent<MeshCollider>();

            //=======================Inject Component========================
            //inject poke interactable
            pi.InjectOptionalPointableElement(pcm);
            pi.InjectSurfacePatch(ccs);

            //inject pointable canvas
            pc.InjectCanvas(can);

            //inject pointable canvas mesh
            pcm.InjectCanvasMesh(canvasCylinder);

            //inject canvas cylinder
            canvasCylinder.InjectCanvasRenderTexture(crt);
            canvasCylinder.InjectCylinder(cylinder);
            canvasCylinder.InjectMeshFilter(canvasMesh.GetComponent<MeshFilter>());
            canvasCylinder.InjectOptionalMeshCollider(collider.GetComponent<MeshCollider>());

            //inject ovr canvas mesh renderer
            ovrCanvasR.InjectCanvasRenderTexture(crt);
            ovrCanvasR.InjectMeshRenderer(canvasMesh.GetComponent<MeshRenderer>());
            ovrCanvasR.InjectCanvasMesh(canvasCylinder);

            //add cylinder surface
            cs.InjectCylinder(cylinder);

            //inject clipped cylinder surface
            ccs.InjectCylinderSurface(cs);
            clipper.Add(canvasCylinder);
            ccs.InjectClippers(clipper);
        }
    }
}

#endif