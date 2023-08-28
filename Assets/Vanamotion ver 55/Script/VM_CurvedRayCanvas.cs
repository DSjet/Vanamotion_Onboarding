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
    public class VM_CurvedRayCanvas : MonoBehaviour
    {
        [MenuItem("GameObject/Vanamotion/Curved Ray Canvas", false, 0)]
        private static void CreateCurvedRayCanvas()
        {
            //=======================add pointable canvas========================
            GameObject newObject = new GameObject("Curved Ray Canvas");
            Selection.activeGameObject = newObject;

            //Add cylinder
            Cylinder cylinder =  newObject.AddComponent<Cylinder>();
            cylinder.Radius= 1.51f;

            //========================add cylinder surface========================
            CylinderSurface cs = newObject.AddComponent<CylinderSurface>();
            cs.InjectCylinder(cylinder);
            cs.InjectNormalFacing(CylinderSurface.NormalFacing.In);
            cs.InjectHeight(0f);

            //make game object curved panel child new object
            GameObject curvedPanel = new GameObject("Curved Panel");
            curvedPanel.transform.parent = newObject.transform;
            curvedPanel.transform.localPosition = new Vector3(0, 0, 1.5f);

            //add pointable canvas
            PointableCanvas pc = curvedPanel.AddComponent<PointableCanvas>();

            //add pointable canvas mesh
            PointableCanvasMesh pcm = curvedPanel.AddComponent<PointableCanvasMesh>();

            //add poke interactable
            RayInteractable ri = curvedPanel.AddComponent<RayInteractable>();
            
            //=======================add canvas========================
            GameObject canvas = new GameObject("Canvas");
            canvas.transform.parent = curvedPanel.transform;
            canvas.AddComponent<Canvas>();
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();

            //add canvas render texture
            CanvasRenderTexture crt = canvas.AddComponent<CanvasRenderTexture>();
            crt.InjectCanvas(canvas.GetComponent<Canvas>());

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

            //====================add game object mesh collider====================
            GameObject meshCollider = new GameObject("Mesh Collider");
            meshCollider.transform.parent = curvedPanel.transform;

            CanvasCylinder cCylinder = meshCollider.AddComponent<CanvasCylinder>();
            OVRCanvasMeshRenderer ovrCanvasMesh = meshCollider.AddComponent<OVRCanvasMeshRenderer>();

            //add desfult component for mesh
            meshCollider.AddComponent<MeshRenderer>();
            meshCollider.AddComponent<MeshFilter>();

            //add mesh collider
            MeshCollider mc = meshCollider.AddComponent<MeshCollider>();

            //add collider surface
            ColliderSurface colliderSurface = meshCollider.AddComponent<ColliderSurface>();

            //====================inject components====================
            //collider surface
            colliderSurface.InjectCollider(mc);

            //ovr canvas mesh
            ovrCanvasMesh.InjectCanvasMesh(cCylinder);
            ovrCanvasMesh.InjectMeshRenderer(meshCollider.GetComponent<MeshRenderer>());
            ovrCanvasMesh.InjectCanvasRenderTexture(crt);
            
            //canvas cylinder
            cCylinder.InjectCylinder(cylinder);
            cCylinder.InjectOptionalMeshCollider(mc);
            cCylinder.InjectMeshFilter(meshCollider.GetComponent<MeshFilter>());
            cCylinder.InjectCanvasRenderTexture(crt);

            //Canvas Render Texture
            crt.InjectCanvas(can);

            //pointable canvas
            pc.InjectCanvas(can);

            //pointable canvas mesh
            pcm.InjectCanvasMesh(cCylinder);
            pcm.InjectOptionalForwardElement(pc);

            //Ray Interactable
            ri.InjectOptionalPointableElement(pcm);
            ri.InjectSurface(colliderSurface);
            ri.InjectOptionalSelectSurface(cs);
        }
    }
}
#endif