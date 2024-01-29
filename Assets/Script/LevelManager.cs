using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float fame=30f;
    public float descRate=1.0f;
    public GameObject wrinkle;
    public GameObject Acne;
    public GameObject Mite;
    public Transform limitLD,limitRU;
    private float MobSpawnCounter;
    public int SpawnRate=2;
    public float botox=100;
    public int MobCount=0;
    public GameObject MaskOn;
    public GameObject Face;
    public Material material0;
    public Material material1;
    public Material material2;
    public Material material3;
    public GameObject arrow;
    // Start is called before the first frame update
    public int nowfame=1;
    private Material[] to_material;
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
        instance=this;
        MobSpawnCounter=0;
        botox=100;
        fame=30f;
        nowfame=1;
        MobCount=0;
        Face.GetComponent<Renderer>().material = material1;
        to_material=new Material[4];
        to_material[0]=material0;
        to_material[1]=material1;
        to_material[2]=material2;
        to_material[3]=material3;
    }
    private void Start() {
        AudioManager.Instance.PlayMusic("testBGM");
    }
    public void AddFame(float x)
    {
        fame+=x;
        if(fame>=100f)
        {
            //win
        }
    }
    // Update is called once per frame
    void Update()
    {
        botox=Mathf.Min(botox,100);
        fame=Mathf.Min(fame,100);
        if(fame>=99.5)
        {
            SceneManager.LoadScene("Cutscene2");
        }
        fame-=descRate*Time.deltaTime*MobCount*0.3f;
        if(fame<0)
        {
            SceneManager.LoadScene("GameOver");
            //die
        }
        
        MobSpawnCounter+=Time.deltaTime;
        if(MobSpawnCounter>=1f)
        {
            MobSpawnCounter=0;
            int i;
            for(i=1;i<=SpawnRate;i++)
            {
                int randInt=Random.Range(1,4);
                float x1=limitLD.position.x,x2=limitRU.position.x;
                if(x1>x2){float tmp=x1;x1=x2;x2=tmp;}
                float randx=Random.Range(x1,x2);
                float y1=limitLD.position.y,y2=limitRU.position.y;
                if(y1>y2){float tmp=y1;y1=y2;y2=tmp;}
                float randy=Random.Range(y1,y2);
                if(randInt==1)
                {
                    AudioManager.Instance.PlaySFX("wrinkle3");
                    Instantiate(wrinkle,new Vector3(randx,randy,0),new Quaternion(0,0,0,0));
                }
                else if(randInt==2)
                {
                    AudioManager.Instance.PlaySFX("acne1");
                    Instantiate(Acne,new Vector3(randx,randy,0),new Quaternion(0,0,0,0));
                }
                else if(randInt==3)
                {
                    AudioManager.Instance.PlaySFX("miteAppear");
                    Instantiate(Mite,new Vector3(randx,randy,0),new Quaternion(0,0,0,0));
                }
                MobCount++;
            }
        }
        ChangeFace();
        CheckBotox();
    }
    void CheckBotox()
    {
        if(botox-0<=0.1)
        {
            arrow.SetActive(true);
        }
        else
        {
            arrow.SetActive(false);
        }
    }
    
    void ChangeFace()
    {
        int to_fame;
        // Debug.Log(fame);
        if (fame<=20f)
        {
            to_fame=0;
        }
        else if(fame<=50f)
        {
            to_fame=1;
        }
        else if(fame<=80f)
        {
            to_fame=2;
        }
        else
        {
            to_fame=3;
        }
        // Debug.Log(nowfame);
        // Debug.Log(to_fame);
        if(to_fame!=nowfame)
        {
            nowfame=to_fame;
            StartCoroutine(ActivateMask(to_fame));
        }
        // Debug.Log(fame);
    }
    IEnumerator ActivateMask(int to_fame)
    {
        MaskOn.SetActive(true);
        AudioManager.Instance.PlaySFX("flash");
        yield return new WaitForSeconds(2);
        MaskOn.SetActive(false);
        Face.GetComponent<Renderer>().material = to_material[to_fame];
        // nowfame=to_fame;
    }
}
