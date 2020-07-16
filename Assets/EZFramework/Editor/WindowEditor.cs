using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EZFramework
{
    public class WindowEditor : MonoBehaviour
    {
        [UnityEditor.MenuItem("EZModel/AssetBundle", false, 0)]
        private static void BuildAssetWindows()
        {
            AssetBundleBuild.CreatWindow();
        }


        [UnityEditor.MenuItem("EZModel/CreateFolder/DefaultScript", true, 11)]
        private static bool FolderGame()
        {
            return Selection.activeGameObject != null;
        }

        [UnityEditor.MenuItem("EZModel/CreateFolder/DefaultScript")]
        private static void CreateFolders()
        {
            if (FolderGame())
            {
                CreateFolder.CreatFolders(Selection.activeGameObject);
            }
        }

        [UnityEditor.MenuItem("EZModel/CreateFolder/UGUIScript", true, 11)]
        private static bool FolderGame1()
        {
            return Selection.activeGameObject != null;
        }

        [UnityEditor.MenuItem("EZModel/CreateFolder/UGUIScript")]
        private static void CreateFolders1()
        {
            if (FolderGame())
            {
                CreateFolder.CreatFolders(Selection.activeGameObject, CreateFolder.ScriptType.UGUI);
            }
        }


    }
}