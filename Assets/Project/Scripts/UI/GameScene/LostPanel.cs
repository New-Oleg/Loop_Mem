using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LostPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI LevelCountText;

    [SerializeField]
    private GameObject loosPanel;

    private int LevelCount;

    private void OnEnable()
    {
        RoadWalker.DropCard += LevelCountPlass;
        FightController.OnLoos += ShowLoosPanel;
    }

    private void OnDisable()
    {
        RoadWalker.DropCard -= LevelCountPlass;
        FightController.OnLoos -= ShowLoosPanel;
    }

    private void LevelCountPlass()
    {
        LevelCount++;
    }

    private void ShowLoosPanel()
    {
        LevelCountText.text = "Levels: " + LevelCount;

        if (YG2.saves.BestScore < LevelCount)
        {
            YG2.saves.BestScore = LevelCount;
        }
        YG2.saves.Coin += LevelCount / 3;
        YG2.SaveProgress();


        loosPanel.SetActive(true);
    }
    private void UnshowLoosPanel()    // Сделать перезапуск / продолжение игры
    {
        loosPanel.SetActive(true);
    }

    public void GoToMainMenu() // добавить сохранение результата
    {
        SceneManager.LoadScene("MainMenu");
    }
}
