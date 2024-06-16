using UnityEngine;

public class Kup : MonoBehaviour
{
    public bool _HareketEdebilirMi;

    float _GelisHizi;

    void Start()
    {
        _GelisHizi = GameManager.instance._KupGelisHizi;
    }

  
    void Update()
    {
        if (_HareketEdebilirMi)
        {
            transform.Translate(_GelisHizi * Time.deltaTime * Vector3.forward);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Carpistirici") || other.CompareTag("Zemin"))
        {
            GameManager.instance.OyunBitti();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kup"))
        {
            GameManager.instance._ToplananKupSayisi++;
            GameManager.instance.YeniKupGelsin();
            if (GameManager.instance._ToplananKupSayisi == 1)
            {
                collision.gameObject.tag = "Untagged";
            }
        }
    }
}
