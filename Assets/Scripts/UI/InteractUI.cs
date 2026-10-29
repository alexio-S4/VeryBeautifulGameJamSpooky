using UnityEngine;
using UnityEngine.UI;

public class FillBarUI : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    private void Awake()
    {
        if (_images == null)
        { Debug.Log("Add images in inspector"); }
    }

    private void Start() { Hide(); } //Initialize

    public void Show()
    {
        foreach (var img in _images)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (var img in _images)
        {
            img.gameObject.SetActive(false);
        }
    }


    public void Progress(float value)
    {
        _images[1].fillAmount = Mathf.Clamp01(value);
    }
}
