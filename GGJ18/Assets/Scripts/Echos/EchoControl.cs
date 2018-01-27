using UnityEngine;

public class EchoControl : MonoBehaviour {

    public int intensity;

    private SonarShader sonarShader;

    // Use this for initialization
    void Start () {
        sonarShader = GetComponent<SonarShader>();

    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            sonarShader.StartSonarRing(transform.position, intensity);
        }
        // Start sonar ring from the contact point
    }
}
