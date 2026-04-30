using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public TextMeshProUGUI boneText;
    
    // Start is called before the first frame update
    private void Start()
    {
        SetBoneText(0);
    }

    // Create a standalone function that can update the 'countText' UI and
    // check if all the bones have been collected
    public void SetBoneText(int bone)
    {
        if (boneText == null)
        {
            Debug.LogError("boneText is not assigned in GameView!");
            return;
        }

        boneText.text = "Bones Collected: " + bone;
    }
}