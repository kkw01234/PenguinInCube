using UnityEngine;
using UnityEngine.EventSystems;

public class OpenAnswerPanel : MonoBehaviour, IPointerUpHandler
{
    public GameObject answerPanel;

    //버튼을 눌렀을 때 패널을 킴
    public void OnPointerUp(PointerEventData eventData)
    {
        answerPanel.SetActive(true);
    }
}
