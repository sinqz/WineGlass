using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject MainRote;
    public GameObject Point;
    public GameObject MainPanel;
    private CanvasGroup TempGroup;

    private void Start()
    {
        TempGroup = MainPanel.GetComponent<CanvasGroup>();
    }

    public void OnClickMove()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        TempGroup.blocksRaycasts = false;
        MainRote.SetActive(true);
        Point.transform.eulerAngles = new Vector3(0, 0, 0);
        int rannum = Random.Range(4, 7);
        int Ang = 60 * rannum + 720 - 30;
        while (Ang > 0)
        {
            Point.transform.Rotate(0, 0, -10);
            Ang -= 10;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.4f);
        MainRote.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        PlayerManger.NumMobile = rannum;
        PlayerManger.Instance.tempPlayer.IsRun = true;
        TempGroup.blocksRaycasts = true;
    }
}
