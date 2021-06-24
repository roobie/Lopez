using System;
using System.Linq;
using Option;
using Option.Abstractions;

namespace Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt1 = "10".ToOption();
            // aka `map' - Option A -> (A -> B) -> Option B
            var opt2 = opt1.Select(s => s + "1");
            var opt3 = opt2.Select(s => Convert.ToInt32(s));
            // aka `flatMap' - Option A -> (A -> Option B) -> Option B
            var opt4 = opt3.SelectMany(_ => Option<int>.None);
            var opt5 = opt4.Select(v => v / 0); // No exn here because None

            var opt6 = Option<int>.Some(420);
            var opt7 = opt6.Where(y => y < 2);

            int? noInt = null;
            var opt8 = noInt.AsOption();

            Console.WriteLine($@"1: {opt1}
2: {opt2.IntoOption()}
3: {opt3.IntoOption()}
4: {opt4.Select(x => 3).IntoOption()}
5: {opt5.IntoOption()}
6: {opt6.IntoOption()}
7.1: {opt7.IntoOption()}
7.2: {opt7.IntoOption()}
8.1: {opt8.IntoOption()}
8.2: {opt8}

{opt8.Where(x => x==1)}
");
            var noneInt = Option<int>.None;
            Console.WriteLine($@"
var noneInt = Option<int>.None;
noneInt.Equals(noneInt)              = {noneInt.Equals(noneInt)}
noneInt.Equals(Option<int>.None)     = {noneInt.Equals(Option<int>.None)}
noneInt.Equals((int?)null)           = {noneInt.Equals((int?)null)}
Option<int?>.None.Equals((int?)null) = {Option<int?>.None.Equals((int?)null)}
");
        }
    }
}
