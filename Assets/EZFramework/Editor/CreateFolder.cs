using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace EZFramework
{
    public class CreateFolder : MonoBehaviour
    {
        //对象资源存放地址
        private const string Path = "/Project/";

        //Script文件夹
        private const string ScriptPath = "/Script";

        //Resources文件夹
        private const string ResourcesPath = "/Resources";

        //切图文件夹
        private const string Image = "/Image";

        //基础脚本模板路径
        private const string StandardPath = "/Editor/ScriptTemplate/Standard.cs";


        /// <summary>
        /// 创建脚本文件夹，预制体文件夹
        /// </summary>
        /// <param name="game"></param>
        public static void CreatFolders(GameObject game, ScriptType type = ScriptType.Default)
        {
            string scrPath = $"{Application.dataPath}{Path}{game.name}{ScriptPath}";
            CreatFolder(scrPath);
            switch (type)
            {
                case ScriptType.Default:
                    CreateStandardCs(game.name, scrPath);
                    break;
                case ScriptType.UGUI:
                    CreateCustomUICs(game, scrPath);
                    break;
                default:
                    break;
            }

            string resPath = $"{Application.dataPath}{Path}{game.name}{ResourcesPath}";
            CreatFolder(resPath);
            CreatPrefab(game, resPath);

            string imgPath = $"{Application.dataPath}{Path}{game.name}{Image}";
            CreatFolder(imgPath);

        }

        public enum ScriptType
        {
            Default,
            UGUI
        }

        /// <summary>
        /// 是否存在该路径，没有就创建
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreatFolder(string path)
        {
            DirectoryInfo mydir = new DirectoryInfo(path);
            if (!mydir.Exists)
            {
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();
            }
            return true;
        }

        /// <summary>
        /// 基础脚本模板
        /// </summary>
        /// <param name="csName"></param>
        /// <param name="path"></param>
        public static void CreateStandardCs(string csName, string path)
        {
            string sb = File.ReadAllText(Application.dataPath + StandardPath);
            sb = sb.Replace("Standard", csName);
            System.IO.File.WriteAllText($"{path}/{csName}.cs", sb.ToString());
        }

        /// <summary>
        /// 自定义脚本模板
        /// </summary>
        /// <param name="csName"></param>
        /// <param name="path"></param>
        public static void CreateCustomUICs(GameObject game, string path)
        {

            StringBuilder sb = new StringBuilder();
            //命名空间
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using EZFramework;");
            sb.AppendLine();

            //类开始
            sb.AppendLine($"public class {game.name} : EZMonoBehaviour");
            sb.AppendLine("{");

            //生成属性
            var paths = TemplateRule(game, sb);

            //添加Start
            sb.AppendLine("\n\tpublic override void Start()");
            sb.AppendLine("\t{");
            sb.AppendLine();

            Assignment(paths, sb, game);

            sb.AppendLine();
            sb.AppendLine("\t}");

            //添加End
            sb.AppendLine("\n\tpublic override void End()");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tEZUIGroup.Close(this);");
            sb.AppendLine("\t}");

            //类结尾
            sb.AppendLine();
            sb.AppendLine("}");
            System.IO.File.WriteAllText($"{path}/{game.name}.cs", sb.ToString());

            ////自动添加脚本(新建脚本不灵，且不需要)
            //game.AddComponent(Type.GetType($"{game.name}, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null"));
            AssetDatabase.Refresh();

        }

        /// <summary>
        /// 从子物体上获取信息
        /// </summary>
        /// <param name="game"></param>
        /// <param name="sb"></param>
        public static List<string[]> TemplateRule(GameObject game, StringBuilder sb)
        {
            sb.AppendLine("\tprivate Transform thisTra;");
            List<string[]> gamePaths = new List<string[]>();
            Transform[] childs = game.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childs.Length; i++)
            {
                string[] tempname = Rules(childs[i].name.ToLower());
                if (!string.IsNullOrEmpty(tempname[0]))
                {
                    sb.AppendLine($"\n\tprivate {tempname[0]} m_{tempname[1]};");
                    gamePaths.Add(new string[] { "m_" + tempname[1], GetHierarchyPath(childs[i].gameObject, game.transform), tempname[0] });
                }
            }
            return gamePaths;
        }

        /// <summary>
        /// 获取脚本类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string[] Rules(string name)
        {
            string[] strs = name.Split('_');
            string[] values = new string[2];
            if (strs.Length < 2)
            {
                return values;
            }
            switch (strs[strs.Length - 1])
            {
                case "btn":
                    values[0] = "Button";
                    break;
                case "tog":
                    values[0] = "Toggle";
                    break;
                case "img":
                    values[0] = "Image";
                    break;
                case "text":
                    values[0] = "Text";
                    break;
                case "slider":
                    values[0] = "Slider";
                    break;
                case "tra":
                    values[0] = "Transform";
                    break;
                case "game":
                    values[0] = "GameObject";
                    break;
                case "input":
                    values[0] = "InputField";
                    break;
                case "toggroup":
                    values[0] = "ToggleGroup";
                    break;
                default:
                    values[0] = string.Empty;
                    break;
            }
            strs[strs.Length - 1] = "";
            values[1] = string.Join(",", strs).Replace(",", "");
            return values;
        }

        /// <summary>
        /// 查找物体赋值
        /// </summary>
        /// <param name="paths">paths[i][0] 属性名，paths[i][1]物体路径，paths[i][2]类名</param>
        /// <param name="sb"></param>
        /// <param name="className"></param>
        private static void Assignment(List<string[]> paths, StringBuilder sb, GameObject game)
        {
            sb.AppendLine("\t\tthisTra = EZUIGroup.Open(this);");
            sb.AppendLine();
            for (int i = 0; i < paths.Count; i++)
            {
                string str = string.Format("{0}{1}{2}", "\"", paths[i][1], "\"");
                sb.AppendLine($"\t\t{paths[i][0]} = thisTra.Find({str}).GetComponent<{paths[i][2]}>();");
            }
        }

        /// <summary>
        /// 获取Canvas下的路径
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static string GetHierarchyPath(GameObject gameObject, Transform tra)
        {
            List<string> paths = new List<string>();
            Transform current = gameObject.transform;
            while (current)
            {
                paths.Insert(0, current.name);
                current = current.parent == tra ? null : current.parent;
            }
            return string.Join("/", paths);
        }


        /// <summary>
        /// 创建预制体到指定路径
        /// </summary>
        /// <param name="game"></param>
        /// <param name="path"></param>
        public static void CreatPrefab(GameObject game, string path)
        {
            if (PrefabUtility.IsAnyPrefabInstanceRoot(game))
            {
                PrefabUtility.UnpackPrefabInstance(game, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            }
            path = string.Format("{0}{1}{2}{3}", path, "/", game.name, ".prefab");
            GameObject prefabObj = PrefabUtility.SaveAsPrefabAssetAndConnect(game, path, InteractionMode.AutomatedAction);
            AssetDatabase.Refresh();
        }
    }
}
