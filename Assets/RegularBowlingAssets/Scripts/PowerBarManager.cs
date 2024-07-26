using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarManager : MonoBehaviour
{
    public Image powerBarMask;
    public float powerBarChangeSpeed = 1;
    private float maxPower = 30;
    private float currentPower = 0;
    private bool poweringUp = true;
    private bool powerBarOn = false;

    private void Start()
    {
        powerBarMask.fillAmount = currentPower / maxPower;
    }

    public IEnumerator UpdatePowerBar()
    {
        while (powerBarOn)
        {
            if (poweringUp)
            {
                currentPower += powerBarChangeSpeed;
                if (currentPower >= maxPower)
                {
                    poweringUp = false;
                }
            }
            else
            {
                currentPower -= powerBarChangeSpeed;
                if (currentPower <= 0)
                {
                    poweringUp = true;
                }
            }
            float fill = currentPower / maxPower;
            powerBarMask.fillAmount = fill;
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    public void SetPowerBarOn(bool powerBarOn)
    {
        this.powerBarOn = powerBarOn;
    }

    public void SetCurrentPower(float currentPower)
    {
        this.currentPower = currentPower;
        powerBarMask.fillAmount = currentPower / maxPower;
    }

    public float GetPowerRatio()
    {
        return currentPower / maxPower;
    }
}
