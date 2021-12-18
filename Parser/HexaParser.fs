module HexaParser
open FParsec
open FishLibrary
open System.Collections.Generic

//type Packet = Literal of (int * int) * int64 | Operator of (int * int) *  Packet list 

let bitsToInt x = System.Convert.ToInt32(x,2)
let bitsToLong x = System.Convert.ToInt64(x,2)
let flatten   = List.fold (+) ""

let rec repeatParser p number = 
    match number with 
    | 0 -> preturn []
    | _ -> pipe2 p (repeatParser p (number-1)) (fun a b -> (a::b))

let one = pstring "1" >>% "1"
let zero = pstring "0" >>% "0"
let bit = one <|> zero
let repeatBits number = repeatParser bit number |>> flatten 
let bitsFromSubstring length =  repeatBits length |>> bitsToInt

let version = bitsFromSubstring 3
let packtetID = bitsFromSubstring 3
let literalID = pstring "100" |>> bitsToInt

let block = one >>. repeatBits 4
let lastBlock = zero >>.  repeatBits 4
let blockBitsParser =pipe2 (many block) lastBlock (fun x y -> ( bitsToLong) (flatten x + y) )



let rec packetParser = literal <|>operator0 <|> operator1
and  strToPackets  str =
    match run (many packetParser) str with
    | Success(result, x, y)   ->  result
    | Failure(errorMsg, x, y) -> failwith ("Cannot parse lel "+ errorMsg)
and literal = version  .>>.?  literalID .>>.?   blockBitsParser  |>> fun ((a,b),c) -> new Packet(a,b,c)
and operator0 = version  .>>.? packtetID .>>? zero .>>. (bitsFromSubstring 15 >>=  repeatBits) |>> fun ((a,b),d) ->   new Packet(a,b,new List<Packet>(strToPackets d))
and operator1 =   version  .>>.? packtetID  .>>? one  .>>. multiplePacket |>> fun ((a,b),(c:Packet list)) -> new Packet(a,b, new List<Packet>(c))
and multiplePacket = bitsFromSubstring 11 >>=( fun length ->  repeatParser packetParser length )


let strToPacket  str =
    match run packetParser str with
    | Success(result, _, _)   -> result
    | Failure(errorMsg, _, _) -> failwith ("Cannot parse lel "+ errorMsg)
