using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo;

class Program
{
    public static void Main()
    {
        List<int> list=new List<int>(){1,2,3,4,5,6,7,8,9,10};

        var querySyntax= from obj in list where obj >2 & obj<8 select obj;

        foreach(int item in querySyntax)
        {
            System.Console.WriteLine(item);
        }
        System.Console.WriteLine("----------------------------");

        var methodSyntax= list.Where(obj=> obj>3 & obj<=9);

        foreach(int item in methodSyntax)
        {
            System.Console.WriteLine(item);
        }
        System.Console.WriteLine("----------------------------");

        var mixedSyntax=
    }
}