using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;


public class ShootProjectile : MonoBehaviourPunCallbacks
{
    public GameObject projectilePrefab;
    public Camera ARCamera;
    public float shootForce = 10f;
    public float raycastLength = 1000000f;
    private ScoreManager scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }

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
        GameObject projectile = PhotonNetwork.Instantiate(projectilePrefab.name, ARCamera.transform.position,
            ARCamera.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(ARCamera.transform.forward * shootForce, ForceMode.Impulse);

        // Raycast pour détecter les collisions avec les objets qui ont un collider
        RaycastHit hit;
        if (Physics.Raycast(ARCamera.transform.position, ARCamera.transform.forward, out hit, raycastLength))
        {
            Debug.Log("Hit " + hit.transform.name);
            //tell witch player hit the target
            Debug.Log(PhotonNetwork.LocalPlayer.NickName+" hit the target");
        }
        
        //if we hit object tagged target then destroy the target after 4 seconds
        if (hit.transform.tag == "Target")
        {
            //update the score of the player who hit the target
            scoreManager.playerScores[PhotonNetwork.LocalPlayer.ActorNumber - 1].score += 1;
            //destroy the target
            
           
            StartCoroutine(DestroyTarget(hit.transform.gameObject));
            
            
        }

        // Destruction du projectile après 2 secondes
        Destroy(projectile, 10f);

    }
    
    //destroy the target after 4 seconds
    IEnumerator DestroyTarget(GameObject target)
    {
        yield return new WaitForSeconds(4);
        PhotonNetwork.Destroy(target);
    }
}
