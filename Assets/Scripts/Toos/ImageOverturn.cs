using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOverturn : Image
{
    /// <summary>
    /// 是否水平翻转
    /// </summary>
    public bool FlipHorizontal
    {
        get { return flipHor; }
        set
        {
            flipHor = value;
            UpdateGeometry();
        }
    }

    /// <summary>
    /// 是否垂直翻转
    /// </summary>
    public bool FlipVertical
    {
        get { return flipVer; }
        set
        {
            flipVer = value;
            UpdateGeometry();
        }
    }
    [SerializeField]
    protected bool flipHor;
    [SerializeField]
    protected bool flipVer;

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);

        if (flipHor || flipVer)
        {
            Vector2 rectCenter = rectTransform.rect.center;

            int vertCount = toFill.currentVertCount;
            for (int i = 0; i < vertCount; i++)
            {
                UIVertex uiVertex = new UIVertex();
                toFill.PopulateUIVertex(ref uiVertex, i);

                Vector3 pos = uiVertex.position;
                uiVertex.position = new Vector3(
                    flipHor ? (pos.x + (rectCenter.x - pos.x) * 2) : pos.x,
                    flipVer ? (pos.y + (rectCenter.y - pos.y) * 2) : pos.y,
                    pos.z);

                toFill.SetUIVertex(uiVertex, i);
            }
        }
    }
}
