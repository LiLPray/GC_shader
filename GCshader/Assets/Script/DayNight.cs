using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    [Tooltip("Dur�e d'un cycle complet en secondes.")]
    public float dayLengthInSeconds = 120f;

    [Header("References")]
    [Tooltip("La lumi�re directionnelle repr�sentant le Soleil.")]
    public Light sunLight;

    [Header("Intensity Settings")]
    [Tooltip("Intensit� de la lumi�re du Soleil � midi.")]
    public float maxSunIntensity = 1f;
    [Tooltip("Intensit� de la lumi�re du Soleil � minuit.")]
    public float minSunIntensity = 0.1f;

    private float rotationSpeed;

    void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Aucune lumi�re directionnelle (Soleil) n'est assign�e !");
            return;
        }

        // Calcule la vitesse de rotation en degr�s par seconde (360� pour un cycle complet)
        rotationSpeed = 360f / dayLengthInSeconds;
    }

    void Update()
    {
        // Fais pivoter la lumi�re directionnelle
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Ajuste l'intensit� de la lumi�re en fonction de l'angle
        AdjustSunlightIntensity();
    }

    private void AdjustSunlightIntensity()
    {
        // R�cup�re l'angle actuel entre le Soleil et l'horizon
        float sunAngle = Vector3.Dot(transform.forward, Vector3.down);

        // Interpole l'intensit� de la lumi�re en fonction de l'angle
        float intensity = Mathf.Lerp(minSunIntensity, maxSunIntensity, Mathf.Clamp01(sunAngle));
        sunLight.intensity = intensity;
    }
}
