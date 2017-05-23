namespace LargePermutationsGenerator
{

	interface IOutputBuffer
	{
		void AddToBuffer(string element);
		void AddToBuffer(char[] element);
	}

}