using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOrbit : MonoBehaviour
{
    Transform playerTransform;
    public float orbitSpeed;
    public float orbitRadius;
    public float spriteRotateSpeed;
    public List<Transform> objects;

    private float angleOffset;

    private void Awake()
    {
        playerTransform = transform.parent;
        angleOffset = 360f / objects.Count;
    }

    private void FixedUpdate()
    {
        SpinObject();
        OrbitObject();
    }

    private void SpinObject()
    {
        foreach (Transform t in objects)
        {
            t.Rotate(Vector3.forward, spriteRotateSpeed * Time.deltaTime);
        }

    }

    private void OrbitObject()
    {
        float angle = orbitSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward, angle);

        //this.transform.RotateAround(playerTransform.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        for (int i = 0; i < objects.Count; i++)
        {
            float childAngle = i * angleOffset + angle;
            Vector3 dir = new Vector3(Mathf.Cos(childAngle * Mathf.Deg2Rad), Mathf.Sin(childAngle * Mathf.Deg2Rad), 0f);
            objects[i].localPosition = dir * orbitRadius;
        }
    }
}
