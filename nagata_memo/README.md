# MyGameManeger.cs

`MyGameManeger`クラスが定義されている．

読み込まれると，以下の行で`MyGameManeger`型の変数が宣言・初期化される．  
`static`キーワードにより，クラス内に1つだけ存在することが保証される．
> MyGameManeger.cs (L8)
```cs
public static MyGameManeger instance = null;
```

`instance == null`の場合，クラス自身が代入される．
> MyGameManeger.cs (L95)
```cs
instance = this;
```

ここに，ほとんどのグローバル変数やフラグが宣言されている．  
以下のようにして参照する．
> AerodynamicCalculator.cs (L938)
```cs
else if (MyGameManeger.instance.PlaneName == "Ray") {...}
```

# Sliderについて

<img width="300px" src="img/Inspector_On_Value_Changed.png" />

赤枠のところにドラッグ＆ドロップするのはオブジェクト（スライダー自体など）  
**スクリプトではないことに注意**

