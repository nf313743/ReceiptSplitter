open System
open ReceiptSplitter
open ReceiptSplitter.PdfParser
open System.IO

let inputPath = "./files"
let outputPath = inputPath

let outputFileName (fileName: string) =
    let f = Path.Combine(outputPath, "out-" + fileName.Replace("pdf", "txt"))
    f

[<EntryPoint>]
let main argv =

    let fileName = "2023-06-23.pdf"
    let filePath = Path.Combine(inputPath, fileName)

    let pdfDetails =
        { FilePath = filePath
          StartCondition = "Delivery summary"
          StopCondition = "Order summary" }

    let items = getItems pdfDetails

    items
    |> List.iter (fun x -> printfn "%s\t\t%f" x.Description x.Price)

    let tt =
        items
        |> List.map (fun x -> sprintf "%s\t%f" x.Description x.Price)

    System.IO.File.WriteAllLines((outputFileName fileName), tt)
    0