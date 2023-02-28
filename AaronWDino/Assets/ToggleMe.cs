using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMe : MonoBehaviour
{
    public void ToggleOnOrOff()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
