using Mosframe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryItem : DynamicScrollViewItemExample
{
    public Image background { get { return GetComponent<Image>(); } }
    private readonly Color[] colors = new Color[] { new Color(213f / 255f, 215f / 255f, 249f / 255f, 255f / 255f), Color.clear };
    public override void onUpdateItem(int index)
    {
        base.onUpdateItem(index);
        this.background.color = this.colors[Mathf.Abs(index - 1) % this.colors.Length];
    }
}
