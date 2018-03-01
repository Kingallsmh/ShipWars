using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraScript : MonoBehaviour {

    PostProcessingProfile myProfile;

	// Use this for initialization
	void Start () {

        //Get the current behaviour component attached to this gameobject
        PostProcessingBehaviour behaviour = GetComponent<PostProcessingBehaviour>();
        if(behaviour == null || behaviour.profile == null){
            return;
        }

        //Instantiate a new profile(copy values) and set it to the behaviour we are using
        myProfile = Instantiate(behaviour.profile);
        behaviour.profile = myProfile;
	}

    private void Update()
    {
        if(Input.GetKeyDown("r")){
            SetChannelMainValues(new Vector3(1, 0 ,0));
        }
        else if(Input.GetKeyDown("g")){
            SetChannelMainValues(new Vector3(0, 1, 0));
        }
        else if (Input.GetKeyDown("b"))
        {
            SetChannelMainValues(new Vector3(0, 0, 1));
        }
    }

    /// <summary>
    /// Input what colour channels should be shown on camera. Colours will only affect
    /// their own color values. I.E. Red affects red, not blue or green in the red vector.
    /// </summary>
    /// <param name="rgb">Rgb.</param>
    private void SetChannelMainValues(Vector3 rgb){
        var channel = myProfile.colorGrading.settings;
        channel.channelMixer.red = new Vector3(rgb.x, 0, 0);
        channel.channelMixer.green = new Vector3(0, rgb.y, 0);
        channel.channelMixer.blue = new Vector3(0, 0, rgb.z);
        myProfile.colorGrading.settings = channel;
        Debug.Log("Input: " + rgb + " output" + channel.channelMixer.red);
    }
}
