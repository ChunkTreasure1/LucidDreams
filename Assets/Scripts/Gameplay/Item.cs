using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float DropSoundLevel = 50f;
    [SerializeField] private Sprite Image;
    [SerializeField] private string Tooltip;
    [SerializeField] public bool IsEnabled = true;

    public bool IsPickedUp = false;
    public string Name;
    public bool HaveBeenUsedForCrafting = false;

    public string GetToolTip()
    {
        return Tooltip;
    }

    public float GetSoundLevel() { return DropSoundLevel; }

    public virtual void Use()
    {
        Debug.Log("Used");
    }

    public Sprite GetImage()
    {
        return Image;
    }

    private void Update()
    {
        if (HaveBeenUsedForCrafting && !IsPickedUp)
        {
            Destroy(gameObject);
        }
    }

}
