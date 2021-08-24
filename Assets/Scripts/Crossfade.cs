using System.Linq;
using UnityEngine;

public class Crossfade : MonoBehaviour
{

    public void StopEndAnimation()
    {
        FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad).crossfadeEndAnimation = false;
    }
    public void StopStartAnimation()
    {
        FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad).crossfadeStartAnimation = false;
    }
}
