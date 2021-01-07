using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    void Awake() 
    {
        int numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if(numberOfMusicPlayers > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
