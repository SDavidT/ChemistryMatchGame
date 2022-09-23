using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    public Animator panelSettings;

    public void ExitSettings()
    {
        if (panelSettings != null)
        {
            panelSettings.SetBool("Out2", true);
            panelSettings.SetBool("In", false);
        }
    }

    public void InSettings()
    {
        if (panelSettings != null)
        {
            panelSettings.SetBool("In", true);
            panelSettings.SetBool("Out2", false);
        }
    }
}
