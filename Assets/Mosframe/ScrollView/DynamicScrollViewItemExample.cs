
namespace Mosframe
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class DynamicScrollViewItemExample : UIBehaviour, IDynamicScrollViewItem
    {
        public Text title;
       
        public virtual void onUpdateItem(int index)
        {
            this.title.text = $"{index+1}";
        }
    }
}