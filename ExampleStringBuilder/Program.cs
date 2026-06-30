using System.Text;

var sb = new StringBuilder();
for (int i = 1; i <= 1_000_000; i++)
{
    sb.Append(i);
}

Console.WriteLine(sb.ToString());