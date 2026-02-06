import java.util.Arrays;

public class Pipe {
    public static int nowpipe;
    public static int[] pipe = {800,800,800,800,800,800,1400,1400,1400,1400};
    public static int[] PipeCut = {435,435,435,404,404,404,437,437,245,245,740,740,300,390,390,449,449,417,417,384,384,342,342,293,293};
    public static int[] PipeSet = new int[25];
    
    public static void main(String[] args) {
        System.out.println("Start");
        Arrays.sort(PipeCut);
        permute();
    }

    private static void permute() {
        if (0 >= PipeCut.length - 1) { 
            nowpipe =0;
            for (int i2=0;i2<10 ;i2++) {
                for (int i=nowpipe;i<25 ;i++) {
                    if(pipe[i2] >= PipeCut[i])
                    {
                        PipeCut[i2] -= PipeCut[i];
                        PipeSet[i]=PipeCut[i];
                        PipeCut[i] = 0;
                        nowpipe++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if(PipeCut[24] == 0)
            {
                System.out.println(Arrays.toString(PipeSet)+"A");
            }
            for (int ix =0;ix<25 ;ix++ ) {
                PipeCut[ix] = PipeSet[ix];
                System.out.println(Arrays.toString(PipeSet)+"B");
                PipeSet[ix] = 0;
            }
            System.out.println("End");
            return;
        }

        for (int i = 0; i < PipeCut.length; i++) { 
            
            if (i > 0 && PipeCut[i] == PipeCut[0]) continue;
            swap(0, i);
            System.out.println(Arrays.toString(PipeSet)+"C");
            permute();
            swap(0, i);
        }
    }

    private static void swap(int i, int j) {
        int temp = PipeCut[i];
        PipeCut[i] = PipeCut[j];
        PipeCut[j] = temp;
    }
}