using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarUI : MonoBehaviour, IControl
{
    private void Awake()
    {
        GetControl = transform.GetComponentInChildren<Image>();
        GetText = transform.GetComponentInChildren<Text>();
    }

    public Text GetText { get; private set; }

    public Image GetControl { get; private set; }
   
    public GameObject Instance { get { return gameObject; } }
    public Selectable Control { get { return null; } }
}