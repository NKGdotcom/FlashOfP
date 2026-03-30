using UnityEngine;

public class DebugManager : MonoBehaviour
{
    void Update()
    {
        // 左クリックした瞬間
        if (Input.GetMouseButtonDown(0))
        {
            // 画面のクリック位置からRay（見えない光線）を飛ばす
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // 何に当たったかログに出す
                Debug.Log($"<color=yellow>Rayが当たったオブジェクト: {hit.collider.gameObject.name}</color>");
            }
            else
            {
                Debug.Log("<color=blue>何も当たりませんでした</color>");
            }
        }
    }
}
