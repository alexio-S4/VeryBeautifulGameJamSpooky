using UnityEngine;
using UnityEngine.UI;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private Image _image;
    private void Awake()
    {
        if (_image == null) { _image = GetComponentInChildren<Image>(); }
    }

    public void Show() => _image.gameObject.SetActive (true);
    public void Hide() => _image.gameObject.SetActive (false);

    public void Progress(float value)
    {
        _image.fillAmount = Mathf.Clamp01(value);
    }
}
