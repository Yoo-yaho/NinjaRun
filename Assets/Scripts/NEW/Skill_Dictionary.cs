using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Skill_Dictionary : MonoBehaviour
{

    // ��ų ���� ����
    public int Skill_Count = 3;

    // ��ų ����Ʈ ����
    public List<string> Skill = new List<string>();

    // ����� ��ų�� ��ȣ ���� ����
    public List<int> Skill_Selection = new List<int>();

    // ��ų ���� �ؽ�Ʈ ��� �迭 ���� -> ���� �ν����� â���� ���� ��û
    public TextMeshProUGUI[] Skill_Text;

    // ��ų ���� �ؽ�Ʈ ��ųʸ� ����
    Dictionary<string, string> D_Skill_Text = new Dictionary<string, string>();

    // ��ų ���� �̹��� ��� �̹��� �迭 ���� -> ���� �ν����� â���� ���� ( ������Ʈ ��ġ )
    public Image[] Skill_Image;

    // ��ų �̹��� ��ųʸ� ����
    Dictionary<string, Image> D_Skill_Image = new Dictionary<string, Image>();

    // ��ų ���� �̹��� ���� ��������Ʈ �迭 ���� -> ���� �ν����� â���� ���� ��û
    public Sprite[] Skill_Sprite;

    // ��ų ��������Ʈ ��ųʸ� ����
    Dictionary<string, Sprite> D_Skill_Sprite = new Dictionary<string, Sprite>();

    // ��ų 

    // ------------------------------
    // ����, ��ų ��� ���θ� �����Ѵٸ�, ��ų�� ��� ���� ���θ� �̰����� ����



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

    // ���� ���� ( ����Ʈ �ʱ�ȭ / ��ųʸ��� Public���� ������ ���� ���� )
    void StartSetting()
    {
        Clear(); // ��ųʸ� ����Ʈ Ŭ����

        Add_Skill_List(); // ��ų ��� �߰�
        Add_Skill_Text(); // ��ų �̸� �߰�


    }

    // ��ų ��� �ۼ� ( �̸� ���� ��, ��� ��ųʸ� �� ���� ) 
    void Add_Skill_List()
    {
        int i = 0;
        // ��ų ������ŭ ����Ʈ�� ��ų�� ����
        for (i = 0; i< Skill_Count; i++)
        {
            Skill.Add(i.ToString());
        }
    }

    // ��ų ����� �ؽ�Ʈ�� ������
    void Add_Skill_Text()
    {
        // �а���
        D_Skill_Text.Add(Skill[0], "Cloaking");

        // ����
        D_Skill_Text.Add(Skill[1], "PowerOverwhelming");

        // ���� �𸣴� ��?
        D_Skill_Text.Add(Skill[2], "Nothing");
        
        // Skill_Count ������ŭ, �ؽ�Ʈ�� ���÷� ����� �մϴ�

    }


    // 3���� ĭ���� ����Ʈ�� �ִ� ��ų���� �ߺ����� �ʰ� ������
    void Skill_Choice()
    {
        // �ߺ����� ���� �� ���� ������
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
        int Skill_1 = Skill_Selection[0]; // ���� �� ����
        int Skill_2 = Skill_Selection[1];
        int Skill_3 = Skill_Selection[2];

        // �̹���/�ؽ�Ʈ ���� �κ�
        for(int i = 0; i<Skill_Count; i++)
        {
            // ù ��° ����
            if (i == 0)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_1];
                Skill_Text[i].text = D_Skill_Text[Skill_1.ToString()];
            }

            // �� ��° ����
            if (i == 1)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_2];
                Skill_Text[i].text = D_Skill_Text[Skill_2.ToString()];
            }

            // �� ��° ����
            if (i == 2)
            {
                Skill_Image[i].sprite = Skill_Sprite[Skill_3];
                Skill_Text[i].text = D_Skill_Text[Skill_3.ToString()];
            }
        }

    }
}
