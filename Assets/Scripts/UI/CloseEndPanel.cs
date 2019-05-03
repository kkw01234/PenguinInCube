using UnityEngine;
using UnityEngine.EventSystems;

public class CloseEndPanel : MonoBehaviour, IPointerUpHandler
{
    public GameObject startButton;
    public GameObject overPanel;

    //버튼을 눌렀을 때 패널을 끄고 시작 버튼 활성화
    public void OnPointerUp(PointerEventData eventData)
    {
        if(GGUMI.instance == null)
        {
            //최고, 현재 기록 표시 초기화
            GameManager.instance.nowRecordText.SetActive(false);
            GameManager.instance.bestRecordText.SetActive(false);
            overPanel.SetActive(false);
            startButton.SetActive(true);
        }
    }
}
