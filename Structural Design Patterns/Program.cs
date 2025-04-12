using System;
using System.Collections.Generic;

public interface IText
{
  string GetFormattedText();
}

public class PlainText : IText
{
    private readonly string _text;

    public PlainText(string text)
    {
        _text = text;
    }
    public string GetFormattedText() => _text;
}

public abstract class TextDecorator : IText
{
    protected IText _innerText;

    protected TextDecorator(IText innerText)
    {
        _innerText = innerText;
    }

    public virtual string GetFormattedText() => _innerText.GetFormattedText();
}
public class BoldDecorator : TextDecorator
{
    public BoldDecorator(IText innerText) : base(innerText) { }

    public override string GetFormattedText() => $"{_innerText.GetFormattedText()} [bold]";
}
public class ItalicDecorator : TextDecorator
{
    public ItalicDecorator(IText innerText) : base(innerText) { }

    public override string GetFormattedText() => $"{_innerText.GetFormattedText()} [italic]";
}
public class ColorDecorator : TextDecorator
{
    private readonly string _color;

    public ColorDecorator(IText innerText, string color) : base(innerText)
    {
        _color = color;
    }

    public override string GetFormattedText() => $"{_innerText.GetFormattedText()} [color: {_color}]";
}
class Program
{
    static void Main()
    {
        IText plainText = new PlainText("This is some text that is: ");

        IText boldText = new BoldDecorator(plainText);

        IText boldItalicText = new ItalicDecorator(boldText);

        IText formattedText = new ColorDecorator(boldItalicText, "red");

        Console.WriteLine(formattedText.GetFormattedText());
    }
}

