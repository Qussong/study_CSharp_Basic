

using System.Globalization;

string withSpace = "    Hello   ";
string trimmed = withSpace.Trim();              // "Hello"
string trimmedStart = withSpace.TrimStart();    // "Hello   "
string trimmedEnd = withSpace.TrimEnd();        // "    Hello"

string withChars = "****Hello****";
string trimmedChars = withChars.Trim('*');          // Hello
string trimmedStartChars = withChars.TrimStart('*');// Hello****
string trimmedEndChars = withChars.TrimEnd('*');    // ****Hello

string mixedChars = "-*_Hello_*--";
string trimmedMixed = mixedChars.Trim('-', '*', '_');           // Hello
string trimmedStartMixed = mixedChars.TrimStart('-', '*', '_'); // Hello_*--
string trimmedEndMixed = mixedChars.TrimEnd('-', '*', '_');     // -*_Hello
Console.WriteLine(trimmedMixed);        // Hello
Console.WriteLine(trimmedStartMixed);   // Hello_*--
Console.WriteLine(trimmedEndMixed);     // -*_Hello

Console.ReadKey();