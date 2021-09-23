public class Calculator
{
	public String A = null;
	public int B = 0;
	public String tasu(int value)
	{
		if(A == null)
		{
			B = value;
		}
		else
		{
			B = B + value;
		}
		A = "+";
		return String.valueOf(B);
	}
	public String hiku(int value)
	{
		if(A == null)
		{
			B = value; //BeforeValue
		}
		else
		{
			B = B - value; //BeforeValue
		}
		A = "-";
		return String.valueOf(B);
	}
	public String kakeru(int value)
	{
		if(A == null)
		{
			B = value;
		}
		else
		{
			B = B * value;
		}
		A = "*";
		return String.valueOf(B);
	}
	public String waru(int value)
	{
		if(A == null)
		{
			B = value;
		}
		else
		{
			B = B / value;
		}
		A = "/";
		return String.valueOf(B);
	}
}