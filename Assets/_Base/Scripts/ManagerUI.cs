using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    [Header( "Components" ), SerializeField]
    private PanelBase panelMenu;
    [SerializeField]
    private PanelBase panelHUD;
    [SerializeField]
    private PanelBase panelLoading;
    [SerializeField]
    private PanelBase panelScore;

    // Panel HUD
    [Header( "Ingame HUD" ), SerializeField] private UnityEngine.UI.Text currentPlayer;
    [SerializeField] private UnityEngine.UI.Text winner;
    //[SerializeField] private UnityEngine.UI.Text score;
    //[SerializeField] private UnityEngine.UI.Text enemycount;

    void Awake()
    {
        Director.Instance.managerUI = this;
    }

    private void Update()
    {
        if( Director.Instance.currentScene == Structs.GameScene.Ingame )
        {

        }
    }

    #region Panel management
    public void SetPanels()
    {
        switch( Director.Instance.currentScene )
        {
            case Structs.GameScene.Menu:
                panelMenu.Show();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Hide();
                break;

            case Structs.GameScene.Ingame:
                panelMenu.Hide();
                panelHUD.Show();
                panelLoading.Hide();
                panelScore.Hide();
                break;

            case Structs.GameScene.Score:
                panelMenu.Show();
                panelHUD.Hide();
                panelLoading.Hide();
                panelScore.Show();
                break;

            case Structs.GameScene.LoadingGame:
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                panelScore.Hide();
                break;

            default: // Error!!
                panelMenu.Hide();
                panelHUD.Hide();
                panelLoading.Show();
                panelScore.Hide();
                break;
        }
    }
    #endregion

    #region Ingame HUD management
    public void SetCurrentPlayer( string to )
    {
        currentPlayer.text = "Current Player: " + to;
    }

    public void SetWinner( GameManager.Players to )
    {
        if( to == GameManager.Players.None )
        {
            winner.text = "Winner: Draw";
        }
        else
        {
            winner.text = "Winner: " + to.ToString();
        }
    }
    #endregion


    #region Buttons
    public void ButtonPlay()
    {
        Director.Instance.GameBegin();
    }

    public void ButtonGameEnd()
    {
        Director.Instance.GameEnd( GameManager.Players.None );
    }

    public void ButtonExit()
    {
        Director.Instance.Exit();
    }
    #endregion
}
