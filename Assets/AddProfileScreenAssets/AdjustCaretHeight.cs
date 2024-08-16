using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdjustCaretHeight : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        var caret = inputField.transform.Find("Text Area/Caret");

        if (caret != null)
        {
            // Adjust the caret's scale
            RectTransform caretTransform = caret.GetComponent<RectTransform>();
            caretTransform.localScale = new Vector3(1, 2, 1);
        }
    }
}
