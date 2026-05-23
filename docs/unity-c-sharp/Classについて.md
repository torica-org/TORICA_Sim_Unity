# Classについて

```cs

public class SomethingClass
{
  public 
  protected
  private
}

private SomethingClass something;
private GameManager gm;

private GameObject sliderObj;
private Slider sliderComp;

void Start()
{
  gm = GameManager.instance;

  sliderObj = GameObject.Find("TestSlider");
  sliderComp = sliderObj.GetComponent<Slider>();

}

void Update()
{
  
}


```
