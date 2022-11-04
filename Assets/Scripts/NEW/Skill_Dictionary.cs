using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Skill_Dictionary : MonoBehaviour
{

    // 스킬 개수 변수
    public int Skill_Count = 3;

    // 스킬 리스트 생성
    public List<string> Skill = new List<string>();

    // 등록할 스킬의 번호 설정 변수
    public List<int> Skill_Selection = new List<int>();

    // 스킬 설명 텍스트 출력 배열 설정 -> 따로 인스펙터 창에서 삽입 요청
    public TextMeshProUGUI[] Skill_Text;

    // 스킬 설명 텍스트 딕셔너리 생성
    Dictionary<string, string> D_Skill_Text = new Dictionary<string, string>();

    // 스킬 선택 이미지 출력 이미지 배열 생성 -> 따로 인스펙터 창에서 삽입 ( 오브젝트 위치 )
    public Image[] Skill_Image;

    // 스킬 이미지 딕셔너리 생성
    Dictionary<string, Image> D_Skill_Image = new Dictionary<string, Image>();

    // 스킬 선택 이미지 저장 스프라이트 배열 생성 -> 따로 인스펙터 창에서 삽입 요청
    public Sprite[] Skill_Sprite;

    // 스킬 스프라이트 딕셔너리 생성
    Dictionary<string, Sprite> D_Skill_Sprite = new Dictionary<string, Sprite>();

    // 스킬 

    // ------------------------------
    // 만약, 스킬 사용 여부를 조절한다면, 스킬의 사용 가능 여부를 이곳에서 측정



    private bool Is_Skill_01
    {
        get
        {
            return Is_Skill_01;
        }

    }
    // ------------------------------

    void Awake()
    {
        StartSetting();
        // Skill_Text[0].text = D_Skill_Text[Skill[0]];
    }

    private void OnEnable()
    {
        Skill_Choice();
        Skill_Apply();
    }


    void Update()
    {
        
    }

    void Clear()
    {
        D_Skill_Image.Clear();
        D_Skill_Text.Clear();
    }

    // 시작 세팅 ( 리스트 초기화 / 딕셔너리에 Public으로 적용한 값들 적용 )
    void StartSetting()
    {
        Clear(); // 딕셔너리 리스트 클리어

        Add_Skill_List(); // 스킬 목록 추가
        Add_Skill_Text(); // 스킬 이름 추가


    }

    // 스킬 목록 작성 ( 이름 변경 시, 모든 딕셔너리 값 변경 ) 
    void Add_Skill_List()
    {
        int i = 0;
        // 스킬 개수만큼 리스트에 스킬을 더함
        for (i = 0; i< Skill_Count; i++)
        {
            Skill.Add(i.ToString());
        }
    }

    // 스킬 목록의 텍스트를 삽입함
    void Add_Skill_Text()
    {
        // 둔갑술
        D_Skill_Text.Add(Skill[0], "Cloaking");

        // 무적
        D_Skill_Text.Add(Skill[1], "PowerOverwhelming");

        // 나도 모르는 것?
        D_Skill_Text.Add(Skill[2], "Nothing");
        
        // Skill_Count 개수만큼, 텍스트는 수시로 적어야 합니다

    }


    // 3개의 칸에서 리스트에 있는 스킬들을 중복되지 않게 추출함
    void Skill_Choice()
    {
        // 중복되지 않을 때 까지 추출함
        do
        {
            Skill_Selection[0] = Random.Range(0, Skill_Count);
            Skill_Selection[1] = Random.Range(0, Skill_Count);
            Skill_Selection[2] = Random.Range(0, Skill_Count);

        } while (!(Skill_Selection[0] != Skill_Selection[1] &&
                 Skill_Selection[1] != Skill_Selection[2] &&
                 Skill_Selection[2] != Skill_Selection[0]));
        
    }
    void Skill_Apply()
    {
        int Skill_1 = Skill_Selection[0]; // 추출 후 저장
        int Skill_2 = Skill_Selection[1];
        int Skill_3 = Skill_Selection[2];

        // 이미지/텍스트 적용 부분
        for(int i = 0; i<Skill_Count; i++)
        {
            // 첫 번째 슬롯
            if (i == 0)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_1];
                Skill_Text[i].text = D_Skill_Text[Skill_1.ToString()];
            }

            // 두 번째 슬롯
            if (i == 1)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_2];
                Skill_Text[i].text = D_Skill_Text[Skill_2.ToString()];
            }

            // 세 번째 슬롯
            if (i == 2)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_3];
                Skill_Text[i].text = D_Skill_Text[Skill_3.ToString()];
            }
        }

    }
}
