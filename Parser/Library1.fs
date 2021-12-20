module FSharpParser
open FParsec
open FishLibrary


let pFish,pFishImplement = createParserForwardedToRef()
let pNumber = pint64 |>> fun x -> new Fish(x)
let pPair =( pstring "[" >>. pFish .>> pstring "," .>>. pFish .>> pstring "]") |>>  fun (x,y) -> new Fish(x,y)
pFishImplement := pNumber <|> pPair

let public strToFish  str =
    match run pFish str with
    | Success(result, _, _)   ->  result
    | Failure(errorMsg, _, _) -> failwith ("Cannot parse lel "+ errorMsg)

