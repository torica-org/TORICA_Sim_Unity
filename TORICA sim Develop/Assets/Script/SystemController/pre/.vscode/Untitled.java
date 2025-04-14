import java.util.*;

public class Untitled {
        static int[] pipeList = {435,435,435,404,404,404,437,437,245,245,740,740,300,390,390,449,449,417,417,384,384,342,342,293,293};
        static int[] pipe = {800,800,800,800,800,800,1400,1400,1400,1400};
        static int nowPipe;
        static int min800;
        static int remove800A = -1;
        static int remove800B = -1;
        static int min1400;
        static String result[] = new String[10];
        static boolean OK;
        public static void main(String[] args) throws Exception {
        
        min800 = 800 - pipeList[0];//min800の初期値
        
        for(int i0=0; ; i0++)//1つ目の値のループ処理
        {
            if(min800 > 800-pipeList[SearchList()])//一つ入れた時点で最小の場合
            {
                min800 = 800-pipeList[SearchList()];
            }
            
            for(int i=SearchList()+1;i<25;i++)//2つ目の値のループ処理
            {
                if(800 - pipeList[SearchList()] - pipeList[i] > 0)
                {
                    for(int i2=SearchList()+1;i2<25;i2++)
                    {
                        if(min800 > 800 - pipeList[SearchList()] - pipeList[i] - pipeList[i2] && 800 - pipeList[SearchList()] - pipeList[i] -pipeList[i2] > 0 && i2 != i)
                        {
                            min800 = 800 - pipeList[SearchList()] - pipeList[i] -pipeList[i2];
                            remove800A = i;
                            remove800B = i2;
                        }
                        else if (min800 > 800 - pipeList[SearchList()] - pipeList[i] )
                        {
                            min800 = 800 - pipeList[SearchList()] - pipeList[i];
                            remove800A = i;
                        }
                    }
                }
            }
            if(remove800A != -1 && remove800B != -1)
            {
                result[nowPipe]=String.valueOf(pipeList[SearchList()])+","+String.valueOf(pipeList[remove800A])+","+String.valueOf(pipeList[remove800B]);
                pipeList[SearchList()]=0;
                pipeList[remove800A]=0;
                remove800A =0;
                pipeList[remove800B]=0;
                remove800B =0;
            }
            else if (remove800B != -1)
            {
                result[nowPipe]=String.valueOf(pipeList[SearchList()])+","+String.valueOf(pipeList[remove800A])+",0";
                pipeList[SearchList()]=0;
                pipeList[remove800A]=0;
                remove800A =0;
            }
            else
            {
                result[nowPipe]=String.valueOf(pipeList[SearchList()])+",0,0";
                pipeList[SearchList()]=0;
            }
            
            for(int ix =0;ix>25;ix++)//全ての変数が0になっているかのチェック
            {
                if(pipeList[ix] != 0)
                {
                    break;
                }
                if(ix == 24)
                {
                    OK=true;
                }
            }
            if(OK)
            {
                break;
            }
        }
        System.out.println(result);
    }
    
    public static int SearchList()
    {
        int A;
        for(int a=0;;a++)
        {
            if(pipeList[a] != 0)
            {
                A = a;
                break;
            }
            
            if(a ==24)
            {
                A=0;
                break;
            }
        }
        return A;
    }
}