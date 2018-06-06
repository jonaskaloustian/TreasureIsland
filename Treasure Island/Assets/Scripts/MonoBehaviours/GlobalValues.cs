using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalValues : MonoBehaviour {

    public int[] defaultValues;
    private Text[] resourceTexts;

    private void Awake()
    {
        resourceTexts = new Text[3];
        for (int i = 0; i < resourceTexts.Length; i++)
        {
            resourceTexts[i] = transform.GetChild(i).GetComponent<Text>();
            resourceTexts[i].text = System.Enum.GetName((typeof(Resource)), i) + " : " + defaultValues[i].ToString();
        }
    }

    private void InitializeValues()
    {

    }

    public bool CheckIfValueStrictlySuperior (Resource resource ,int value)
    {
        if (defaultValues[(int)resource] > value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfValueStrictlyInferior(Resource resource, int value)
    {
        if (defaultValues[(int)resource] < value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfValueEquals(Resource resource, int value)
    {
        if (defaultValues[(int)resource] == value)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddToValue (Resource resource, int value)
    {
        defaultValues[(int)resource] += value;
        UpdateValue(resource, value);
    }

    public void SusbstractFromValue (Resource resource, int value)
    {
        defaultValues[(int)resource] -= value;
        UpdateValue(resource, value);
    }

    public void SetValue (Resource resource, int value)
    {
        defaultValues[(int)resource] = value;
        UpdateValue(resource, value);
    }

    private void UpdateValue(Resource resource, int value)
    {
        resourceTexts[(int)resource].text = resource.ToString() + " : " + defaultValues[(int)resource];
    }

}
