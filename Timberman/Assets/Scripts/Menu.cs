using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play(){
        SoundManager.Instance.PlaySound(SoundManager.Instance.FxPlay);
        SceneManager.LoadScene("Game");
    }
}
