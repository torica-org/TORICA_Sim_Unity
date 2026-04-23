# Serializeについて

## インスペクターに変数を表示するには
以下，2つの方法がある．
- フィールドを`public`にする
- `private`フィールドに`[SerializeField]`をつける
しかし，以下の条件に当てはまる場合には表示されない．
- static フィールド（インスタンスに紐づかない）
- Dictionary 型（Unity標準では非対応）
- Serializableが付いていないカスタムクラス
- Unityがサポートしていないジェネリック構造

## インスペクターに変数を表示させない
`[System.NonSerialized]`をつける

## 参考
- [Unityのシリアライズ完全ガイド！データの保存・読み込みを自由自在に | C-BA Unity-memo](https://cbagames.jp/2025/03/02/unity-serialize-guide/)
