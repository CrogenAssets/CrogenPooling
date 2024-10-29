using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySFX("debris-break-253779");
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Instance.PlayBGM("break-the-action-sport-upbeat-rock-intro-185016");
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.PlayBGM("tea-time-on-a-rainy-day-everyday-natural-piano-205092");
        }
    }
}
