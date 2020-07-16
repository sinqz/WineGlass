using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EZFramework
{
    public class InIt : MonoSingletonTemplateScript<InIt>
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            DontDestroyOnLoad(new GameObject("[Application]", typeof(InIt)));
            PlayerPrefs.DeleteAll();
        }

        protected override void Awake()
        {
            //游戏启动脚本，一切都从此处开始
            GameObject GameController = new GameObject("GameController");
            GameController.AddComponent<GameController>();
        }

        private void Start()
        {
            OnLoad();
        }

        public void OnLoad()
        {

            EZComponent.AddConment<InItUI>();
            EZComponent.AddConment<MenuBG>();
            EZComponent.AddConment<MenuLogin>();
        }

        private void Update()
        {
            EZComponent.Update();
        }

        private void LateUpdate()
        {
            EZComponent.LateUpdate();
        }


    }
}