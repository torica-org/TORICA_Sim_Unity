# MonoBehaviourについて

MonoBehaviourはクラスです．

Unity上で`"Create" -> "C# Script"`とすると，最初からこれが継承されています．

これを継承したクラスのクラス名は，ファイル名と一致している必要があります．
そうでなければUnityで認識されません．

# MonoBehaviour「が」継承しているクラス
名前空間は`UnityEngine`です．

`Behaviour`クラスを継承しています．

`Behaviour`は`Component`クラスを継承しています．

`Component`は`Object`クラスを継承しています．

なんてややこしい．

## これを継承すると主なメリット
- `GameObject`のコンポーネントにできる（アタッチできる／`AddComponent`できる）．
- （ある`GameObject`のコンポーネントなら）`Awake()`, `Start()`, `Update()`, `FixedUpdate()`, ... などの
関数が自動的に呼ばれる．
- ~~`Instantiate()`（ある`GameObject`のクローンを生成する強力な関数）が使える．~~ これは`Object`クラスの
静的関数だったので，別に`MonoBehaviour`を継承していなくても良かった...

## これを継承するデメリット
- **`new`キーワードでインスタンス化できなくなる...(^ω^#)** マジで嫌だ
  - `MonoBehaviour`を継承する`static`なクラスのメソッドを利用することで，
  `MonoBehaviour`が使えるクラスを`new`できる（~~UIClassesで使用~~ 廃止）．

## よく使うメソッド（関数）
### 自動的に呼ばれる関数
- Start()
- Update()
- FixedUpdate()
- LateUpdate()
- OnGUI()
- OnDisable()
- OnEnable()
### Public関数
- GetComponent<>()
- GetComponentInChildren<>()
- GetComponentInParent<>()
### Static関数
- Destroy(ゲームオブジェクト）
- DontDestroyOnLoad()
- Instantiate()

## 参考になりそうなページ
[MonoBehaviour - Unity スクリプトリファレンス](https://docs.unity3d.com/ja/2021.3/ScriptReference/MonoBehaviour.html)
[【Unityの基礎】MonoBehaviour徹底解説【初心者向け】 - 渋谷ほととぎす通信](https://shibuya24.info/entry/unity-monobehaviour)
