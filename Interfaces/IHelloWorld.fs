namespace Interfaces

open Orleans

type ErrorMessage = string

[<GenerateSerializer>]
type HelloWorldBResult =    
    | Failed of ErrorMessage
    | Completed of string

type IHelloWorldA =
    inherit IGrainWithIntegerKey
    abstract member SayHelloA : Result<string, ErrorMessage>
    
type IHelloWorldB =
    inherit IGrainWithIntegerKey
    abstract member SayHelloB : HelloWorldBResult