using HoloToolkit.Unity;
using UnityEngine;

public class SurroundEmitter : MonoBehaviour
{
    public Material defaultMaterial;
    public Material disabledMaterial;
    public GameObject inactiveNeuronObject;
    public GameObject activeNeuronObject;

    [SerializeField]
    private AudioSource emitter;
    [SerializeField]
    private string eventName;
    [SerializeField]
    private Light toggleLight;

    private Renderer meshRenderer;
    private int clickCount;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
        defaultMaterial = meshRenderer.material;
        clickCount = 0;
    }

    public void PlayEvent()
    {
        UAudioManager.Instance.PlayEvent(this.eventName, this.emitter);
    }

    private void OnSelected()
    {
        // This code is commented out so that only the GestureManager will direct interactions between
        // the interactible items and the user. 
        // Interactions coming through the CursorManager will be ignored.
        // That's because the GestureManage sends an "OnSelect" message, while CursorManager sends "OnSelected".
        /*if (this.emitter.mute)
        {
            this.emitter.mute = false;
            this.toggleLight.enabled = true;
            meshRenderer.material = defaultMaterial;
        }
        else
        {
            this.emitter.mute = true;
            this.toggleLight.enabled = false;
            meshRenderer.material = disabledMaterial;
        }*/
    }

    private void OnSelect()
    {
        if (this.tag != "proxy")
        {
            if (this.emitter.mute)
            {
                this.emitter.mute = false;
                this.toggleLight.enabled = true;
                meshRenderer.material = defaultMaterial;
            }
            else
            {
                this.emitter.mute = true;
                this.toggleLight.enabled = false;
                meshRenderer.material = disabledMaterial;
            }
        }
        else {
            clickCount++;
            if (clickCount < 3)
            {
                if (this.emitter.mute)
                {
                    this.emitter.mute = false;
                    this.toggleLight.enabled = true;
                    meshRenderer.material = defaultMaterial;
                }
                else
                {
                    this.emitter.mute = true;
                    this.toggleLight.enabled = false;
                    meshRenderer.material = disabledMaterial;
                }
            }
            else if (clickCount == 3)
            {
                if (inactiveNeuronObject != null)
                {
                    activeNeuronObject.SetActive(false);
                    inactiveNeuronObject.SetActive(true);
                    clickCount = 0;
                }
            }
            else if (clickCount > 3)
            {
                clickCount = 0;
            }
        }
    }
}