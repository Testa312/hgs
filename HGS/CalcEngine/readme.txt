////////////////////////////////////////////////////////////////
//
// CalcEngine
// A Calculation Engine for .NET
//
////////////////////////////////////////////////////////////////
----------------------------------------------------------------
2022.8.28
1、增加了求解变量表达式（循环引用报异常）。
例：
    var ce = new CalcEngine.CalcEngine();
  //var dct = new CalcDictionary(ce);
  //ce.Variables = dct;
    var dct = ce.Variables;
    dct["Amount"] = 12;
    dct["OfferPrice"] = 12.32;
    dct["Item1"] = "=Item2 * OfferPrice";
    dct["Item2"] = "=Item3 * 0.06";
    dct["Item3"] = "=Item2*0.06";

    // this will print "8.8704" 
    // (Amount * OfferPrice) * 0.06 = 12 * 12.32 * 0.06 = 8.8704
    //Console.WriteLine(ce.Evaluate("Item2"));
    //textBox1.Text = ce.Evaluate("1-Amount * OfferPrice").ToString();
    //CalcEngine.Expression ex = ce.Parse("= Item1 * sin(Amount)");
    textBox1.Text = ce.Evaluate("=(Item1)").ToString();
----------------------------------------------------------------
Version 1.0.0.2		Aug 2011

- TEXT function now uses CurrentCulture instead of InvariantCulture
	note: InvariantCulture does a weird job with currency formats!

----------------------------------------------------------------
Version 1.0.0.1		Aug 2011

- Fixed BindingExpression to update the DataContext when the 
  expression is fetched from the cache.

- Honor CultureInfo.TextInfo.ListSeparator
	so in US systems you would write "Sum(123.4, 567.8)"
	and in DE systems you would write "Sum(123,4; 567,8)"

- Changed default CultureInfo to InvariantCulture
	to make the component deterministic by default

- Improved Expression comparison logic
	both expressions should be of the same type

----------------------------------------------------------------
Version 1.0.0.0		Aug 2011

- First release on CodeProject:
	http://www.codeproject.com/KB/recipes/CalcEngine.aspx