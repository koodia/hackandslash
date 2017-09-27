//using UnityEngine;
using System.Collections;
using System; 
using System.Diagnostics;

public static class CustomExceptions
{
	public const string dquotes = "''";

	[Conditional("DEBUG")] 
	public static void Assert(bool condition, string msg) 
	{ 
		if (condition) 
		{
			//throw new CustomException(msg);
			UnityEngine.Debug.LogWarning(msg);
			UnityEngine.Debug.Break();
		}
	}
}

public class DontForgetToSetException: Exception  // UnassignedReferenceException();
{
        public DontForgetToSetException()
        {
        }

        public DontForgetToSetException(string message)
            : base(message)
        {
        }

        public DontForgetToSetException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }







