namespace ReceiptSplitter

open System
open System.Text
open iText.Kernel.Pdf
open iText.Kernel.Pdf.Canvas.Parser



module PdfParser =

    let private extractLines (pdfDocument: PdfDocument) =
        let builder =
            { 1 .. pdfDocument.GetNumberOfPages() }
            |> Seq.fold
                (fun (sb: StringBuilder) i ->
                    let contents =
                        PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i))

                    sb.Append(contents).Append(Environment.NewLine))
           
                (StringBuilder())

        builder.ToString().Split("\n") |> List.ofArray

    let extractItem (line: string) =
        let indexOfPound = line.IndexOf("\u00A3")
        let desc = line.Substring(0, indexOfPound - 1)

        let price =
            System.Decimal.Parse(line.Substring(indexOfPound + 1))

        { Description = desc; Price = price }


    let getItems pdfDetails =
        let pdfReader = new PdfReader(pdfDetails.FilePath)
        let pdfDocument = new PdfDocument(pdfReader)

        let items =
            pdfDocument
            |> extractLines
            |> List.skipWhile (fun x -> not (x.Contains(pdfDetails.StartCondition)))
            |> List.skip 1
            |> List.takeWhile (fun x -> not (x.Contains(pdfDetails.StopCondition)))
            |> List.map extractItem

        items




// let private rec alternativeParse foo (sb:StringBuilder) n =
//     match n with
//     | 0 -> sb
//     | _ ->
//         let ff = foo (sb) (n-1)
//         let contents = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(n));
//         ff.Append(contents)
