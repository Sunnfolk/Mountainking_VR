using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Place this Script on the Moles / Chickens
public class Mole : MonoBehaviour
{
    
    public float visibleHeight = 0.2f;
    public float hiddenHeight = -0.3f;
    public float speed = 4f;
    public float disappearDuration = 0.5f;

    private Collider _coll;

    //On Hit Stuff
    public float hitCooldown = 0.5f;
    float hitTimer;
    bool isHit;

    private Vector3 targetPosition;
    private float disappearTimer = 0f;

    void Start()
    {
        _coll = gameObject.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Awake()
    {
        hitTimer = hitCooldown;

        targetPosition = new Vector3
            (transform.localPosition.x,
            hiddenHeight,
            transform.localPosition.z
            );

        transform.localPosition = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0f)
        {
            Hide();
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * speed);
    }

    public void Rise()
    {
        targetPosition = new Vector3
            (transform.localPosition.x,
            visibleHeight,
            transform.localPosition.z
            );

        disappearTimer = disappearDuration;
    }

    public void Hide()
    {
        targetPosition = new Vector3
            (transform.localPosition.x,
            hiddenHeight,
            transform.localPosition.z
            );

    }

    public void OnHit()
    {
        isHit = true;
        Hide();
    }

    public void ColliderDeactivate()
    {

        _coll.enabled = false;
    }

    public void ColliderActivate()
    {

        _coll.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeactivateField" || other.gameObject.name == "HammerTop")
        {
            Debug.Log("DEACTIVATE");
            ColliderDeactivate();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DeactivateField")
        {
            Debug.Log("Activate");
            ColliderActivate(); 
        }
    }
}

