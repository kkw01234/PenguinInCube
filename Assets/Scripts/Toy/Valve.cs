using UnityEngine;

public class Valve : MonoBehaviour
{
    public GameObject watering;
    private int breakCount = 0;

    private void OnTriggerStay(Collider other)
    {
        if (GGUMI.instance.joyActionButton.isPressed && !GetComponent<Animation>().isPlaying)
        {
            GetComponent<Animation>().Play();
            breakCount++;
            if (breakCount > 6)
            {
                ParticleSystem.MainModule particleSystemMain = watering.GetComponent<ParticleSystem>().main;
                particleSystemMain.maxParticles = breakCount - 5;
                ParticleSystem.EmissionModule particleSystemEmission = watering.GetComponent<ParticleSystem>().emission;
                particleSystemEmission.rateOverTime = (breakCount - 4) / 3;
            }
            else if (breakCount > 5)
                watering.SetActive(true);
        }
    }
}
