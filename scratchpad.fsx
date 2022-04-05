#r "nuget: itext7"

#load "Domain.fs"
#load "PdfParser.fs"

open ReceiptSplitter

type LineItem = Value of string

let myPrint x = printfn "%A" x


let items =
    [ "1 New York Bakery Co. Bagels, Plain x5 £1.00"
      "1 Tofoo Naked Tofu, Organic 280g £2.00"
      // "1 Sainsbury's Colombian Coffee Cake, Taste the Difference 400g (Serves"
      // "6) £2.75"
      "1 Sainsbury's Kale 200g £0.63" ]

let lineContainsPrice (line: string) =
    //line.IndexOf("\u00A3") > -1
    line.IndexOf("£") > -1


let rec lineItemParser (remainingLines: string list) (previousLine: string Option) : CartItem list =
    match remainingLines with
    | [] -> []
    | x :: xs ->
        if lineContainsPrice x then
            let item = PdfParser.extractItem x

            let item' =
                match previousLine with
                | None -> item
                | Some x ->
                    { item with
                          Description = x + item.Description }

            item' :: lineItemParser xs None
        else
            lineItemParser xs (Some x)


//let result = lineItemParser items None

// result |> List.iter myPrint


