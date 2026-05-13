using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action_Script : MonoBehaviour
{
    //전체적인 전투 스크립트
    //GameManager 오브젝트 내 삽입
    [Header("플레이어 공격/방어력")]
    public Action_Item player_equipment;
    [Header("적 공격/방어력")] 
    public Action_Item enemy_equipment;

    [Header("플레이어 공격/방어력 텍스트")]
    public GameObject player_att_obj;
    [Header("적 공격/방어력 텍스트")]
    public GameObject enemy_att_obj;

    [Header("플레이어 기본 공격/방어력 텍스트")]
    public GameObject default_player_att_obj;
    [Header("적 기본 공격/방어력 텍스트")]
    public GameObject default_enemy_att_obj;

    [Header("후공 시 보이는 적 행동 텍스트")]
    public GameObject enemy_next_action_obj;

    [Header("선택횟수 텍스트")]
    public GameObject player_choice_num_obj;
    [Header("현재 플레이어 행동")]
    public GameObject player_now_obj;
    [Header("현재 적 행동")]
    public GameObject enemy_now_obj;

    enum ACT //행동
    {
        None,
        Attack,
        Defense,
        CounterAttack
 
    }

    bool is_touch =false; //턴 시작 시 중복 클릭 오류 방지

    public int player_all_damages; //플레이어가 주는 데미지량/방어량
    public int enemy_all_damages; //적이 주는 데미지량/적의 방어량
    public int player_frist_hp; //반격 기술을 위해 전투 시작 시 hp 저장

    List<ACT> player_action_turn = new List<ACT>(); //턴 지정용 리스트
    List<ACT> enemy_action_turn = new List<ACT>();

    Coroutine Coroutine_Delay;//딜레이용 코루틴

    GameObject[] act_drag = new GameObject[3];

    IEnumerator Wait_Delay(float wait_time)
    {

        while (GameManager.Manager.turn < GameManager.Manager.All_TURN)
        {
            yield return new WaitForSeconds(wait_time);//wait_time만큼 딜레이 후 다음 코드 실행
            Check();
        }
        StopCoroutine(Wait_Delay(0));//코루틴 종료  
        Invoke("Reset_Turn", 2);//2초 뒤 턴 리셋

    }


    // Start is called before the first frame update
    void Start()
    {
        
        player_frist_hp = GameManager.Manager.player_hp;

        enemy_next_action_obj.GetComponent<Text>().text = "적의 행동\n";
        Enemy_Action_Frist();

        default_player_att_obj.GetComponent<Text>().text ="기본 공격 / 방어력: "+ player_equipment.item_act.ToString()+"\n추가 공격 / 방어력: "+ player_equipment.min.ToString()+" ~ "+player_equipment.max.ToString();
        default_enemy_att_obj.GetComponent<Text>().text = "기본 공격 / 방어력: " + enemy_equipment.item_act.ToString() + "\n추가 공격 / 방어력: " + enemy_equipment.min.ToString() + " ~ " + enemy_equipment.max.ToString();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Manager.action_sequence)//후공 시
            enemy_next_action_obj.SetActive(true); //적 행동 텍스트 보임

        else
            enemy_next_action_obj.SetActive(false);

        player_att_obj.GetComponent<Text>().text = "플레이어의 이번 턴 총 공격/ 방어력\n"+player_all_damages.ToString();
        enemy_att_obj.GetComponent<Text>().text = "적의 이번 턴 총 공격/ 방어력\n"+ enemy_all_damages.ToString();
        player_choice_num_obj.GetComponent<Text>().text = player_action_turn.Count.ToString() + " 번째 선택";

    }

    void Reset_Turn()
    {
        GameManager.Manager.turn = 0; //모든 판정이 끝나면 턴 리셋
        GameManager.Manager.ACT_TURN = 1;
        GameManager.Manager.Random_Bool(); //선공 후공 재결정
       


        player_action_turn.Clear(); //플레이어 행동 리셋
        player_now_obj.GetComponent<Text>().text = " ";
        player_now_obj.SetActive(false); //플레이어 행동 표시 끔

        enemy_action_turn.Clear(); //적 행동 리셋
        enemy_next_action_obj.GetComponent<Text>().text = "적의 행동\n";
        Enemy_Action_Frist();
        enemy_now_obj.GetComponent<Text>().text = " ";
        enemy_now_obj.SetActive(false); //적 행동 표시 끔
        is_touch = false; //버튼 터치 여부 리셋

        player_frist_hp = GameManager.Manager.player_hp; //반격을 위해 턴 시작 시의 체력 저장


    }

    void Enemy_Action_Frist() //적 ai 선공
    {
        if (!GameManager.Manager.action_sequence) //적이 선공이라면
        {
            for (int i = 0; i < GameManager.Manager.All_TURN; i++)
            {
                ACT enemy_act = (ACT)(Random.Range(1, 3));
                this.enemy_action_turn.Add(enemy_act);  //적의 행동. 랜덤하게 정해짐
            }

            for (int i = 0; i < GameManager.Manager.All_TURN; i++) //적 행동 입력
            {
                List<ACT> list = enemy_action_turn;
                enemy_next_action_obj.GetComponent<Text>().text += list[i].ToString() + " ";
            }

        }
     
       
    }

    void Enemy_Action_End()
    {
        if (GameManager.Manager.action_sequence) //적이 후공이라면
        {
            for (int i = 0; i < GameManager.Manager.All_TURN; i++) //후공은 플레이어 행동을 보고 결정되기에 따로 함수로 작성(턴 시작 시에 결정)
            {
                switch (player_action_turn[i])
                {
                    case ACT.Attack:
                        if (enemy_equipment.item_act > player_equipment.item_act)
                            enemy_action_turn[i] = ACT.Attack;
                        else
                            enemy_action_turn.Add(ACT.Defense);
                        break;

                    case ACT.Defense:
                        enemy_action_turn.Add(ACT.Attack);
                        break;

                    case ACT.CounterAttack:
                        {
                            ACT enemy_act = (ACT)(Random.Range(1, 3));
                            this.enemy_action_turn.Add(enemy_act);
                        }
                        break;
                }
            }

        }

    }


    void Action(ACT Player_Act, ACT Enemy_Act) //판정
    {
      

        switch (Enemy_Act) //적의 행동
        {
            case ACT.Attack: //공격

                if (GameManager.Manager.action_sequence) //플레이어 선공(적 후공)
                    enemy_all_damages = enemy_equipment.item_act;
                else //플레이어 후공(적 선공)
                    this.enemy_all_damages = enemy_equipment.item_act + Random.Range(enemy_equipment.min, enemy_equipment.max);
                break;


            case ACT.Defense: //방어
                if (Player_Act == ACT.Defense) //만약 플레이어도 방어라면
                    return; //턴 넘어감

                if (GameManager.Manager.action_sequence) //플레이어 선공(적 후공)
                    this.enemy_all_damages = enemy_equipment.item_act + Random.Range(enemy_equipment.min, enemy_equipment.max);

                else //플레이어 선공(적 선공)
                    this.enemy_all_damages = enemy_equipment.item_act;
                break;

        }

        switch (Player_Act) //나의 행동
        {
            case ACT.Attack: //공격
              
                if (GameManager.Manager.action_sequence) //플레이어 선공
                    this.player_all_damages = player_equipment.item_act + Random.Range(player_equipment.min, player_equipment.max); //총 데미지량
                else //후공
                    this.player_all_damages = player_equipment.item_act;

                if(Enemy_Act == ACT.Attack)
                {
                    if (this.player_all_damages > this.enemy_all_damages) //플레이어의 데미지가 더 크다면
                    {
                        GameManager.Manager.Enemy_Damages(this.player_all_damages); //적 데미지
                    }
                    else if (this.player_all_damages == this.enemy_all_damages)//같다면
                    {
                        //아무 일 없음
                    }
                    else //더 적다면
                    {
                        GameManager.Manager.Player_Damages(this.enemy_all_damages); //플레이어 데미지
                    }
                   
                }
                else if(Enemy_Act == ACT.Defense)
                {
                    if (this.player_all_damages > this.enemy_all_damages) //플레이어의 데미지가 더 크다면
                    {
                        GameManager.Manager.Enemy_Damages(this.player_all_damages); //적 데미지
                    }
                    else if (this.player_all_damages <= this.enemy_all_damages)//같거나 더 적다면
                    {
                        //아무 일 없음
                    }
                   
                }
                break;

            case ACT.Defense: //방어
                if (GameManager.Manager.action_sequence) //플레이어 선공
                    this.enemy_all_damages = enemy_equipment.item_act;
                else //플레이어 후공
                    this.enemy_all_damages = enemy_equipment.item_act + Random.Range(enemy_equipment.min, enemy_equipment.max);


                if (this.player_all_damages > this.enemy_all_damages) //플레이어의 방어가 데미지보다 더 크다면
                {
                    //방어 성공. 아무 일 없음
                }
                else if (this.player_all_damages == this.enemy_all_damages)//같다면
                {
                    //아무 일 없음
                }
                else //작다면
                {
                    GameManager.Manager.Player_Damages(this.enemy_all_damages); //방어 실패/데미지

                }
                break;

            case ACT.CounterAttack: //반격
                if (player_frist_hp > GameManager.Manager.player_hp) //만약 데미지를 입었다면
                {
                    this.player_all_damages = player_equipment.item_act + Random.Range(player_equipment.min, player_equipment.max); //총 데미지량
                    GameManager.Manager.Enemy_Damages(this.player_all_damages); //무조건 적에게 데미지
                }
                break;

        }
    }

    public void Action_Start_Button()
    {
        if (player_action_turn.Count == GameManager.Manager.All_TURN && !is_touch)//마지막 턴까지 선택했고 이미 버튼을 누르지 않았을 때
        {
            Enemy_Action_End(); //후공이라면 여기서 적의 행동 결정
            player_now_obj.SetActive(true);
            enemy_now_obj.SetActive(true);

            is_touch = true;
            Coroutine_Delay = StartCoroutine(Wait_Delay(2.0f)); //코루틴 실행 
        }

    }

    void Check() //딜레이를 위해 판정을 따로 함수로 뺌
    {
        enemy_now_obj.GetComponent<Text>().text = "적의 이번 행동 " + enemy_action_turn[GameManager.Manager.turn].ToString();
        player_now_obj.GetComponent<Text>().text = "이번 행동 " + player_action_turn[GameManager.Manager.turn].ToString();

        Debug.Log(player_action_turn[GameManager.Manager.turn].ToString() + " " + enemy_action_turn[GameManager.Manager.turn].ToString());

        ACT p = player_action_turn[GameManager.Manager.turn];
        ACT e = enemy_action_turn[GameManager.Manager.turn];
        Action(p, e);
        GameManager.Manager.turn++;
    }

    //아래 함수는 Action_Base에서 사용

    public void Attack_Act() //공격 시
    {
        if(player_action_turn.Count != GameManager.Manager.All_TURN)
        {
            player_action_turn.Add(ACT.Attack);
            Debug.Log("a");
        }
          
    }

    public void Defense_Act() //방어 시
    {
        if (player_action_turn.Count != GameManager.Manager.All_TURN)
        {
            player_action_turn.Add(ACT.Defense);
            Debug.Log("D");

        }
    }
    public void Counterattack_Act() //반격 시
    {
        if (player_action_turn.Count != GameManager.Manager.All_TURN)
        {
            player_action_turn.Add(ACT.CounterAttack);
            Debug.Log("C");

        }
           
    }


    //----------------

    public void Reset_Act() //행동 취소
    {
        player_action_turn.Clear();
        GameManager.Manager.ACT_TURN = 1;
        is_touch = false;
    }


}
