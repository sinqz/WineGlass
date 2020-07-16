using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace EZFramework
{
    public class AssetBundleBuild : EditorWindow
    {
        public static void CreatWindow()
        {
            Rect rect = new Rect(0, 0, 560, 480);
            AssetBundleBuild myWindow = (AssetBundleBuild)EditorWindow.GetWindowWithRect(typeof(AssetBundleBuild), rect, true, "AssetBundleBulid");//创建窗口
        }

        private BuildTarget target = BuildTarget.StandaloneWindows;
        private string path = Application.streamingAssetsPath;
        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("选择打包平台");
            this.target = (BuildTarget)EditorGUILayout.EnumPopup(target);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            path = EditorGUILayout.TextField("资源储存路径:", path, GUILayout.MaxWidth(400));
            if (GUILayout.Button("选择储存路径"))
            {
                path = EditorUtility.OpenFolderPanel("Select Package Path", Application.dataPath, "");
            }
            EditorGUILayout.EndHorizontal();


            if (GUILayout.Button("Build", GUILayout.MaxHeight(20)))
            {
                BuildAssetBundle();
            }
        }

        private void BuildAssetBundle()
        {
            path = path + "/" + BuildTarget.StandaloneWindows.ToString();
            if (!string.IsNullOrEmpty(path))
            {
                CreateFolder.CreatFolder(path);
                BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, target);
            }
        }
    }
}

