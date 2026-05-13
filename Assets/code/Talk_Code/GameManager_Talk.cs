using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Talk : MonoBehaviour
{

    //기본적으로 DontDestroy_Gamemanager-is_talk_start가 true거나 Talk_Scene_Start_Talk가 같이 있지 않으면 꺼져 있는 상태 (Talk_box_hide 함수)
    //스토리 대사 출력
    static int MAX = 0; //텍스트 갯수
    bool is_show_talk = true;
    string[] talk; //텍스트 내용 저장
    
    [Header("선택지 화면")]
    public GameObject choice_background;
    [Header("선택지 버튼 텍스트")]
    public GameObject button_1_text;
    public GameObject button_2_text;

    public bool is_check_1 = false; //분기점 넘어가는 용도
    public bool is_check_2 = false; //분기점 넘어가는 용도
    public bool is_no_click = false; //클릭 불가능. 분기점/CG 띄우기에서 사용
    public bool is_talk_start_this = false; //Update함수가 Talk_Start()실행 전에 실행되는 것 방지
    public bool is_check_3 = false; //아이템 확인 분기점 넘어가는 용도
    public bool is_check_4 = false; //아이템 확인 분기점 넘어가는 용도

    public bool is_two_text = false; //두 번째 칸 사용 시
    

    [Header("대사창 텍스트")]
    public GameObject talk_box_text; //말
    [Header("대사창의 이름 텍스트")]
    public GameObject name_box_text; //이름

    [Header("배경 이미지 오브젝트")]
    public GameObject background_image; //배경
    [Header("Cg 오브젝트")]
    public GameObject cg_image; //cg

    [Header("사람 이미지 배열/[0]이 왼쪽,[1]이 오른쪽")]
    public GameObject[] show_human_image;

    [Header("배경음 재생기(AudioSource)")]
    public AudioSource Audio;


    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        Audio.Play();
        Image_Change_Default();
    }

    // Update is called once per frame
    void Update()
    {
        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start)
        {
            Invoke("Talk_Start", 0.01f); //다른 씬에서 넘어올 때 연타로 인한 대사 넘어감 방지용 딜레이
            Debug.Log("Talk " + talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num]);
        }
        else if (DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start_sign)
        {
            Invoke("Talk_Start_Update", 0.01f); //다른 씬에서 넘어올 때 연타로 인한 대사 넘어감 방지용 딜레이
            Debug.Log("Talk_sign");
        }
        
        else if (is_talk_start_this)
        {

            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length != 0 && !is_no_click)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    num_two_sign(); //두 번째 칸 기호 확인
                    //Debug.Log(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1]);
                    //Debug.Log("Click");
                    Debug.Log(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num]);


                    int plusLineNumber = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1;
                    

                    if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith('#'))//배경 변경
                    {
                        Image_Change_Background();
                    }
                    else if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("@"))//사망자에 따라 바뀌는 대사일 경우
                    {
                        Talk_Box_Root();
                    }
                    else if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("$")) //다른 씬으로 넘어가는 경우
                    {
                        Talk_Box_Scnen();
                    }
                    else if (plusLineNumber < MAX && (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("%") || talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith(")")))
                    //대화창 끔/켬, 닫는 괄호는 배경 아이템 클릭 시 사용
                    {
                        Hide_Human_image();
                        Talk_Box_Reversal();
                    }
                    else if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("^")) //사람 이미지만 숨김
                    {
                        Hide_Human_image();
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
                        Talk_Box_Default();

                    }
                    else if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("&")) //CG 뜸
                    {
                        Image_Change_Cg();
                    }
                    /*                    else if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("F")) //얼굴
                                        {
                                            *//*Face_Change();*//* //위 업데이트에서 처리하므로 Talk_Box_Default()로 넘어가지 않도록 처리 - 현재 Talk_Box_Default에서 처리중
                                        }*/

                    else if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("M"))//음악 재생
                    {
                        Sound_Play(); //시작 시 음악 재생은 First_Image_Change_Background에 삽입
                    }
                    else if (plusLineNumber < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("W"))//음악 종료
                    {
                        Sound_Stop();

                    }
                   
                    else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("1") | talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("2"))
                    //선택지들일 경우(Talk_Box_Default와 동시작동하지 않도록 뺌)
                    {
                        if (!is_two_text)
                            Talk_Box_Choice_num();
                    }
                    else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("3") || talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("4"))
                    //기타 분기 대사
                    {
                        if (!is_two_text)
                            Talk_Box_Mini_num();
                    }
                    else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("/") || talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("("))
                    //무시(일반적인 배경 아이템 클릭 시 사용)
                    { //(는 아이템 클릭(Talk_Obj_Click_Object)시 사용
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num ++;
                    }
                    else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("-")) //미니게임 클리어 여부
                    {
                       
                        Talk_Box_Mini_Check();
                    }
                  
                    else
                    {
                       Talk_Box_Default();
                    }

                   
                    if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("*")) //메인 선택지일 경우
                    {
                        Talk_Box_Choice();
                    }

                   
                }


            }

        }
        else
        {
            Talk_Box_Hide();
        }

    }

    void num_two_sign() //두 번째 칸에 기호 확인
    {

      

        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length >= 2)
        {
            //두 번째 칸에 획득여부 확인이 있을 때(선택지와 동시에 작동해야 하는 등)
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 1); //맨 앞 기호 제거
            string non_string_2 = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1); //맨 앞 기호 제거
                                                                                                                                                                                              
            if (non_string_2.StartsWith("+")) //특정 아이템이 있을 경우, 다음에 +가 있으면 즉시 넘어가기에 대사에 스킵용 한 줄 추가
            {

                if (!is_two_text)
                {
                    is_two_text = true;
                    Talk_Box_Item_Check_2();
                }
                else
                {
                    is_two_text = false;
                    is_check_3 = false;
                    is_check_4 = false;
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+=1;
                    Update(); //클릭 후 즉시 + 다음 대사가 창에 뜨게 함
                }

            }


            else if (non_string.StartsWith("#") || non_string.StartsWith("$"))
            //# = 아이템 획득한 적 있음 $= 없음/ 위 선택지처럼 숫자로 하면 배경 변경 등 숫자 충돌 위험 때문에 변경
            {
                Talk_Box_Choice_num_2();
            }
            else if (non_string_2.StartsWith("D")) //사망
            {
                int dead_num = int.Parse(non_string_2.Substring(1, non_string_2.Length - 1)); //맨 앞 기호 제거                                                                                                                                                                                         
                DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[dead_num] = true;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
                Update();

            }

           
          
        }
    
    }

    void Talk_Reset()//파일의 끝까지 읽었다면
    {
        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 > MAX)
        {
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = 0;
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num++; //다음 파일로 넘어감
            Talk_Box_Hide();
        }
    }

    public void Talk_Box_Reversal() //텍스트 박스 - 현재 상태와 반대로 변경
    {
        is_show_talk = !is_show_talk;
        gameObject.SetActive(is_show_talk);
    }

    public void Talk_Box_Show() //텍스트 박스 보임
    {
        is_talk_start_this = true;
        is_show_talk = true;
        gameObject.SetActive(is_show_talk);
    }

    public void Talk_Box_Hide() //텍스트 박스 꺼짐
    {
        is_talk_start_this = false;
        is_show_talk = false;
        gameObject.SetActive(is_show_talk);
    }

    void Hide_Human_image() //사람 이미지 숨김
    {
        show_human_image[0].SetActive(false); //이미지 개수가 적으므로 반복문 사용 안함
        show_human_image[1].SetActive(false);
    }
    public void Talk_Start()
    {
        DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start = false;
        Hide_Human_image();
        string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num]; //다수의 파일 찾음
       // string path = Application.streamingAssetsPath + @"\1_Start.txt"; //파일 찾음
        MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
        talk = new string[MAX];
        talk = System.IO.File.ReadAllLines(path); //파일 내용
        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith('#'))//배경 변경
        {
            First_Image_Change_Background();//맨 처음 대사에 배경 변경이 있을 경우
        }
        else if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith('M'))//배경 변경
        {
            First_Sound_Background();// 맨 처음 대사에 음악 변경이 있을 경우
           
        }

        Talk_Box_Show();
       Talk_Box_Default(); //출력
    }

    public void Talk_Start(int num) //오버라이딩. num번째의 대사 파일 출력
    {
        Hide_Human_image();
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num = num;
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = 0; //새 대사 파일을 불러오므로 시작 지점도 리셋
        
        string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num];
        MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
        talk = new string[MAX];
        talk = System.IO.File.ReadAllLines(path); //파일 내용

        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+1].StartsWith('#'))//배경 변경
        {
            First_Image_Change_Background();//맨 처음 대사에 배경 변경이 있을 경우
        }
        else if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith('M'))//음악 변경
        {
            First_Sound_Background();
          
        }
        Talk_Box_Show();
       
       Talk_Box_Default(); //출력
    }

    public void Talk_Start(int num1,int num2) //오버라이딩. num1번째의 대사 파일/num2번째의 대사 줄 출력
    {
        Hide_Human_image();
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num = num1;
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = num2;

        string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num]; 
        MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
        talk = new string[MAX];
        talk = System.IO.File.ReadAllLines(path); //파일 내용
        Talk_Box_Show();
        Face_Change();
       Talk_Box_Default(); //출력
    }

    public void Talk_Start_item(int num1, int num2) //아이템 출력용. 한 줄 출력하더라도 %로 바로 끄고 넘어가야 하기 때문
    {
        Hide_Human_image();
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num = num1;
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = num2;

        string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num];
        MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
        talk = new string[MAX];
        talk = System.IO.File.ReadAllLines(path); //파일 내용
        Talk_Box_Show();

        Update(); //출력
    }

    public void Talk_Start_Update()
    {
        DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start_sign = false;
        Hide_Human_image();
        string path = Application.streamingAssetsPath + DontDestroy_Gamemanager.Dont_Destroy_Instance.talk_name[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Files_Num]; //다수의 파일 찾음
                                                                                                                                                                               // string path = Application.streamingAssetsPath + @"\1_Start.txt"; //파일 찾음
        MAX = System.IO.File.ReadAllLines(path).Length; //현재 파일의 전체 줄 수
        talk = new string[MAX];
        talk = System.IO.File.ReadAllLines(path); //파일 내용
        Talk_Box_Show();
        Update(); //출력
    }


    void Talk_Box_Default()
    {
        if(is_two_text)
        {
           
            Debug.Log(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num]);
            Face_Change();
            Talk_Reset();
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(2, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 2); //맨 앞 기호 제거

            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("F"))//얼굴 변경
                Face_Change();
            else
            {

                string[] non_string_array = non_string.Split(" ", 2, StringSplitOptions.None);
                name_box_text.GetComponent<Text>().text = non_string_array[0];
                talk_box_text.GetComponent<Text>().text = non_string_array[1];
            }
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
        }
        else
        {
            Debug.Log('a');
            Talk_Reset();
            
           
            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("F"))//얼굴 변경
                Face_Change();
            else
            {
                string[] string_array = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Split(" ", 2, StringSplitOptions.None); //띄어쓰기로 나눔
                
                Debug.Log(string_array[0] + " : " + string_array[1]);
                name_box_text.GetComponent<Text>().text = string_array[0];
                talk_box_text.GetComponent<Text>().text = string_array[1];
                Image_Change_Default();
            }
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
        }

    }

    void Talk_Box_Default_OtherText() //현재 얼굴변경시 사용
    {
        if (is_two_text)
        {

           

        }
        else
        {

            Talk_Reset();

            string[] string_array = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Split(" ", 3, StringSplitOptions.None); //띄어쓰기로 나눔

            name_box_text.GetComponent<Text>().text = string_array[1];
            talk_box_text.GetComponent<Text>().text = string_array[2];




        }

    }

    /* void Check_Talk_Box_Default() //두번째칸 없을 때 업데이트로 변경
     {
         Talk_Box_Default();

         string[] string_array = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Split(" ", 2, StringSplitOptions.None); //띄어쓰기로 나눔
         if (string_array.Length == 1)
             Debug.Log(string_array[0]);
         //Update();
         else
         {
             Debug.Log(string_array[1]);
         }

     }*/



    void Image_Change_Background()
    {
        Hide_Human_image();
            
        int background_num = int.Parse(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1)); //맨 앞 기호 제거
      
        background_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.background_array[background_num];
        background_image.SetActive(true);
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
        Talk_Box_Default();
    }

    void First_Image_Change_Background()//맨 처음 대사에 배경 변경이 있을 경우
    {
        Hide_Human_image();

        int background_num = int.Parse(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1)); //맨 앞 기호 제거
       
        background_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.background_array[background_num];
        background_image.SetActive(true);

        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num ++; //Sound_Play체크, 현재 이 시점에서 배경 다음 판정용
        Debug.Log(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num]);

        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1 < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith('M'))
        {
            First_Sound_Background();
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++; //Sound_Play에서도 1 더하므로 한 번만 더함
        }
        else
            Update();
        //배경만 처리하고 바로 첫 대사가 나오므로 Talk_Box_Default() 필요없음
    }

    void First_Sound_Background()//맨 처음 대사에 음악 변경이 있을 경우
    {
        Sound_Play();
        Update();
        //배경만 처리하고 바로 첫 대사가 나오므로 Talk_Box_Default() 필요없음
    }
    void Sound_Play() //배경음 재생
    {
        int Sound_num = int.Parse(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1)); //맨 앞 기호 제거
        Audio.clip = (DontDestroy_Gamemanager.Dont_Destroy_Instance.sound_array[Sound_num]);
        //Audio.loop = true;
        Audio.Play();
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;   
    }


    void Sound_Stop() //배경음 종료
    { 
        Audio.loop = false;
        Audio.Stop();

        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
    }

    void Talk_box_Choice_Print()
    {

/*        if (is_two_text)
        {
            Talk_Box_Choice_num_2();
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
            int n = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length;
          
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(2, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 2); //맨 앞 기호 제거
            string[] non_string_array = non_string.Split(" ", 2, StringSplitOptions.None);
            name_box_text.GetComponent<Text>().text = non_string_array[0];
            talk_box_text.GetComponent<Text>().text = non_string_array[1];
        }
        else*/
        {
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 1); //맨 앞 기호 제거
            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && non_string.StartsWith("F"))//얼굴 변경
            {

                string[] string_array = non_string.Split(" ", 3, StringSplitOptions.None); //띄어쓰기로 나눔, 첫 번째가 표정, 나머지가 이름/내용
                int face_num = int.Parse(string_array[0].Substring(1, string_array[0].Length - 1)); //맨 앞 기호 제거

                Image_Change_Default(string_array[1], face_num);
                //Debug.Log(string_array[1] + " " + face_num);
                Debug.Log(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num]);
                Talk_Reset();
                name_box_text.GetComponent<Text>().text = string_array[1];
                talk_box_text.GetComponent<Text>().text = string_array[2];

            }
            else
            {
                //Image_Change_Default();
               
                string[] non_string_array = non_string.Split(" ", 2, StringSplitOptions.None);
                name_box_text.GetComponent<Text>().text = non_string_array[0];
                talk_box_text.GetComponent<Text>().text = non_string_array[1];

            }

            
        }
          
    }


    void Talk_box_Print_2()//두번째칸까지 제거
    {
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
        string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(2, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 2); //맨 앞 기호 제거
        string[] non_string_array = non_string.Split(" ", 2, StringSplitOptions.None);
        name_box_text.GetComponent<Text>().text = non_string_array[0];
        talk_box_text.GetComponent<Text>().text = non_string_array[1];
    }


    void Talk_Box_Choice()
    {

        is_no_click = true;
        choice_background.SetActive(is_no_click);
        string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 1); //맨 앞 기호 제거
        string[] non_string_array = non_string.Split("/"); //구분자로 선택지를 나눔
        name_box_text.GetComponent<Text>().text = "";
        talk_box_text.GetComponent<Text>().text = "";
        button_1_text.GetComponent<Text>().text = non_string_array[0];
        button_2_text.GetComponent<Text>().text = non_string_array[1];

    }

    void Talk_Box_Choice_num() //바로 다음 줄 출력
    {
        if (is_check_1)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("1"))//다음 줄이 선택지 1번이라면    
            {
                Talk_box_Choice_Print();
            }
            else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("2") | talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("_"))//두 번째 선택지 구간을 만났다면
            {
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; talk[i].StartsWith("2"); i++) //기호가 없는 곳까지 넘어감
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i+1;
                }

                is_check_1 = false;
                Update(); 

            }
            else //그 이외의 것이라면
            {
                is_check_1 = false;
                 Update();
            }

        }

        if (is_check_2)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("2")) //다음 줄이 선택지 2번이라면
            {
                Talk_box_Choice_Print();
            }
            else
            {
                is_check_2 = false;
                Update();
            }

        }

    }

    void Talk_Box_Choice_num_Default() //즉시 출력 시 문제가 생길 경우(+등을 거치는 경우)
    {
        if (is_check_1)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("1"))//다음 줄이 선택지 1번이라면    
            {
                Talk_box_Choice_Print();
            }
            else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("2") | talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("_"))//두 번째 선택지 구간을 만났다면
            {
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; talk[i].StartsWith("2"); i++) //기호가 없는 곳까지 넘어감
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i + 1;
                }

                is_check_1 = false;
                Talk_Box_Default();

            }
            else //그 이외의 것이라면
            {
                is_check_1 = false;
                Talk_Box_Default();
            }

        }

        if (is_check_2)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("2")) //다음 줄이 선택지 2번이라면
            {
                Talk_box_Choice_Print();
            }
            else
            {
                is_check_2 = false;
                Talk_Box_Default();
            }

        }

    }
    void Talk_Box_Mini_Check()//첫 번째에 -,미니게임 클리어했는지 확인, 분할 기호는 itmecheck와 동일
    {

        if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length >= 2)
        {
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, 1);
           
            //뒤에 있는 숫자 확인, item_check에 대입해서 획득 여부 확인

            int item_num = int.Parse(non_string);
            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[item_num])//성공했다면
            {
                is_check_3 = true;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
            }
            else
            {
                //DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; !talk[i].StartsWith("4"); i++) //기호가 있는 곳까지 넘어감
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i; 
                }
              
                is_check_4 = true;
            }


         
            

            if(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+1].StartsWith("%"))
            {
                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
            }
            
        }
        Talk_Box_Mini_num();

    }
    void Talk_Box_Mini_num()
    {
        if (is_check_3)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("3"))//다음 줄이 선택지 1번이라면    
            {
                Talk_box_Choice_Print();
            }
            else if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("4") | talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("-"))//두 번째 선택지 구간을 만났다면
            {
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; talk[i].StartsWith("4"); i++) //기호가 없는 곳까지 넘어감
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i;
                }

                is_check_3 = false;
                Update();

            }
            else //그 이외의 것이라면
            {
                is_check_3 = false;
                Update();
            }

        }

        if (is_check_4)
        {
            if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].StartsWith("4")) //다음 줄이 선택지 2번이라면
            {
                Talk_box_Choice_Print();
            }
            else
            {
                is_check_4 = false;
                Update();
            }

        }

    }
    void Talk_Box_Item_Check_2()//두 번째에 +,아이템 있는지 확인
    {

        if (talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length >= 3)
        {
            string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(2, 1);
            //뒤에 있는 숫자 확인, item_check에 대입해서 획득 여부 확인

            int item_num = int.Parse(non_string);
            if (DontDestroy_Gamemanager.Dont_Destroy_Instance.item_check[item_num])//있다면
            {
                is_check_3 = true;
                DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2;
                //+ 체크 부분 넘어감(Talk_Box_Default에서 1회 더 더해서 처리)
            }
            else
            {
                //DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
                string non_string_2 = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1);
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; !non_string_2.StartsWith("$"); i++) //기호가 있는 곳까지 넘어감
                {
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i - 1; //Talk_Box_Choice_num_2()에서 1회 덧셈하고 출력하므로 1 뺌
                    non_string_2 = talk[i].Substring(1, talk[i].Length - 1);
                  
                }
                is_check_4 = true;
            }


         
        }

        Talk_Box_Choice_num_2();

    }


    void Talk_Box_Choice_num_2() //2번째 번호, 아이템 체크용 
    {
        String non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1);
       
        if (is_check_3)
        {
            if (non_string.StartsWith("#"))//아이템이 있다면
            {
                Talk_box_Print_2();
            }
            else if (non_string.StartsWith("$"))//아이템이 없는 구간 선택지를 만났다면
            {
                for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1; non_string.StartsWith("$"); i++) //기호가 없는 곳까지 넘어감
                {
                    non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num +1].Length - 1);
                    if (non_string.StartsWith("+"))
                    {
                        is_two_text = false;
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 2; //+부분 스킵
                        Talk_Box_Choice_num_Default();
                        break;
                    }
                    else
                    {
                        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i;
                        Talk_Box_Default();
                    }
                }
                
                is_check_3 = false;
            }
            else //그 이외의 것이라면
            {
                //is_check_3 = false;
                Talk_Box_Default();
            }

        }

        if (is_check_4)
        {
          
            if (non_string.StartsWith("$")) //아이템이 없다면
            {
                Talk_box_Print_2();
            }
            else
            {
                //is_check_4 = false;
                Talk_Box_Default();
            }

        }

    }

    void Talk_Box_Root()
    {
        if (DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[1]&&DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[2]&&DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[3])
        {
            //아무도 생존하지 않음

            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 4; //바로 이후 대사로 넘어감
            Talk_Box_Default();
            return;
        }
        List<string> root_talk = new List<string>(); 

        if(!DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[1]) //false일때 생존한 상태
        {
            root_talk.Add(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 2]);
            root_talk[root_talk.Count - 1] = Dead_Human_Name(root_talk[root_talk.Count - 1]);
        }
        if(!DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[2])
        {
            root_talk.Add(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 3]);
            root_talk[root_talk.Count - 1] = Dead_Human_Name(root_talk[root_talk.Count - 1]);
        }
        if(!DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[3])
        {
            root_talk.Add(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 4]);
            root_talk[root_talk.Count - 1] = Dead_Human_Name(root_talk[root_talk.Count - 1]);
        }

        int random_num = UnityEngine.Random.Range(0, root_talk.Count);
        string root_talk_text = root_talk[random_num];

        if (Input.GetMouseButtonDown(0))
        {
            string[] string_array = root_talk_text.Split(" ", 2, StringSplitOptions.None); //띄어쓰기로 나눔

            name_box_text.GetComponent<Text>().text = string_array[0];
            talk_box_text.GetComponent<Text>().text = string_array[1];
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num += 4;
          
        }
    }

    void Talk_Box_Scnen()
    { 
        string non_string = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Length - 1); //맨 앞 기호 제거
     
        SceneManager.LoadScene(non_string);
    }

    string Dead_Human_Name(string text) //죽은 피해자 이름 출력
    {
        if (text.Contains("(죽은 피해자)"))
        {
            int s = 0;
            
            for(int i =0;i<DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human.Length;i++)
            {
                if (DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[i])
                    s++;
            }
            if(s > 1) //사망 인원 1명 초과 시
            {
                return text.Replace("(죽은 피해자)", "죽은 피해자들");

            }
            else
            {
                if (DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[1])
                {
                    text = text.Replace("(죽은 피해자)", "에이든"); //대체
                }
                if (DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[2])
                {
                    text = text.Replace("(죽은 피해자)", "예린");
                }
                if (DontDestroy_Gamemanager.Dont_Destroy_Instance.dead_human[3])
                {
                    text = text.Replace("(죽은 피해자)", "서연");
                }

            }

        }
        return text;

    }

    //선택지 버튼 클릭 시
    public void click_button_1()
    {
        is_check_1 = true;
        is_no_click = false;
        choice_background.SetActive(is_no_click);
        Talk_box_Choice_Print();

    }
    public void click_button_2()
    {
        is_no_click = false;
        choice_background.SetActive(is_no_click);
        is_check_2 = true;

        for (int i = DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num; !talk[i].StartsWith("2"); i++) //기호가 있는 곳까지 넘어감
        {
            DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num = i;
        }
        num_two_sign();
        Talk_box_Choice_Print();
    }



    void Image_Change_Cg()
    {
        Hide_Human_image();

        cg_image.SetActive(true);
        background_image.SetActive(true);
        int cg_num = int.Parse(talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Substring(1, talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num + 1].Length - 1)); //맨 앞 기호 제거
        background_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.cg_array[cg_num];
        cg_image.GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.cg_array[cg_num];
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num+=2;
        is_no_click = true;
        Talk_Box_Default();

        //이미지를 끄는 건 Cg오브젝트 안에 Cg_Off로 작성함
    }

    void Face_Change()
    {
  if (DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num < MAX && talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].StartsWith("F"))//얼굴 변경
            {

                string[] string_array = talk[DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num].Split(" ", 3, StringSplitOptions.None); //띄어쓰기로 나눔, 첫 번째가 표정, 나머지가 이름/내용
                int face_num = int.Parse(string_array[0].Substring(1, string_array[0].Length - 1)); //맨 앞 기호 제거

                Image_Change_Default(string_array[1], face_num);
                //Debug.Log(string_array[1] + " " + face_num);
                Talk_Box_Default_OtherText();// 미호출 버전으로 다시 부름, 덧셈은 함수 내에서 처리

            }
            else
            {
                //Image_Change_Default();
            }
        
     
    }

        void Image_Change_Default(int f = 0)
    {
        //Debug.Log(name_box_text.GetComponent<Text>().text);
        
            switch (name_box_text.GetComponent<Text>().text)
            {
                case "현":
                    show_human_image[0].SetActive(true);
                    show_human_image[0].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[0].Human_Face_image[f];
                    show_human_image[1].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);//반대편의 이미지는 어둡게 처리
                    show_human_image[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    break;

                case "근육질남성":
                case "에이든":
                    show_human_image[1].SetActive(true);
                    show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[1].Human_Face_image[f];
                    show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                    show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
                case "젊은여성1":
                case "예린":
                    show_human_image[1].SetActive(true);
                    show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[2].Human_Face_image[f];
                    show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
                case "젊은여성2":
                case "서연":
                    show_human_image[1].SetActive(true);
                    show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[3].Human_Face_image[f];
                    show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

                case "인석":
                case "범인":
                    show_human_image[0].SetActive(true);
                show_human_image[0].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[4].Human_Face_image[f];
                show_human_image[1].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

                 case "설향":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[5].Human_Face_image[f];
                show_human_image[1].GetComponent<Image>().sprite = null;
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(0, 0, 1, 1);
                break;

            //case "연구원1":
            case "상사":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[6].Human_Face_image[f];
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            default:
                Hide_Human_image();
                break;
            }


    }

    void Image_Change_Default(string name, int f = 0)
    {
        Debug.Log(name);

        switch (name)
        {
            case "현":
                show_human_image[0].SetActive(true);
                show_human_image[0].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[0].Human_Face_image[f];
                show_human_image[1].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);//반대편의 이미지는 어둡게 처리
                show_human_image[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "근육질남성":
            case "에이든":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[1].Human_Face_image[f];
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "젊은여성1":
            case "예린":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[2].Human_Face_image[f];
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case "젊은여성2":
            case "서연":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[3].Human_Face_image[f];
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "인석":
            case "범인":
                show_human_image[0].SetActive(true);
                show_human_image[0].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[4].Human_Face_image[f];
                show_human_image[1].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            case "설향":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[5].Human_Face_image[f];
                show_human_image[1].GetComponent<Image>().sprite = null;
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(0, 0, 1, 1);
                break;

            //case "연구원1":
            case "상사":
                show_human_image[1].SetActive(true);
                show_human_image[1].GetComponent<Image>().sprite = DontDestroy_Gamemanager.Dont_Destroy_Instance.Human_Mamber_image[6].Human_Face_image[f];
                show_human_image[0].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                show_human_image[1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;

            default:
                Hide_Human_image();
                break;
        }


    }


}
