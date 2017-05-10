using UnityEngine;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{

    #region Variables
    public GameManager gameManager;
    public ManagerCamera managerCamera;
    //public Player player;
    //public ManagerMap managerMap;
    //public ManagerEntity managerEntity;
    //public WavesManager waveManager;
    public ManagerInput managerInput;
    public ManagerUI managerUI;
    //public ScoreManager scoreManager;

    public Structs.GameMode currentGameMode { private set; get; }
    public Structs.GameDifficulty currentGameDifficulty { private set; get; }
    public Structs.GameView currentGameView { private set; get; }
    public Structs.GameScene currentScene;

    public bool isPaused;
    #endregion


    #region Singleton
    private static Director instance;

    public static Director Instance
    {
        get { return instance; }
    }

    static Director()
    {
        GameObject obj = GameObject.Find( "Director" );

        if( obj == null )
        {
            obj = new GameObject( "Director", typeof( Director ) );
        }

        instance = obj.GetComponent<Director>();
    }
    #endregion


    #region Monobehaviour
    private void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    #endregion


    #region Scene management
    private void ChangeScene( Structs.GameScene to )
    {
        currentScene = to;

        //Debug.Log("Change scene to: " + currentScene);

        switch( currentScene )
        {
            case Structs.GameScene.Initialization:
                SwitchToMenu();
                break;

            case Structs.GameScene.Menu:
                managerInput.SetEvents();
                managerUI.SetPanels();
                break;

            case Structs.GameScene.LoadingGame:
                gameManager.ResetGame();
                gameManager.StartGame();
                managerUI.SetPanels();
                managerCamera.SwitchToCamera( ManagerCamera.Cameras.Static );

                // And go!
                SwitchToIngame();
                break;

            case Structs.GameScene.Ingame:
                gameManager.OnEndGame += GameEnd;


                managerInput.SetEvents();
                managerUI.SetPanels();
                break;

            case Structs.GameScene.GameEnd:
                gameManager.OnEndGame -= GameEnd;
                managerCamera.SwitchToCamera( ManagerCamera.Cameras.Motion );
                //gameManager.ResetGame(); // not necessary
                managerUI.SetCurrentPlayer( GameManager.Players.None.ToString() );
                //managerUI.SetWinner( gameManager.GetCurrentPlayer().ToString() );

                SwitchToScore();
                break;

            case Structs.GameScene.Score:
                managerInput.SetEvents();
                managerUI.SetPanels();
                break;

            case Structs.GameScene.Exit:
                Application.Quit();
                break;
        }

    }
    #endregion


    #region Game logic
    public void SetGameSettings( Structs.GameMode gameMode, Structs.GameDifficulty gameDifficulty, Structs.GameView viewMode )
    {
        currentGameMode = gameMode;
        currentGameDifficulty = gameDifficulty;
        currentGameView = viewMode;
    }
    

    #endregion


    #region Game cycle
    // This is the first thing that begins the whole game
    public void EverythingBeginsHere()
    {
        ChangeScene( Structs.GameScene.Initialization );
    }

    // This is automatic
    private void SwitchToMenu()
    {
        ChangeScene( Structs.GameScene.Menu );
    }

    public void GameBegin()
    {
        ChangeScene( Structs.GameScene.LoadingGame );
    }

    // This is automatic
    private void SwitchToIngame()
    {
        ChangeScene( Structs.GameScene.Ingame );
    }

    public void GameEnd( GameManager.Players winner )
    {
        ChangeScene( Structs.GameScene.GameEnd );
    }

    // This is automatic
    private void SwitchToScore()
    {
        ChangeScene( Structs.GameScene.Score );
    }

    public void Exit()
    {
        Debug.Log( "Exit game!" );
        ChangeScene( Structs.GameScene.Exit );
    }
    #endregion


    #region Input
    public void MouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;

        if( Physics.Raycast( ray, out hit, 100 ) )
        {
            Transform thingie = hit.collider.transform;
            if( thingie.CompareTag( "BoardPosition" ) )
            {
                var pos = thingie.GetComponent<BoardPosition>();
                //Debug.Log( "This is BoardPosition " + pos.transform.name );
                gameManager.PlayTurn( pos.row, pos.column );

            }
            //else
            //{
            //    Debug.Log( "This isn't a BoardPosition:"+ hit.transform.name );
            //}
        }
    }
    #endregion
}
