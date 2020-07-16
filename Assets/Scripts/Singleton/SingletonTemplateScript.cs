using UnityEngine;
using System.Collections;

/// <summary>
/// 非组件的单例模板类
/// </summary>
public abstract class SingletonTemplateScript<T> : System.IDisposable  where T : new()
{
	//当我们自定义的类及其业务逻辑中引用某些托管和非托管资源，就需要实现IDisposable接口，
	//实现对这些资源对象的垃圾回收

	#region 双重检测机制安全线程非组件单例模板

	private static T instance;
	static readonly object padlock = new object ();

	public static T Instance {
		get {
			if (instance == null) {
				lock (padlock) {
					if (instance == null) {
						instance = new T ();
					}
				}
			}
			return instance;
		}
	}

	#endregion

	#region 方法

	public virtual void Dispose ()
	{
		//todo
	}

	#endregion

}
