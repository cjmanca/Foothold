using UnityEditor;
using UnityEngine;

public class BuildModBundles
{
    [MenuItem("Tools/Build Mod AssetBundles")]
    public static void BuildAll()
    {
        string outputPath = "Assets/ModBundles";
        System.IO.Directory.CreateDirectory(outputPath);

        BuildPipeline.BuildAssetBundles(
            outputPath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64 // match game platform
        );

        Debug.Log("Mod asset bundles built to: " + outputPath);
    }
}