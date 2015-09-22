using System;
using SharedCodeFS;

namespace SharedCodeCS
{
	public class FooClass
	{
		public static string Foo ()
		{
            return "(C# + " + SharedFSType.ForCS + ")";
		}
	}
}

