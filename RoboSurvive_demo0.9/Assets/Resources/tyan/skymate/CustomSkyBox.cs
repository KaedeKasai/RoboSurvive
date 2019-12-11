using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomSkyBox : MonoBehaviour
{
    //光
    [SerializeField]
    private Light directionalLight;

    //初期背景
    [SerializeField]
    private Material start;


    //空のマテリアル
    [SerializeField]
    private Material material1;

    [SerializeField]
    private Material material2;

    [SerializeField]
    private Material material3;

    [SerializeField]
    private Material material4;

    [SerializeField]
    private Material material5;

    [SerializeField]
    private Material material6;


    //次の空のマテリアル
    private Material change;

    //前の空のマテリアル
    private Material before;

    //前の空のマテリアルのブレンド
    private float before_blend;

    //マテリアルのカウント
    private int cnt = 0;

    //遷移完了したかどうかの判断
    private bool isBlendComplete;

    //ブレンドの値（めちゃくちゃ変化する：0.0～１.0）
    private float _blend;

    //何秒かけて遷移するのか
    [SerializeField]
    private float blendDuration;


    //太陽の色の変数
    //RGBのR
    private float sunr = 255f;
    //RGBのG
    private float sung = 255f;
    //RGBのB
    private float sunb = 255f;


    //Fog(霧)の色
    //RGBのR
    private float fogr = 255f;
    //RGBのG
    private float fogg = 255f;
    //RGBのB
    private float fogb = 255f;


    //太陽の向き
    public float sunX = 9;
    public float sunY = -77;
    //太陽の光
    public float sunLight = 0.8f;

    //地面の照り返し
    public float reflection = 0.8f;


    //空模様を変えるまで待つ時間の変数
    public float waitTime = 60.0f;




    void Awake()
    {
        _blend = 0.0f;
        directionalLight.intensity = 0.0f;
    }

    void Start()
    {
        StartCoroutine(gameLogic());
    }

    IEnumerator gameLogic()
    {
        bool toggle = true;
        while (true)
        {
            //好きな秒数処理を止められる
            //yield return new WaitForSeconds(waitTime);

            //太陽の向きの判定
            switch (cnt)
            {
                case 0:
                    sunX = 15;
                    sunY = -30;
                    break;
                case 1:
                    sunX = 64;
                    sunY = -207;
                    break;
                case 2:
                    sunX = 170;
                    sunY = -183;
                    break;
                case 3:
                    sunX = 18;
                    sunY = 18;
                    break;
                case 5:
                    sunX = 9;
                    sunY = -77;
                    break;
                case 6:
                    sunX = 15;
                    sunY = -30;
                    break;

            }
            yield return StartCoroutine(CO_BlendSkyBoxTexture(toggle));
            toggle = !toggle;

            switch (cnt)
            {
                //流そうな気配があるのでデモ用に半分の時間に変えておくとかどうかな？
                case 0:
                    waitTime = 60.0f;
                    break;

                case 1:
                    waitTime = 180.0f;
                    break;

                case 2:
                    waitTime = 360.0f;
                    break;

                case 3:
                    waitTime = 360.0f;
                    break;

                case 4:
                    waitTime = 60.0f;
                    break;

                case 5:
                    waitTime = 360.0f;
                    break;

                case 6:
                    waitTime = 300.0f;
                    break;

                case 7:
                    waitTime = 60.0f;
                    break;

            }
        }

    }

    public IEnumerator CO_BlendSkyBoxTexture(bool toggle)
    {
        isBlendComplete = true;
        float startValue = toggle ? 0.0f : 1.0f;
        float endValue = toggle ? 1.0f : 0.0f;

        DOTween.To(blend => _blend = blend, startValue, endValue, blendDuration)
            .OnComplete(() => {
                isBlendComplete = false;
            });

        directionalLight.transform.DORotate(
            new Vector3(sunX, sunY, 0),  // 終了時点のRotation
            blendDuration          // アニメーション時間
            );
        
        cnt++;
        yield return new WaitWhile(() => isBlendComplete == true);

    }

    void Update()
    {
        //お天気の判定
        UpdateMaterial();
    }


    void UpdateMaterial()
    {
        switch (cnt)
        {
            case 0:
                change = start;
                before = material6;
                sunLight = 0.8f;
                reflection = 0.8f;
                break;

            case 1:
                change = material1;
                before = material6;
                before_blend = 1;
                //日差しの強さの計算
                sunLight = 0.6f + (_blend * 0.2f);
                //地面の照り返しの強さ
                reflection = 1.2f - (_blend * 0.4f);
                break;

            case 2:
                change = material2;
                before = material1;
                before_blend = 0;
                //日差しの強さの計算
                sunLight = 0.8f - (1.0f - _blend) * 0.6f;
                //地面の照り返しの強さ
                reflection = 0.8f + ((1.0f - _blend) * 1.2f);
                break;

            case 3:
                change = material3;
                before = material2;
                before_blend = 1;
                //日差しの強さの計算
                sunLight = 0.2f + (_blend * 0.4f);
                //日差しの色（白→オレンジ）
                sung = 255f - (_blend * 55f);
                sunb = 255f - (_blend * 107f);
                //Fogの色（白→黒）
                fogr = 255f - (_blend * 225f);
                fogg = 255f - (_blend * 225f);
                fogb = 255f - (_blend * 225f);
                break;

            case 4:
                change = material4;
                before = material3;
                before_blend = 0;
                //日差しの色（オレンジ→白）
                sung = 200f + ((1.0f - _blend) * 55f);
                sunb = 148f + ((1.0f - _blend) * 107f);
                
                //地面の照り返しの強さ
                reflection = 2.0f - ((1.0f - _blend) * 0.7f);
                break;

            case 5:
                change = material5;
                before = material4;
                before_blend = 1;
                //ずっと夜なので変化なし
                break;

            case 6:
                change = material6;
                before = material5;
                before_blend = 0;
                //Fogの色（黒→白）
                fogr = 25f + ((1.0f - _blend) * 225f);
                fogg = 25f + ((1.0f - _blend) * 225f);
                fogb = 25f + ((1.0f - _blend) * 255f);
                break;

            case 7:
                change = material1;
                before = material6;
                before_blend = 1;
                cnt = 1;
                //日差しの強さの計算
                sunLight = 0.6f + (_blend * 0.2f);
                //地面の照り返しの強さ
                reflection = 1.3f - (_blend * 0.5f);
                break;
        }


        RenderSettings.skybox = change;
        directionalLight.intensity = sunLight;
        RenderSettings.ambientIntensity = reflection;
        //太陽の色、初期値
        directionalLight.color = new Color(sunr / 255f, sung / 255f, sunb / 255f);
        //Fogの色、初期値
        RenderSettings.fogColor = new Color(fogr / 255f, fogg / 255f, fogb / 255f);
        change.SetFloat("_Blend", _blend);
        before.SetFloat("_Blend", before_blend);

    }
}
