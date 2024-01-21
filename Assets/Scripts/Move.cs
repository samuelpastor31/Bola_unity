using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{

    public Material blackMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material redMaterial;
    public Material yellowMaterial;
    private int colorIndex = 0;
    private Material[] materials;
    public float forceValue;
    private Rigidbody rb;
    public float jumpValue;
    private AudioSource audioSource;
    private Impulse impulseScript;
    public bool shouldThrowStone = false; // Bandera para controlar si la corrutina debe seguir ejecutándose
    private Coroutine throwStoneCoroutine; // Almacena la referencia a la corrutina ThrowStone



    // Start is called before the first frame update
    void Start()
    {
        materials = new Material[]{blackMaterial, redMaterial, blueMaterial, greenMaterial, yellowMaterial};
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        impulseScript = FindObjectOfType<Impulse>(); // Asigna impulseScript buscando el objeto en la escena
        
    }

    // Update is called once per frame
    void Update()
        
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
            audioSource.Play();
        }
        if(Input.touchCount == 1)
            if (Input.touches[0].phase == TouchPhase.Began && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
                audioSource.Play();
            }


        if (transform.position.y <= -2){
        SceneManager.LoadScene("SampleScene");
    }

    }

    void FixedUpdate(){
                rb.AddForce(new Vector3(Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical"))* forceValue);
                rb.AddForce(new Vector3(Input.acceleration.x,
                0,
                Input.acceleration.y)* forceValue);

    
    }

    private void OnCollisionEnter(Collision collision){

        if(collision.gameObject.tag == "Enemy"){
        print("Collision");
        Destroy(collision.gameObject);
        }

        //Acabar juego si choca con el enemigo
        if(collision.gameObject.tag == "End"){
        print("Collision");
        SceneManager.LoadScene("Awake");

        }


        if (collision.gameObject.CompareTag("Button") && shouldThrowStone == false) {
            shouldThrowStone = true; // Activa para iniciar la corrutina
            throwStoneCoroutine = impulseScript.StartCoroutine(impulseScript.ThrowStone());
            print("Activado");
        }
        
        if (collision.gameObject.CompareTag("ButtonDesactivate") && shouldThrowStone == true) {
            shouldThrowStone = false;
            if (throwStoneCoroutine != null) {
                impulseScript.StopCoroutine(throwStoneCoroutine); // Detiene la corrutina almacenada
                print("Desactivado");
            }
        }
    
    }

    private void OnTriggerEnter(Collider other){
    Renderer renderer = GetComponent<Renderer>();
    renderer.material = materials[colorIndex % materials.Length];
    colorIndex++;
    }

    
}
