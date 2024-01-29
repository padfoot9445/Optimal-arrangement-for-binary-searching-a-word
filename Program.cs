using System.Text;
namespace Program;
public static class P
{
    static double Permutations = 0;
    static double TotalSteps = 0;
    static StringBuilder sb = new();
    private static int GetCharacterOccuranceValue(char input)
    {
        switch(input)
        {
            case 'e': return 5688; 
            case 'm': return 1536; 
            case 'a': return 4331; 
            case 'h': return 1531; 
            case 'r': return 3864; 
            case 'g': return 1259; 
            case 'i': return 3845; 
            case 'b': return 1056; 
            case 'o': return 3651; 
            case 'f': return 924; 
            case 't': return 3543; 
            case 'y': return 906; 
            case 'n': return 3392; 
            case 'w': return 657; 
            case 's': return 2923; 
            case 'k': return 561; 
            case 'l': return 2798; 
            case 'v': return 513; 
            case 'c': return 2313; 
            case 'x': return 148; 
            case 'u': return 1851; 
            case 'z': return 139; 
            case 'd': return 1725; 
            case 'j': return 100; 
            case 'p': return 1614; 
            case 'q': return 1;
            default: throw new Exception();
        }
    }
    private static (char[], int)[] Results = [];
    private static int ResultIndex = 0;
    private static char[] Chars= [];
    private static void Check(char[] order)
    {
        BSearch(order);
    }
    private static void BSearch(char[] searchArray)
    {//172.93 //104 //103

        //68.03
        int BSearchTotalSteps = 0;
        int steps = 0;
        int startPos;
        int endPos;
        int Length;
        int charToFindPosition = 0;
        foreach (char CharToFind in Chars)
        {
            TotalSteps += steps;
            steps = 0;
            //Stack<int> Remaining = new(ThreeToTen);
            startPos = 0;
            endPos = searchArray.Length - 1;
            Length = endPos - startPos + 1;
            //charToFindPosition = position[CharToFind];
            for(int i=0; i< searchArray.Length; i++){ if (searchArray[i] == CharToFind) charToFindPosition = i; }
            while(Length > 2)
            {
                int FlooredHalf = (int)Math.Floor((double)(Length/ 2)) + startPos;
                steps++;
                if (charToFindPosition <= FlooredHalf)
                {
                    endPos = FlooredHalf;
                } 
                else
                {
                    startPos = FlooredHalf + 1;
                }
                Length = endPos - startPos + 1;
            }
            if (Length == 2)
            {
                if(charToFindPosition != startPos){
                    steps++;
                }
                steps++;
                BSearchTotalSteps += steps * GetCharacterOccuranceValue(CharToFind);
            }
        
        }
        Results[ResultIndex++] = (searchArray, BSearchTotalSteps);
    }

    private static char[] IterativeSwap(char[] list, int a, int b)
    {
        char[] tlist = (char[])list.Clone();
        (tlist[b], tlist[a]) = (tlist[a], tlist[b]);
        return tlist;
    }

    public static void GetPer(char[] list, int factorial)
    {
        int x = list.Length - 1;
        IterativeGetPer(list, 0, x, factorial);
    }

    private static void IterativeGetPer(char[] list, int k, int m, int factorial)
    {
        Stack<(int, int, char[])> stack = new(factorial);
        stack.Push((k, m, list));
        while(stack.Count > 0)
        {
            (k, m, list) = stack.Pop();
            if(k == m)
            {
                Permutations++;
                Check(list);
            }
            else
            {
                for(int i=k; i <= m; i++)
                {
                    stack.Push((k + 1, m, IterativeSwap(list, k, i)));
                }
            }
        }
    }
    static int Main()
    {
        //const string str = "abcdefghijklmnopqrstuvwxyz";
        const string str = "abcdefghijklmnop";
        //const string str = "ab";
        int factorial = 1;
        for(int i=str.Length; i > 1; i--){ factorial *= i; }
        Results = new (char[], int)[factorial];
        char[] arr = str.ToCharArray();
        Chars = arr;
        int sbCharBuffer = (str.Length + 26 + 6) * factorial;

        sb = new(sbCharBuffer);
        GetPer(arr, factorial);
        double TotalChars = 0;
        foreach(char i in str)
        {
            TotalChars += GetCharacterOccuranceValue(i);
        }
        
        Output(TotalChars);
        return 0;
    }
    static void Output(double TotalChars)
    {
        foreach((char[], int) i in Results)
        {
            sb.Append(i.Item1);
            sb.AppendLine($" | {i.Item2} | {i.Item2 / TotalChars}");
        }
        var e = Results.MinBy(i => i.Item2);
        sb.AppendLine();
        sb.Append(e.Item1);
        sb.AppendLine($" | {e.Item2} | {e.Item2 / TotalChars}");
        sb.AppendLine($"Permutations computed: {Permutations}");
        sb.AppendLine($"Total Lookups Completed: {TotalSteps}");
        string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter("""D:\coding\c#\Best B-search array\output.txt"""))
        {
            outputFile.Write(sb.ToString());
        }
    }
}
