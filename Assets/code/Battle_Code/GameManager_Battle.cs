using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //GameManager 오브젝트 내 삽입

    public static GameManager Manager;

  
    public int All_TURN = 3; //총 턴 수
    public int ACT_TURN = 1; //현재 행동 결정 턴(Act_Base에서 씀)

    public bool action_sequence; // 선공/ 후공 결정 - ture = player 선
    public int player_hp = 100;
    public int enemy_hp = 100;
    public int turn = 0; //몇 번째 턴인지 확인하는 변수
    

    [Header("플레이어 체력 바")]
    public GameObject player_hp_obj;
    public GameObject player_hp_num_obj;

    [Header("적 체력 바")]
    public GameObject enemy_hp_obj;
    public GameObject enemy_hp_num_obj;

    [Header("선공 후공 텍스트")]
    public GameObject action_sequence_obj;

    [Header("승리 판정 텍스트")]
    public GameObject win_obj;

    private void Awake()
        
    {
  
        Manager = this;
        Random_Bool();
    }

    // Update is called once per frame
    void Update()
    {
        player_hp_obj.GetComponent<Image>().fillAmount = player_hp / 100.0f;
        enemy_hp_obj.GetComponent<Image>().fillAmount = enemy_hp / 100.0f; 
        player_hp_num_obj.GetComponent<Text>().text = player_hp.ToString();
        enemy_hp_num_obj.GetComponent<Text>().text = enemy_hp.ToString();

        if (action_sequence)
            action_sequence_obj.GetComponent<Text>().text = "플레이어 선공 " + turn.ToString() + "턴";
        else
            action_sequence_obj.GetComponent<Text>().text = "플레이어 후공 " + turn.ToString() + "턴";

        if (player_hp < 0)
        {
            win_obj.SetActive(true);
            win_obj.GetComponent<Text>().text = "플레이어 패배";
        }
           
        else if (enemy_hp < 0)
        {
            win_obj.SetActive(true);
            win_obj.GetComponent<Text>().text = "플레이어 승리";
        }
           
    }

    public void Player_Damages(int damages) //플레이어 체력 감소 함수
    {
        player_hp -= damages;
    }

    public void Enemy_Damages(int damages) //적 체력 감소 함수
    {
       enemy_hp -= damages;
    }

    public void Random_Bool()//선공 후공 결정
    {
        action_sequence = (Random.value > 0.5f); //랜덤으로 50%확률
    }
}
