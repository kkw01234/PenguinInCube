using UnityEngine;
using UnityEngine.EventSystems;

public class CloseAnswerPanel : MonoBehaviour, IPointerUpHandler
{
    public GameObject answerPanel;

    //버튼을 눌렀을 때 패널을 끔
    public void OnPointerUp(PointerEventData eventData)
    {
        answerPanel.SetActive(false);
    }
}
