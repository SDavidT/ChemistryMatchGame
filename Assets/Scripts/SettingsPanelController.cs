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
            panelSettings.SetBool("Out", true);
            panelSettings.SetBool("In", false);
        }
    }

    public void InSettings()
    {
        if (panelSettings != null)
        {
            panelSettings.SetBool("In", true);
            panelSettings.SetBool("Out", false);
        }
    }
}
