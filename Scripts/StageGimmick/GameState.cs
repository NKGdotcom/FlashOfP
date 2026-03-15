using UnityEngine;
/// <summary>
/// ステートの種類
/// </summary>
public enum State
{
    EXPLAIN, //説明
    GAME_ACT, //ゲーム実行中
    DRAG_ACT, //ドラッグアンドドロップ中
    STAGE_SELECT, //ステージ選択中
}
/// <summary>
/// ゲームのステートの管理
/// </summary>
public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private State nowState = State.EXPLAIN;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        nowState = State.EXPLAIN;
    }
    /// <summary>
    /// ステートを設定
    /// </summary>
    /// <param name="_state"></param>
    public void SetState(State _state)
    {
        nowState = _state;
    }
    /// <summary>
    /// チュートリアルの説明中
    /// </summary>
    /// <returns></returns>
    public bool IsExplain()
    {
        return nowState == State.EXPLAIN;
    }
    /// <summary>
    /// ゲームのタスク中
    /// </summary>
    /// <returns></returns>
    public bool IsGame()
    {
        return nowState == State.GAME_ACT;
    }

    /// <summary>
    /// ドラッグアンドドロップ中
    /// </summary>
    /// <returns></returns>
    public bool IsDrag()
    {
        return nowState == State.DRAG_ACT;
    }

    /// <summary>
    /// ステージ選択中
    /// </summary>
    /// <returns></returns>
    public bool IsStageSelect()
    {
        return nowState == State.STAGE_SELECT;
    }
}
