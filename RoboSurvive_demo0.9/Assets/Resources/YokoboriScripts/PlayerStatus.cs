using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStatus : MonoBehaviour
{
    //プレイヤー基本ステータス
    //0:Health,1:Stamina,2:ArmerHead,3:ArmerBody,4:ArmerHandR,5:ArmerHandL,6:ArmerRegR,7:ArmerRegL,8:ArmerFootR,9:ArmerFootL
    public float[] status= new float[10]{100,1,0,0,0,0,0,0,0,0};
    //0:Scanner
    public bool[] attachment = new bool[] {false };

    //スキャナーを目にあたっちできるようにしたい

    //アイテム個数　詳細アイテムはコメントを参照
    //植物系
    //0:Wood,1:WoodenStick,2:WoodenPlanks,3:Mycena
    public int[] wood = new int[] { 0,0,0,0 };
    //石材系
    //0:rock,1:Stone,2:PolishedStone,3:FrakedStone
    public int[] rock = new int[] { 0,0,0,0 };
    //金属系
    //0:Plate,1:Bar,2:Cylinder,3:Wire,4:Cable
    public int[] scrap = new int[]{0,0,0,0};
    //アイテム系
    //0:InstantCell 1:Flagment
    public int[] item = new int[] {0,0 };
    //設計図   
    //0:StoneAxe,1:StonePickAxe,2:InstantCell,3:Generator
    public bool[] blueprint = new bool[] {true,true,true,false };

        //USB的なのを集めて設計図入手　スキャンしたものをフラグメントを一定数集めて設計図に
        //クラフト台で作成したら設計図をtrueに

    public int[] color = new int[4] { 0, 0, 0, 0 };

    public FirstPersonController fpc;

    GameObject _effect;
    //わからん
    //GameObject _vfx;

   　//スタミナ
    Slider _sliderS;
    //体力
    Slider _sliderH;

    RawImage _debuff;

    //取得したアイテム名
    Text _GetItemName;
    //取得・不足・作成
    Text _Notice;

    // Start is called before the first frame update
    void Start()
    {
        _sliderS = GameObject.Find("Stamina").GetComponent<Slider>();
        _sliderS.value = 1f;

        _sliderH = GameObject.Find("Health").GetComponent<Slider>();
        _sliderH.value = 1f;

        _debuff = GameObject.Find("debuff").GetComponent<RawImage>();
        _debuff.color = new Color(255, 255, 255, 0);

        _GetItemName = GameObject.Find("GetItemName").GetComponent<Text>();
        _Notice = GameObject.Find("Notice").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {



        fpc = GetComponent<FirstPersonController>();
        if (status[1] <= 1f)
        {
            //テストができないため一時的にコメントアウト
            //fpc.m_WalkSpeed = 3f;
            //fpc.m_RunSpeed = 3f;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                status[1] -= 0.1f;
                _sliderS.value = status[1] / 100;
            }
        }else if(status[1] <= 100f)
        {
            status[1] += 0.01f;
            _sliderS.value = status[1]/100;
        }else if (status[1] <= 0)
        {
            status[1] += 0.01f;
            _sliderS.value = 0;
        }

        if (Input.GetKey(KeyCode.E))
        {
            status[1] = 100f;
            _sliderS.value = 1;
            //テストができないため一時的にコメントアウト
            //fpc.m_WalkSpeed = 5f;
            //fpc.m_RunSpeed = 10f;
            
        }

        Debug.Log(status[1]);

    }


    
    private void OnCollisionEnter(Collision collision)
    {
        fpc = GetComponent<FirstPersonController>();
        //アイテムの取得
        if (collision.gameObject.tag == "Material")
        {
            switch (collision.gameObject.name)
            {
                case "Wood":
                    wood[0] += 1;
                    break;
                case "Mycena":
                    wood[3] += 1;
                    break;
                case "rock":
                    rock[0] += 1;
                    break;
                case "stone":
                    rock[1] += 1;
                    break;
                case "PolishedStone":
                    rock[2] += 1;
                    break;
                case "FrakedStone":
                    rock[3] += 1;
                    break;
                case "Plate":
                    scrap[0] += 1;
                    break;
                case "Bar":
                    scrap[1] += 1;
                    break;
                case "Cylinder":
                    scrap[2] += 1;
                    break;
                case "Wire":
                    scrap[3] += 1;
                    break;
                case "Cable":
                    scrap[4] += 1;
                    break;
                case "Flagment":
                    item[1] += 1;
                    break;
                
            }
            _Notice.text = "取得";
            _GetItemName.text = collision.gameObject.name;
            Destroy(collision.gameObject);
        }
        //アイテム取得ここまで

        //敵or攻撃の接触
        if (collision.gameObject.tag == "Enemy")
        {
            switch (collision.gameObject.name)
            {
                case "smallATK":
                   StartCoroutine( Damage(65535f,1f));
                    break;
                case "middleATK":
                   StartCoroutine( Damage(65535f, 3f));
                    break;
                case "hardATK":
                    StartCoroutine( Damage(65535f, 5f));
                    break;
                case "acid":
                    StartCoroutine(Damage(10f,1f));
                    _debuff.color = new Color(0, 255, 0, 255);
                    _debuff.texture = (Texture)Resources.Load("CFX3_T_Skull");
                    break;
                case "electric":
                    StartCoroutine( Damage(3f,1f));
                    _debuff.color = new Color(255, 255, 0, 255);
                    _debuff.texture = (Texture)Resources.Load("CFX2_T_HeartBrokenSlice");
                    //テストができないため一時的にコメントアウト
                    //fpc.m_WalkSpeed = fpc.m_WalkSpeed / 2;
                    //fpc.m_RunSpeed = fpc.m_RunSpeed / 2;
                    break;
                case "noise":
                    StartCoroutine(Damage(10f, 1f));
                    break;
                case "fire":
                    StartCoroutine(Damage(10f, 1f));
                    break;
                case "chun":
                    StartCoroutine(Damage(99f, 1f));
                    break;
                default:
                    break;
            }
        }
        //敵or攻撃の接触ここまで

        //設計図の作成-cbpという名前のrigidBに必要数Flagmentを所持した状態で接触すると設計図が作成される(trueになる)
        if(collision.gameObject.name == "cbp")
        {
            if(item[1] >= 5)
            {
                item[1] -= 5;
                blueprint[3] = true;
                _Notice.text = "作成";
                _GetItemName.text ="発電機の設計図";

            }
            else
            {
                _Notice.text = "不足：Flagment";
                _GetItemName.text = "発電機の設計図";
            }
        }
    }

    //i = 継続ダメージ判別 j = ダメージ量
    private IEnumerator Damage(float i,float j)
    {
        status[0] -= j;
        _sliderH.value -= j/100;
        Debug.Log(status[0]);
        //もしかしたらここかも
        while (i != 65535 && i >= 0)
        {

            status[0] -= j;
            _sliderH.value -= j / 100;
            i = i - 1f;

            yield return new WaitForSeconds(1);
        }

        reset();

    }

    private void reset()
    {
        _debuff.color = new Color(0, 0, 0, 0);
        //テストができないため一時的にコメントアウト
        //fpc.m_WalkSpeed = 5f;
        //fpc.m_RunSpeed = 10f;
        Destroy(_effect);
    }

}
