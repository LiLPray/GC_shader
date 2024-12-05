using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    [Tooltip("Durée d'un cycle complet en secondes.")]
    public float dayLengthInSeconds = 120f;

    [Header("References")]
    [Tooltip("La lumière directionnelle représentant le Soleil.")]
    public Light sunLight;

    [Header("Intensity Settings")]
    [Tooltip("Intensité de la lumière du Soleil à midi.")]
    public float maxSunIntensity = 1f;
    [Tooltip("Intensité de la lumière du Soleil à minuit.")]
    public float minSunIntensity = 0.1f;

    private float rotationSpeed;

    void Start()
    {
        if (sunLight == null)
        {
            Debug.LogError("Aucune lumière directionnelle (Soleil) n'est assignée !");
            return;
        }

        // Calcule la vitesse de rotation en degrés par seconde (360° pour un cycle complet)
        rotationSpeed = 360f / dayLengthInSeconds;
    }

    void Update()
    {
        // Fais pivoter la lumière directionnelle
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);

        // Ajuste l'intensité de la lumière en fonction de l'angle
        AdjustSunlightIntensity();
    }

    private void AdjustSunlightIntensity()
    {
        // Récupère l'angle actuel entre le Soleil et l'horizon
        float sunAngle = Vector3.Dot(transform.forward, Vector3.down);

        // Interpole l'intensité de la lumière en fonction de l'angle
        float intensity = Mathf.Lerp(minSunIntensity, maxSunIntensity, Mathf.Clamp01(sunAngle));
        sunLight.intensity = intensity;
    }
}
