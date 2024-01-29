using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LevelManager2 : MonoBehaviour
{
    public static LevelManager2 instance;
    public float fame=200f;
    public float botox=0f;
    public float botox_max=50f;
    public GameObject arrow;
    public GameObject MaskOn;
    public GameObject Face;
    public Material material0;
    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;
    public Material material7;
    public Material material8;
    public int nowfame=5;
    private Material[] to_material;
    private Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    
    // Start is called before the first frame update
    private void Start() 
    {
        AudioManager.Instance.PlayMusic("testBGM");
        // 获取 Global Volume 的引用
        globalVolume = FindObjectOfType<Volume>();

        // 获取 Color Adjustments
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            // 初始设置（可选）
            colorAdjustments.saturation.value = -100;
        }
    }
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        botox=0f;
        fame=200f;
        nowfame=5;
        Face.GetComponent<Renderer>().material = material5;
        to_material=new Material[9];
        to_material[0]=material0;
        to_material[1]=material1;
        to_material[2]=material2;
        to_material[3]=material3;
        to_material[4]=material4;
        to_material[5]=material5;
        to_material[6]=material6;
        to_material[7]=material7;
        to_material[8]=material8;
    }
    public void suck(float x)
    {
        float delta=Mathf.Min(botox_max-botox,x);
        botox+=delta;
        fame-=delta;
        if(fame<=0)
        {
            //win
        }
    }
    // Update is called once per frame
    void Update()
    {
        botox=Mathf.Min(botox,botox_max);
        fame=Mathf.Min(fame,200);
        if(fame<=0)
        {
            //win
            SceneManager.LoadScene("Cutscene3");
        }
        CheckBotox();
        ChangeFace();
    }
    void CheckBotox()
    {
        if(50f-botox<=0.1)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    public void SetSaturation(float newSaturation)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = newSaturation;
        }
    }
    void ChangeFace()
    {
        int to_fame;
        float satur=-100;
        // Debug.Log(fame);
        if(fame>=180)
        {
            to_fame=5;
            satur=-100;
        }
        else if(fame>=160)
        {
            to_fame=4;
            satur=-85;
        }
        else if(fame>=140)
        {
            to_fame=3;
            satur=-70;
        }
        else if(fame>=120)
        {
            to_fame=2;
            satur=-55;
        }
        else if(fame>=100)
        {
            to_fame=1;
            satur=-40;
        }
        else if(fame>=80)
        {
            to_fame=0;
            satur=-25;
        }
        else if(fame>=60)
        {
            to_fame=6;
            satur=-10;
        }
        else if(fame>=40)
        {
            to_fame=7;
            satur=5;
        }
        else
        {
            to_fame=8;
            satur=20;
        }
        // Debug.Log(nowfame);
        // Debug.Log(to_fame);
        if(to_fame!=nowfame)
        {
            nowfame=to_fame;
            StartCoroutine(ActivateMask(to_fame,satur));
        }
        // Debug.Log(fame);
    }
    IEnumerator ActivateMask(int to_fame,float satur)
    {
        MaskOn.SetActive(true);
        AudioManager.Instance.PlaySFX("flash");
        yield return new WaitForSeconds(2);
        MaskOn.SetActive(false);
        SetSaturation(satur);
        Face.GetComponent<Renderer>().material = to_material[to_fame];
        // nowfame=to_fame;
    }
}
