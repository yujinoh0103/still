using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;
using static UnityEditor.PlayerSettings;

public class map_make : MonoBehaviour
{
    static int STACK_MAX = 100;
    static int START_POINT_NUMBER = 5;
    static int h = 7, w = 7;

    [Header("클리어 오브젝트")]
    public GameObject clear;

    [Header("현재 컬러 확인용 오브젝트")]
    public GameObject color_check;


    public GameObject block;
    public GameObject[,] block_map;
    Color block_color;
    public int clear_num;

    public Sprite[] wire;

    


    public class element
    {
        public int c;
        public int r;

        public element()
        {
            c = 0;
            r = 0;
        }

        public element(int x, int y)
        {
            c = y;
            r = x;
        }
    }

    public class stack
    {
        public element[] data;
        public int top;
        public stack()
        {
            data = new element[STACK_MAX];
            top = -1;
        }
    }

    public bool is_empty(stack s)
    {
        return (s.top == -1);
    }

    public bool is_full(stack s)
    {
        return (s.top == STACK_MAX);
    }

    public void stack_reset(stack s)
    {
        s.top = -1;
    }

    public void push(stack s, element val)
    {
        if(is_full(s))
            return;
        s.top++;
        s.data[s.top] = val;
    }

    element pop(stack s) 
    {
        if(is_empty(s))
            return null;
        element re = s.data[s.top];
        s.top--;
        return re;
    }


    // Start is called before the first frame update
    void Start()
    {
       map();
    }

  
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 1000f);

        
            if (hit.collider != null)
            {

                if (hit.transform.tag == "Start")
                {
                    block_color = hit.transform.GetComponent<SpriteRenderer>().color; //색깔 저장
                    color_check.GetComponentInChildren<Image>().color = block_color;
                }
                if (hit.transform.tag == "End")
                {
                    block_color = hit.transform.GetComponent<SpriteRenderer>().color; //색깔 저장
                    color_check.GetComponentInChildren<Image>().color = block_color;
                }
                if (hit.transform.tag == "Land")
                {
                    if(block_color != new Color(0,0,0,0))
                        hit.transform.GetComponent<SpriteRenderer>().color = block_color;

                    All_Color_Shape_Change();
                }

            }
        }
    }

    void map() //기본 판
    {
        block_map = new GameObject[h, w];
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                GameObject block_o = Instantiate(block, new Vector3(-3f + x, -3f + y, 0), Quaternion.identity);
                block_o.name = (x, y).ToString();
                block_map[x, y] = block_o;
                block_o.tag = "Land";
            }
        }
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
        {
            case 9: //101호 거실
                Map_101();
                break;
            case 11: //102호 거실
                Map_102();
                break;
        }
       
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if(block_map[x, y].tag == "Start" || block_map[x, y].tag == "End") //시작/끝 부분의 스프라이트 구별
                {
                    block_map[x, y].GetComponent<SpriteRenderer>().sprite = wire[4];
                }
            }
        }
    }
    void Map_101()
    {
        //시작
        block_map[2, 6].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //빨
        block_map[2, 6].tag = "Start";

        block_map[0, 3].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1); //초
        block_map[0, 3].tag = "Start";
        
        block_map[0, 6].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1); //파
        block_map[0, 6].tag = "Start";
        
        block_map[1, 6].GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1); //노
        block_map[1, 6].tag = "Start";

        block_map[5, 5].GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1); //보
        block_map[5, 5].tag = "Start";

        //끝
        block_map[3, 4].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //빨
        block_map[3, 4].tag = "End";

        block_map[1, 0].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1); //초
        block_map[1, 0].tag = "End";

        block_map[2, 4].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1); //파
        block_map[2, 4].tag = "End";

        block_map[2, 2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1); //노
        block_map[2, 2].tag = "End";

        block_map[3, 1].GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1); //보
        block_map[3, 1].tag = "End";
    }

    void Map_102()
    {
        //시작
        block_map[0, 6].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //빨
        block_map[0, 6].tag = "Start";

        block_map[3, 3].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1); //초
        block_map[3, 3].tag = "Start";

        block_map[1, 3].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1); //파
        block_map[1, 3].tag = "Start";

        block_map[2, 5].GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1); //노
        block_map[2, 5].tag = "Start";

        block_map[2, 6].GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1); //보
        block_map[2, 6].tag = "Start";

        //끝
        block_map[6, 1].GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1); //빨
        block_map[6, 1].tag = "End";

        block_map[4, 5].GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1); //초
        block_map[4, 5].tag = "End";

        block_map[6, 4].GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1); //파
        block_map[6, 4].tag = "End";

        block_map[5, 6].GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1); //노
        block_map[5, 6].tag = "End";

        block_map[6, 6].GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1); //보
        block_map[6, 6].tag = "End";
    }

    public void All_Color_Shape_Change()
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (block_map[x, y].tag == "Land")
                {
                    image_check(block_map[x, y], x, y);
                }

            }

        }
    }

    public void image_check(GameObject block_obj, int x, int y)
    {
       
        Color color;
        color = block_obj.transform.GetComponent<SpriteRenderer>().color;
        int connect = 0;

        Array.Clear(block_obj.GetComponent<block_information>().check_connection, 0, block_obj.GetComponent<block_information>().check_connection.Length);
        
        if(color != new Color(1,1,1,1)) //흰색이 아닐 때
        {
            //block_map[x + 1, y] != null 사용할 시 배열크기 오류 뜸
            if (x + 1 < w && block_map[x + 1, y].GetComponent<SpriteRenderer>().color == color) //오
            {
                block_obj.GetComponent<block_information>().check_connection[0] = true;
            }
            if (x - 1 >= 0 && block_map[x - 1, y].GetComponent<SpriteRenderer>().color == color)//왼 
            {
                block_obj.GetComponent<block_information>().check_connection[1] = true;
            }
            if (y + 1 < h && block_map[x, y + 1].GetComponent<SpriteRenderer>().color == color)//위
            {
                block_obj.GetComponent<block_information>().check_connection[2] = true;
            }
            if (y - 1 >= 0 && block_map[x, y - 1].GetComponent<SpriteRenderer>().color == color)//아래
            {
                block_obj.GetComponent<block_information>().check_connection[3] = true;
            }
        }

       

        for(int i = 0;i< block_obj.GetComponent<block_information>().check_connection.Length;i++)
        {
            if (block_obj.GetComponent<block_information>().check_connection[i])
            {
                connect++;
            }
        }

       // Debug.Log(connect);
       switch(connect)
        {
            case 4:
                block_obj.GetComponent<SpriteRenderer>().sprite = wire[3];
                break;

            case 3:
                block_obj.GetComponent<SpriteRenderer>().sprite = wire[2];
                if(Array.IndexOf(block_obj.GetComponent<block_information>().check_connection, false) == 0) //연결되지 않은 부분 확인(오른쪽). false인 인덱스 반환
                {
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                else if (Array.IndexOf(block_obj.GetComponent<block_information>().check_connection, false) == 1) //연결되지 않은 부분 확인(왼쪽)
                {
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else if (Array.IndexOf(block_obj.GetComponent<block_information>().check_connection, false) == 2) //연결되지 않은 부분 확인(위쪽)
                {
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else if (Array.IndexOf(block_obj.GetComponent<block_information>().check_connection, false) == 3) //연결되지 않은 부분 확인(아래쪽)
                {
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                }
                break;
            case 2:

                if(block_obj.GetComponent<block_information>().check_connection[0]&& block_obj.GetComponent<block_information>().check_connection[1]) //오/왼 연결
                {
                    block_obj.GetComponent<SpriteRenderer>().sprite = wire[0];
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else if (block_obj.GetComponent<block_information>().check_connection[2] && block_obj.GetComponent<block_information>().check_connection[3])//위/아래 연결
                {
                    block_obj.GetComponent<SpriteRenderer>().sprite = wire[0];
                    block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    block_obj.GetComponent<SpriteRenderer>().sprite = wire[1];
                    if (block_obj.GetComponent<block_information>().check_connection[0]) //연결된 부분 확인(오른쪽)
                    {
                        if(block_obj.GetComponent<block_information>().check_connection[2])//위
                        {
                            block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                        }
                        else if(block_obj.GetComponent<block_information>().check_connection[3])//아래
                        {
                            block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                        }
                       
                    }

                    else if (block_obj.GetComponent<block_information>().check_connection[1]) //연결된 부분 확인(왼쪽)
                    {
                        if (block_obj.GetComponent<block_information>().check_connection[2])//위
                        {
                            block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                        }
                        else if (block_obj.GetComponent<block_information>().check_connection[3])//아래
                        {
                            block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        }

                    }

                }

                break;
            default:
                block_obj.GetComponent<SpriteRenderer>().sprite = wire[0];
                block_obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;
        }
    }


    
    public void map_check() //성공 여부 체크(버튼)
    {

        element here = new();
        stack stack_map = new();
        element input_pos = new();

        //모든 색의 연결 여부 검사용 배열들
        element[] Start_Points = new element[START_POINT_NUMBER]; 
        element[] End_Points = new element[START_POINT_NUMBER]; 
        Color[] here_color = new Color[START_POINT_NUMBER];
        int i = 0, j = 0;

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                // 스타트지점 발견
                if (block_map[x, y].tag == "Start")
                {
                    input_pos = new(x, y);
                    Start_Points[i]= input_pos;
                    here_color[i] = block_map[x, y].transform.GetComponent<SpriteRenderer>().color; //검사하는 색 here_color에 모두 저장
                    i++;
                }
             
            }

        }

         for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            { //도착지점 - start와 색이 같아야 같은 배열에 위치
                if (block_map[x, y].tag == "End" && here_color[j] == block_map[x, y].transform.GetComponent<SpriteRenderer>().color)
                {
                    input_pos = new(x, y);
                    End_Points[j] = input_pos;
                    j++;
                }
            }

        }
        for(int k =0; k < START_POINT_NUMBER; k++)
        {
            stack_reset(stack_map);
            here = new();
            push(stack_map, Start_Points[k]); //스타트지점 스택에 입력
            //Debug.Log("Start " + Start_Points[k].r+ " " + Start_Points[k].c);
           // Debug.Log("Start " + End_Points[k].r+ " " + End_Points[k].c);


            while (here != End_Points[k]) //마지막에 도착하지 않은 동안 반복
            {
                /*
                    사방의 블록 검사 함수
                    스택, 지금 검사하는 블록 색, 좌우, 위아래 좌표
                */

                push_loc(stack_map, here_color[k], here.r - 1, here.c);
                push_loc(stack_map, here_color[k], here.r + 1, here.c);
                push_loc(stack_map, here_color[k], here.r, here.c - 1);
                push_loc(stack_map, here_color[k], here.r, here.c + 1);

                if (is_empty(stack_map)) //비어있다면
                {
                    map_tag_reset();
                    //clear_num = 0;
                    Debug.Log("실패");
                   
                    return;
                }
                else
                {

                    here = pop(stack_map);
                    if (block_map[here.r, here.c].transform.tag == "End") //도착 지점이라면
                    {

                        Debug.Log("성공");
                        clear_num++;
                        break; //다음 검사를 위해 whlie 탈출
                    }
                    else if (block_map[here.r, here.c].transform.tag == "Start") ; //시작 지점이 check로 변하는 것 방지. 아무 작동도 안 함
                    else //도착 지점이 아니라면
                    {
                        block_map[here.r, here.c].transform.tag = "Check"; //갔던 곳은 변경
                    }
                    //Debug.Log("top" + stack_map.top);
                    //Debug.Log("here" + block_map[here.r, here.c]);

                }
            }

            
        }


        if (clear_num == 5)
        {
            clear.SetActive(true);

            switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_room_num)
            {
                case 9: //101호 거실
                    Map_101();
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[2] = true;
                    break;
                case 11: //102호 거실
                    DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[3] = true;
                    Map_102();
                    break;
            }
            DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start = true;
            SceneManager.LoadScene("Game_Scene");
        }

    }
    void push_loc(stack s,Color c,int x,int y)
    {
        if (x < h && x > -1 && y < w && y > -1) //배열 크기를 넘어가지 않도록
        {
            if (block_map[x, y] != null && block_map[x, y].GetComponent<SpriteRenderer>().color == c &&(block_map[x, y].tag == ("Land") || block_map[x, y].tag == ("End")))
            {
                //색이 같고 이미 가지 않았다면 PUSH
                element input_pos = new(x, y);
                push(s, input_pos);
            }
               
        }
       
    }

    void map_tag_reset() //실패 시 검사 여부 모두 리셋
    {
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                if (block_map[x, y].transform.tag == "Check")
                    block_map[x, y].transform.tag = "Land"; 
            }

        }
    }

   
}