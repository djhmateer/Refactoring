namespace Mateer.Bootstrapper
{
    class Program
    {
        static void Main()
        {
            // make it easy to run different files
            // Usually use TestDriven.NET now to run from the method.

            //1 Reformat
            //Reformat.ReadFile.ReadFromAFileAndDisplayAQuote(null);

            //2 ChangeNamesMercilessly
            //ChangeNames.ReadFile.ReadFromAFileAndDisplayAQuote();

            //345 R# Suggestions, Comments and DeadCode
            //RSharpCommentsAndDeadCode.ETL.ReadFromAFileAndDisplayQuote();

            //6
            //MakeSmallerMethods.ETL.ReadFromAFileAndDisplayQuote();

            //QuoteProcessor.ETL.Main();
            //QuoteProcessor.Refactored.QuoteProcessor.Main();
            Mateer.RSharpCommentsAndDeadCode.ETL.ReadFromAFileAndDisplayQuote();
        }
    }
}


