using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    Material mat;
    RenderParams rp;
    static readonly int numInstances = 10;
    List<Matrix4x4> instData = new();
    Mesh mesh;

    Quaternion rot = Quaternion.identity;
    Vector3 scale = new Vector3(0.2f, 0.2f, 0.2f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");

        mat = new(Shader.Find("Universal Render Pipeline/Lit"));
        // permanently borrowed from https://discussions.unity.com/t/how-to-make-a-urp-lit-material-semi-transparent-using-script-and-then-set-it-back-to-being-solid/942231/3
        mat.SetFloat("_Surface", 1);
        mat.SetFloat("_Blend", 0);
        mat.SetInt("_IgnoreProjector", 1);           // Ignore projectors (like rain)
        mat.SetInt("_ReceiveShadows", 0);            // Disable shadow reception
        mat.SetInt("_ZWrite", 0);                    // Disable z-writing
        mat.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        mat.renderQueue = (int)RenderQueue.Transparent;
        mat.color = Color.white;
        mat.SetColor("_BaseColor", mat.color);
        mat.enableInstancing = true;



        rp = new RenderParams(mat);


        for (int i = 0; i < numInstances; ++i)
        {
            instData.Add(Matrix4x4.TRS(new Vector3(-4.5f + i, 0.0f, 5.0f), rot, scale));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        instData.RemoveAt(Random.Range(0, instData.Count-1));
        instData.Add(Matrix4x4.TRS(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5), 5.0f), rot, scale));

        Graphics.RenderMeshInstanced(rp, mesh, 0, instData);
    }
}
