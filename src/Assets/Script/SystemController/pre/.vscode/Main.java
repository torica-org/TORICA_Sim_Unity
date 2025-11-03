import java.util.Arrays;

public class Main {

    public static void main(String[] args) {
        int[] array = new int[25];
        for (int i = 0; i < array.length; i++) {
            array[i] = i + 1;
        }
        permute(array, 0);
    }

    private static void permute(int[] array, int index) {
        if (index >= array.length - 1) { 
            System.out.println(Arrays.toString(array));
            return;
        }

        for (int i = index; i < array.length; i++) { 
            swap(array, index, i);
            permute(array, index + 1);
            swap(array, index, i);
        }
    }

    private static void swap(int[] array, int i, int j) {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}