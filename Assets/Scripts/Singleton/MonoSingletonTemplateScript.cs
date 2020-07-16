using UnityEngine;

/// <summary>
/// 组件的单例模板类
/// 为什么要写这个类？
/// 因为我项目中可能要写不止一个单例，避免重复的写代码，这里写一个通用的单例基类，将来只要继承这个单例模板类，他就是一个单例了
/// </summary>
public abstract class MonoSingletonTemplateScript<T> : MonoBehaviour where T : MonoBehaviour
{
	#region 双重检测机制安全线程组件单例模板

	private static T instance;
	static readonly object padlock = new object ();

	public static T Instance {
		get {
			if (instance == null) {	
				lock (padlock) {
					if (instance == null) {	
						//默认是没挂载的，我们动态创建一个对象并自动挂载组件单利脚本
						var obj = new GameObject (typeof(T).Name);
						instance = obj.AddComponent<T> ();
						DontDestroyOnLoad(obj);
					}
				}
			}
			return instance;
		}
	}

	#endregion

	#region Unity回调

	//将来子类可以重写他，也可以不重写
	protected virtual void Awake ()
	{
		instance = this as T;
	}

	#endregion
}
