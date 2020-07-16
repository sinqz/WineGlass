using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class UISpriteAdapter : MonoBehaviour
{

    [System.Serializable]
    public class SpriteInfo
    {
        public SpriteRenderer Value = null;
        public EFillModel Model = EFillModel.ShowAll;
    }

    public enum EFillModel
    {
        /// <summary>
        /// 显示图片的所有内容
        /// </summary>
        ShowAll,
        /// <summary>
        /// 使图片内容填满屏幕
        /// </summary>
        Full,
        /// <summary>
        /// 根据图片高度填充屏幕
        /// </summary>
        WithHeight,
        /// <summary>
        /// 根据图片宽度填充屏幕
        /// </summary>
        WithWidth

    }

    public enum EUpdateType
    {
        /// <summary>
        /// 只在唤醒时更新一次
        /// </summary>
        UpdateOnAwake,
        /// <summary>
        /// 再每次视口发生变化的时候更新一次
        /// </summary>
        UpdateOnViewportChanged
    }

    public EUpdateType TickType = EUpdateType.UpdateOnAwake;
    public SpriteInfo[] Members;
    Camera Viewport;
    float ScreenRatio;

    void Awake()
    {
        Viewport = GetComponent<Camera>();
        UpdateAll();
    }

    private void LateUpdate()
    {
        if (TickType != EUpdateType.UpdateOnViewportChanged) return;
        if (ScreenRatio != Viewport.aspect)
        {
            UpdateAll();
        }
    }

    /// <summary>
    /// 更新所有应用屏幕适配的图片
    /// </summary>
    void UpdateAll()
    {
        for (int i = 0; i < Members.Length; i++)
        {
            AdaptSpriteRender(Members[i]);
        }

        ScreenRatio = Viewport.aspect;
    }

    /// <summary>
    /// 使sprite铺满整个屏幕
    /// </summary>
    private void AdaptSpriteRender(SpriteInfo _spriteInfo)
    {
        SpriteRenderer spriteRenderer = _spriteInfo.Value;
        Vector3 scale = spriteRenderer.transform.localScale;
        float cameraheight = Viewport.orthographicSize * 2;
        float camerawidth = cameraheight * Viewport.aspect;
        float texr = (float)spriteRenderer.sprite.texture.width / spriteRenderer.sprite.texture.height;
        float viewr = camerawidth / cameraheight;
        switch (_spriteInfo.Model)
        {
            case EFillModel.WithHeight:
                //> 根据图片高度进行填充
                scale *= cameraheight / spriteRenderer.bounds.size.y;
                break;
            case EFillModel.WithWidth:
                //> 根据图片宽度进行填充
                scale *= camerawidth / spriteRenderer.bounds.size.x;
                break;
            case EFillModel.Full:
                //> 填满整个屏幕
                if (viewr >= texr)
                {
                    if (viewr >= 1 && texr >= 1 || texr < 1)
                        scale *= camerawidth / spriteRenderer.bounds.size.x;
                    else
                        scale *= cameraheight / spriteRenderer.bounds.size.y;
                }
                else
                {
                    if (viewr <= 1 || texr > 1)
                        scale *= cameraheight / spriteRenderer.bounds.size.y;
                    else
                        scale *= camerawidth / spriteRenderer.bounds.size.x;
                }
                break;
            default:
                //> 在屏幕上显示图片的全部内容
                if (viewr >= texr)
                {
                    scale *= cameraheight / spriteRenderer.bounds.size.y;
                }
                else
                {
                    scale *= camerawidth / spriteRenderer.bounds.size.x;
                }
                break;
        }
        spriteRenderer.transform.localScale = scale;
    }
}
