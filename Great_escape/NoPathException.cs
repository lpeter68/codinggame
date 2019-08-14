using System;

public class NoPathException : Exception
{
    public NoPathException()
    {
    }

    public NoPathException(string message)
        : base(message)
    {
    }

    public NoPathException(string message, Exception inner)
        : base(message, inner)
    {
    }
}