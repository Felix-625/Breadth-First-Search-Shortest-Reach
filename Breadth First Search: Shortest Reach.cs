using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'bfs' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER m
     *  3. 2D_INTEGER_ARRAY edges
     *  4. INTEGER s
     */

    public static List<int> bfs(int n, int m, List<List<int>> edges, int s)
    {
         // Build adjacency list
    List<int>[] graph = new List<int>[n + 1];
    for (int i = 1; i <= n; i++)
    {
        graph[i] = new List<int>();
    }
    
    foreach (var edge in edges)
    {
        int u = edge[0];
        int v = edge[1];
        graph[u].Add(v);
        graph[v].Add(u);
    }
    
    // Initialize distances array with -1 (unreachable)
    int[] distances = new int[n + 1];
    for (int i = 1; i <= n; i++)
    {
        distances[i] = -1;
    }
    
    // BFS starting from node s
    Queue<int> queue = new Queue<int>();
    queue.Enqueue(s);
    distances[s] = 0;
    
    while (queue.Count > 0)
    {
        int current = queue.Dequeue();
        
        foreach (int neighbor in graph[current])
        {
            if (distances[neighbor] == -1)
            {
                distances[neighbor] = distances[current] + 6;
                queue.Enqueue(neighbor);
            }
        }
    }
    
    // Prepare result excluding the starting node
    List<int> result = new List<int>();
    for (int i = 1; i <= n; i++)
    {
        if (i != s)
        {
            result.Add(distances[i]);
        }
    }
    
    return result;

    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            List<List<int>> edges = new List<List<int>>();

            for (int i = 0; i < m; i++)
            {
                edges.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(edgesTemp => Convert.ToInt32(edgesTemp)).ToList());
            }

            int s = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> result = Result.bfs(n, m, edges, s);

            textWriter.WriteLine(String.Join(" ", result));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
