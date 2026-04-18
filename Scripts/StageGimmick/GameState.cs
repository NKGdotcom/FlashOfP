using UnityEngine;
/// <summary>
/// ゲームの進行状態の種類
/// </summary>
public enum State
{
    EXPLAIN, //説明
    GAME_ACT, //ゲーム実行中
    DRAG_ACT, //ドラッグアンドドロップ中
    STAGE_SELECT, //ステージ選択中
}

/// <summary>
/// ゲーム全体の進行状態を管理する
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
    /// ゲームの状態を新しいものに上書き設定する
    /// </summary>
    /// <param name="_state"></param>
    public void SetState(State _state)
    {
        nowState = _state;
    }

    /// <summary>
    /// チュートリアル等の説明中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsExplain() => nowState == State.EXPLAIN;

    /// <summary>
    /// ゲーム本編のプレイ中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsGame() => nowState == State.GAME_ACT;

    /// <summary>
    /// ドラッグ&ドロップの操作中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsDrag() => nowState == State.DRAG_ACT;

    /// <summary>
    /// ステージ選択中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsStageSelect() => nowState == State.STAGE_SELECT;
}
