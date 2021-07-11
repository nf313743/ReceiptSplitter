open System
open ReceiptSplitter
open ReceiptSplitter.PdfParser


[<EntryPoint>]
let main argv =
    let pdfDetails =
        { FilePath = "receipt.pdf"
          StartCondition = "Delivery summary"
          StopCondition = "Order summary" }

    let items = getItems pdfDetails
    items |> List.iter(fun x-> printfn "%A" x)

    0
