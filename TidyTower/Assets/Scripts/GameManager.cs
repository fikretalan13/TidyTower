using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("----- KÜP YÖNETÝMÝ -----")]
    [SerializeField]
    GameObject[] _Kupler;
    int _AktifKupIndex;

    [SerializeField]
    GameObject[] _KupSoketleri;
    int _AktifKupSoketIndex;

    [SerializeField]
    Transform[] _Carpistiricilar;
   

    [SerializeField]
    CameraController cameraController;


    public float _KupGelisHizi;

    bool _DokunmaAktif;
    bool _OyunBittiMi;

    public int _ToplananKupSayisi;

    int _SahneIndex;

    [Header("----- UI YÖNETÝMÝ -----")]
    [SerializeField]
    GameObject[] _Paneller;

    [SerializeField]
    TextMeshProUGUI _BestScoreText;


    [SerializeField]
    Image[] _ButtonGorselleri;

    [SerializeField]
    Sprite[] _SpriteObjeleri;


    [Header("----- SES YÖNETÝMÝ -----")]
    [SerializeField]
    AudioSource[] _Sesler;


  


    private void Awake()
    {
        SahneIlkIslemleri();
        _SahneIndex = SceneManager.GetActiveScene().buildIndex;
        if(instance == null)
        {
            instance = this;
        }
        else
        {
                Destroy(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0 && Input.touchCount == 1 && _DokunmaAktif && !_OyunBittiMi)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (_AktifKupIndex != 0)
                {
                    _Kupler[_AktifKupIndex - 1].GetComponent<Kup>()._HareketEdebilirMi = false;
                    _Kupler[_AktifKupIndex - 1].GetComponent<Rigidbody>().useGravity = true;
                    _Sesler[3].Play();
                    _DokunmaAktif = false;
                    cameraController._Hedef = _Kupler[_AktifKupIndex - 1].transform;

                }
            }


         
        } 
    }

    public void OyunBitti()
    {
        
        if(_ToplananKupSayisi > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore",_ToplananKupSayisi);

        }
        _BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        _Sesler[2].Play();
        _OyunBittiMi = true;
        PanelAc(3);
        Time.timeScale = 0;
    }

    public void YeniKupGelsin()
    {
        if (!_OyunBittiMi)
        {
            if (_AktifKupIndex != 0)
            {
                _Kupler[_AktifKupIndex - 1].tag = "Untagged";

            }

            _Kupler[_AktifKupIndex].transform.SetPositionAndRotation(_KupSoketleri[_AktifKupSoketIndex].transform.position, _KupSoketleri[_AktifKupSoketIndex].transform.rotation);

            _Kupler[_AktifKupIndex].SetActive(true);
            _AktifKupIndex++;

            for (int i = 0; i < _KupSoketleri.Length; i++)
            {
                _KupSoketleri[i].transform.position = new Vector3(_KupSoketleri[i].transform.position.x, _KupSoketleri[i].transform.position.y+0.1f, _KupSoketleri[i].transform.position.z);

                _Carpistiricilar[i].transform.position = new Vector3(_Carpistiricilar[i].transform.position.x, _Carpistiricilar[i].transform.position.y + 0.1f, _Carpistiricilar[i].transform.position.z);

            }


            if(_AktifKupSoketIndex==1)
            {
                _AktifKupSoketIndex = 0;
            }
            else
            {
                _AktifKupSoketIndex = 1;

            }

          
            _DokunmaAktif = true;
        }
    }






    public void ButonIslemleri(string ButonDeger)
    {
        switch (ButonDeger)
        {
            case "Durdur":
                _Sesler[1].Play();
                PanelAc(2);
                Time.timeScale = 0;
                break;
            case "DevamEt":
                _Sesler[1].Play();
                PanelKapat(2);
                Time.timeScale = 1;
                break;

            case "OyunaBasla":
                _Sesler[1].Play();
                PanelKapat(1);
                PanelAc(0);
                YeniKupGelsin();               
                break;
            case "Tekrar":
                _Sesler[1].Play();
                SceneManager.LoadScene(_SahneIndex);
                Time.timeScale = 1;
                break;
            case "Cikis":
                _Sesler[1].Play();
                PanelAc(4);
                break;
            case "Evet":
                _Sesler[1].Play();
                Application.Quit();
                print("Cýkýþ yapýldý");
                break;
            case "Hayir":
                _Sesler[1].Play();
                PanelKapat(4);
                break;

            case "OyunSes":
                _Sesler[1].Play();

                if (PlayerPrefs.GetInt("OyunSes") == 0)
                {
                    PlayerPrefs.SetInt("OyunSes", 1);
                    _ButtonGorselleri[0].sprite = _SpriteObjeleri[0];
                    _Sesler[0].mute = false;
                }
                else
                {
                    PlayerPrefs.SetInt("OyunSes", 0);
                    _ButtonGorselleri[0].sprite = _SpriteObjeleri[1];
                    _Sesler[0].mute = true;
                }
                break;

            case "EfektSes":
                _Sesler[1].Play();

                if (PlayerPrefs.GetInt("EfektSes") == 0)
                {
                    PlayerPrefs.SetInt("EfektSes", 1);
                    _ButtonGorselleri[1].sprite = _SpriteObjeleri[2];

                    for (int i = 1; i < _Sesler.Length; i++)
                    {
                        _Sesler[i].mute = false;
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("EfektSes", 0);
                    _ButtonGorselleri[1].sprite = _SpriteObjeleri[3];
                    for (int i = 1; i < _Sesler.Length; i++)
                    {
                        _Sesler[i].mute = true;
                    }
                }
                break;

        }

    }
    void SahneIlkIslemleri()
    {

        _BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();

        if(!PlayerPrefs.HasKey("OyunSes"))
        {
            PlayerPrefs.SetInt("OyunSes",1);
            PlayerPrefs.SetInt("EfektSes",1);
        }

        if (PlayerPrefs.GetInt("OyunSes") == 1)
        {
            _ButtonGorselleri[0].sprite = _SpriteObjeleri[0];
            _Sesler[0].mute = false;
        }
        else
        {
            _ButtonGorselleri[0].sprite = _SpriteObjeleri[1];
            _Sesler[0].mute = true;
        }


        if (PlayerPrefs.GetInt("EfektSes") == 1)
        {
            _ButtonGorselleri[1].sprite = _SpriteObjeleri[2];

            for (int i = 1; i < _Sesler.Length; i++)
            {
                _Sesler[i].mute = false;
            }
        }
        else
        {
            _ButtonGorselleri[1].sprite = _SpriteObjeleri[3];
            for (int i = 1; i < _Sesler.Length; i++)
            {
                _Sesler[i].mute = true;
            }
        }
    }
    void PanelAc(int Index)
    {
        _Paneller[Index].SetActive(true);
    }
    void PanelKapat(int Index)
    {
        _Paneller[Index].SetActive(false);
    }
}
