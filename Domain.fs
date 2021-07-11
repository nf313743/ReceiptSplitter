namespace ReceiptSplitter

type CartItem = 
    { Description:string
      Price: decimal }

type PdfDetails = 
    { FilePath:string
      StartCondition:string
      StopCondition:string }