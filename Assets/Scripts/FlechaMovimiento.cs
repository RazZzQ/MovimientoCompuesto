using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaMovimiento : MonoBehaviour
{
    public Rigidbody Flecha;
    public Transform Objetivo;

    public float AlturaMaxima = 25;
    public float Gravedad = -18;

    public bool DibujarDireccion;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }

        if (DibujarDireccion)
        {
            DireccionDeIda();
        }
    }

    void Launch()
    {
        Physics.gravity = Vector3.up * Gravedad;
        Flecha.useGravity = true;
        Flecha.velocity = CalcularLanzamiento().initialVelocity;
    }

    Lanzamiento CalcularLanzamiento()
    {
        float displacementY = Objetivo.position.y - Flecha.position.y;
        Vector3 displacementXZ = new Vector3(Objetivo.position.x - Flecha.position.x, 0, Objetivo.position.z - Flecha.position.z);
        float time = Mathf.Sqrt(-2 * AlturaMaxima / Gravedad) + Mathf.Sqrt(2 * (displacementY - AlturaMaxima) / Gravedad);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Gravedad * AlturaMaxima);
        Vector3 velocityXZ = displacementXZ / time;

        return new Lanzamiento(velocityXZ + velocityY * -Mathf.Sign(Gravedad), time);
    }

    void DireccionDeIda()
    {
        Lanzamiento launchData = CalcularLanzamiento();
        Vector3 previousDrawPoint = Flecha.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * Gravedad * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = Flecha.position + displacement;

            // Cambia la rotación de la flecha para que mire hacia el punto de dibujo
            Flecha.transform.LookAt(drawPoint);

            Debug.DrawLine(previousDrawPoint, drawPoint, Color.red);
            previousDrawPoint = drawPoint;
        }
    }

    struct Lanzamiento
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public Lanzamiento(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
