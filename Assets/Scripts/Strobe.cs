// Some Credit goes to:
// https://answers.unity.com/questions/742466/camp-fire-light-flicker-control.html
using UnityEngine;
using System.Collections;
 
public class Strobe : MonoBehaviour
{
    public float MaxReduction = 0.2f;
    public float MaxIncrease = 0.2f;
    public float RateDamping = 0.1f;
    public float Strength = 300;
    public bool StopFlickering;

    private Light _lightSource;
    public float _baseIntensity;
    private bool _flickering;

    public void Reset()
    {
        MaxReduction = 0.2f;
        MaxIncrease = 0.2f;
        RateDamping = 0.1f;
        Strength = 300;
    }

    public void Start()
    {
        _lightSource = GetComponent<Light>();
        if (_lightSource == null)
        {
            Debug.LogError("Flicker script must have a Light Component on the same GameObject.");
            return;
        }
        _baseIntensity = _lightSource.intensity;
    }

    void Update()
    {
        //if (!StopFlickering && !_flickering)
        //{
        //    StartCoroutine(DoFlicker());
        //}
        if (!StopFlickering)
        {
            float newIntensity = 
                _baseIntensity
                + (MaxIncrease + MaxReduction)
                    * (Mathf.Sin(2 * Mathf.PI * Time.time / RateDamping) + 1)
                    / 2
                - MaxReduction;
            _lightSource.intensity = newIntensity;
            //Mathf.Lerp(
            //    _lightSource.intensity,
            //    newIntensity,
            //    //Random.Range(
                //    _baseIntensity - MaxReduction,
                //    _baseIntensity + MaxIncrease
                //),
            //    Strength * Time.deltaTime
            //);
        }
    }

    private IEnumerator DoFlicker()
    {
        _flickering = true;
        while (!StopFlickering)
        {
            float newIntensity = 
                _baseIntensity
                + (MaxIncrease + MaxReduction)
                    * (Mathf.Sin(Time.time) + 1)
                    / 2
                - MaxReduction;
            Mathf.Lerp(
                _lightSource.intensity,
                newIntensity,
                //Random.Range(
                //    _baseIntensity - MaxReduction,
                //    _baseIntensity + MaxIncrease
                //),
                Strength * Time.deltaTime
            );
            yield return new WaitForSeconds(RateDamping);
        }
        _flickering = false;
    }
}
