

public enum SceneType
{
    ST_MENU,//注册场景
    ST_MAIN,//游戏大厅
    ST_GAME,//游戏场景
}




public class SceneConstant
{
    //所有场景的名称
    private const  string SCENE_Menu = "Menu";
    private const string SCENE_Main = "Main";
    private const string SCENE_Game = "Game";

    //根据场景的类型获取场景名称
    public static string GetNameWithType(SceneType m_SceneType)
    {
        string tmpName = "";
        switch (m_SceneType)
        {
            case SceneType.ST_MENU:
                tmpName = SCENE_Menu;
                break;
            case SceneType.ST_MAIN:
                tmpName = SCENE_Main;
                break;
            case SceneType.ST_GAME:
                tmpName = SCENE_Game;
                break;
        }
        return tmpName;
    }
}
