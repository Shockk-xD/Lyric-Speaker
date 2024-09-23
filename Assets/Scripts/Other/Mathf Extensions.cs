public static class MathfExtensions
{
    public static int LoopedClamp(int value, int min, int max) {
        if (value > max)
            return min;
        else if (value < min)
            return max;
        else
            return value;
    }
}
