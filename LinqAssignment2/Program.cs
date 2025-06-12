using System;
using System.Collections.Generic;
using System.Linq;
namespace LinqAssignment2;

class Program
{
    public static void Main()
    {
        List<string> places= new List<string>(){"ABU DHABI","AMSTERDAM","ROME","PARIS","CALIFORNIA","LONDON","NEW DELHI","ZURICH","NAIROBI"};

        var output= (from place in places orderby place.Length ascending select place).ToList();

        output.ForEach(output=> System.Console.WriteLine(output));
        
    }
}