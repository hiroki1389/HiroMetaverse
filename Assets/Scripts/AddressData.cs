using UnityEngine;

// ゲームオブジェクトを追加した時間を保持するクラス
public class AddressData
{
    private GameObject gameObject;
    private System.DateTime time;

    // コンストラクタ
    public AddressData(GameObject gameObject, System.DateTime time)
    {
        this.gameObject = gameObject;
        this.time = time;
    }

    public void print()
    {
        Debug.Log("gameObject: " + this.gameObject.name);
        Debug.Log("time: " + this.time);
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public System.DateTime getTime()
    {
        return time;
    }
}
