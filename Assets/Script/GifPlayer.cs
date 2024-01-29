using UnityEngine;

public class GifPlayer : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 10.0f;
    public float GifInterval=0f;

    public int index;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Debug.Log(frames.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private float tim=0;
    void Update()
    {
        GifInterval-=Time.deltaTime;
        if(GifInterval<0)
        {
            index = (int)(Time.time * framesPerSecond) % frames.Length;
            spriteRenderer.sprite = frames[index];
            tim+=Time.deltaTime;
            if(tim>=5f)
            {
                tim=0;
                GifInterval=Random.Range(0.0f,3.0f);
            }
        }
    }
}