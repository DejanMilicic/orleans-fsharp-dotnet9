namespace Grains

open System.Runtime.CompilerServices
[<assembly: InternalsVisibleTo("Host")>]
do()

open System.Threading.Tasks
open Orleans

type IYourReminderGrain =
    inherit IGrainWithStringKey
    inherit IRemindable
    
    abstract member WakeUp : unit -> Task

type ErrorMessage = string

[<GenerateSerializer>]
type HelloWorldRequest = 
    | SayHello of string
    | CountDown of int

[<GenerateSerializer>]
type HelloWorldResult =   
    | Failed of ErrorMessage
    | Completed of string
    
type IHelloWorldGrain = 
    inherit IGrainWithGuidKey
    abstract member SayHello : HelloWorldRequest -> Task<HelloWorldResult>
type HelloWorldGrain() =
    inherit Grain()

    interface IHelloWorldGrain with
        member this.SayHello(request: HelloWorldRequest) : Task<HelloWorldResult> =
            match request with
            | SayHello name -> 
                Task.FromResult(HelloWorldResult.Completed $"You said {name} to me")
            | CountDown count when count > 0 ->
                Task.FromResult(HelloWorldResult.Completed $"Counting down: {count}")
