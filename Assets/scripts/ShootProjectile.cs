using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Camera ARCamera;
    public float shootForce = 10f;
    public float raycastLength = 1000000f;

    void Update()
    {
        // Détection de l'appui sur le bouton espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Fonction de tir du projectile depuis la caméra AR
    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(ARCamera.transform.forward * shootForce, ForceMode.Impulse);

        // Raycast pour détecter les collisions avec les objets qui ont un collider
        RaycastHit hit;
        if (Physics.Raycast(ARCamera.transform.position, ARCamera.transform.forward, out hit, raycastLength))
        {
            Debug.Log("Hit " + hit.transform.name);
        }

        // Destruction du projectile après 2 secondes
        Destroy(projectile, 3f);

    }
}
