using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using UnityEngine;

/// <summary>
/// プレイヤーが一定間隔でジャンプをさせるクラス
/// </summary>
public class PlayerJump : MonoBehaviour
{
    [Header("コンポーネント参照")]
    [Tooltip("物理移動を管理するコンポーネント")]
    [SerializeField] private PlayerRbMover playerRbMover;
    [Tooltip("ジャンプのトリガーとなる単語（WordJump）の配列")]
    [SerializeField] private WordJump[] wordJumps;
    
    //ジャンプパラメータ
    private float jumpPower;
    private float jumpInterval;
    //非同期処理
    private CancellationTokenSource abilityCts;

    private void Awake()
    {
        if(playerRbMover == null) { Debug.LogError("playerRbMoverが参照されていません"); return; }
        if(wordJumps == null) { Debug.LogError("wordJumpsが参照されていません"); return; }
    }

    private void OnEnable()
    {
        //オブジェクトが有効になったとき、古いトークンがあれば破棄
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
        }
        abilityCts = new CancellationTokenSource();
        //非同期の自動ジャンプループを開始
        AutoJumpLoopAsync(abilityCts.Token).Forget();
    }

    private void OnDisable()
    {
        //オブジェクトが無効化された時、実行中の非同期ループを強制終了
        if (abilityCts != null)
        {
            abilityCts.Cancel();
            abilityCts.Dispose();
            abilityCts = null;
        }
    }

    /// <summary>
    /// PlayerDataからジャンプパラメータをセット
    /// </summary>
    /// <param name="_data"></param>
    public void SetParameter(PlayerData _data)
    {
        jumpPower = _data.JumpPower;
        jumpInterval = _data.JumpInterval;
    }

    /// <summary>
    /// トリガーがONの間、一定間隔でジャンプを繰り返す非同期ループ
    /// </summary>
    /// <param name="_token"></param>
    /// <returns></returns>
    public async UniTask AutoJumpLoopAsync(CancellationToken _token)
    {
        //OnDisableでキャンセルされるまで無限ループ
        while (true)
        {
            //配列の中に、1つでもジャンプトリガーがONのWordが出るまで処理を一時停止
            await UniTask.WaitUntil(() => wordJumps.Any(w => w != null && w.IsJumpTrigger), cancellationToken: _token);
            
            //待機が明けたらジャンプ実行
            SoundManager.Instance.PlaySE(SESource.JUMP);
            playerRbMover.JumpRb(jumpPower);

            //次のジャンプ判定まで指定したインターバル待機
            await UniTask.Delay(TimeSpan.FromSeconds(jumpInterval), cancellationToken: _token);
        }
    }

    /// <summary>
    /// ジャンプの効果をリセット
    /// </summary>
    public void ResetJump()
    {
        foreach(var wordJump in wordJumps)
        {
            if (wordJump != null)
            {
                wordJump.ResetWord();
            }
        }
    }
}
