using Game.Runtime;
using TMPro;
using UnityEngine;

public class BoneView : ObserverMonoBehaviour
{
    public TextMeshProUGUI boneText;

    protected override void Subscribe()
    {
        var model = ServiceResolver.Resolve<GameStateModel>();
        model.BoneCount.onValueChanged += SetBoneText;
    }

    protected override void Unsubscribe()
    {
        var model = ServiceResolver.Resolve<GameStateModel>();
        model.BoneCount.onValueChanged -= SetBoneText;
    }

    // Create a standalone function that can update the 'countText' UI and
    // check if all the bones have been collected
    private void SetBoneText(int bone)
    {
        if (boneText == null)
        {
            Debug.LogError("boneText is not assigned in BoneView!");
            return;
        }

        boneText.text = "Bones Collected: " + bone;
    }
}