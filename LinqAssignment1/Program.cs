using System;
using System.Collections.Generic;
using System.Linq;
namespace LinqAssignment1;

class Program
{
    public static void Main()
    {
        List<string> places= new List<string>(){"ABU","DHABI","AMSTERDAM","ROME","MADURAI","LONDON","NEW DELHI","MUMBAI","NAIROBI"};

        var output= (from place in places where place.StartsWith('M') && place.EndsWith('I') select place).ToList();

        output.ForEach(output=> System.Console.WriteLine(output));
        
    }
}