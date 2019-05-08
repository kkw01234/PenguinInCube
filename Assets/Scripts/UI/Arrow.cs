using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int number;
    public Animation aniGateOpen;
    public Animation aniGateClose;

    private void OnTriggerStay(Collider player)
    {
        if (GGUMI.instance.joyActionButton.isPressed)
        {
            GGUMI.instance.joyActionButton.isPressed = false;
            GameManager.instance.CheckAnswer(number);
            if (number == GameManager.instance.answerNumber)
                StartCoroutine("GetIntoDoor");
        }
    }

    IEnumerator GetIntoDoor()
    {
        int moveIndex = (GameManager.instance.answerNumber + 2) % 4;
        if (GameManager.instance.level == 5)
            moveIndex = 4;
        GGUMI.instance.isMoveToDoor = true;
        GGUMI.instance.transform.LookAt(aniGateOpen.transform);
        aniGateOpen.Play("GateOpen"); //들어갈 문 열기
        for (int i = 0; i < 120; i++)
        {
            GGUMI.instance.transform.position += GGUMI.instance.transform.forward * 0.013f;
            //다음 방으로 이동
            if (i == 55)
            {
                aniGateOpen.Play("GateIdle"); //들어간 문을 닫아줌
                //문 앞으로 순간이동
                GGUMI.instance.transform.position = new Vector3(
                    GameManager.instance.arrows[moveIndex].transform.position.x,
                    GGUMI.instance.gameObject.transform.position.y,
                    GameManager.instance.arrows[moveIndex].transform.position.z);
                aniGateClose.Play("GateClose"); //나온 문을 닫아줌
                Cube.instance.ResetToy();
                Cube.instance.GenerateToy();
                GameManager.instance.loadProblem = true;
            }
            yield return new WaitForSeconds(0.01f);
        }
        GGUMI.instance.isMoveToDoor = false;
    }
}
