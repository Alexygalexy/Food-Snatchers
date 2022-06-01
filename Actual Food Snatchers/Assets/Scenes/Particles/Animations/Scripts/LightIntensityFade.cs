using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightIntensityFade : MonoBehaviour
{
    // Duration of the effect.
    [SerializeField]
    private float duration = 1.0f;

    // Delay of the effect.
    [SerializeField]
    private float delay = 0.0f;

    /// Final intensity of the light.
    [SerializeField]
    private float finalIntensity = 0.0f;

    // Base intensity, automatically taken from light parameters.
    [SerializeField]
    private float baseIntensity;

    // If <c>true</c>, light will destructs itself on completion of the effect
    [SerializeField]
    private bool autodestruct;

    [SerializeField]
    private float lifetime = 0.0f;

    [SerializeField]
    private float duration_delay;


    void Start()
    {
        Light light = GetComponent<Light>();
        baseIntensity = light.intensity;
    }

    void OnEnable()
    {
        lifetime = 0.0f;
        duration_delay = delay;
        if (delay > 0) light.enabled = false;
    }

    void Update()
    {
        if (duration_delay > 0)
        {
            duration_delay -= Time.deltaTime;
            if (duration_delay <= 0)
            {
                light.enabled = true;
            }
            return;
        }

        if (lifetime / duration < 1.0f)
        {
            light.intensity = Mathf.Lerp(baseIntensity, finalIntensity, lifetime / duration);
            lifetime += Time.deltaTime;
        }
        else
        {
            if (autodestruct)
                GameObject.Destroy(this.gameObject);
        }

    }
}