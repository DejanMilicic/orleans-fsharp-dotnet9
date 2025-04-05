namespace Grains

open System.Threading.Tasks
open Orleans

type IYourReminderGrain =
    inherit IGrainWithStringKey
    inherit IRemindable
    
    abstract member WakeUp : unit -> Task

type ErrorMessage = string

[<GenerateSerializer>]
type HelloWorldResult =   
    | Failed of ErrorMessage
    | Completed of string
    
    
type IHelloWorldGrain = 
    inherit IGrainWithGuidKey
    abstract member SayHello : string -> Task<HelloWorldResult>
type HelloWorldGrain() =
    inherit Grain()

    interface IHelloWorldGrain with
        member this.SayHello(request: string) : Task<HelloWorldResult> =
            Task.FromResult(HelloWorldResult.Failed "Hello back")