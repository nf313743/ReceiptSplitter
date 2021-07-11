#r "nuget: itext7"

#load "Domain.fs"
#load "Parser.fs"

open ReceiptSplitter
open ReceiptSplitter.PdfParser

let pdfDetails =
    { FilePath = "receipt.pdf"
      StartCondition = "Delivery summary"
      StopCondition = "Order summary" }

let line = "1 Sainsbury's Pink Lady Apples x6 \u00A32.60"
let indexOfPound = line.IndexOf("\u00A3")
let desc = line.Substring(0, indexOfPound - 1)
let price = line.Substring(indexOfPound + 1)

let items = getItems pdfDetails

